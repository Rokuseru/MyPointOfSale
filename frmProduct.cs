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
    public partial class frmProduct : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBConnection dbCon = new DBConnection();
        SqlDataReader dataReader;
        public frmProduct()
        {
            InitializeComponent();
            conn = new SqlConnection(dbCon.myConnection());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void loadBrand()
        {
            try
            {
                cBoxBrand.Items.Clear();
                conn.Open();
                cmd = new SqlCommand("SELECT brand FROM tblBrand", conn);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    cBoxBrand.Items.Add(dataReader[0].ToString());
                }
                dataReader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        public void loadCategory()
        {
            try
            {
                cBoxCategory.Items.Clear();
                conn.Open();
                cmd = new SqlCommand("SELECT category FROM tblCategory", conn);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    cBoxCategory.Items.Add(dataReader[0].ToString());
                }
                dataReader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
