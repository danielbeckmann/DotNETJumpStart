using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ImageApp.Common;
using ImageApp.DataModel;
using ImageApp.Services;
using RestSharp.Portable;
using Windows.System.Profile;
using Windows.UI.Popups;

namespace ImageApp.ViewModels
{
    /// <summary>
    /// The viewmodel for the login page.
    /// </summary>
    public class LoginViewModel : BindableBase
    {
        private SessionService session;

        private string userName;

        public LoginViewModel()
        {
            this.session = new SessionService();
            this.RegisterCommand = new DelegateCommand(this.Register);
        }

        /// <summary>
        /// Gets or sets the command to register.
        /// </summary>
        public DelegateCommand RegisterCommand { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { this.SetProperty(ref this.userName, value); }
        }

        /// <summary>
        /// Performs the login.
        /// </summary>
        /// <returns>True on success</returns>
        public async Task<bool> LoginAsync()
        {
            return await this.session.LoginAsync();
        }

        /// <summary>
        /// Performs the registration.
        /// </summary>
        /// <returns>True on success</returns>
        private async void Register(object obj)
        {
            var result = await this.session.RegisterAsync(this.userName);
            if (result)
            {
                NavigationService.Current.Navigate(typeof(MainPage));
            }
            else
            {
                var dialog = new MessageDialog("Dieser Benutzername ist bereits vergeben.", "Fehler");
                await dialog.ShowAsync();
            }
        }
    }
}
