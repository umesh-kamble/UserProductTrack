using Caliburn.Micro.Xamarin.Forms;
using ProductDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductDelivery.ViewModels
{
    public class DeliveryDetailViewModel : BaseScreen
    {
        public IEnumerable<Delivery> DeliveryList { get; set; }

        public DeliveryDetailViewModel()
        {
            DeliveryList = DbContext.Instance.DbConn.Table<Delivery>().ToList();
        }
    }
}
