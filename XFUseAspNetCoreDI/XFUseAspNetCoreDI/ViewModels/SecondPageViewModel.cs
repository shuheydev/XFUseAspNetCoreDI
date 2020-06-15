using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
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

        //private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;

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
        public SecondPageViewModel(IHttpClientFactory httpClientFactory = null)
        {
            this._httpClientFactory = httpClientFactory;

            this.Message = "This is Second page!";

            //this._httpClient = httpClientFactory.CreateClient();

            GetPrefecturesDataCommand = new Command(async (_) =>
            {
                //HttpClientを名前で取得
                var httpClient = this._httpClientFactory.CreateClient("covid19_japan");

                List<Prefecture> prefecturesData=null;
                HttpResponseMessage response = null;

                #region Method1 responseを受け取ってから文字列→System.Text.Jsonを使ってオブジェクトへ
                //リクエストを投げて,結果を取得する
                //BaseAddressを予め設定してあるので,BaseAddress以降をパラメータとして与えるだけでよい
                response = await httpClient.GetAsync("prefectures");

                if(response.IsSuccessStatusCode)
                {
                    //response.EnsureSuccessStatusCode();

                    //レスポンスからJSON文字列を取得
                    var prefecturesJsonString = await response.Content.ReadAsStringAsync();

                    //JSON文字列をデシリアライズしてList<Prefecture>型のデータに変換
                    prefecturesData = JsonSerializer.Deserialize<List<Prefecture>>(prefecturesJsonString);
                }
                else
                {

                }
                #endregion

                #region Method2 直接オブジェクトに変換する System.Net.Http.Json
                try
                {
                    prefecturesData = await httpClient.GetFromJsonAsync<List<Prefecture>>("prefectures");
                }
                catch (Exception ex)
                {

                }
                #endregion

                #region Method3 responseのJsonからオブジェクトへ System.Net.Http.Json
                 response = await httpClient.GetAsync("prefectures");

                if(response.IsSuccessStatusCode)
                {
                    prefecturesData = await response.Content.ReadFromJsonAsync<List<Prefecture>>();
                }
                else
                {

                }
                #endregion

                //プロパティに入れる
                this.PrefecturesData = prefecturesData;
            });
        }
    }
}
