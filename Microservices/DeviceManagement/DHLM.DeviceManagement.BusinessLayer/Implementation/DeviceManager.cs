using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Devices;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DHLM.DeviceManagement.BusinessLayer.Abstraction;
using DHLM.DeviceManagement.DeviceAccess;
using DHLM.Common.DataAccess.Repository;
 
namespace DHLM.DeviceManagement.BusinessLayer.Implementation
{    
    public class DeviceManager : IDeviceManager
    {
        private IConfiguration configuration;
        private IDeviceAccess _IDeviceAccess { get; }
        private ILogRepository _IRepo { get; }
        
        /* constructor */
        public DeviceManager(IDeviceAccess iDeviceAccess, ILogRepository iRepo,
                                IConfiguration _configuration)
        {
            _IDeviceAccess  = iDeviceAccess;
            _IRepo          = iRepo;
            configuration  = _configuration;
        }
        /* constructor */
        public async Task<bool> UpdateDeviceProperty(string updateJson)
        {
            Console.WriteLine("Inside UpdateDeviceProperty: " + updateJson);
            await _IDeviceAccess.UpdateTwinData(updateJson);
            _IRepo.WriteTelegram(updateJson); //convert to async
            return true;
        }
        public bool IsHealthy()
        {
            Random r = new Random();
            int theNumber = r.Next(1, 10);
            if(configuration["UnHealthy"] == "true")
            {
                if(theNumber > 4)
                {
                    return false;
                }
            }            
            return true;
        }
    }
}