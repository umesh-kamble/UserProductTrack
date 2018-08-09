using Caliburn.Micro.Xamarin.Forms;
using ProductDelivery.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProductDelivery.ViewModels
{
    public class MainViewModel : BaseScreen
    {
        private readonly INavigationService _navigationService;
        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            DisplayName = "Welcome!";
            IntroLabel = "Hello World via Caliburn.Micro!";
            Name = "Umesh Kamble";
            GetItemList();
        }

        private string _label;
        public IEnumerable<Employee> ItemList { get; set; }

        public string IntroLabel
        {
            get { return _label; }
            set { Set(ref _label, value); }
        }
        public string Name { get; set; }

        private void GetItemList()
        {
            ItemList = new EmployeeDb().GetEmployList();
        }

        public async void NavigatetoDelivery()
        {
            //NavigationPage navigationPage = new NavigationPage();
            //NavigationPageAdapter navigationPageAdapter = new NavigationPageAdapter(navigationPage);
            await _navigationService.NavigateToViewModelAsync(typeof(DeliveryDetailViewModel));
        }
    }
}
