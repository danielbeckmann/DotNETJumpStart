using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ImageApp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ImageApp
{
    public sealed partial class RegisterPage : Page
    {
        private LoginViewModel viewModel;

        public RegisterPage()
        {
            this.InitializeComponent();
            this.viewModel = this.DataContext as LoginViewModel;
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            bool result = await this.viewModel.RegisterAsync();

            if (result)
            {
                this.Frame.Navigate(typeof(MainPage));
            }
            else
            {
                var dialog = new MessageDialog("Dieser Benutzername ist bereits vergeben.", "Fehler");
                await dialog.ShowAsync();
            }
        }
    }
}
