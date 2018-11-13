using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;

namespace DHLM.DeviceManagement.API.Controllers
{
    public class BaseController : Controller, IBaseController
    {
        public IConfiguration _Configuration { get; }
        public BaseController(IConfiguration configuration) 
        {
            _Configuration = configuration;
        }
        public string CompanyName
        {
            get
            {
                if(!string.IsNullOrWhiteSpace(Request.Headers["companyName"]))
                {
                    return  Request.Headers["companyName"].ToString();
                }
                else
                {
                    return _Configuration.GetValue<string>("companyName");
                }
            }
        }

        public string UserId
        {
            get
            {
                return  Request.Headers["userId"].ToString();
            }
        }
    }
    
}