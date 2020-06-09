using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFUseAspNetCoreDI.ViewModels;

namespace XFUseAspNetCoreDI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SecondPage : ContentPage
    {
        public SecondPage(SecondPageViewModel viewModel = null)
        {
            InitializeComponent();

            this.BindingContext = viewModel;
        }
    }
}