using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XFUseAspNetCoreDI.Models;
using XFUseAspNetCoreDI.Services;

namespace XFUseAspNetCoreDI.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly IDataService _dataService;

        private ICollection<Person> _people;
        public ICollection<Person> People
        {
            get => _people;
            set => SetProperty(ref _people, value);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public ICommand GetPeopleCommand { get; }

        public MainPageViewModel(IDataService dataService)
        {
            this.Message = "Hello, Dependency Injection!";
            this._dataService = dataService;

            GetPeopleCommand = new Command(_ => this.People = _dataService.FindAll());
        }
    }
}
