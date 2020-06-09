using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFUseAspNetCoreDI.Views;

namespace XFUseAspNetCoreDI
{
    public partial class App : Application
    {
        public App(MainPage mainPage = null)
        {
            InitializeComponent();

            MainPage = new NavigationPage(mainPage);
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
