using System;
using System.Collections.Generic;
using System.Net.Http;
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

        private readonly HttpClient _httpClient;

        //public SecondPageViewModel(HttpClient httpClient)
        //{
        //    this.Message = "This is Second page!";

        //    this._httpClient = httpClient;
        //}

        public SecondPageViewModel(IHttpClientFactory httpClientFactory)
        {
            this.Message = "This is Second page!";

            this._httpClient = httpClientFactory.CreateClient();
        }
    }
}
