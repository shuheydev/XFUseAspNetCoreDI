using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;

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

        public ICommand GetPrefecturesDataCommand { get; }
        public SecondPageViewModel(IHttpClientFactory httpClientFactory)
        {
            this.Message = "This is Second page!";

            //this._httpClient = httpClientFactory.CreateClient();

            this._httpClient = httpClientFactory.CreateClient("covid19_japan");

            GetPrefecturesDataCommand = new Command(async (_) =>
            {
                //リクエストを投げて,結果を取得する
                //BaseAddressを予め設定してあるので,BaseAddress以降をパラメータとして与えるだけでよい
                var response = await this._httpClient.GetAsync("prefectures");

                //response.EnsureSuccessStatusCode();

                var prefecturesJsonString = await response.Content.ReadAsStringAsync();
            });
        }
    }
}
