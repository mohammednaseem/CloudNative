using System;
using System.Text;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DHLM.Vehicle.Simulator
{
    class Program
    {   
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }
        static string theSampleDataFile;
        private static Dictionary<string, string> Init()
        {
            Dictionary<string, string> allSettings = new Dictionary<string, string>();
            string settingsFileLocation = Directory.GetCurrentDirectory() 
                                            + Path.DirectorySeparatorChar
                                            + "settings.json";
            string settingsData = File.ReadAllText(settingsFileLocation);
            
            JObject o = (JObject)JsonConvert.DeserializeObject(settingsData);

            foreach (JProperty property in o.Properties())
            {
                allSettings.Add(property.Name, property.Value.ToString());
            }
            return allSettings;
        }
        private static async Task MainAsync()
        {
            Dictionary<string, string> allSettings = Init();
            theSampleDataFile = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "vehicledata.json";

            VehicleInAction vehcileInAction = new VehicleInAction(theSampleDataFile, allSettings);
            if(File.Exists(theSampleDataFile))
            {
                Console.WriteLine(theSampleDataFile);
            }
            else
            {
                Console.WriteLine("The file does not exist");
            }
            await vehcileInAction.Simulate();
            Console.WriteLine("Simulated!");
        }
    }

    class VehicleInAction
    {
        string TheSampleDataFile;
        Dictionary<string, string> theSettings;
        public VehicleInAction(string sampleFIle, Dictionary<string, string> allSettings)
        {
            TheSampleDataFile = sampleFIle;
            theSettings       = allSettings;
        }

        public async Task Simulate()
        {
            Random r = new Random();
            int vehicle = r.Next(1, 5);
            
            Console.WriteLine("Ready to simulaTe");
            try
            {
                bool v = await CreateVehicleIfNotExistsAndSendMessageAsync(vehicle.ToString());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private async Task<bool> CreateVehicleIfNotExistsAndSendMessageAsync(string vehicleName)
        { 
            string iotHubConnectionString = theSettings["IotHubConnectionString"];
            RegistryManager registryManager = RegistryManager.CreateFromConnectionString(iotHubConnectionString);
            Console.WriteLine("Getting details of " + vehicleName);
            Device device = await registryManager.GetDeviceAsync(vehicleName);
            Console.WriteLine("Got details of " + vehicleName);
            if (device == null)
            { // this is not registered device ..
                Console.WriteLine("Vehcile does not exist" + vehicleName);
                await registryManager.AddDeviceAsync(new Device(vehicleName));
                device = await registryManager.GetDeviceAsync(vehicleName);
            }
            if(device != null)
            {
                Console.WriteLine(vehicleName + "Connect And SendMessage To Hub");
                await ConnectAndSendMessageToHubAsync(device, vehicleName);
            }
            else 
            {
                Console.WriteLine("Issue creating vehcile" );
            }
            return true;
        }

        private async Task<bool> ConnectAndSendMessageToHubAsync(Device device, string vehicleName)
        {                        
            string iotHubDns = theSettings["IotHubDns"];
            DeviceClient deviceClient = DeviceClient.Create(iotHubDns, 
                                            new DeviceAuthenticationWithRegistrySymmetricKey(device.Id, 
                                                    device.Authentication.SymmetricKey.PrimaryKey), 
                                                    Microsoft.Azure.Devices.Client.TransportType.Amqp_Tcp_Only);

            string sampleData = File.ReadAllText(TheSampleDataFile);
            JObject o = (JObject)JsonConvert.DeserializeObject(sampleData);
            DateTime dateTime = DateTime.Now;

            foreach (JProperty property in o.Properties())
            {
                JObject innerMessageJObject = JObject.Parse(property.Value.ToString());
                string timeOffset = (string)innerMessageJObject.SelectToken("containerdt");
                DateTime dt = dateTime.AddSeconds(Convert.ToDouble(timeOffset) - Convert.ToDouble("20"));

                string theSequence = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                theSequence = theSequence.Replace("/","");
                theSequence = theSequence.Replace(":","");
                theSequence = theSequence.Replace(" ","");

                Console.WriteLine("The Message :");
                    
                JObject messageJObject = new JObject(
                    new JProperty("telemetry",
                        new JObject( 
                            new JProperty("telgramtype", (string)innerMessageJObject.SelectToken("telgramtype")),
                            new JProperty("deviceId", (string)innerMessageJObject.SelectToken("deviceId")),
                            new JProperty("sequence", theSequence),
                            new JProperty("vehicletype", (string)innerMessageJObject.SelectToken("vehicletype")),
                            new JProperty("engineon", (string)innerMessageJObject.SelectToken("engineon")),
                            new JProperty("containerdt", dt.ToString("o"))
                        )
                    )
                );
                
                string theMessage = messageJObject.ToString();
                var message = new Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes(theMessage));
 
                try
                {
                    await deviceClient.SendEventAsync(message);
                    Console.WriteLine(theMessage);
                    Thread.Sleep(1001);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return true;
        }
    }
}