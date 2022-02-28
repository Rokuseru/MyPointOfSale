using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MyPointOfSale
{
    public partial class mainForm : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBConnection dbCon = new DBConnection();
        public mainForm()
        {
            InitializeComponent();

            conn = new SqlConnection(dbCon.myConnection());
            conn.Open();
        }
        

        private void btnManageBrand_Click(object sender, EventArgs e)
        {
            frmBrandList brandList = new frmBrandList();
            brandList.TopLevel = false;
            mainFormPanel.Controls.Add(brandList);
            brandList.BringToFront();
            brandList.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            frmCategoryLists categoryLists = new frmCategoryLists();
            categoryLists.TopLevel = false;
            mainFormPanel.Controls.Add(categoryLists);
            categoryLists.BringToFront();
            categoryLists.loadCategory();
            categoryLists.Show();
        }

        private void btnManageProduct_Click(object sender, EventArgs e)
        {
            #pragma warning disable CS0612 // Type or member is obsolete
            frmProductList productList = new frmProductList();
            #pragma warning restore CS0612 // Type or member is obsolete
            productList.TopLevel = false;
            mainFormPanel.Controls.Add(productList);
            productList.BringToFront();
            productList.loadRecords();
            productList.Show();          
        }

        private void btnStockIn_Click(object sender, EventArgs e)
        {
            frmStockIn frmStock = new frmStockIn();
            frmStock.TopLevel = false;
            mainFormPanel.Controls.Add(frmStock);
            frmStock.loadStockIn();
            frmStock.Show();
        }

        private void btnShowPos_Click(object sender, EventArgs e)
        {
            frmPointOfSale pointOfSale = new frmPointOfSale();
            pointOfSale.Show();
        }
    }
}
