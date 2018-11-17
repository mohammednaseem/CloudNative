using System;
using System.Threading.Tasks;

namespace DHLM.DeviceManagement.BusinessLayer.Abstraction
{
    public interface IDeviceManager
    {
        bool IsHealthy();
        Task<bool> UpdateDeviceProperty(string updateProp);        
    }
}