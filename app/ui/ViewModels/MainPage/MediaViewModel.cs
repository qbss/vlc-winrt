﻿using System;
using System.Diagnostics;
using VLC_WINRT.Common;
using VLC_WINRT.Utility.Commands;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.System.Threading;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace VLC_WINRT.ViewModels.MainPage
{
    public class MediaViewModel : BindableBase
    {
        private StorageFile _file;
        private ImageSource _imageBrush;
        private PlayCommand _play;
        private string _subtitle = string.Empty;
        private string _title = string.Empty;

        public MediaViewModel(StorageFile storageFile)
        {
            Title = storageFile.Name;
            Subtitle = storageFile.FileType.ToUpper() + " File";
            File = storageFile;
            Play = new PlayCommand();
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public StorageFile File
        {
            get { return _file; }
            set
            {
                SetProperty(ref _file, value);
                ThreadPool.RunAsync(GenerateThumbnail);
            }
        }

        public ImageSource Image
        {
            get { return _imageBrush; }
            set { SetProperty(ref _imageBrush, value); }
        }

        public string Subtitle
        {
            get { return _subtitle; }
            set { SetProperty(ref _subtitle, value); }
        }

        public PlayCommand Play
        {
            get { return _play; }
            set { SetProperty(ref _play, value); }
        }

        private async void GenerateThumbnail(IAsyncAction operation)
        {
            //TODO: make this whole thing keyed on an enum
            try
            {
                StorageItemThumbnail thumb = await File.GetThumbnailAsync(ThumbnailMode.VideosView);

                DispatchHelper.Invoke(() =>
                                          {
                                              var image = new BitmapImage();
                                              image.SetSource(thumb);
                                              Image = image;
                                          });
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error getting thumbnail");
                Debug.WriteLine(ex);
            }
        }
    }
}