using Caliburn.Micro.Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ProductDelivery.ViewModels
{
    public class SignUpViewModel : BaseScreen
    {
        private readonly INavigationService _navigationService;
        public SignUpViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }

        public async void RegisterCommand()
        {
            if (Password.Equals(RepeatPassword) && !string.IsNullOrEmpty(Username))
            {
                Preferences.Set("IsLogged", true);
                await _navigationService.NavigateToViewModelAsync(typeof(MainViewModel));
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Password Not Match", "Please try again!", "OK");
            }
        }
    }
}
