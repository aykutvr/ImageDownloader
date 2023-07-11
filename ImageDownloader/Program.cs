using ImageDownloader;
using System.Net;

Settings.LoadConfiguration();

Downloader imageDownloader = new Downloader();
imageDownloader.Start();