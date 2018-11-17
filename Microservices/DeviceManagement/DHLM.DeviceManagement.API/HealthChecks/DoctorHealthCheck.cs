using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using DHLM.DeviceManagement.BusinessLayer.Abstraction;

namespace DHLM.DeviceManagement.API.HealthChecks
{
    // Simulates a health check for an application dependency 
    // This is part of the readiness/liveness probe sample.
    public class DoctorHealthCheck : IHealthCheck
    {
        public static readonly string HealthCheckName = "DoctorHealthCheck";         
        private IDeviceManager DeviceManager;
        public DoctorHealthCheck(IDeviceManager devManager)
        {            
            DeviceManager = devManager;
        }
      
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            if(DeviceManager.IsHealthy())
            {
                return Task.FromResult(HealthCheckResult.Healthy("DeviceManager is healthy"));
            }
            return Task.FromResult(new HealthCheckResult(status: context.Registration.FailureStatus, 
                                                            description: "DeviceManager is sick"));           
        }
    }
}
