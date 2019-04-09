using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory
{
    public partial class Credit_Note_Help : Form
    {
        private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private DataTable fieldTable = new DataTable();
        private BindingSource source = new BindingSource();

        public DataGridViewCellCollection c;
        public Credit_Note_Help()
        {
            InitializeComponent();
        }

        private void Credit_Note_Help_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM tbl_CreditNote";
                adapter.SelectCommand = cmd;
                adapter.Fill(table);
                source.DataSource = table;
                dgItems.DataSource = source;

                fieldTable.Columns.Add("key");
                fieldTable.Columns.Add("value");
                cmbFilter.DataSource = fieldTable;
                cmbFilter.ValueMember = "key";
                cmbFilter.DisplayMember = "value";
                fieldTable.Rows.Add("CN_Id", "Id");
                fieldTable.Rows.Add("CN_Doc_No", "Doc No");

                fieldTable.Rows.Add("CN_Date", "Date Gen");
                fieldTable.Rows.Add("CN_DateHij", "Date Hij");

                fieldTable.Rows.Add("CN_Reffrence_No", "Doc Reference No");
                fieldTable.Rows.Add("CUSTOMER_CODE", "Customer Code");
                fieldTable.Rows.Add("CUSTOMER_NAME_ENG", "Customer Name");
                fieldTable.Rows.Add("NOTES", "NOTES");
                fieldTable.Rows.Add("CN_Balance", "Credit Balance");
                fieldTable.Rows.Add("CN_Sales_Man", "Sales Man");
                fieldTable.Rows.Add("Tax_Amount", "Tax Amount");
                fieldTable.Rows.Add("VAT", "VAT");
                fieldTable.Rows.Add("Gross_Amount", "Gross Amount");
                fieldTable.Rows.Add("Discount_Amount", "Discount Amount");
              



                DataGridViewColumnCollection c = dgItems.Columns;
                c["CN_Id"].HeaderText = "Id";
                c["CN_Doc_No"].HeaderText = "Doc. No.";
                c["CN_Date"].HeaderText = "Date[GRE]";
                c["CN_DateHij"].HeaderText = "Date Hij";
            
                c["CN_Reffrence_No"].HeaderText = "Doc Reference No";
                c["CUSTOMER_CODE"].HeaderText = "Customer Code";
                c["CUSTOMER_NAME_ENG"].HeaderText = "Customer Name";
                c["NOTES"].HeaderText = "NOTES";
                c["CN_Balance"].HeaderText = "Credit Balance";
                c["CN_Sales_Man"].HeaderText = "Sales Man";
                c["Tax_Amount"].HeaderText = "Tax Amount";

                c["VAT"].HeaderText = "VAT";
                c["Gross_Amount"].HeaderText = "Gross Amount";
                c["Discount_Amount"].HeaderText = "Discount Amount";
              

            }
            catch
            {
                conn.Close();
                MessageBox.Show("Eror");
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null)
            {
                c = dgItems.CurrentRow.Cells;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null && MessageBox.Show("Are you sure?", "Delete Credit Note", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = "DELETE FROM tbl_CreditNote WHERE CN_Id = '" + dgItems.CurrentRow.Cells[0].Value;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Credit Note Deleted Deleted!");
                    dgItems.Rows.Remove(dgItems.CurrentRow);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (txtFilter.Text == "")
            {
                source.Filter = "";
            }
            else
            {
                source.Filter = cmbFilter.SelectedValue + " LIKE '" + txtFilter.Text + "%'";
            }
        }
    }
}
