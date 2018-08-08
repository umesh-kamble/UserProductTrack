using System.Collections.Generic;
using System.IO;
using System.Linq;
using SQLite;
using System;
using ProductDelivery.Models;

namespace ProductDelivery
{    
	interface IDBInterface
	{
		SQLiteConnection CreateConnection();
	}

    public class DbContext
    {
        protected SQLiteConnection database;
        protected static object locker = new object();

        public DbContext CreateConnection()
        {
            //Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TodoSQLite.db3")
            // database = new SQLiteConnection(path);

            var sqliteFilename = "TodoSQLite.db3";
            string documentsPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            // string libraryPath = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(documentsPath, sqliteFilename);


            database = new SQLiteConnection(path);
            IntializeTable();
            return this;
        }

        private void IntializeTable()
        {
            database.CreateTable<Employee>();
            database.CreateTable<Product>();
            database.CreateTable<Delivery>();
            database.CreateTable<Customer>();
        }
        public int Save <T> (T data)
        {
            lock (locker)
            {
                return database.Insert(data);
            }
        }

        public int SaveList<T>(List<T> dataList)
        {
            lock (locker)
            {
                return database.InsertAll(dataList);
            }
        }
    }
}



