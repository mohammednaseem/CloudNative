using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Azure.Devices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DHLM.DeviceManagement.DeviceAccess;

namespace DHLM.DeviceManagement.DeviceAccess.AzureIoTHub
{
    public class DeviceTwin : IDeviceAccess
    {
        private string messageDeviceTwinFunctionalityNotFound = "Device Twin functionality not found." + Environment.NewLine + "Make sure you are using the latest Microsoft.Azure.Devices package.";
        private string ConnString;
        private string DeviceName;
        
        public DeviceTwin()
        {
        }

        public DeviceTwin(string connectionString, string deviceName)
        {
            ConnString = connectionString;//"HostName=naseemiothub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=07vgW5pZYJmaIY83Yoz9qulmXXOWWeM16kgTe1V5x1M="; //connectionString;
            DeviceName = deviceName;//"aStrangeDevice"; //deviceId;
        }

        public async Task<bool> UpdateTwinData(string updateJson)
        {
            Console.WriteLine("Inside UpdateTwinData: " + updateJson);
            Console.WriteLine("ConnString: " + ConnString);
            dynamic registryManager = RegistryManager.CreateFromConnectionString(ConnString);
            if (registryManager != null)
            {
                try
                {
                    Console.WriteLine("DeviceName: " + DeviceName);
                    var deviceTwin = await registryManager.GetTwinAsync(DeviceName); 
                    var patch = new
                    {
                        properties = new
                        {
                            desired = new
                            {
                                temperatureConfig = new {
                                configId = Guid.NewGuid(),
                                desiredTemperature = updateJson
                                }
                            }
                        },
                            tags = new
                        {
                            engineType = "Fast",
                            manufacturer = "Honda"
                        }
                    };
                    
                        await registryManager.UpdateTwinAsync(DeviceName, 
                                    JsonConvert.SerializeObject(patch), deviceTwin.ETag);
                    }
                    catch (Exception ex)
                    {
                        string errMess = "Update Twin failed. Exception: " + ex.ToString();
                        Console.WriteLine(errMess);
                        Console.WriteLine(ex);
                    }
                }
                return true; ;
        }
    }
}