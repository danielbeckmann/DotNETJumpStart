using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Portable;
using Windows.System.Profile;
using System.Net.Http;
using ImageApp.DataModel;

namespace ImageApp.Services
{
    /// <summary>
    /// The service to handle the session.
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

                var deviceId = BitConverter.ToString(bytes);
                return deviceId;
            }
        }

        /// <summary>
        /// Gets or sets a username for the current session.
        /// </summary>
        public static string UserName { get; private set; }

        /// <summary>
        /// Performs a login on the api.
        /// </summary>
        /// <returns>True on success</returns>
        public async Task<bool> LoginAsync()
        {
            var deviceId = SessionService.DeviceId;
            using (var client = new RestClient(Config.ApiAddress))
            {
                var request = new RestRequest("users/{id}", HttpMethod.Get);
                request.AddUrlSegment("id", deviceId);

                try
                {
                    var result = await client.Execute<User>(request);
                    SessionService.UserName = result.Data.Name;

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Registers the user on the api.
        /// </summary>
        /// <returns>True on success</returns>
        public async Task<bool> RegisterAsync(string userName)
        {
            var user = new User { Identifier = SessionService.DeviceId, Name = userName };
            using (var client = new RestClient(Config.ApiAddress))
            {
                var request = new RestRequest("users", HttpMethod.Post);
                request.AddBody(user);

                try
                {
                    var result = await client.Execute(request);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
