using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XFUseAspNetCoreDI.Models;
using XFUseAspNetCoreDI.Services;
using XFUseAspNetCoreDI.Views;

namespace XFUseAspNetCoreDI.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        private ICollection<Person> _people;
        public ICollection<Person> People
        {
            get => _people;
            set => SetProperty(ref _people, value);
        }

        public ICommand GetPeopleCommand { get; }
        public ICommand ShowNotificationCommand { get; }
        public ICommand NavigateToSecondPageCommand { get; }

        private readonly IDataService _dataService;
        private readonly INotificationService _notificationService;

        public MainPageViewModel(IDataService dataService, INotificationService notificationService, SecondPage secondPage)
        {
            this.Message = "Hello, Dependency Injection!";
            this._dataService = dataService;
            this._notificationService = notificationService;

            GetPeopleCommand = new Command(_ =>
            {
                this.People = _dataService.FindAll();
            });

            ShowNotificationCommand = new Command(_ =>
            {
                _notificationService.ScheduleNotification("My Notification", "This is Test");
            });

            NavigateToSecondPageCommand = new Command(_ =>
            {
                //App.Current.MainPage.Navigation.PushAsync(Startup.ServiceProvider.GetService<SecondPage>());
                App.Current.MainPage.Navigation.PushAsync(secondPage);
            });
        }
    }
}
