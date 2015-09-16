using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using RestSharp.Portable;
using Windows.System.Profile;
using System.Net.Http;

namespace ImageApp.Services
{
    /// <summary>
    /// The service to 
    /// </summary>
    public class SessionService
    {
        /// <summary>
        /// Gets the unique device id.
        /// </summary>
        public static string DeviceId
        {
            get
            {
                var token = HardwareIdentification.GetPackageSpecificToken(null);
                var hardwareId = token.Id;
                var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(hardwareId);

                byte[] bytes = new byte[hardwareId.Length];
                dataReader.ReadBytes(bytes);

                return BitConverter.ToString(bytes);
            }
        }

        /// <summary>
        /// Gets or sets a username for the current session.
        /// </summary>
        public static string UserName { get; set; }

        /// <summary>
        /// Performs a login on the api.
        /// </summary>
        /// <returns>True on success</returns>
        public async Task<bool> LoginAsync()
        {
            // TODO: replace
            return true;
        }

        /// <summary>
        /// Registers the user on the api.
        /// </summary>
        /// <returns>True on success</returns>
        public async Task<bool> RegisterAsync()
        {
            // TODO: replace
            return true;
        }
    }
}
