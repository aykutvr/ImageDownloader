## ImageDownloader
### Introduction
The ImageDownloader is a simple yet powerful application designed to download images from the web in bulk. It allows users to specify the number of images to download, the maximum number of parallel downloads, and the folder where the images will be saved.

### Program Overview
The program consists of three main components:

* Program.cs
* Downloader.cs
* Settings.cs

### What the Program Does

#### 1. Configuration Setup:
The program begins by loading configuration settings. It prompts the user to input the number of images to download, the maximum number of parallel downloads, and the destination folder for the downloaded images.

#### 2. Initialization:
It then creates an instance of the Downloader class, which is responsible for managing the download process.

#### 3. Download Process:
* The Downloader class starts the download process. It handles the downloading of images, progress updates, and cancellation requests.
* Images are downloaded from a sample URL (https://picsum.photos/200/300) and saved to the specified output folder.
* The program supports parallel downloads, meaning it can download multiple images at the same time, up to the limit set by the user.

#### 4. Progress and Cancellation:
* The program provides real-time progress updates on the console, showing how many images have been downloaded out of the total.
* The user can cancel the download process at any time by pressing CTRL+C. If the download is cancelled, any partially downloaded images are removed.

### Key Functionalities
* Bulk Image Download: Downloads a specified number of images from a given URL.
* Parallel Downloads: Supports downloading multiple images concurrently, up to a user-defined limit.
* Progress Updates: Provides real-time feedback on the download progress.
* Cancellation Handling: Allows the user to cancel the download process, cleaning up any incomplete downloads.

This application is ideal for users who need to download multiple images efficiently and want to manage the download process with ease.
