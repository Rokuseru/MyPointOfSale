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
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        public void loadStockInHistory()
        {
            try
            {
                int i = 0;

                dataGridStockHistory.Rows.Clear();
                conn.Open();
                cmd = new SqlCommand("SELECT * FROM viewStockIn WHERE sidate BETWEEN '"+dt1.Value.ToString("dd-MMM-yyyy") +"' AND '"+ dt2.Value.ToString("dd-MMM-yyyy") + "' AND status LIKE 'Done'", conn);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    i += 1;
                    dataGridStockHistory.Rows.Add(i, dataReader["id"].ToString(), dataReader["refno"].ToString(), dataReader["pcode"].ToString(), dataReader["pdesc"].ToString(), DateTime.Parse(dataReader["sidate"].ToString()).ToString("dd-MMM-yyyy"), dataReader["stockindateby"].ToString(), dataReader["quantity"].ToString());
                }
                dataReader.Close();
                conn.Close();

            }
            catch (Exception ex)
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
                cmd = new SqlCommand(@"SELECT dbo.tblStockIn.id, dbo.tblStockIn.refno, dbo.tblStockIn.pcode, dbo.tblStockIn.sidate, dbo.tblStockIn.quantity, dbo.tblStockIn.stockindateby, dbo.tblProduct.pdesc 
                                       FROM dbo.tblStockIn INNER JOIN dbo.tblProduct ON dbo.tblStockIn.pcode = dbo.tblProduct.pcode WHERE refno LIKE '"+ txtbRefNo.Text +"' AND status LIKE 'Pending'" ,conn);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    i += 1;
                    dataGridView2.Rows.Add(i, dataReader["id"].ToString(), dataReader["refno"].ToString(), dataReader["pcode"].ToString(), dataReader["pdesc"].ToString(), DateTime.Parse(dataReader["sidate"].ToString()).ToString("dd-MMM-yyyy"), dataReader["stockindateby"].ToString(), dataReader["quantity"].ToString());
                }
                dataReader.Close();
                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtbRefNo_KeyPress(object sender, KeyPressEventArgs e)
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

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colName = dataGridView2.Columns[e.ColumnIndex].Name;
                if (colName == "Delete")
                {
                    if (MessageBox.Show("Delete this item?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        conn.Open();
                        cmd = new SqlCommand("DELETE FROM tblStockIn WHERE id = '" + dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", conn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Deleted Successfully", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        conn.Close();
                        loadStockIn();
                        refresh();
                    }

                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmStockIn_Load(object sender, EventArgs e)
        {
            loadStockIn();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            #pragma warning disable CS0612 // Type or member is obsolete
            frmSearchProduct_StockIn frmSearchProduct = new frmSearchProduct_StockIn(this);
            #pragma warning restore CS0612 // Type or member is obsolete
            frmSearchProduct.loadProduct();
            frmSearchProduct.ShowDialog();
        }
        public void clear()
        {
            txtbRefNo.Clear();
            txtbStockInBy.Clear();
            dtpStockInDate.Value = DateTime.Now;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
         
                if (dataGridView2.Rows.Count > 0)
                {
                    if (MessageBox.Show("Are you sure you want to save record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        for (int i = 0; i < dataGridView2.Rows.Count; i++)
                        {
                        //This will update tblProduct quantity
                        conn.Open();
                        cmd = new SqlCommand("UPDATE tblProduct SET quantity=quantity + "+ int.Parse(dataGridView2.Rows[i].Cells[7].Value.ToString())+" WHERE pcode LIKE '" + dataGridView2.Rows[i].Cells[3].Value.ToString() + "'",conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        //This will update tblStockIn quantity
                        conn.Open();
                        cmd = new SqlCommand("UPDATE tblStockIn SET quantity=quantity + "+ int.Parse(dataGridView2.Rows[i].Cells[7].Value.ToString()) +", status = 'Done' WHERE id LIKE '"+ dataGridView2.Rows[i].Cells[1].Value.ToString() + "'",conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        }
                        clear();
                        loadStockIn();
                    }
                }
        }
        public void refresh()
        {
            try
            {
                int i = 0;

                dataGridView2.Rows.Clear();
                conn.Open();
                cmd = new SqlCommand("SELECT * FROM viewStockIn", conn);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    i += 1;
                    dataGridView2.Rows.Add(i, dataReader["id"].ToString(), dataReader["refno"].ToString(), dataReader["pcode"].ToString(), dataReader["pdesc"].ToString(), DateTime.Parse(dataReader["sidate"].ToString()).ToString("dd-MMM-yyyy"), dataReader["stockindateby"].ToString(), dataReader["quantity"].ToString());
                }
                dataReader.Close();
                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void btnLoadRec_Click(object sender, EventArgs e)
        {
            loadStockInHistory();
        }
    }
}
