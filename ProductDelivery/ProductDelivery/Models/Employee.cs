using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductDelivery
{
	[Table("Employee")]
	public class Employee
    {
		public string EmployeeName { get; set; }

		[PrimaryKey, AutoIncrement]
		public Int32 EmployeeId { get; set; }

		public string Password { get; set; }

		public string Role { get; set; }

		public bool Active { get; set; }
    }
}
