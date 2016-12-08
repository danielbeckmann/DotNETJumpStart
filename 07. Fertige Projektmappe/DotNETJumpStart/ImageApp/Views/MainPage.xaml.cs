using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ImageApp.ViewModels;
using Windows.UI.Popups;

// Die Vorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 dokumentiert.

namespace ImageApp.Views
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MainViewModel viewModel;

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            this.viewModel = this.DataContext as MainViewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await this.viewModel.GetPostsAsync();
        }

        private void CameraButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddPostPage));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var m = new MessageDialog("liked");
            await m.ShowAsync();
        }
    }
}
