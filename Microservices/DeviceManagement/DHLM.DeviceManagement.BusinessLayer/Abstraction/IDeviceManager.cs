using System;
using System.Threading.Tasks;

namespace DHLM.DeviceManagement.BusinessLayer.Abstraction
{
    public interface IDeviceManager
    {
        Task<bool> UpdateDeviceProperty(string updateProp);        
    }
}