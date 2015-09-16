using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ImageApp.ViewModels;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ImageApp
{
    public sealed partial class AddPostPage : Page
    {
        private AddPostViewModel viewModel;

        public AddPostPage()
        {
            this.InitializeComponent();
            this.viewModel = this.DataContext as AddPostViewModel;
        }

        public void ContinueFileOpenPicker(FileOpenPickerContinuationEventArgs args)
        {
            if (args.Files.Count > 0)
            {
                var pickedImage = args.Files[0];
                this.viewModel.Image = pickedImage;
            }
        }
    }
}
