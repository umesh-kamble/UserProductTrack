using Caliburn.Micro;
using Shared = App2; // required for VSTemplate

namespace App2.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(IoC.Get<Shared.App>());
        }
    }
}
