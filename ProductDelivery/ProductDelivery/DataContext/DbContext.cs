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
			get {
				if(instance ==null)
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

		public int Save <T> (T data)
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

		public void InsertMockData()
		{
			List<Employee> emplist = new List<Employee>
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

			SaveList(emplist);
		}
	}
}
