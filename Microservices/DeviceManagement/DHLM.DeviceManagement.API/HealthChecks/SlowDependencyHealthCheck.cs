using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DHLM.DeviceManagement.API.HealthChecks
{
    // Simulates a health check for an application dependency that takes a while to initialize.
    // This is part of the readiness/liveness probe sample.
     public class SlowDependencyHealthCheck : IHealthCheck
    {
        public static readonly string HealthCheckName = "slow_dependency";

        private readonly Task _task;

        public SlowDependencyHealthCheck()
        {
            _task = Task.Delay(15 * 0);
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (_task.IsCompleted)
            {
                return Task.FromResult(HealthCheckResult.Healthy("Dependency is ready"));
            }

            return Task.FromResult(new HealthCheckResult(
                status: context.Registration.FailureStatus, 
                description: "Dependency is still initializing"));
        }
    }
}
