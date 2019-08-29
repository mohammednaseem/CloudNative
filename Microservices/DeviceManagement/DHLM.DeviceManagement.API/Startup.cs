using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ApplicationInsights.AspNetCore;
using Microsoft.ApplicationInsights.Extensibility;
using System.Threading.Tasks;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using DHLM.DeviceManagement.API.ExceptionHandling; 
using DHLM.DeviceManagement.API.Services;
using DHLM.DeviceManagement.API.HealthChecks;
using DHLM.DeviceManagement.BusinessLayer.Abstraction;
using DHLM.DeviceManagement.BusinessLayer.Implementation;
using DHLM.DeviceManagement.DeviceAccess;
using DHLM.DeviceManagement.DeviceAccess.AzureIoTHub;
using DHLM.Common.DataAccess.Repository;
using DHLM.Common.DataAccess.AzureSql;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.ApplicationInsights.Kubernetes;
using Okta.AspNetCore;


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
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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

           /* services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://dev-212903.oktapreview.com/oauth2/default";
                    options.Audience = "api://default";
                    options.RequireHttpsMetadata = false;
                });*/

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = OktaDefaults.ApiAuthenticationScheme;
                options.DefaultChallengeScheme = OktaDefaults.ApiAuthenticationScheme;
                options.DefaultSignInScheme = OktaDefaults.ApiAuthenticationScheme;
            })
            .AddOktaWebApi(new OktaWebApiOptions()
            {
                OktaDomain = "https://dev-212903.oktapreview.com",
                ClientId = "0oahtdomprbxet1fI0h7",
                //Audience = "api://default",
                Audience = "api://default",
            });

            string storageAccount = Configuration["SqlConnectionString"];
            services.AddSingleton<ILogRepository>(a => new AzureSql(storageAccount)); 
            services.AddSingleton<IDeviceAccess, DeviceTwin>(); 
            services.AddSingleton<IDeviceManager, DeviceManager>();
            services.AddSingleton<IHostedService, EventOutputProcessor>(); 

            services
                .AddHealthChecks()
                //.AddCheck<SlowDependencyHealthCheck>("Slow", failureStatus: null, tags: new[] { "ready", })
                .AddCheck<DoctorHealthCheck>("Doctor", failureStatus: null, tags: new[] { "live", });
            
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
            {// Exclude all checks, just return a 200.
                Predicate = (check) => false,
            });
            
            // The liveness filters out all checks and just returns success
            app.UseHealthChecks("/health/live", new HealthCheckOptions()
            {
                Predicate = (check) => check.Tags.Contains("live"), 
            });
            
            app.UseCors("AllowAllOrigins");
           // app.UseMiddleware(typeof(ErrorWrappingMiddleware));
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseAuthentication();
            
            app.UseMvcWithDefaultRoute();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DHLM Device Management Microservice V1");
            });
        }        
    }
}