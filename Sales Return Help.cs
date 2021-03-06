﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sys_Sols_Inventory.Model;
namespace Sys_Sols_Inventory
{
    public partial class Sales_Return_Help : Form
    {
        //  private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private BindingSource source = new BindingSource();
        public DataGridViewCellCollection c;
        private DataTable fieldTable = new DataTable();
        private string type;
        Class.Transactions trans = new Class.Transactions();
        Class.ModifiedTransaction modtrans = new Class.ModifiedTransaction();
        Login lg = (Login)Application.OpenForms["Login"];
        clsHelper help = new clsHelper();
        public Sales_Return_Help(int i, string docType)
        {
            InitializeComponent();
            if (i == 0)
            {
                btnDelete.Enabled = false;
            }
            else
            {
                btnOK.Enabled = false;
            }

            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            type = docType;
        }
        public Sales_Return_Help()
        {
            InitializeComponent();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            if (keyData == (Keys.Escape))
            {
                this.Close();

            }
            //  else if (e.KeyCode == Keys.S && e.Control)

            return base.ProcessCmdKey(ref msg, keyData);

        }
        private void Sales_Return_Help_Load(object sender, EventArgs e)
        {
            try
            {
                help.Type = type;
              //  cmd.CommandText = "SELECT  CONVERT(NVARCHAR, DOC_ID) AS DOC_ID,DOC_NO,CONVERT(NVARCHAR,DOC_DATE_GRE,103) AS DOC_DATE_GRE,DOC_DATE_HIJ,DOC_REFERENCE,CURRENCY_CODE,CUSTOMER_CODE,CUSTOMER_NAME_ENG,SALESMAN_CODE,NOTES,CONVERT(NVARCHAR,TOTAL_AMOUNT) AS TOTAL_AMOUNT,TAX_TOTAL,VAT,DISCOUNT,NET_AMOUNT,PAY_CODE,CARD_NO FROM INV_SALES_HDR WHERE DOC_TYPE != '" + type + "'";       //and DOC_TYPE!='SAL.CREDITNOTE'       
              //  adapter.Fill(table);
                table = help.getAllSalesVoucher();
                source.DataSource = table;
                dgItems.DataSource = source;

                fieldTable.Columns.Add("key");
                fieldTable.Columns.Add("value");
                cmbFilter.DataSource = fieldTable;
                cmbFilter.ValueMember = "key";
                cmbFilter.DisplayMember = "value";
                fieldTable.Rows.Add("DOC_ID", "Inv No.");
                fieldTable.Rows.Add("DOC_NO", "Doc. No.");
                fieldTable.Rows.Add("DOC_DATE_GRE", "Date[GRE]");
                fieldTable.Rows.Add("DOC_DATE_HIJ", "Date[HIJ]");
                fieldTable.Rows.Add("DOC_REFERENCE", "Doc. Ref.");
                fieldTable.Rows.Add("NOTES", "Notes");
                fieldTable.Rows.Add("TOTAL_AMOUNT", "Total Amount");

                DataGridViewColumnCollection c = dgItems.Columns;
                c["DOC_ID"].HeaderText = "Inv No.";
                c["DOC_NO"].HeaderText = "Doc. No.";
                c["DOC_DATE_GRE"].HeaderText = "Date[GRE]";
                c["DOC_DATE_HIJ"].HeaderText = "Date[HIJ]";
                c["DOC_REFERENCE"].HeaderText = "Doc. Ref.";
                c["CURRENCY_CODE"].HeaderText = "Currency Code";
                c["CUSTOMER_CODE"].HeaderText = "Customer Code";
                c["SALESMAN_CODE"].HeaderText = "Salesman Code";
                c["NOTES"].HeaderText = "Notes";
                c["TAX_TOTAL"].HeaderText = "Tax Amount";
                c["DISCOUNT"].HeaderText = "Discount Amount";
                c["TOTAL_AMOUNT"].HeaderText = "Total Amount";
                c["NET_AMOUNT"].HeaderText = "Nett Amount";
                c["PAY_CODE"].HeaderText = "Pay Type";
                c["CARD_NO"].HeaderText = "Card No.";
            }
            catch
            {
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null && MessageBox.Show("Are you sure?", "Stock Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {

                    string id = Convert.ToString(dgItems.CurrentRow.Cells["DOC_NO"].Value);
                    string dat = Convert.ToString(dgItems.CurrentRow.Cells["DOC_DATE_GRE"].Value);
                    //conn.Open();
                    //cmd.CommandText = "DELETE FROM INV_SALES_HDR WHERE DOC_NO = '" + dgItems.CurrentRow.Cells[0].Value + "';DELETE FROM INV_SALES_DTL WHERE DOC_NO = '" + dgItems.CurrentRow.Cells[0].Value + "'";
                    //cmd.ExecuteNonQuery();
                    help.DocNo = dgItems.CurrentRow.Cells[0].Value.ToString();
                    help.deleteSales();
                    MessageBox.Show("Item Deleted!");
                    AddtoDeletedTransaction(id);

                    modifiedtransaction(id, dat);
                    DeleteTransation(id);
                    dgItems.Rows.Remove(dgItems.CurrentRow);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    //conn.Close();
                }
            }
        }

        private void DeleteTransation(string Id)
        {
            try
            {
                
                    trans.VOUCHERTYPE = "Sales Return";
              
                trans.VOUCHERNO = Id;
                trans.DeletePurchaseTransaction();
            }
            catch
            {
            }

        } 
        public void modifiedtransaction(string ID, string date)
        {
            try
            {
               
                    modtrans.VOUCHERTYPE = "Sales Return";
               
                 
                modtrans.Date = date;
                modtrans.USERID = lg.EmpId;
                modtrans.VOUCHERNO = ID;
                modtrans.NARRATION = "";
                modtrans.STATUS = "Delete";
                modtrans.MODIFIEDDATE = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"); ;
                modtrans.insertTransaction();
            }
            catch
            { }
        }

        public void AddtoDeletedTransaction(string id)
        {
            string vchr;
            try
            {
                
              
                    vchr = "Sales Return";
                    modtrans.VOUCHERNO = id;
                    modtrans.VOUCHERTYPE = "Sales Return";
                    modtrans.insertDeletedTransaction();
               
              //  conn.Open();
               // cmd.CommandText = "insert into     tbl_deletedTransaction(VOUCHERNO, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, DATED, NARRATION, USERID, SYSTEMTIEM, ACCID) select    VOUCHERNO, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, DATED, NARRATION, USERID, SYSTEMTIEM, ACCID from tb_Transactions where VOUCHERNO='" + id + "' and VOUCHERTYPE='" + vchr + "'";
                // cmd.CommandText = "DELETE FROM INV_PURCHASE_HDR WHERE DOC_NO = '" + id + "';DELETE FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + id + "';DELETE FROM INV_STK_TRX_HDR WHERE DOC_REFERENCE = '" + id + "';DELETE FROM INV_STK_TRX_DTL WHERE DOC_REFERENCE = '" + id + "'";
              //  cmd.ExecuteNonQuery();
                //  MessageBox.Show("Record Deleted!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
               // conn.Close();
            }

        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            
            if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null)
            {
                c = dgItems.CurrentRow.Cells;
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

        private void dgItems_DoubleClick(object sender, EventArgs e)
        {
            btnOK.PerformClick();
        }

        private void dgItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK.PerformClick();
            }
        }
    }
}
