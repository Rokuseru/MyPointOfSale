using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPointOfSale
{
    public partial class frmProductList : Form
    {
        public frmProductList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmProduct product = new frmProduct();
            product.btnUpdate.Enabled = false;
            product.btnSave.Enabled = true;
            product.loadCategory();
            product.loadBrand();
            product.ShowDialog();
        }
    }
}
