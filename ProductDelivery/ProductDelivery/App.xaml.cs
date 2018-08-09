using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using ProductDelivery.Services;
using ProductDelivery.Utils;
using ProductDelivery.ViewModels;
using Xamarin.Essentials;
using ProductDelivery.DataContext;

namespace ProductDelivery
{
    public partial class App : FormsApplication
    {
        private readonly SimpleContainer _container;

        public App(SimpleContainer container)
        {
            InitializeComponent();

            this._container = container;

            // TODO: Register additional viewmodels and services
            _container
                // automatically register all viewmodels
                .AllTypesOf<BaseScreen>(GetType().GetTypeInfo().Assembly, ContainerRegistrationKind.PerRequest)
                // alternatively, register each viewmodel individually
                //.PerRequest<MainViewModel>()

                // register services
                .Singleton<IEventAggregator, EventAggregator>();
                
            // setup root page as a navigation page
            PrepareViewFirst();
            if (!container.HasHandler(typeof(LoginViewModel), null))
            {
                container
                    .PerRequest<LoginViewModel>()
                    .PerRequest<MainViewModel>()
                    .PerRequest<SignUpViewModel>();
            }
            // navigate to main view
            var myValue = Preferences.Get("IsLogged", false);
            if (!myValue)
            {
                container.GetInstance<INavigationService>()
               .For<LoginViewModel>()
               .Navigate(false);
            }
            else
            {
                container.GetInstance<INavigationService>()
              .For<MainViewModel>()
              .Navigate(false);
            }

        }

        protected override void PrepareViewFirst(NavigationPage navigationPage)
        {
            _container.Instance<INavigationService>(new NavigationPageAdapter(navigationPage));
        }


        protected override void OnStart()
        {
            // Handle when your app starts

            // Force container to create instances of all IApplicationServices by calling toArray()
            var services = _container.GetAllInstances<IApplicationService>().ToArray();

            foreach (var service in services)
            {
                service.Initialize();
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
