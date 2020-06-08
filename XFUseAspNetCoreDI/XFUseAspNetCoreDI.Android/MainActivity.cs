using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.Res;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using XFUseAspNetCoreDI.Services;
using XFUseAspNetCoreDI.Droid.Services;

namespace XFUseAspNetCoreDI.Droid
{
    [Activity(Label = "XFUseAspNetCoreDI", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            //セットアップ
            Startup.Init(ConfigureServices);

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        /// <summary>
        /// プラットフォーム固有の機能のDIはこちら
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="services"></param>
        private void ConfigureServices(HostBuilderContext ctx,IServiceCollection services)
        {
            //ここにDIしたいクラスを追加する
            //今回は通知機能
            services.AddSingleton<INotificationService, AndroidNotificationService>();
        }
    }
}