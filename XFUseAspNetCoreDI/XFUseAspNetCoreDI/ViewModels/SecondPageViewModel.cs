using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace XFUseAspNetCoreDI.ViewModels
{
    public class SecondPageViewModel : BaseViewModel
    {
        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }
        public SecondPageViewModel()
        {
            this.Message = "This is Second page!";
        }
    }
}
