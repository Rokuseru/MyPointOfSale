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
    public partial class frmProductList : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBConnection dbCon = new DBConnection();
        SqlDataReader dR;
        public frmProductList()
        {
            InitializeComponent();
            conn = new SqlConnection(dbCon.myConnection());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmProduct product = new frmProduct(this);
            product.btnUpdate.Enabled = false;
            product.btnSave.Enabled = true;
            product.loadCategory();
            product.loadBrand();
            product.ShowDialog();
        }

        private void metrotbSearch_TextChanged(object sender, EventArgs e)
        {
            loadRecords();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;

            if (colName == "Edit")
            {
                frmProduct product = new frmProduct(this);
                product.btnSave.Enabled = false;
                product.btnUpdate.Enabled = true;
                product.txtbProductCode.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                product.txtbDescription.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                product.txtbPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                product.cBoxBrand.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                product.cBoxCategory.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                product.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Delete Record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)==DialogResult.Yes)
                {
                    conn.Open();
                    cmd = new SqlCommand("DELETE FROM tblProduct WHERE pcode LIKE '"+dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()+"'",conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    loadRecords();
                }
            }
        }
        public void loadRecords()
        {
            int i = 0;

            dataGridView1.Rows.Clear();
            conn.Open();
            cmd = new SqlCommand("SELECT p.pcode, p.pdesc, b.brand, c.category, p.price, p.quantity FRom tblProduct p LEFT JOIN tblBrand b on p.bid = b.id LEFT join tblCategory c on p.cid = c.id where p.pdesc LIKE '" + metrotbSearch.Text+"%'", conn);
            dR = cmd.ExecuteReader();
            while (dR.Read())
            {
                i += 1;
                dataGridView1.Rows.Add(i, dR[0].ToString(), dR[1].ToString(), dR[2].ToString(), dR[3].ToString(), dR[4].ToString(), dR[5].ToString());
            }
            dR.Close();
            conn.Close();
        }
    }
}
