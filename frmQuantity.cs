using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MyPointOfSale
{
    public partial class frmQuantity : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBConnection dbCon = new DBConnection();
        SqlDataReader dR;

        frmPointOfSale frmPoint;
        public frmQuantity(frmPointOfSale frmPointOfSale)
        {
            InitializeComponent();
            frmPoint = frmPointOfSale;
        }

        private void frmQuantity_Load(object sender, EventArgs e)
        {
            
        }
    }
}
