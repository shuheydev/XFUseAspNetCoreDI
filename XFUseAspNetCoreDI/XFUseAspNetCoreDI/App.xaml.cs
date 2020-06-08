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

            //ServiceProviderからMainPageのインスタンスを取得
            MainPage = Startup.ServiceProvider.GetService<MainPage>();//Xamarin.Forms.Xamlに定義されてる拡張メソッドGetService<T>
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
