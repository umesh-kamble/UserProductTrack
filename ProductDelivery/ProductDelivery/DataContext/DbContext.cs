using System.Collections.Generic;
using System.IO;
using System.Linq;
using SQLite;
using System;
using ProductDelivery.Models;
using System.Data;

namespace ProductDelivery
{
	public class DbContext
	{
		public SQLiteConnection DbConn;

		protected static object locker = new object();

		private static DbContext instance;
		public static DbContext Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new DbContext();
				}
				return instance;
			}
		}

		private DbContext()
		{
			lock (locker)
			{
				var sqliteFilename = "ProductDeliverySQLite.db3";
				string documentsPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
				//string libraryPath = Path.Combine(documentsPath, "..", "Library");
				var path = Path.Combine(documentsPath, sqliteFilename);

				DbConn = new SQLiteConnection(path);
				this.IntializeTable();
			}
		}

		private void IntializeTable()
		{
			if (!TableExists<Employee>())
			{
				DbConn.CreateTable<Employee>();
				DbConn.CreateTable<Product>();
				DbConn.CreateTable<Delivery>();
				DbConn.CreateTable<Customer>();
				InsertMockData();
			}
		}
		public bool TableExists<T>()
		{
			const string cmdText = "SELECT name FROM sqlite_master WHERE type='table' AND name=?";
			var cmd = DbConn.CreateCommand(cmdText, typeof(T).Name);
			return cmd.ExecuteScalar<string>() != null;
		}

		public int Save<T>(T data)
		{
			lock (locker)
			{
				return DbConn.Insert(data);
			}
		}

		public int SaveList<T>(List<T> dataList)
		{
			lock (locker)
			{
				return DbConn.InsertAll(dataList);
			}
		}

		public List<T> GetList<T>() where T : new()
		{
			lock (locker)
			{
				return DbConn.Table<T>().ToList<T>();
			}
		}

		public void InsertMockData()
		{
			List<Employee> empList = new List<Employee>
			{
				new Employee
				{
					EmployeeName = "Rehan",
					Password = "123",
					Active = true

				},new Employee
				{
					EmployeeName = "Umesh",
					Password = "123",
					Active = true

				},new Employee
				{
					EmployeeName = "Prasad",
					Password = "123",
					Active = false
				}
			};

			List<Product> productList = new List<Product>
			{
				new Product{ Id =1, ProductName="Honor 7A (Gold, 32 GB)  (3 GB RAM)", ProductType="Mobile (Electronic)"},
				new Product{ Id =2, ProductName="Peter England University Men's Printed Casual Spread Shirt", ProductType="Clothing"},
				new Product{ Id =3, ProductName="Philips 8.5 W Round B22 LED Bulb ", ProductType="LED Bulb (Electronic)"}
			};

			List<Customer> customerList = new List<Customer>
			{
				new Customer{ Id =1, CustomerName ="Prasad Raghorte", Address="Jai MahaKali Chawk Nagpur", MobileNo =9665603404, AlternateNo=00},
				new Customer{ Id =2,CustomerName ="Parvez", Address="Muslim Ground Mominpura Nagpur", MobileNo =9021362236, AlternateNo=00},
				new Customer{ Id =3,CustomerName ="Kamble", Address="Near Jyanti Mention Manish Nagar Nagpur", MobileNo =9665603404, AlternateNo=00}
			};

			List<Delivery> deliveryList = new List<Delivery>
			{
				new Delivery{ CustomerId =1, ClientName ="Prasad Raghorte", Address="Jai MahaKali Chawk Nagpur", ProductId=1 },
				new Delivery{ CustomerId =2, ClientName ="Parvez", Address="Muslim Ground Mominpura Nagpur", ProductId=2},
				new Delivery{ CustomerId =3, ClientName ="Kamble", Address="Near Jyanti Mention Manish Nagar Nagpur", ProductId=3},
			};



			SaveList(empList);
			SaveList(productList);
			SaveList(customerList);
			SaveList(deliveryList);
		}
	}
}
