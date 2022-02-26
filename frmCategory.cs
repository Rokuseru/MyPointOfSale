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
    public partial class frmCategory : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBConnection dbCon = new DBConnection();
        frmCategoryLists categoryLists;
        public frmCategory(frmCategoryLists frmCategory)
        {
            InitializeComponent();

            conn = new SqlConnection(dbCon.myConnection());
            categoryLists = frmCategory;

        }
        public void clear()
        {
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            txtBoxCategory.Clear();
            txtBoxCategory.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to add this category?", "Saving Category...", MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    conn.Open();
                    cmd = new SqlCommand("INSERT INTO tblCategory(Category) VALUES (@category)", conn);
                    cmd.Parameters.AddWithValue("@category", txtBoxCategory.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Added Successfully", "Saved!");
                    clear();
                    categoryLists.loadCategory();
                }
            }
            catch(Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Update Category?", "Update Record",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cmd = new SqlCommand("UPDATE tblCategory SET Category = @category WHERE ID LIKE '" + lblD.Text + "'", conn);
                    cmd.Parameters.AddWithValue("@category", txtBoxCategory.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Updated Successfully.");
                    clear();
                    categoryLists.loadCategory();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Cancel Operation?", "Abort Operation...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
