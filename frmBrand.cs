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
    public partial class frmBrand : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBConnection dbCon = new DBConnection();
        frmBrandList frmList;
        public frmBrand(frmBrandList List)
        {
            InitializeComponent();
            conn = new SqlConnection(dbCon.myConnection());
            frmList = List;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to add this brand?", "", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    conn.Open();
                    cmd = new SqlCommand("INSERT INTO tblBrand(Brand) VALUES (@brand)", conn);
                    cmd.Parameters.AddWithValue("@brand", txtBoxBrandName.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Brand has been added.");
                    clear();

                    frmList.loadRecords();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void clear()
        {
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            txtBoxBrandName.Clear();
            txtBoxBrandName.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmBrand_Load(object sender, EventArgs e)
        {
 
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this brand?", "Update Record", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    conn.Open();
                    cmd = new SqlCommand("UPDATE tblBrand SET Brand = @brand WHERE ID LIKE '" +lblD.Text+"'", conn);
                    cmd.Parameters.AddWithValue("@brand", txtBoxBrandName.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Updated Successfully.");
                    clear();
                    frmList.loadRecords();
                    this.Dispose();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Cancel Operation?", "Abort", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    this.Close();
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
