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
    public partial class frmPointOfSale : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBConnection dbCon = new DBConnection();
        SqlDataReader dR;
        public frmPointOfSale()
        {
            InitializeComponent();
            conn = new SqlConnection(dbCon.myConnection());

            this.KeyPreview = true;
            
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            mainForm mainForm = new mainForm();
            this.Close();
        }

        private void frmPointOfSale_Load(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToLongDateString();
        }

        private void getTransacNo()
        {
            try
            {
                string sdate = DateTime.Now.ToString("yyyyMMdd");
                string transNo;
                int count;
                conn.Open();
                cmd = new SqlCommand("SELECT TOP 1 transacno FROM tblCart WHERE transacno LIKE '"+ sdate +"%' ORDER BY id DESC", conn);
                dR = cmd.ExecuteReader();
                dR.Read();
                if (dR.HasRows)
                {
                    transNo = dR[0].ToString();
                    count = int.Parse(transNo.Substring(8, 4));
                    lblTransNo.Text = sdate + (count + 1);
                }
                else
                {
                    transNo = sdate + "1001";
                    lblTransNo.Text = transNo;
                }
                dR.Close();
                conn.Close();


            }catch(Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNewTransac_Click(object sender, EventArgs e)
        {
            getTransacNo();
        }

        private void mtbScanBc_Click(object sender, EventArgs e)
        {
      
        }

        private void mtbScanBc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (mtbScanBc.Text == String.Empty)
                {
                    return;
                }
                else
                {
                    conn.Open();
                    cmd = new SqlCommand("SELECT * FROM tblProduct WHERE barcode LIKE'" + mtbScanBc.Text + "'", conn);
                    dR = cmd.ExecuteReader();
                    dR.Read();
                    if (dR.HasRows)
                    {
                        frmQuantity frmQuan = new frmQuantity(this);
                        frmQuan.ShowDialog();
                    }
                    dR.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
