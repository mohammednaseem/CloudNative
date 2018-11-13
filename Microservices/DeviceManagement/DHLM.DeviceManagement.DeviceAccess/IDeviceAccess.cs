using System.Threading.Tasks;

namespace DHLM.DeviceManagement.DeviceAccess
{
    public interface IDeviceAccess
    {
        Task<bool> UpdateTwinData(string updateJson);
    }
}