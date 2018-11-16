using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Cors;
using DHLM.DeviceManagement.API.ExceptionHandling; 
using DHLM.DeviceManagement.API.Services;
using DHLM.DeviceManagement.API.HealthChecks;

namespace DHLM.DeviceManagement.API
{  
    public class Startup
    {
        private readonly ILogger _logger; 
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env, ILogger<Startup> logger)
        {
            Configuration = new ConfigurationBuilder()
                            .SetBasePath(env.ContentRootPath)
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                            .AddEnvironmentVariables()
                            .Build();
             _logger = logger;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
//            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors(options =>
            {
                 options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                    });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",  new Info { Title = "DHLM API", Version = "v1" });
            });

             services
                .AddHealthChecks()
                //.AddCheck<SlowDependencyHealthCheck>("Slow", failureStatus: null, tags: new[] { "ready", })
                .AddCheck<SlowDependencyHealthCheck>("Slow", failureStatus: null, tags: new[] { "ready", });

            //services.AddSingleton<ILogger, AppLogger>();
            services.AddSingleton<IHostedService, EventOutputProcessor>(); 
             
            _logger.LogInformation("Added TodoRepository to services");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {            
            if (env.IsDevelopment())
            {
                _logger.LogInformation("In Development environment");
                app.UseDeveloperExceptionPage();
            }
            app.UseHealthChecks("/health/ready", new HealthCheckOptions()
            {
                Predicate = (check) => check.Tags.Contains("ready"), 
            });

            // The liveness filters out all checks and just returns success
            app.UseHealthChecks("/health/live", new HealthCheckOptions()
            {
                // Exclude all checks, just return a 200.
                Predicate = (check) => false,
            });
            
            app.UseCors("AllowAllOrigins");
           // app.UseMiddleware(typeof(ErrorWrappingMiddleware));
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseMvc();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DHLM Device Management Microservice V1");
            });
        }        
    }
}