using ImageApp.DataModel;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System.Profile;

namespace ImageApp.Utils
{
    /// <summary>
    /// The service to 
    /// </summary>
    public class DeviceUtils
    {
        /// <summary>
        /// Gets the unique device id.
        /// </summary>
        public static string DeviceId
        {
            get
            {
                if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.System.Profile.HardwareIdentification"))
                {
                    var token = HardwareIdentification.GetPackageSpecificToken(null);
                    var hardwareId = token.Id;
                    var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(hardwareId);

                    byte[] bytes = new byte[hardwareId.Length];
                    dataReader.ReadBytes(bytes);

                    var deviceId = BitConverter.ToString(bytes).Replace("-", "");
                    return deviceId;
                }
                else
                {
                    return "UNKNOWN";
                }
            }
        }
    }
}
