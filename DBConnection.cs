using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPointOfSale
{
    class DBConnection
    {
        public string myConnection()
        {
            string con = @"Data Source=desktop-8al5gok\sqlexpress;Initial Catalog=MyPointofsaleDemo_DB;Integrated Security=True";
            return con;
        }
    }
}
