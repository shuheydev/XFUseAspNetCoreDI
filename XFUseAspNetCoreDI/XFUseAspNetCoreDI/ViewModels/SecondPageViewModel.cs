using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Text.Unicode;
using System.Windows.Input;
using XFUseAspNetCoreDI.Models.Covid19;

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

        private List<Prefecture> _prefecturesData;
        public List<Prefecture> PrefecturesData
        {
            get => _prefecturesData;
            set => SetProperty(ref _prefecturesData, value);
        }

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

                //レスポンスからJSON文字列を取得
                var prefecturesJsonString = await response.Content.ReadAsStringAsync();

                //JSON文字列をデシリアライズしてList<Prefecture>型のデータに変換
                var prefecturesData = JsonSerializer.Deserialize<List<Prefecture>>(prefecturesJsonString);

                //プロパティに入れる
                this.PrefecturesData = prefecturesData;
            });
        }
    }
}
