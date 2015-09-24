using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ImageApp.Common;
using ImageApp.Services;
using Windows.System.Profile;

namespace ImageApp.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private SessionService session;

        private string userName;

        public LoginViewModel()
        {
            this.session = new SessionService();
        }

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
        public async Task<bool> RegisterAsync()
        {
            return await this.session.RegisterAsync(this.userName);
        }
    }
}
