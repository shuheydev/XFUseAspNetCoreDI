using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Essentials;
using XFUseAspNetCoreDI.Services;
using XFUseAspNetCoreDI.ViewModels;

namespace XFUseAspNetCoreDI
{
    public class Startup
    {
        public static IServiceProvider ServiceProvider { get; set; }
        public static void Init()
        {
            var a = Assembly.GetExecutingAssembly();
            using var stream = a.GetManifestResourceStream("XFUseAspNetCoreDI.appsettings.json");

            var host = new HostBuilder().ConfigureHostConfiguration(c =>
                {
                    //これは?
                    c.AddCommandLine(new string[] { $"ContentRoot={FileSystem.AppDataDirectory}" });

                    //設定ファイルを読む
                    c.AddJsonStream(stream);
                })
                .ConfigureServices((c, x) =>
                {

                    ConfigureServices(c, x);
                })
                .ConfigureLogging(l=>l.AddConsole(o=> {
                    o.DisableColors = true;
                }))
                .Build();

            ServiceProvider = host.Services;
        }

        static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            if(ctx.HostingEnvironment.IsDevelopment())
            {
                services.AddSingleton<IDataService, MockDataService>();
            }
            else
            {
                services.AddSingleton<IDataService, DataService>();
            }

            services.AddTransient<MainPageViewModel>();
            services.AddTransient<MainPage>();
        }
    }
}
