using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFUseAspNetCoreDI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Startup.Init();

            //ServiceProviderからMainPageのインスタンスを取得
            MainPage = Startup.ServiceProvider.GetService<MainPage>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
