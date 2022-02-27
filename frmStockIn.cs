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
    public partial class frmStockIn : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dataReader;
        DBConnection dbCon = new DBConnection();
        public frmStockIn()
        {
            InitializeComponent();
            conn = new SqlConnection(dbCon.myConnection());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void loadProduct()
        {

            try
            {
                int i = 0;
                dataGridView1.Rows.Clear();
                conn.Open();
                cmd = new SqlCommand("SElECT pcode, pdesc, quantity FROM tblProduct WHERE pdesc LIKE '%" + metrotbSearch.Text + "%' ORDER BY pdesc", conn);
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try

    {
                string colName = dataGridView1.Columns[e.ColumnIndex].Name;

                if (colName == "Select")
                {
                    if (MessageBox.Show("Add this item?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        conn.Open();

                        cmd = new SqlCommand("INSERT INTO tblStockIn (refno, pcode, sidate, stockindateby) VALUES (@refno, @pcode, @sidate, @stockindateby)",conn);/* + dataGridView1.Rows[e.RowIndex].Cells[1].ToString() + "'", conn);*/
                        cmd.Parameters.AddWithValue("@refno", txtbRefNo.Text);
                        cmd.Parameters.AddWithValue("@pcode", dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                        cmd.Parameters.AddWithValue("@sidate", dtpStockInDate.Value);
                        cmd.Parameters.AddWithValue("@stockindateby", txtbStockInBy.Text);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Added Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadStockIn();
                    }

                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void loadStockIn()
        {
            try
            {
                int i = 0;

                dataGridView2.Rows.Clear();
                conn.Open();

                cmd = new SqlCommand("SELECt * FROM viewStockIn" ,conn);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    i += 1;
                    dataGridView2.Rows.Add(i, dataReader["id"].ToString(), dataReader["refno"].ToString(), dataReader["pcode"].ToString(), dataReader["pdesc"].ToString(), dataReader["sidate"].ToString(), dataReader["stockindateby"].ToString(), dataReader["quantity"].ToString());
                }
                dataReader.Close();
                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtbStockInBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 46)
            {
                //accepts .(dot) character
            }
            else if (e.KeyChar == 8)
            {
                //accepts backspace
            }
            else if ((e.KeyChar < 48) || (e.KeyChar > 57))
            {
                //accepts numbers from 0-9
                e.Handled = true;
            }
        }
    }
}
