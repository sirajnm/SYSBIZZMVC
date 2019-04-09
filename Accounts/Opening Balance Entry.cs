using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using Sys_Sols_Inventory.Model;

namespace Sys_Sols_Inventory.Accounts
{
    public partial class Opening_Balance_Entry : Form
    {
        Class.AccountGroup accgrp = new Class.AccountGroup();
        Class.Ledgers Ledg = new Class.Ledgers();
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Login lg = (Login)Application.OpenForms["Login"];
        clsCommon cmnObj = new clsCommon();

        private string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        
        Class.Transactions trans = new Class.Transactions();

        string UpdateLedgerId;
        string ID="";
        public Opening_Balance_Entry()
        {
            InitializeComponent();
        }
        public void getgrpname()
        {
            DataTable dt = new DataTable();
            cmbUnder.DisplayMember = "LEDGERNAME";
            cmbUnder.ValueMember = "LEDGERID";
            dt = Ledg.Selectledger();
            DataRow row = dt.NewRow();
            dt.Rows.InsertAt(row, 0);
            cmbUnder.DataSource = dt;


        }
        private void Opening_Balance_Entry_Load(object sender, EventArgs e)
        {
            Class.CompanySetup CompStep = new Class.CompanySetup();
            dateTimePicker1.Text = CompStep.GettDate();

            cmbUnder.Focus();
            OPENTYPE.SelectedIndex=0;
            getgrpname();
            GetOpeningBalances();
        }

        private void cmbUnder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode== Keys.Tab)
            {
                if (cmbUnder.SelectedIndex >= 0)
                {
                    dateTimePicker1.Focus();
                }
            }
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                OPENING_BAL.Focus();
            }

        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (OPENING_BAL.Text != "")
                {
                    OPENTYPE.Focus();

                }
            }
        }

        public bool Valid()
        {

            if (cmbUnder.SelectedIndex <=0 )
            {
                MessageBox.Show("Please Select Account");
                cmbUnder.Focus();
                return false;

            }
            else if(OPENING_BAL.Text == "")
            {
                MessageBox.Show("Please Enter Opening Balance");
                OPENING_BAL.Focus();
                return false;

            }
            else
            {
                return true;
            }
            
        }



        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_bulkOpening.Visible == false)
                {
                    if (Valid())
                    {
                        DeleteTrans();
                        Addtransaction();
                        ClearItems();
                    }
                }
                else
                {
                    if (dgv_bulkOpening.RowCount > 0)
                    {

                        DeleteTrans();
                        Addtransaction_Bulk();
                        ClearItems();
                        GetOpeningBalances();
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void ClearItems()
        {
            cmbUnder.SelectedIndex = -1;
            OPENING_BAL.Text = "0.00";
            cmbUnder.Focus();
            dgv_bulkOpening.Visible = false;
        }
        public void Addtransaction()
        {

        //    DataTable dt = new DataTable();
        //    dt = led.MaxLedGerid();

            trans.VOUCHERTYPE = "Opening Balance";
            trans.DATED = dateTimePicker1.Value.ToString("MM/dd/yyyy");
            trans.NARRATION = "Opening Balance";
            Login log = (Login)Application.OpenForms["Login"];
            trans.USERID = log.EmpId;
            if (ID == "")
            {
                
                    trans.ACCNAME = cmbUnder.Text;
                    trans.PARTICULARS = "OPENING BALANCE";
                    trans.VOUCHERNO = "";
              //      if (dt.Rows.Count > 0)
                    trans.ACCID = cmbUnder.SelectedValue.ToString();
                    if (OPENTYPE.Text == "CR")
                    {
                        if (OPENING_BAL.Text != "")
                            trans.CREDIT = OPENING_BAL.Text;
                        else
                            trans.CREDIT = "0";
                        trans.DEBIT = "0";
                    }
                    else
                    {
                        if (OPENING_BAL.Text != "")
                            trans.DEBIT = OPENING_BAL.Text;
                        else
                            trans.DEBIT = "0";
                        trans.CREDIT = "0";
                    }   
                    trans.SYSTEMTIME = DateTime.Now.ToString();
                    trans.BRANCH = lg.Branch;
                    trans.insertTransaction();
                #region
                //  trans.PARTICULARS = cmbUnder.Text;
                  //  trans.ACCNAME = "Suspense Account";
                  //  trans.VOUCHERNO = "";
                  ////  if (dt.Rows.Count > 0)
                  //   //   trans.ACCID = cmbUnder.SelectedValue.ToString();
                  //  trans.ACCID = "202";
                  //  if (OPENING_BAL.Text != "")
                  //      trans.DEBIT = OPENING_BAL.Text;
                  //  else
                  //      trans.DEBIT = "0";
                  //  trans.CREDIT = "0";
                  //  trans.SYSTEMTIEM = DateTime.Now.ToString();
                  //  trans.BRANCH = lg.Branch;
                  //  trans.insertTransaction();
                //}
                //else
                //{
                //    trans.ACCNAME = cmbUnder.Text;
                //    trans.PARTICULARS = "Suspense Account";
                //    trans.VOUCHERNO = "";
                // //   if (dt.Rows.Count > 0)
                //    trans.ACCID = cmbUnder.SelectedValue.ToString();

                //    if (OPENING_BAL.Text != "")
                //        trans.DEBIT = OPENING_BAL.Text;
                //    else
                //        trans.DEBIT = "0";
                //    trans.VOUCHERTYPE = "Opening Balance";
                //    trans.CREDIT = "0";
                //    trans.BRANCH = lg.Branch;
                //    trans.SYSTEMTIEM = DateTime.Now.ToString();
                //    trans.insertTransaction();

                //    trans.PARTICULARS = cmbUnder.Text;
                //    trans.ACCNAME = "Suspense Account";
                //    trans.VOUCHERNO = "";
                //    trans.VOUCHERTYPE = "Opening Balance";
                // //   if (dt.Rows.Count > 0)
                //    //    trans.ACCID = cmbUnder.SelectedValue.ToString();
                //    trans.ACCID = "202";
                //    if (OPENING_BAL.Text != "")
                //        trans.CREDIT = OPENING_BAL.Text;
                //    else
                //        trans.CREDIT = "0";
                //    trans.DEBIT = "0";
                //    trans.BRANCH = lg.Branch;
                //    trans.SYSTEMTIEM = DateTime.Now.ToString();
                //    trans.insertTransaction();
                //}
                #endregion
            }
            else
            {
                DeleteTrans();
                
                    trans.ACCNAME = cmbUnder.Text;
                    trans.PARTICULARS = "OPENING BALANCE";
                    trans.DATED = dateTimePicker1.Value.ToString("MM/dd/yyyy");
                    trans.VOUCHERNO = "";
                 //   if (dt.Rows.Count > 0)
                    trans.ACCID = UpdateLedgerId;
                    if (OPENTYPE.Text == "CR")
                    {
                        if (OPENING_BAL.Text != "")
                            trans.CREDIT = OPENING_BAL.Text;
                        else
                            trans.CREDIT = "0";
                        trans.DEBIT = "0";
                    }
                    else
                    {
                        if (OPENING_BAL.Text != "")
                            trans.DEBIT = OPENING_BAL.Text;
                        else
                            trans.DEBIT = "0";
                        trans.CREDIT = "0";
                    }   
                    trans.SYSTEMTIME = DateTime.Now.ToString();
                    trans.BRANCH = lg.Branch;
                    trans.insertTransaction();
                    #region
                    //    trans.PARTICULARS = cmbUnder.Text;
                //    trans.ACCNAME = "Suspense Account";
                //    trans.VOUCHERNO = "";
                ////    if (dt.Rows.Count > 0)
                //     //   trans.ACCID = UpdateLedgerId;
                //    trans.ACCID = "202";
                //    if (OPENING_BAL.Text != "")
                //        trans.DEBIT = OPENING_BAL.Text;
                //    else
                //        trans.DEBIT = "0";
                //    trans.CREDIT = "0";
                //    trans.SYSTEMTIEM = DateTime.Now.ToString();
                //    trans.BRANCH = lg.Branch;
                //    trans.insertTransaction();
                //}
                //else
                //{
                //    trans.ACCNAME = cmbUnder.Text;
                //    trans.PARTICULARS = "Suspense Account";
                //    trans.VOUCHERNO = "";
                // //   if (dt.Rows.Count > 0)
                //        trans.ACCID = UpdateLedgerId;

                //    if (OPENING_BAL.Text != "")
                //        trans.DEBIT = OPENING_BAL.Text;
                //    else
                //        trans.DEBIT = "0";
                //    trans.CREDIT = "0";
                //    trans.SYSTEMTIEM = DateTime.Now.ToString();
                //    trans.BRANCH = lg.Branch;
                //    trans.insertTransaction();

                //    trans.PARTICULARS = cmbUnder.Text;
                //    trans.ACCNAME = "Suspense Account";
                //    trans.VOUCHERNO = "";
                //    trans.BRANCH = lg.Branch;
                ////    if (dt.Rows.Count > 0)
                //     ///  trans.ACCID = UpdateLedgerId;
                //     trans.ACCID = "202";
                //    if (OPENING_BAL.Text != "")
                //        trans.CREDIT = OPENING_BAL.Text;
                //    else
                //        trans.CREDIT = "0";
                //    trans.DEBIT = "0";
                //    trans.SYSTEMTIEM = DateTime.Now.ToString();
                //    trans.insertTransaction();
                    //}
                    #endregion
                    GetOpeningBalances();
            }
        }

        public void Addtransaction_Bulk()
        {
            for (int i = 0; i < dgv_bulkOpening.RowCount-1; i++)
            {
                try
                {
                    trans.VOUCHERTYPE = "Opening Balance";
                    trans.DATED = dateTimePicker1.Value.ToString("MM/dd/yyyy"); 
                    trans.NARRATION = "Opening Balance";
                    Login log = (Login)Application.OpenForms["Login"];
                    trans.USERID = log.EmpId;
                    trans.ACCNAME = dgv_bulkOpening.Rows[i].Cells["Account Name"].Value.ToString();
                    trans.PARTICULARS = "OPENING BALANCE";
                    trans.VOUCHERNO = "";
                    //      if (dt.Rows.Count > 0)
                    trans.ACCID = dgv_bulkOpening.Rows[i].Cells["ACCID"].Value.ToString();

                    if (dgv_bulkOpening.Rows[i].Cells["Credit"].Value.ToString() != "")
                    {
                        trans.CREDIT = dgv_bulkOpening.Rows[i].Cells["Credit"].Value.ToString();
                    }
                    else
                    {
                        trans.CREDIT = "0.00";
                    }
                    if (dgv_bulkOpening.Rows[i].Cells["Debit"].Value.ToString() != "")
                    {
                        trans.DEBIT = dgv_bulkOpening.Rows[i].Cells["Debit"].Value.ToString();
                    }
                    else
                    {
                        trans.DEBIT = "0.00";
                    }

                    trans.SYSTEMTIME = DateTime.Now.ToString();
                    trans.BRANCH = lg.Branch;
                    trans.insertTransaction();
                }
                catch (Exception ex)
                {
 
                }
            }
        }

        public void DeleteTrans()
        {          
            try
            {
                if (dgv_bulkOpening.Visible == false)
                {
                    trans.ACCID = cmbUnder.SelectedValue.ToString();
                    trans.VOUCHERTYPE = "Opening Balance";
                    trans.DeleteTransaction();
                }
                else
                {
                    for (int i = 0; i < dgv_bulkOpening.RowCount; i++)
                    {
                        trans.ACCID = dgv_bulkOpening.Rows[i].Cells["ACCID"].Value.ToString();
                        trans.VOUCHERTYPE = "Opening Balance";
                        trans.DeleteTransaction();
                    }
                }

            }
            catch
            {
            }
        }

        

        private void OPENTYPE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (OPENING_BAL.Text != "")
                {
                    btnOK.Focus();

                }
            }
        }


        public void GetOpeningBalances()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = trans.GetOpeningBalance();
                for (int j = 0; j < dt.Rows.Count; ++j)
                {
                    if (dt.Rows[j]["DEBIT"].ToString() == "0.00" && dt.Rows[j]["CREDIT"].ToString() == "0.00")
                    {
                        DataRow[] DEB = dt.Select("CREDIT='0.00'");
                        DataRow[] rows;
                        rows = dt.Select("CREDIT='0.00'");
                        foreach (DataRow row in rows)
                        {
                            if (row["CREDIT"].ToString() == "0.00" && row["DEBIT"].ToString() == "0.00")
                            {
                                dt.Rows.Remove(row);
                            }
                        }  
                    }
                }
                dgitems.DataSource = dt;
                dgitems.Columns[0].Visible = false;
                dgitems.Columns[1].Visible = false;
            }
            catch
            {
            }
        }

        private void dgitems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgitems.CurrentRow.Index >= 0)
            {
                ID = dgitems.CurrentRow.Cells["ACCID"].Value.ToString();
                UpdateLedgerId = dgitems.CurrentRow.Cells["ACCID"].Value.ToString();
                cmbUnder.SelectedValue = dgitems.CurrentRow.Cells["ACCID"].Value.ToString();
               // cmbUnder.SelectedText = "";
              //  cmbUnder.SelectedText= dgitems.CurrentRow.Cells["ACCNAME"].Value.ToString();
                OPENING_BAL.Text = dgitems.CurrentRow.Cells["CREDIT"].Value.ToString();
                dateTimePicker1.Text = Convert.ToDateTime(dgitems.CurrentRow.Cells["DATED"].Value).ToString();
                OPENTYPE.SelectedIndex = 0;
          

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearItems();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure to Delet transaction","Alert",MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                DeleteTrans();
                GetOpeningBalances();
                ClearItems();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure to exit", "alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                try
                {
                    if (lg.Theme == "1")
                    {

                        ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();

                        mdi.maindocpanel.SelectedPage.Dispose();
                    }
                    else
                    {
                        this.Close();
                        //ComponentFactory.Krypton.Navigator.KryptonPage kp = new ComponentFactory.Krypton.Navigator.KryptonPage();

                        //mdi.maindocpanel.SelectedPage.Dispose();
                    }


                }
                catch
                {
                    this.Close();
                }
            }
        }
        int indx;
        private void dgitems_KeyDown(object sender, KeyEventArgs e)
        {
            

                
            
            if (e.KeyCode == Keys.Down)
            {

                if (dgitems.CurrentRow.Index >= 0 && dgitems.CurrentRow.Index < dgitems.RowCount)
                {
                    try
                    {

                        indx = dgitems.CurrentRow.Index + 1;
                        ID = dgitems.Rows[indx].Cells["ACCID"].Value.ToString();
                        UpdateLedgerId = dgitems.Rows[indx].Cells["ACCID"].Value.ToString();
                        cmbUnder.SelectedValue = dgitems.Rows[indx].Cells["ACCID"].Value.ToString();

                        OPENING_BAL.Text = dgitems.Rows[indx].Cells["CREDIT"].Value.ToString();
                        dateTimePicker1.Text = Convert.ToDateTime(dgitems.Rows[indx].Cells["DATED"].Value).ToString();
                        OPENTYPE.SelectedIndex = 0;
                    }
                    catch { }

                }

            }
            if (e.KeyCode == Keys.Up)
            {
                try
                {
                    if (dgitems.CurrentRow.Index >= 0)
                    {
                        indx = dgitems.CurrentRow.Index - 1;
                        ID = dgitems.Rows[indx].Cells["ACCID"].Value.ToString();
                        UpdateLedgerId = dgitems.Rows[indx].Cells["ACCID"].Value.ToString();
                        cmbUnder.SelectedValue = dgitems.Rows[indx].Cells["ACCID"].Value.ToString();

                        OPENING_BAL.Text = dgitems.Rows[indx].Cells["CREDIT"].Value.ToString();
                        dateTimePicker1.Text = Convert.ToDateTime(dgitems.Rows[indx].Cells["DATED"].Value).ToString();
                        OPENTYPE.SelectedIndex = 0;


                    }
                }
                catch { }
            }

        }

        private void Btn_Browse_Click(object sender, EventArgs e)
        {
            dgv_bulkOpening.Visible = true;
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                string filePath = openFileDialog1.FileName;
                string extension = Path.GetExtension(filePath);
                string header = "YES";
                string conStr, sheetName;

                conStr = string.Empty;
                switch (extension)
                {

                    case ".xls": //Excel 97-03
                        conStr = string.Format(Excel03ConString, filePath, header);
                        break;

                    case ".xlsx": //Excel 07
                        conStr = string.Format(Excel07ConString, filePath, header);
                        break;
                }

                //Get the name of the First Sheet.
                using (OleDbConnection con = new OleDbConnection(conStr))
                {
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.Connection = con;
                        con.Open();
                        DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                        con.Close();
                    }
                }

                //Read Data from the First Sheet.
                using (OleDbConnection con = new OleDbConnection(conStr))
                {
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        using (OleDbDataAdapter oda = new OleDbDataAdapter())
                        {
                            DataTable dt = new DataTable();
                           // cmd.CommandText = "SELECT * From [" + sheetName + "]";
                          //  cmd.Connection = con;
                          //  con.Open();
                          //  oda.SelectCommand = cmd;
                         //   oda.Fill(dt);
                         //   con.Close();
                            dt = cmnObj.getAllFromTable(sheetName);
                            //Populate DataGridView.
                            dgv_bulkOpening.DataSource = dt;
                        }
                    }
                }
            }
        }


        
           
    }
}
