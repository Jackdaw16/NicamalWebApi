using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace NicamalWebApi
{
    public class ConfigureMvcOptions: IConfigureOptions<MvcOptions>
    {
        private readonly IHostingEnvironment _env;

        public ConfigureMvcOptions(IHostingEnvironment env)
        {
            _env = env;
        }
        public void Configure(MvcOptions options)
        {
            if (_env.IsDevelopment())
            {
                options.SslPort = 44335;
            }
            else
            {
                options.Filters.Add(new RequireHttpsAttribute());
            }
        }
    }
}