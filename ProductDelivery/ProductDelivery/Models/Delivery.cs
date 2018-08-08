using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductDelivery.Models
{
	[Table("Delivery")]

	public class Delivery
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		public int Status { get; set; }

		public double Expdate { get; set; }

		public string Date { get; set; }

		public string ClientName { get; set; }

		public string title { get; set; }

		public int priority { get; set; }

		public string description { get; set; }

		public string Comment { get; set; }

		public string Note { get; set; }

		public double Lat { get; set; }
		public double Lon { get; set; }

		public string Imageurl { get; set; }

		public string EmployeeId { get; set; }

		public string CustomerId { get; set; }

		public string ProductId { get; set; }
	}
}
