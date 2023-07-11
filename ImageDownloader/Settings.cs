using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageDownloader
{
    internal class Settings
    {

        [Display(Name = "Image Count")]
        public static int ImageCount { get; set; } = 0;

        [Display(Name = "Max. Parallel Download Limit")]
        public static int Paralellism { get; set; } = 0;

        [Display(Name = "Output Folder")]
        public static string OutputFolder { get; set; } = "./outputs";


        internal static void LoadConfiguration()
        {
            Console.Write("Enter the number of images to download: ");
            ImageCount = ReadInputAsInt();

            Console.Write("Enter the maximum parallel download limit: ");
            Paralellism = ReadInputAsInt();

            Console.Write("Enter the save path (default: ./outputs): ");
            OutputFolder = Console.ReadLine();

            if (string.IsNullOrEmpty(OutputFolder))
            {
                OutputFolder = "./outputs";
            }

            Directory.CreateDirectory(OutputFolder);
        }

        private static int ReadInputAsInt()
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int result) && result > 0)
                {
                    return result;
                }

                Console.Write("Invalid input. Please enter a positive integer: ");
            }
        }

    }
}
