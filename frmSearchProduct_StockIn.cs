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
    public partial class frmSearchProduct_StockIn : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dataReader;
        DBConnection dbCon = new DBConnection();
        frmStockIn frmStock = new frmStockIn();

        [Obsolete]
        public frmSearchProduct_StockIn(frmStockIn stockIn)
        {
            InitializeComponent();

            conn = new SqlConnection(dbCon.myConnection());
            frmStock = stockIn;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colName = dataGridView1.Columns[e.ColumnIndex].Name;

                if (colName == "Select")
                {
                    if (frmStock.txtbRefNo.Text == String.Empty)
                    {
                        MessageBox.Show("Please enter a reference number", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmStock.txtbRefNo.Focus();
                        return;
                    }
                    if (frmStock.txtbStockInBy.Text == String.Empty)
                    {
                        MessageBox.Show("Please enter stock in by", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmStock.txtbStockInBy.Focus();
                        return;
                    }

                    if (MessageBox.Show("Add this item?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        conn.Open();

                        cmd = new SqlCommand("INSERT INTO tblStockIn (refno, pcode, sidate, stockindateby) VALUES (@refno, @pcode, @sidate, @stockindateby)", conn);/* + dataGridView1.Rows[e.RowIndex].Cells[1].ToString() + "'", conn);*/
                        cmd.Parameters.AddWithValue("@refno", frmStock.txtbRefNo.Text);
                        cmd.Parameters.AddWithValue("@pcode", dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                        cmd.Parameters.AddWithValue("@sidate", frmStock.dtpStockInDate.Value.ToString("dd-MMM-yyyy"));
                        cmd.Parameters.AddWithValue("@stockindateby", frmStock.txtbStockInBy.Text);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Added Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmStock.loadStockIn();
                        
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void loadProduct()
        {
            try
            {
                int i = 0;
                dataGridView1.Rows.Clear();
                conn.Open();
                cmd = new SqlCommand("SElECT pcode, pdesc, quantity FROM tblProduct ORDER BY pdesc", conn);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    i += 1;
                    dataGridView1.Rows.Add(i, dataReader[0].ToString(), dataReader[1].ToString(), dataReader[2].ToString());
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void metrotbSearch_TextChanged(object sender, EventArgs e)
        {
            loadProduct();
        }
    }
}
