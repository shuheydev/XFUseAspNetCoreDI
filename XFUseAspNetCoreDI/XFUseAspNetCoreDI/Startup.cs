using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using Xamarin.Essentials;
using XFUseAspNetCoreDI.Services;
using XFUseAspNetCoreDI.ViewModels;
using XFUseAspNetCoreDI.Views;
using Microsoft.Extensions.Configuration.EnvironmentVariables;

namespace XFUseAspNetCoreDI
{
    public class Startup
    {
        public static IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nativeConfigureServices">ネイティブ側のConfigureServiceメソッドが渡される</param>
        public static App Init(Action<HostBuilderContext, IServiceCollection> nativeConfigureServices)
        {
            var assm = Assembly.GetExecutingAssembly();
            using var stream = assm.GetManifestResourceStream("XFUseAspNetCoreDI.appsettings.json");

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

            return ServiceProvider.GetService<App>();
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

                //appsettings.json内の値を取得する
                //1.GetValueメソッドを使う
                var val1 = ctx.Configuration.GetValue<string>("Hello");// World
                //2.Configurationからインデクサで取得
                var val2 = ctx.Configuration["Hello"];// World
                //3.2階層目の値を取得する
                var val3 = ctx.Configuration["Xamarin:Forms"];//4.6
            }
            else
            {
                services.AddSingleton<IDataService, DataService>();
            }

            services.AddHttpClient("covid19_japan", c =>
            {
                c.BaseAddress = new Uri("https://covid19-japan-web-api.now.sh/api//v1/");
            });
            //services.AddSingleton<HttpClient>();


            services.AddTransient<MainPageViewModel>();
            services.AddTransient<MainPage>();
            services.AddTransient<SecondPageViewModel>();
            services.AddTransient<SecondPage>();
            services.AddSingleton<App>();
        }
    }
}
