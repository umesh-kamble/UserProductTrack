using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProductDelivery
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            DbContext db = new DbContext().CreateConnection();
            db.SaveData();
            var list = db.GetItems();
            listView.ItemsSource = list;

        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //((App)App.Current).ResumeAtTodoId = (e.SelectedItem as TodoItem).ID;
            //Debug.WriteLine("setting ResumeAtTodoId = " + (e.SelectedItem as TodoItem).ID);
            if (e.SelectedItem != null)
            {
                await DisplayAlert("title",e.SelectedItem.ToString(),"Okay");
                //await Navigation.PushAsync(new TodoItemPage
                //{
                //    BindingContext = e.SelectedItem as TodoItem
                //});
            }
        }
    }
}
