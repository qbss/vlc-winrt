﻿using Windows.Storage;

namespace VLC_WINRT.ViewModels.MainPage
{
    public class RemovableLibraryViewModel : LibraryViewModel
    {
        public RemovableLibraryViewModel(StorageFolder location, string id) : base(location)
        {
            Id = id;
        }

        public string Id { get; private set; }
    }
}