using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductDelivery.Models
{
    [Table("Product")]

    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public Int32 Id { get; set; }

        public string ProductName { get; set; }
        public string ProductType { get; set; }
        
    }
}
