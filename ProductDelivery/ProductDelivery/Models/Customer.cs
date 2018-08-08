using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductDelivery.Models
{
	[Table("Customer")]

	public class Customer
	{
		[PrimaryKey, AutoIncrement]
		public Int32 Id { get; set; }

		public string CustomerName { get; set; }

		public string Address { get; set; }

		public decimal MobileNo { get; set; }

		public decimal AlternateNo { get; set; }
	}

   
}
