using System.Collections.Generic;
using System.IO;
using System.Linq;
using SQLite;
using System;

namespace ProductDelivery
{
    [Table("todoitem")]
    public class TodoItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public bool Done { get; set; }
    }
    interface IDBInterface
    {
        SQLiteConnection CreateConnection();
    }

    public class DbContext
    {
        SQLiteConnection database;
        public DbContext CreateConnection()
        {
            //Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TodoSQLite.db3")
            // database = new SQLiteConnection(path);

            var sqliteFilename = "TodoSQLite.db3";
            string documentsPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
           // string libraryPath = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(documentsPath, sqliteFilename);
           

            database = new SQLiteConnection(path);
            database.CreateTable<TodoItem>();
            database.CreateTable<User>();

            return this;
        }

        static object locker = new object();

        public IEnumerable<TodoItem> GetItems()
        {
            lock (locker)
            {
                return (from i in database.Table<TodoItem>() select i).ToList();
            }
        }

        public IEnumerable<TodoItem> GetItemsNotDone()
        {
            lock (locker)
            {
                return database.Query<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
            }
        }

        public TodoItem GetItem(int id)
        {
            lock (locker)
            {
                return database.Table<TodoItem>().FirstOrDefault(x => x.ID == id);
            }
        }

        public int SaveItem(TodoItem item)
        {
            lock (locker)
            {
                if (item.ID != 0)
                {
                    database.Update(item);
                    return item.ID;
                }
                else
                    return database.Insert(item);
            }
        }

        public int DeleteItem(int id)
        {
            lock (locker)
            {
                return database.Delete<TodoItem>(id);
            }
        }

        public void SaveData()
        {
            var todoItem1 = new TodoItem
            {
                Name = "Learn Xamarin Development",
                Notes = "Attend Xamarin University",
                Done = true
            };
            var todoItem2 = new TodoItem
            {
                Name = "Develop Cross-Platform Apps",
                Notes = "Use XAML",
                Done = false
            };
            var todoItem3 = new TodoItem
            {
                Name = "Publish Apps",
                Notes = "All stores",
                Done = false
            };

            SaveItem(todoItem1);
            SaveItem(todoItem2);
            SaveItem(todoItem3);
        }
    }


    //    static TodoItemDatabase database;

    //    public static TodoItemDatabase Database
    //    {
    //        get
    //        {
    //            if (database == null)
    //            {
    //                database = new TodoItemDatabase(
    //                  Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TodoSQLite.db3"));
    //            }
    //            return database;
    //        }
    //    }

    //    public TodoItemDatabase(string dbPath)
    //    {
    //        database = new SQLiteAsyncConnection(dbPath);
    //        database.CreateTableAsync<TodoItem>().Wait();
    //    }
    //}

    //internal class DbContext1 : IDBInterface
    //{
    //    private string path;
    //    public DbContext1()
    //    {
    //        var sqliteFilename = "Product.db";
    //        string documentsDirectoryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
    //        path = Path.Combine(documentsDirectoryPath, sqliteFilename);
    //    }
    //    public SQLiteConnection CreateConnection()
    //    {
    //        var db = new SQLiteConnection(path);

    //        // This is where we copy in our pre-created database
    //        return db;
    //    }
    //}
}



