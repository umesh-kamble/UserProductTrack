using Caliburn.Micro.Xamarin.Forms;
using ProductDelivery.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ProductDelivery.ViewModels
{
   public class LoginViewModel:BaseScreen
    {
        private readonly INavigationService _navigationService;
        public LoginViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        #region Property
        public string Username { get; set; }
        public string Password { get; set; }
        public bool AreCredentialsInvalid { get; set; }
        #endregion
        public async void AuthenticateCommand()
        {
            Employee employee = new Employee { EmployeeName = Username, Password = this.Password };
             var emp = new EmployeeDb().EmployeeExists(employee);
            if (emp !=null) {
                Preferences.Set("IsLogged", true);
               await _navigationService.NavigateToViewModelAsync(typeof(MainViewModel));
            }
            else
           await App.Current.MainPage.DisplayAlert("Login failed", "Please try again!", "OK");
        }
    }
}
