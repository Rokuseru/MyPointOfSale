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
        SqlDataReader dataReader;
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

        public void loadRecords()
        {
            dataGridView1.Rows.Clear();
            conn.Open();
            cmd = new SqlCommand("SELECT p.pcode, p.pdesc, b.brand, c.category, p.price FROM tblProduct as p FULL JOIN tblBrand AS b ON b.id = p.bid FULL JOIN tblCategory AS c ON c.id = p.cid WHERE p.pdesc LIKE'" + metrotbSearch.Text+ "%'", conn);
            dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                int i = 0;
                i++;
                dataGridView1.Rows.Add(i, dataReader[0].ToString(), dataReader[1].ToString(), dataReader[2].ToString(), dataReader[3].ToString());
            }

            dataReader.Close();
            conn.Close();
        }

        private void metrotbSearch_TextChanged(object sender, EventArgs e)
        {
            loadRecords();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.RowIndex].Name;

            if (colName == "Edit")
            {
                frmProduct product = new frmProduct(this);
                product.btnSave.Enabled = false;
                product.btnUpdate.Enabled = true;
                product.txtbProductCode.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                product.txtbDescription.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                product.txtbPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                product.cBoxBrand.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                product.cBoxCategory.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                product.ShowDialog();
            }
            else
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
    }
}
