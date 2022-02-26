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
    public partial class frmCategoryLists : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBConnection dbCon = new DBConnection();
        SqlDataReader dataReader;
        public frmCategoryLists()
        {
            InitializeComponent();
            conn = new SqlConnection(dbCon.myConnection());
        }
        public void loadCategory()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            conn.Open();
            cmd = new SqlCommand("SELECT * FROM tblCategory ORDER BY category", conn);
            dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                i += 1;
                dataGridView1.Rows.Add(i, dataReader[0].ToString(), dataReader[1].ToString());
            }
            dataReader.Close();
            conn.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmCategory category = new frmCategory(this);
            category.btnSave.Enabled = true;
            category.btnUpdate.Enabled = false;
            category.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colName = dataGridView1.Columns[e.ColumnIndex].Name;

                if (colName == "Edit")
                {
                    frmCategory category = new frmCategory(this);
                    category.txtBoxCategory.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    category.lblD.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    category.btnSave.Enabled = false;
                    category.btnUpdate.Enabled = true;
                    category.ShowDialog();

                }else if (MessageBox.Show("Are you sure to delete this record?", "Delete Record", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cmd = new SqlCommand("DELETE FROM tblCategory WHERE ID LIKE '" + dataGridView1[1, e.RowIndex].Value.ToString() + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Deleted Successfully", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadCategory();
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
