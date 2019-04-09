using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory.reports
{
    public partial class Cheque_Report : Form
    {
        char post, cancel;
        string doc_no, voucher_type, branch, narration, account_name, party_account_id, party_account_name, account_id, amount;
        Class.Cheque_report chqrp = new Class.Cheque_report();
        string current_sts, vou_date;
        public Cheque_Report()
        {
            InitializeComponent();
        }

        private void Cheque_Report_Load(object sender, EventArgs e)
        {
            formLoad();
        }

        public void formLoad()
        {
            if (rb_voucher.Checked)
            {
                cmb_party.DataSource = chqrp.ComboData_Party("PAY_PAYMENT_VOUCHER_HDR");
                cmb_bank.DataSource = chqrp.ComboData_Bank("PAY_PAYMENT_VOUCHER_HDR");
            }
            else
            {
                cmb_party.DataSource = chqrp.ComboData_Party("REC_RECEIPTVOUCHER_HDR");
                cmb_bank.DataSource = chqrp.ComboData_Bank("REC_RECEIPTVOUCHER_HDR");
            }

        }

        private void ch_Bank_CheckedChanged(object sender, EventArgs e)
        {
            if (ch_Bank.Checked)
            {
                cmb_bank.Enabled = true;
            }
            else
            {
                cmb_bank.Enabled = false;
            }
        }

        private void ch_Party_CheckedChanged(object sender, EventArgs e)
        {
            if (ch_Party.Checked)
            {
                cmb_party.Enabled = true;
            }
            else
            {
                cmb_party.Enabled = false;
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void rbtn_all_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_all.Checked)
            {
                gb_date.Enabled = false;
                gb_sort.Enabled = false;
            }
        }

        private void rbtn_sort_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_sort.Checked)
            {
                gb_date.Enabled = true;
                gb_sort.Enabled = true;
            }
        }

        private void btn_view_Click(object sender, EventArgs e)
        {
            if (rb_voucher.Checked)
            {
                if (rbtn_all.Checked)
                {
                    dgv_cheque.DataSource = chqrp.GetAllDatas("PAY_PAYMENT_VOUCHER_HDR");
                }
                else
                {
                    if (!ch_Bank.Checked && !ch_Party.Checked)
                    {
                        dgv_cheque.DataSource = chqrp.GetAllDatas_Bydate(Convert.ToDateTime(dtp_from.Value.ToShortDateString()), Convert.ToDateTime(dtp_to.Value.ToShortDateString()),"PAY_PAYMENT_VOUCHER_HDR");
                    }
                    else if (ch_Bank.Checked && !ch_Party.Checked)
                    {
                        dgv_cheque.DataSource = chqrp.GetAllDatas_bybank(Convert.ToInt32(cmb_bank.SelectedValue), Convert.ToDateTime(dtp_from.Value.ToShortDateString()), Convert.ToDateTime(dtp_to.Value.ToShortDateString()), "PAY_PAYMENT_VOUCHER_HDR");
                    }
                    else if (!ch_Bank.Checked && ch_Party.Checked)
                    {
                        dgv_cheque.DataSource = chqrp.GetAllDatas_byparty(Convert.ToInt32(cmb_party.SelectedValue), Convert.ToDateTime(dtp_from.Value.ToShortDateString()), Convert.ToDateTime(dtp_to.Value.ToShortDateString()), "PAY_PAYMENT_VOUCHER_HDR");
                    }
                    else
                    {
                        dgv_cheque.DataSource = chqrp.GetAllDatas_byBoth(Convert.ToInt32(cmb_bank.SelectedValue), Convert.ToInt32(cmb_party.SelectedValue), Convert.ToDateTime(dtp_from.Value.ToShortDateString()), Convert.ToDateTime(dtp_to.Value.ToShortDateString()), "PAY_PAYMENT_VOUCHER_HDR");
                    }
                }
            }
            else
            {
                if (rbtn_all.Checked)
                {
                    dgv_cheque.DataSource = chqrp.GetAllDatas("REC_RECEIPTVOUCHER_HDR");
                }
                else
                {
                    if (!ch_Bank.Checked && !ch_Party.Checked)
                    {
                        dgv_cheque.DataSource = chqrp.GetAllDatas_Bydate(Convert.ToDateTime(dtp_from.Value.ToShortDateString()), Convert.ToDateTime(dtp_to.Value.ToShortDateString()), "REC_RECEIPTVOUCHER_HDR");
                    }
                    else if (ch_Bank.Checked && !ch_Party.Checked)
                    {
                        dgv_cheque.DataSource = chqrp.GetAllDatas_bybank(Convert.ToInt32(cmb_bank.SelectedValue), Convert.ToDateTime(dtp_from.Value.ToShortDateString()), Convert.ToDateTime(dtp_to.Value.ToShortDateString()), "REC_RECEIPTVOUCHER_HDR");
                    }
                    else if (!ch_Bank.Checked && ch_Party.Checked)
                    {
                        dgv_cheque.DataSource = chqrp.GetAllDatas_byparty(Convert.ToInt32(cmb_party.SelectedValue), Convert.ToDateTime(dtp_from.Value.ToShortDateString()), Convert.ToDateTime(dtp_to.Value.ToShortDateString()), "REC_RECEIPTVOUCHER_HDR");
                    }
                    else
                    {
                        dgv_cheque.DataSource = chqrp.GetAllDatas_byBoth(Convert.ToInt32(cmb_bank.SelectedValue), Convert.ToInt32(cmb_party.SelectedValue), Convert.ToDateTime(dtp_from.Value.ToShortDateString()), Convert.ToDateTime(dtp_to.Value.ToShortDateString()), "REC_RECEIPTVOUCHER_HDR");
                    }
                }
            }
                for (int i = 0; i < dgv_cheque.RowCount; i++)
                {
                    dgv_cheque.Rows[i].Cells[0].Value = i + 1;

                    if (dgv_cheque.Rows[i].Cells["STATUS"].Value.ToString() == "POSTED")
                    {
                        dgv_cheque.Rows[i].Cells["STATUS"].Style.BackColor = Color.Green;
                        dgv_cheque.Rows[i].Cells["STATUS"].Style.ForeColor = Color.White;
                    }
                    else if (dgv_cheque.Rows[i].Cells["STATUS"].Value.ToString() == "CANCELED")
                    {
                        dgv_cheque.Rows[i].Cells["STATUS"].Style.BackColor = Color.Red;
                        dgv_cheque.Rows[i].Cells["STATUS"].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        dgv_cheque.Rows[i].Cells["STATUS"].Style.BackColor = Color.White;
                        dgv_cheque.Rows[i].Cells["STATUS"].Style.ForeColor = Color.Black;
                    }
                }            
        }

        private void dgv_cheque_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                txt_voucher_no.Text = dgv_cheque.Rows[dgv_cheque.CurrentCell.RowIndex].Cells["Voucher_no"].Value.ToString();
                try
                {
                    dtp_transdate.Value = Convert.ToDateTime(dgv_cheque.Rows[dgv_cheque.CurrentCell.RowIndex].Cells["TRANSACTION_DATE"].Value.ToString());
                }
                catch 
                {
                    dtp_transdate.Value = Convert.ToDateTime(DateTime.Now.ToShortTimeString());
                } 

                txt_amount.Text = dgv_cheque.Rows[dgv_cheque.CurrentCell.RowIndex].Cells["Amount"].Value.ToString();
                txt_chqno.Text = dgv_cheque.Rows[dgv_cheque.CurrentCell.RowIndex].Cells["CHQ_NO"].Value.ToString();
                current_sts=cmb_status.Text = dgv_cheque.Rows[dgv_cheque.CurrentCell.RowIndex].Cells["STATUS"].Value.ToString();
                txt_voucher_no.Enabled = txt_amount.Enabled = txt_chqno.Enabled = false;
                vou_date = dgv_cheque.Rows[dgv_cheque.CurrentCell.RowIndex].Cells["T_DATE"].Value.ToString();
                if (cmb_status.Text == "POSTED")
                {
                    btn_update.Enabled = false;                    
                }
                else
                {
                    btn_update.Enabled = true;                    
                }

                if(rb_voucher.Checked)
                {
                int rec_no = Convert.ToInt32(txt_voucher_no.Text);
                DataTable dt = chqrp.VoucherDataById(rec_no,"PAY_PAYMENT_VOUCHER_HDR");
                doc_no = Convert.ToString(dt.Rows[0]["DOC_NO"]);
                 voucher_type = "CHEQUE TRANSACTION";
                 branch = Convert.ToString(dt.Rows[0]["BRANCH"]);
                 narration = Convert.ToString(dt.Rows[0]["NOTES"]);
                 account_id = Convert.ToString(dt.Rows[0]["DEBIT_CODE"]);
                 account_name = Convert.ToString(dt.Rows[0]["DESC1"]);
                 party_account_id = Convert.ToString(dt.Rows[0]["CREDIT_CODE"]);
                 party_account_name = Convert.ToString(dt.Rows[0]["DESC2"]);
                 amount = txt_amount.Text;
                DoubleEntryTransaction transaction = new DoubleEntryTransaction();
                }
                else
                {
                    int rec_no = Convert.ToInt32(txt_voucher_no.Text);
                DataTable dt = chqrp.VoucherDataById(rec_no, "REC_RECEIPTVOUCHER_HDR");
                doc_no = Convert.ToString(dt.Rows[0]["DOC_NO"]);
                voucher_type = "CHEQUE TRANSACTION";
                branch = Convert.ToString(dt.Rows[0]["BRANCH"]);
                narration = Convert.ToString(dt.Rows[0]["NOTES"]);
                account_id = Convert.ToString(dt.Rows[0]["DEBIT_CODE"]);
                account_name = Convert.ToString(dt.Rows[0]["DESC1"]);
                party_account_id = Convert.ToString(dt.Rows[0]["CREDIT_CODE"]);
                party_account_name = Convert.ToString(dt.Rows[0]["DESC2"]);
                amount = txt_amount.Text;
                
                }
            }
            catch { }
        }

        public bool Validation()
        {
            if (cmb_status.Text == "POSTED" || cmb_status.Text == "CANCELED" || cmb_status.Text == "PENDING")
            {
                if (txt_amount.Text == "")
                {
                    MessageBox.Show("Invalid Amount..!!!", "Sysbizz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_amount.Focus();
                    return false;
                }
                else if (txt_chqno.Text == "")
                {
                    MessageBox.Show("Invalid Chq.No..!!!", "Sysbizz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_chqno.Focus();
                    return false;
                }
                else if (txt_voucher_no.Text == "")
                {
                    MessageBox.Show("Invalid Voucher No..!!!", "Sysbizz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_voucher_no.Focus();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                MessageBox.Show("Invalid Status code..!!!", "Sysbizz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmb_status.Focus();
                return false;
            }
        }

        public void clear()
        {
            txt_voucher_no.Text = "";
            txt_chqno.Text = "";
            txt_amount.Text = "";
            cmb_status.Text = "- SELECT -";
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (Validation())            
            {
                if (rb_voucher.Checked)
                {
                    int recno = Convert.ToInt32(txt_voucher_no.Text);
                    DateTime date = Convert.ToDateTime(dtp_transdate.Value.ToShortDateString());
                    if (cmb_status.Text == "POSTED")
                    {
                        post = 'Y';
                        cancel = 'N';   
                    
                            
                            DoubleEntryTransaction transaction = new DoubleEntryTransaction();
                            transaction.insertTransaction(doc_no, voucher_type, dtp_transdate.Value.ToShortDateString(), branch, narration, account_id, account_name, party_account_id, party_account_name, amount);
                        

                    }
                    else if (cmb_status.Text == "CANCELED")
                    {
                        post = 'N';
                        cancel = 'Y';
                    }
                    else if (cmb_status.Text == "PENDING")
                    {
                        post = 'N';
                        cancel = 'N';
                    }

                    if (chqrp.Update_Status(post, cancel, date, recno,"PAY_PAYMENT_VOUCHER_HDR"))
                    {
                        MessageBox.Show("Datas Updated Successfully..!!", "Sysbizz", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear();
                        btn_view.PerformClick();                        
                    }
                    else
                    {
                        MessageBox.Show("Error in Updation..!!!", "Sysbizz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    int recno = Convert.ToInt32(txt_voucher_no.Text);
                    DateTime date = Convert.ToDateTime(dtp_transdate.Value.ToShortDateString());
                    if (cmb_status.Text == "POSTED")
                    {
                        post = 'Y';
                        cancel = 'N';
                        
                            //receipt
                            DoubleEntryTransaction transaction = new DoubleEntryTransaction();
                            transaction.insertTransaction(doc_no, voucher_type, dtp_transdate.Value.ToShortDateString(), branch, narration, account_id, account_name, party_account_id, party_account_name, amount);
                        

                    }
                    else if (cmb_status.Text == "CANCELED")
                    {
                        post = 'N';
                        cancel = 'Y';
                    }
                    else if (cmb_status.Text == "PENDING")
                    {
                        post = 'N';
                        cancel = 'N';
                    }

                    if (chqrp.Update_Status(post, cancel, date, recno, "REC_RECEIPTVOUCHER_HDR"))
                    {
                        MessageBox.Show("Datas Updated Successfully..!!", "Sysbizz", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear();
                        btn_view.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Error in Updation..!!!", "Sysbizz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
            }            

        }

        private void rb_voucher_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_voucher.Checked)
            {
                cmb_party.DataSource = chqrp.ComboData_Party("PAY_PAYMENT_VOUCHER_HDR");
                cmb_bank.DataSource = chqrp.ComboData_Bank("PAY_PAYMENT_VOUCHER_HDR");
            }
            else
            {
                cmb_party.DataSource = chqrp.ComboData_Party("REC_RECEIPTVOUCHER_HDR");
                cmb_bank.DataSource = chqrp.ComboData_Bank("REC_RECEIPTVOUCHER_HDR");
            }
        }

        private void rb_reciept_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_voucher.Checked)
            {
                cmb_party.DataSource = chqrp.ComboData_Party("PAY_PAYMENT_VOUCHER_HDR");
                cmb_bank.DataSource = chqrp.ComboData_Bank("PAY_PAYMENT_VOUCHER_HDR");
            }
            else
            {
                cmb_party.DataSource = chqrp.ComboData_Party("REC_RECEIPTVOUCHER_HDR");
                cmb_bank.DataSource = chqrp.ComboData_Bank("REC_RECEIPTVOUCHER_HDR");
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                DialogResult dr = MessageBox.Show("Are you sure to delete cheque..?", "Sysbizz", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    if (current_sts == "POSTED")
                    {
                        if (rb_voucher.Checked)
                        {
                            string VouId = chqrp.VoucherIdById(txt_voucher_no.Text, "PAY_PAYMENT_VOUCHER_HDR");
                            if (chqrp.Delete_Transaction(VouId))
                            {
                                chqrp.DeleteVoucher(txt_voucher_no.Text, "PAY_PAYMENT_VOUCHER_HDR");
                                MessageBox.Show("Cheque Deleted Sucessfully", "Sysbizz");
                            }

                        }
                        else
                        {
                            string VouId = chqrp.VoucherIdById(txt_voucher_no.Text, "REC_RECEIPTVOUCHER_HDR");
                            if (chqrp.Delete_Transaction(VouId))
                            {
                                chqrp.DeleteVoucher(txt_voucher_no.Text, "REC_RECEIPTVOUCHER_HDR");
                                MessageBox.Show("Cheque Deleted Sucessfully", "Sysbizz");
                            }
                        }
                    }
                    else
                    {
                        if (rb_voucher.Checked)
                        {                            
                                chqrp.DeleteVoucher(txt_voucher_no.Text, "PAY_PAYMENT_VOUCHER_HDR");
                                MessageBox.Show("Cheque Deleted Sucessfully", "Sysbizz");
                        }
                        else
                        {
                                chqrp.DeleteVoucher(txt_voucher_no.Text, "REC_RECEIPTVOUCHER_HDR");
                                MessageBox.Show("Cheque Deleted Sucessfully", "Sysbizz");
                        }
                    }
                    clear();
                    btn_view.PerformClick();
                }
            }
        }  
    }
}
