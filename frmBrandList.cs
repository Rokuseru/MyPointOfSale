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
    public partial class frmBrandList : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dataReader;
        DBConnection dbCon = new DBConnection();
        public frmBrandList()
        {
            InitializeComponent();

            conn = new SqlConnection(dbCon.myConnection());
            loadRecords();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void loadRecords()
        {
            int i = 0;

            dataGridView1.Rows.Clear();
            conn.Open();
            cmd = new SqlCommand("SELECT * FROM tblBrand ORDER BY Brand", conn);
            dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                i += 1;
                dataGridView1.Rows.Add(i, dataReader["id"].ToString(), dataReader["Brand"].ToString());
            }
            dataReader.Close();
            conn.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            frmBrand frmBrand = new frmBrand(this);
            frmBrand.btnUpdate.Enabled = false;
            frmBrand.btnSave.Enabled = true;
            frmBrand.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                frmBrand brand = new frmBrand(this);
                brand.lblD.Text = dataGridView1[1, e.RowIndex].Value.ToString();
                brand.txtBoxBrandName.Text = dataGridView1[2, e.RowIndex].Value.ToString();
                brand.btnSave.Enabled = false;
                brand.btnUpdate.Enabled = true;
                brand.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if(MessageBox.Show("Are you sure to delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cmd = new SqlCommand("DELETE FROM tblBrand WHERE ID LIKE '"+dataGridView1[1,e.RowIndex].Value.ToString()+"'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Deleted Successfully", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadRecords();
                }
            }
        }
    }
}
