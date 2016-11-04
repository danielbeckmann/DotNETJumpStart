using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ImageApp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ImageApp
{
    public sealed partial class SplashPage : Page
    {
        private LoginViewModel viewModel;

        public SplashPage()
        {
            this.InitializeComponent();
            this.viewModel = this.DataContext as LoginViewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            bool result = await this.viewModel.LoginAsync();
            if (result)
            {
                this.Frame.Navigate(typeof(MainPage));
            }
            else
            {
                this.Frame.Navigate(typeof(RegisterPage));
            }
        }
    }
}
