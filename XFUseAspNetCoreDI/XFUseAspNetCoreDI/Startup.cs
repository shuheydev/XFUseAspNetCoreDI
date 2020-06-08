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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nativeConfigureServices">ネイティブ側のConfigureServiceメソッドが渡される</param>
        public static void Init(Action<HostBuilderContext, IServiceCollection> nativeConfigureServices)
        {
            var a = Assembly.GetExecutingAssembly();
            using var stream = a.GetManifestResourceStream("XFUseAspNetCoreDI.appsettings.json");

            var host = new HostBuilder()
                .ConfigureHostConfiguration(c =>
                {
                    //これは?
                    c.AddCommandLine(new string[] { $"ContentRoot={FileSystem.AppDataDirectory}" });

                    //設定ファイルを読む
                    c.AddJsonStream(stream);
                })
                .ConfigureServices((c, x) =>
                {
                    //プラットフォーム固有機能のDIを行うためのコールバックを実行
                    nativeConfigureServices(c, x);
                    ConfigureServices(c, x);
                })
                .Build();

            ServiceProvider = host.Services;
        }

        /// <summary>
        /// プラットフォーム固有ではないサービスのDIはこちら
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="services"></param>
        static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            //開発と本番で登録するクラスを切り替える
            if (ctx.HostingEnvironment.IsDevelopment())
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
