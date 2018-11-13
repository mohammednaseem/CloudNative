using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using DHLM.DeviceManagement.BusinessLayer.Abstraction;
using DHLM.DeviceManagement.BusinessLayer.Model;

namespace DHLM.DeviceManagement.API.Controllers
{
    [Route("api/[controller]")]
    public class DeviceManagerController : BaseController
    {
        public IDeviceManager _IDeviceManager { get; }

        public DeviceManagerController(IDeviceManager deviceManager, IConfiguration configuration )
                                                                    : base(configuration)
        {
            _IDeviceManager = deviceManager;
        }

        // GET api/DeviceManager
        [HttpGet]
        public IEnumerable<string> Get()
        {
            Console.WriteLine("Inside ");
            return new string[] { "value1", "value2" };
        }

        // GET api/DeviceManager/5
        [HttpGet("{temperature}")]
        public string Get(int percentage)
        {          
            if (percentage < 0 || percentage > 100)
                return "incorrect number";
            Stopwatch watch = new Stopwatch();
            watch.Start();            
            while (true)
            {
                // Make the loop go on for "percentage" milliseconds then sleep the 
                // remaining percentage milliseconds. So 40% utilization means work 40ms and sleep 60ms
                if (watch.ElapsedMilliseconds > percentage)
                {
                    Thread.Sleep(100 - percentage);
                    watch.Reset();
                    watch.Start();
                }
            }  
            //return "value";
        }
 
        // POST api/DeviceManager
        [HttpPost]
        public async void Post([FromBody]Modem aModem)
        {
            Console.WriteLine("Modem Name:"         + aModem.Name);
            Console.WriteLine("Modem Temperature:"  + aModem.Temperature);
            await _IDeviceManager.UpdateDeviceProperty(aModem.Temperature);
            Console.WriteLine("Set container temperature to : " + aModem.Temperature);
        }

        // PUT api/DeviceManager/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {  
            Console.WriteLine("value :" + value);
        }

        // DELETE api/DeviceManager/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
