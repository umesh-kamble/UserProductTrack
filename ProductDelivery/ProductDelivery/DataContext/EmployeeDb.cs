using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductDelivery.DataContext
{
	public class EmployeeDb : DbContext
	{
		public EmployeeDb()
		{
			CreateConnection();
		}

		public IEnumerable<Employee> GetEmployList()
		{
			lock (locker)
			{
				return (from i in database.Table<Employee>() select i).ToList();
			}
		}

		public Employee GetEmployee(int id)
		{
			lock (locker)
			{
				return database.Table<Employee>().FirstOrDefault(x => x.EmployeeId == id);
			}
		}

		public Employee EmployeeExists(Employee employee)
		{
			lock (locker)
			{

				return database.Table<Employee>().FirstOrDefault(x => x.EmployeeName == employee.EmployeeName && x.Password == employee.Password);
			}
		}

		public int SaveEmployee(Employee employee)
		{
			lock (locker)
			{
				if (employee.EmployeeId != 0)
				{
					database.Update(employee);
					return employee.EmployeeId;
				}
				else
					return database.Insert(employee);
			}
		}

		public int DeleteEmployee(int id)
		{
			lock (locker)
			{
				return database.Delete<Employee>(id);
			}
		}

		public void SaveEmployee()
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
