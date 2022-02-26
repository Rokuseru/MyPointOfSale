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
        frmProductList prodList;
        public frmProduct(frmProductList frmProductList)
        {
            InitializeComponent();
            conn = new SqlConnection(dbCon.myConnection());
            prodList = frmProductList;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to add this product?", "Saving Product...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string bid = "";
                    string cid = "";
                    conn.Open();
                    cmd = new SqlCommand("SELECT id FROM tblBrand WHERE brand LIKE '" + cBoxBrand.Text + "'", conn);
                    dataReader = cmd.ExecuteReader();
                    dataReader.Read();
                    if (dataReader.HasRows) { bid = dataReader[0].ToString(); }
                    dataReader.Close();
                    conn.Close();

                    conn.Open();
                    cmd = new SqlCommand("SELECT id FROM tblCategory WHERE category LIKE '" + cBoxCategory.Text + "'", conn);
                    dataReader = cmd.ExecuteReader();
                    dataReader.Read();
                    if (dataReader.HasRows) { cid = dataReader[0].ToString(); }
                    dataReader.Close();
                    conn.Close();


                    conn.Open();
                    cmd = new SqlCommand("INSERT INTO tblProduct (pcode, pdesc, bid, cid, price) VALUES (@pcode, @pdesc, @bid, @cid, @price)", conn);
                    cmd.Parameters.AddWithValue("@pcode", txtbProductCode.Text);
                    cmd.Parameters.AddWithValue("@pdesc", txtbDescription.Text);
                    cmd.Parameters.AddWithValue("@bid", bid);
                    cmd.Parameters.AddWithValue("@cid", cid);
                    cmd.Parameters.AddWithValue("@price", txtbPrice.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Saved Successfuly");
                    clear();
                    prodList.loadRecords();
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message, "Error");
            }
           
        }
        public void clear()
        {
            txtbDescription.Clear();
            txtbPrice.Clear();
            cBoxBrand.Text = "";
            cBoxCategory.Text = "";
            txtbProductCode.Focus();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this product?", "Saving Product...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string bid = "";
                    string cid = "";
                    conn.Open();
                    cmd = new SqlCommand("SELECT id FROM tblBrand WHERE brand LIKE '" + cBoxBrand.Text + "'", conn);
                    dataReader = cmd.ExecuteReader();
                    dataReader.Read();
                    if (dataReader.HasRows) { bid = dataReader[0].ToString(); }
                    dataReader.Close();
                    conn.Close();

                    conn.Open();
                    cmd = new SqlCommand("SELECT id FROM tblCategory WHERE category LIKE '" + cBoxCategory.Text + "'", conn);
                    dataReader = cmd.ExecuteReader();
                    dataReader.Read();
                    if (dataReader.HasRows) { cid = dataReader[0].ToString(); }
                    dataReader.Close();
                    conn.Close();

                    conn.Open();
                    cmd = new SqlCommand("UPDATE tblProduct SET pdesc=@pdesc, bid=@bid, cid=@cid, price=@price", conn);
                    cmd.Parameters.AddWithValue("@pcode", txtbProductCode.Text);
                    cmd.Parameters.AddWithValue("@pdesc", txtbDescription.Text);
                    cmd.Parameters.AddWithValue("@bid", bid);
                    cmd.Parameters.AddWithValue("@cid", cid);
                    cmd.Parameters.AddWithValue("@price", txtbPrice.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Updated Successfuly");
                    clear();
                    prodList.loadRecords();
                    this.Dispose();

                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        
    }
}
