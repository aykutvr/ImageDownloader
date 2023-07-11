using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageDownloader
{
    internal class Downloader
    {

        #region Private Variables
        private int downloadedCount;
        private int currentIndex;
        private object lockObject = new object();
        private CancellationTokenSource cancellationTokenSource;
        #endregion

        public event Action<int, int> ProgressChanged;

        public void Start()
        {

            cancellationTokenSource = new CancellationTokenSource();

            Task.Run(StartDownload);


            Console.WriteLine("Press CTRL+C to cancel.\r\n");
            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true;
                cancellationTokenSource.Cancel();
                Console.WriteLine("\nDownload cancelled. Images will be removed.");
                Cleanup();
            };

            while (!cancellationTokenSource.IsCancellationRequested && downloadedCount < Settings.ImageCount)
            {
                Thread.Sleep(1000);
            }

            if(!cancellationTokenSource.IsCancellationRequested)
                Console.WriteLine("\nAll images downloaded successfully.");
        }

        private void StartDownload()
        {
            ProgressChanged += (current, total) =>
            {
                if (cancellationTokenSource.IsCancellationRequested)
                    return;

                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write($"Progress: {current}/{total}");
            };

            Console.WriteLine($"\nDownloading {Settings.ImageCount} images ({Settings.Paralellism} parallel downloads at most)\n");

            List<Task> downloadTasks = new List<Task>();
            for (int i = 1; i <= Settings.ImageCount; i++)
            {
                downloadTasks.Add(DownloadImageAsync(i));
                currentIndex++;
                if (downloadTasks.Count >= Settings.Paralellism)
                {
                    Task.WhenAny(downloadTasks).Wait(cancellationTokenSource.Token);
                    downloadTasks.RemoveAll(t => t.IsCompleted);
                    
                    if (cancellationTokenSource.IsCancellationRequested)
                        downloadTasks.Clear();
                }
            }

            Task.WhenAll(downloadTasks).Wait();
        }
        private async Task DownloadImageAsync(int imageIndex)
        {
            if (cancellationTokenSource.IsCancellationRequested)
            {
                return;
            }

            string imageUrl = $"https://picsum.photos/200/300";
            string imagePath = Path.Combine(Settings.OutputFolder, $"{imageIndex}.png");

            using (var webClient = new WebClient())
            {
                await webClient.DownloadFileTaskAsync(new Uri(imageUrl), imagePath);
            }

            lock (lockObject)
            {
                downloadedCount++;
                ProgressChanged?.Invoke(downloadedCount, Settings.ImageCount);
            }
        }

        private void Cleanup()
        {
            for (int i = 1; i <= downloadedCount; i++)
            {
                string imagePath = Path.Combine(Settings.OutputFolder, $"{i}.png");
                File.Delete(imagePath);
            }
        }
    }
}
