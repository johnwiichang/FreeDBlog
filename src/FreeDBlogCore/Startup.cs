using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeDBlogCore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FreeDBlogCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.Configure<AppSettings>(x =>
            {
                x.FilePath = Configuration.GetSection("Data:BlogHtmlUri:Folder").Value;
                x.RequestDomain = Configuration.GetSection("Data:BlogHtmlUri:RequestDomain").Value;
                x.PageNaviNum = Int32.Parse(Configuration.GetSection("Data:PageNavi:Max").Value);
                x.UserName = Configuration.GetSection("Admin:UserName").Value;
                x.Password = Configuration.GetSection("Admin:Password").Value;
                x.DNXFolder = "./wwwroot/" + x.FilePath + "/";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
