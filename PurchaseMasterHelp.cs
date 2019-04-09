using System;
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
    public partial class PurchaseMasterHelp : Form
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        //private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private BindingSource source = new BindingSource();
        public DataGridViewCellCollection c;
        private string type;
        private DataTable fTabe = new DataTable();
        Class.Transactions trans = new Class.Transactions();
        Class.ModifiedTransaction modtrans = new Class.ModifiedTransaction();
        Login lg = (Login)Application.OpenForms["Login"];
        clsHelper help = new clsHelper();

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {



            if (keyData == (Keys.Escape))
            {

                this.Close();
                return true;
            }

        




            return base.ProcessCmdKey(ref msg, keyData);

        }



        public PurchaseMasterHelp(int i,string docType)
        {
            InitializeComponent();
            //cmd.Connection = conn;
            //adapter.SelectCommand = cmd;
            type = docType;

            if (i == 0)
            {
                btnDelete.Enabled = false;
            }
            else
            {
                btnOK.Enabled = false;
            }

            fTabe.Columns.Add("key");
            fTabe.Columns.Add("value");
            fTabe.Rows.Add("DOC_NO", "Doc. No");
            fTabe.Rows.Add("DOC_DATE_GRE", "Date [GRE]");
            fTabe.Rows.Add("DOC_DATE_HIJ", "Date [HIJ]");
            fTabe.Rows.Add("DOC_REFERENCE", "Doc Reference");
            fTabe.Rows.Add("SUPPLIER_CODE","Supplier Code");
            fTabe.Rows.Add("NOTES","Notes");
            fTabe.Rows.Add("PAY_CODE","Pay Code");
            fTabe.Rows.Add("CARD_NO", "Card No.");
            cmbFilter.DataSource = fTabe;
            cmbFilter.ValueMember = "key";
            cmbFilter.DisplayMember = "Value";
        }

        private void PurchaseMasterHelp_Load(object sender, EventArgs e)
        {
            help.Type = type;
            if (type.StartsWith("LGR.CPR','LGR.PRT"))
                table = help.getAllPurchaseReturnVoucherInPurHelper();//  cmd.CommandText = "SELECT  CONVERT(NVARCHAR, DOC_ID) AS DOC_ID,DOC_NO,CONVERT(NVARCHAR,DOC_DATE_GRE,103) AS DOC_DATE_GRE,DOC_DATE_HIJ,DOC_REFERENCE,CURRENCY_CODE,SUPPLIER_CODE,SalesMan,NOTES,CONVERT(NVARCHAR,TOTAL_FOB_AMOUNT) AS TOTAL_AMOUNT,GROSS,TAX_TOTAL,NET_VAL,PAY_CODE,CARD_NO FROM INV_PURCHASE_HDR WHERE DOC_TYPE = '" + type + "'";       //and DOC_TYPE!='SAL.CREDITNOTE'       
            else
                table = help.getAllPurchaseVoucherInPurHelper(); // cmd.CommandText = "SELECT DOC_NO,CONVERT(NVARCHAR,DOC_DATE_GRE,103) AS C,DOC_DATE_HIJ,DOC_REFERENCE,SUPPLIER_CODE,NOTES,PAY_CODE,CARD_NO,TAX_TOTAL,CESS_AMOUNT,GROSS,DISCOUNT_VAL,NET_VAL,SUP_INV_NO,FREIGHT_AMT FROM INV_PURCHASE_HDR where DOC_TYPE<>'LGR.PRT' ";

            //adapter.Fill(table);
            source.DataSource = table;
            dgItems.DataSource = source;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dgItems.Rows.Count > 0 && dgItems.CurrentRow != null)
            {
                c = dgItems.CurrentRow.Cells;
            }
        }

        private void dgItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK.PerformClick();
            }
        }

        private void dgItems_DoubleClick(object sender, EventArgs e)
        {
            btnOK.PerformClick();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            if (dgItems.CurrentRow != null)
            {
                if (MessageBox.Show("Are you sure? you want to delete Purchase?", "Record Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //string dat = ""; ;
                    //string id = Convert.ToString(dgItems.CurrentRow.Cells["DOC_NO"].Value);
                    //try
                    //{
                    //    dat = Convert.ToDateTime(dgItems.CurrentRow.Cells["c"].Value).ToString("MM/dd/yyyy");
                    //}
                    //catch { }
                    //modifiedtransaction(id,dat);
                    //AddtoDeletedTransaction(id);
                    //DeleteTransation(id);
                    //table.Select("DOC_NO = '"+id+"'").First().Delete();
                    //try
                    //{
                    //    conn.Open();
                    //    cmd.CommandText = "DELETE FROM INV_PURCHASE_HDR WHERE DOC_NO = '" + id + "';DELETE FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + id + "';DELETE FROM INV_STK_TRX_HDR WHERE DOC_REFERENCE = '" + id + "';DELETE FROM INV_STK_TRX_DTL WHERE DOC_REFERENCE = '" + id + "'";
                    //    cmd.ExecuteNonQuery();
                    //    MessageBox.Show("Record Deleted!");
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show(ex.Message);
                    //}
                    //finally
                    //{
                    //    conn.Close();
                    //}
                }
            }
        }

        private void DeleteTransation(string ID)
        {
            try
            {
                if (type == "LGR.PRT")
                {
                    trans.VOUCHERTYPE = "Purchase Return";
                }
                else
                {
                    trans.VOUCHERTYPE = "Purchase";
                }
               
                trans.VOUCHERNO = ID;
                trans.DeletePurchaseTransaction();
            }
            catch
            {
            }


        }

        public void modifiedtransaction(string ID,string date)
        {
            try
            {
                if (type == "LGR.PRT")
                {
                    modtrans.VOUCHERTYPE = "Purchase Return";
                }
                else
                {
                    modtrans.VOUCHERTYPE = "Purchase";
                }
                modtrans.Date = date;
                modtrans.USERID = lg.EmpId;
                modtrans.VOUCHERNO = ID;
                modtrans.NARRATION = "";
                modtrans.STATUS = "Delete";
                modtrans.MODIFIEDDATE =  DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss");
                modtrans.insertTransaction();
            }
            catch
            { }
        }
        public void AddtoDeletedTransaction(string id)
        {
            //string vchr;
            //try
            //{
            //    if (type == "LGR.PRT")
            //    {
            //        vchr = "Purchase Return";
            //    }
            //    else
            //    {
            //       vchr = "Purchase";
            //    }
            //    conn.Open();
            //    cmd.CommandText = "insert into     tbl_deletedTransaction(VOUCHERNO, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, DATED, NARRATION, USERID, SYSTEMTIEM, ACCID) select    VOUCHERNO, VOUCHERTYPE, ACCNAME, PARTICULARS, DEBIT, CREDIT, DATED, NARRATION, USERID, SYSTEMTIEM, ACCID from tb_Transactions where VOUCHERNO='" + id + "' and VOUCHERTYPE='" + vchr + "'";
            //   // cmd.CommandText = "DELETE FROM INV_PURCHASE_HDR WHERE DOC_NO = '" + id + "';DELETE FROM INV_PURCHASE_DTL WHERE DOC_NO = '" + id + "';DELETE FROM INV_STK_TRX_HDR WHERE DOC_REFERENCE = '" + id + "';DELETE FROM INV_STK_TRX_DTL WHERE DOC_REFERENCE = '" + id + "'";
            //    cmd.ExecuteNonQuery();
            //  //  MessageBox.Show("Record Deleted!");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    conn.Close();
            //}

        }
       
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (txtFilter.Text == "")
            {
                source.Filter = "";
            }
            else
            {
                source.Filter = cmbFilter.SelectedValue+" LIKE '"+txtFilter.Text+"%'";
            }
        }
    }
}
