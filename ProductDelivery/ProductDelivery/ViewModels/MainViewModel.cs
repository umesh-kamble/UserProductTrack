using ProductDelivery.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDelivery.ViewModels
{
    public class MainViewModel : BaseScreen
    {
        private string _label;

        public MainViewModel()
        {
            DisplayName = "Welcome!";
            IntroLabel = "Hello World via Caliburn.Micro!";
            Name = "Umesh Kamble";
            GetItemList();
        }

        private void GetItemList()
        {
            ItemList = new EmployeeDb().GetEmployList();
        }

        public IEnumerable<Employee> ItemList { get; set; }

        public string IntroLabel
        {
            get { return _label; }
            set { Set(ref _label, value); }
        }
        public string Name { get; set; }
    }
}
