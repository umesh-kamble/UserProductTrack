using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductDelivery
{
    [Table("User")]
    public class User
    {
        public string UserName { get; set; }

        [PrimaryKey, AutoIncrement]
        public Int32 UserId { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
