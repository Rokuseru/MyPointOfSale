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
            string con = @"Data Source=DESKTOP-8AL5GOK\SQLEXPRESS;Initial Catalog=MY_POS_DEMO_DB;Integrated Security=True";
            return con;
        }
    }
}
