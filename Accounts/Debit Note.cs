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
using System.Drawing.Printing;

namespace Sys_Sols_Inventory.Accounts
{

    public partial class Debit_Note : Form
    {
        private bool HasArabic = true;
        Class.Ledgers led = new Class.Ledgers();
        Class.Transactions trans = new Class.Transactions();
        Initial mdi = (Initial)Application.OpenForms["Initial"];
        Login lg = (Login)Application.OpenForms["Login"];
        TbTransactionsDB transObj = new TbTransactionsDB();
        Class.CompanySetup ComSet = new Class.CompanySetup();
        Class.ModifiedTransaction modtrans = new Class.ModifiedTransaction();

        Model.ItemDirectoryDB Itemdb = new Model.ItemDirectoryDB();
        string decimalFormat = Common.getDecimalFormat();

        DebitNoteDB DebitNoteDB = new DebitNoteDB();
        CreditNoteDB CreditNoteDB = new CreditNoteDB();
        string CompanyName, Address1, Addres1, Addres2, Phone, Fax, Email, TineNo, Billno, Date, CUSID, Website, panno, vat, logo, SalesManCode, salesmanname;


        bool Edit = false;
        string Type = "";

        public Debit_Note()
        {
            InitializeComponent();

        }

        public Debit_Note(int i)
        {
            string query;
            InitializeComponent();
            bindledgers();

            if (i == 0)
            {
                Type = "Debit Note";
            }
            else
            {
                Type = "Credit Note";
            }
            this.Text = Type;
        }

        public void bindledgers()
        {
            DataTable dt1 = new DataTable();
            dt1 = led.Selectledger();
            DataRow row = dt1.NewRow();
            dt1.Rows.InsertAt(row, 0);
            PARTYACC.DataSource = dt1;
            PARTYACC.DisplayMember = "LEDGERNAME";
            PARTYACC.ValueMember = "LEDGERID";

            DataTable dt2 = new DataTable();
            dt2 = led.Selectledger();
            DataRow row2 = dt2.NewRow();
            dt2.Rows.InsertAt(row2, 0);
            CASHACC.DataSource = dt2;
            CASHACC.DisplayMember = "LEDGERNAME";
            CASHACC.ValueMember = "LEDGERID";


        }
        private void Debit_Note_Load(object sender, EventArgs e)
        {
            btnClear.PerformClick();            
            

        }

        private void btnExit_Click(object sender, EventArgs e)
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
                }
            }
            catch
            {
                this.Close();
            }
        }

        public void GetTaxRates()
        {
            //cmd.CommandText = "SELECT TaxId, CODE + ' --- ' +CONVERT(varchar(10),TaxRate)+' %' AS Expr1 FROM GEN_TAX_MASTER";
            DataTable dt = Itemdb.GetTaxRate_Debit();
            //adapter.Fill(dt);
            TAX.DataSource = dt;
            TAX.DisplayMember = "Expr1";
            TAX.ValueMember = "TaxRate";
            TAX.SelectedIndex = -1;
        }

        public bool Valid()
        {
            if (PARTYACC.Text == "" || CASHACC.Text == "")
            {
                MessageBox.Show("Select Accounts");
                return false;
            }
            else if (Convert.ToDouble(AMOUNT.Text) <= 0)
            {
                MessageBox.Show("Amount should be greaterthan 0.00");
                return false;
            }
            else
            {
                return true;
            }

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            trans.CREDIT = "0";
            trans.DEBIT = "0";
            if (Valid())
            {
                if (DialogResult.Yes == MessageBox.Show("Continue to Save?", "Credit Note Save Alert", MessageBoxButtons.YesNo))
                {
                    if (!Edit)
                    {
                        if (Type == "Debit Note")
                        {
                            DebitNoteDB.Id = DebitNoteDB.maxId();
                            DebitNoteDB.DocNo = DOC_NO.Text = generatePayVoucherCode();
                            DebitNoteDB.Date = Convert.ToDateTime(DOC_DATE_GRE.Value.ToShortDateString());
                            DebitNoteDB.DateHIJ = DOC_DATE_HIJ.Text;
                            DebitNoteDB.Reference = doc_reference.Text == "" ? "0" : doc_reference.Text;
                            DebitNoteDB.Customer = Convert.ToInt32(PARTYACC.SelectedValue);
                            DebitNoteDB.CashAccount = Convert.ToInt32(CASHACC.SelectedValue);
                            DebitNoteDB.Note = Noteonreciept.Text;
                            DebitNoteDB.Amount = Convert.ToDecimal(AMOUNT.Text);
                            DebitNoteDB.Balance = Convert.ToDecimal(AMOUNT.Text);
                            DebitNoteDB.Status = true;
                            DebitNoteDB.Tax = TAX.SelectedValue.ToString();
                            DebitNoteDB.TaxInclusive = ch_tax.Checked;
                            DebitNoteDB.Remarks = NOTES.Text;
                            DebitNoteDB.Insert_DebitNote();
                        }
                        else
                        {
                            CreditNoteDB.Id = CreditNoteDB.maxId();
                            CreditNoteDB.DocNo = DOC_NO.Text = generatePayVoucherCode();
                            CreditNoteDB.Date = Convert.ToDateTime(DOC_DATE_GRE.Value.ToShortDateString());
                            CreditNoteDB.DateHIJ = DOC_DATE_HIJ.Text;
                            CreditNoteDB.Reference = doc_reference.Text == "" ? "0" : doc_reference.Text;
                            CreditNoteDB.Customer = Convert.ToInt32(PARTYACC.SelectedValue);
                            CreditNoteDB.CashAccount = Convert.ToInt32(CASHACC.SelectedValue);
                            CreditNoteDB.Note = Noteonreciept.Text;
                            CreditNoteDB.Amount = Convert.ToDecimal(AMOUNT.Text);
                            CreditNoteDB.Balance = Convert.ToDecimal(AMOUNT.Text);
                            CreditNoteDB.Status = true;
                            CreditNoteDB.Tax = TAX.SelectedValue.ToString();
                            CreditNoteDB.TaxInclusive = ch_tax.Checked;
                            CreditNoteDB.Remarks = NOTES.Text;
                            CreditNoteDB.Insert_DebitNote();
                        }

                        trans.VOUCHERTYPE = Type;
                        trans.DATED = DOC_DATE_GRE.Value.ToString("MM/dd/yyyy");
                        trans.NARRATION = Noteonreciept.Text;
                        Login log = (Login)Application.OpenForms["Login"];
                        trans.USERID = log.EmpId;
                        trans.ACCNAME = CASHACC.Text;
                        trans.PARTICULARS = PARTYACC.Text;
                        trans.VOUCHERNO = DOC_NO.Text;
                        trans.ACCID = CASHACC.SelectedValue.ToString();
                        if (Type == "Debit Note")
                        {
                            trans.CREDIT = "0";
                            if (ch_tax.Checked)
                                trans.CREDIT = AMOUNT.Text;
                            else
                                trans.CREDIT = (Convert.ToDecimal(AMOUNT.Text)).ToString();
                        }
                        else
                        {

                            trans.DEBIT = "0";
                            if (ch_tax.Checked)
                                trans.DEBIT = (Convert.ToDecimal(AMOUNT.Text) - Convert.ToDecimal(txt_taxamt.Text)).ToString();
                            else
                                trans.DEBIT = (Convert.ToDecimal(AMOUNT.Text)).ToString();

                        }
                        trans.NARRATION = Noteonreciept.Text;
                        trans.SYSTEMTIME = DateTime.Now.ToString();
                        trans.BRANCH = lg.Branch;
                        trans.insertTransaction();
                        trans.PARTICULARS = CASHACC.Text;
                        trans.ACCNAME = PARTYACC.Text;
                        trans.VOUCHERNO = DOC_NO.Text;
                        if (Type == "Debit Note")
                        {
                            trans.CREDIT = "0";
                            if (ch_tax.Checked)
                                trans.DEBIT = AMOUNT.Text;
                            else
                                trans.DEBIT = (Convert.ToDecimal(AMOUNT.Text) + Convert.ToDecimal(txt_taxamt.Text)).ToString();
                        }
                        else
                        {

                            trans.DEBIT = "0";
                            if (ch_tax.Checked)
                                trans.CREDIT = AMOUNT.Text;
                            else
                                trans.CREDIT = (Convert.ToDecimal(AMOUNT.Text) + Convert.ToDecimal(txt_taxamt.Text)).ToString();

                        }
                        trans.ACCID = PARTYACC.SelectedValue.ToString();
                        trans.SYSTEMTIME = DateTime.Now.ToString();
                        trans.insertTransaction();
                        TaxTransaction();

                        if (ChekPrint.Checked)
                        {
                            PrintingCreditNOte();
                        }
                        //btnClear.PerformClick();
                        PARTYACC.Text = "";
                        CASHACC.Text = "";
                        AMOUNT.Text = "0.00";
                        // TOTAL_BALANCE.Text = "0.00";
                        DOC_NO.Text = "";
                        NOTES.Text = "";
                        Noteonreciept.Text = "";
                        Edit = false;


                    }
                    else
                    {
                        if (Type == "Debit Note")
                        {
                            DebitNoteDB.DocNo = DOC_NO.Text;
                            DebitNoteDB.Id = DebitNoteDB.DocidByDocNo();
                            DebitNoteDB.Date = Convert.ToDateTime(DOC_DATE_GRE.Value.ToShortDateString());
                            DebitNoteDB.DateHIJ = DOC_DATE_HIJ.Text;
                            DebitNoteDB.Reference = doc_reference.Text == "" ? "0" : doc_reference.Text;
                            DebitNoteDB.Customer = Convert.ToInt32(PARTYACC.SelectedValue);
                            DebitNoteDB.CashAccount = Convert.ToInt32(CASHACC.SelectedValue);
                            DebitNoteDB.Note = Noteonreciept.Text;
                            DebitNoteDB.Amount = Convert.ToDecimal(AMOUNT.Text);
                            DebitNoteDB.Balance = Convert.ToDecimal(AMOUNT.Text);
                            DebitNoteDB.Status = true;
                            DebitNoteDB.Tax = TAX.SelectedValue.ToString();
                            DebitNoteDB.TaxInclusive = ch_tax.Checked;
                            DebitNoteDB.Remarks = NOTES.Text;
                            DebitNoteDB.Update_DebitNote();
                        }
                        else
                        {
                            CreditNoteDB.DocNo = DOC_NO.Text;
                            CreditNoteDB.Id = CreditNoteDB.DocidByDocNo();
                            CreditNoteDB.Date = Convert.ToDateTime(DOC_DATE_GRE.Value.ToShortDateString());
                            CreditNoteDB.DateHIJ = DOC_DATE_HIJ.Text;
                            CreditNoteDB.Reference = doc_reference.Text == "" ? "0" : doc_reference.Text;
                            CreditNoteDB.Customer = Convert.ToInt32(PARTYACC.SelectedValue);
                            CreditNoteDB.CashAccount = Convert.ToInt32(CASHACC.SelectedValue);
                            CreditNoteDB.Note = Noteonreciept.Text;
                            CreditNoteDB.Amount = Convert.ToDecimal(AMOUNT.Text);
                            CreditNoteDB.Balance = Convert.ToDecimal(AMOUNT.Text);
                            CreditNoteDB.Status = true;
                            CreditNoteDB.Tax = TAX.SelectedValue.ToString();
                            CreditNoteDB.TaxInclusive = ch_tax.Checked;
                            CreditNoteDB.Remarks = NOTES.Text;
                            CreditNoteDB.Update_DebitNote();
                        }
                        modifiedtransaction();
                        DataTable dt1 = new DataTable();
                        DeleteTransation();
                        trans.VOUCHERTYPE = Type;
                        trans.DATED = DOC_DATE_GRE.Value.ToString("MM/dd/yyyy");
                        trans.NARRATION = Noteonreciept.Text;
                        Login log = (Login)Application.OpenForms["Login"];
                        trans.USERID = log.EmpId;
                        trans.ACCNAME = CASHACC.Text;
                        trans.PARTICULARS = PARTYACC.Text;
                        trans.VOUCHERNO = DOC_NO.Text;
                        trans.ACCID = CASHACC.SelectedValue.ToString();
                        //trans.CREDIT = "0";
                        
                        if (Type == "Debit Note")
                        {
                            trans.CREDIT = "0";
                            if (ch_tax.Checked)
                                trans.DEBIT = AMOUNT.Text;
                            else
                                trans.DEBIT = (Convert.ToDecimal(AMOUNT.Text) + Convert.ToDecimal(txt_taxamt.Text)).ToString();
                        }
                        else
                        {

                            trans.CREDIT = "0";
                            if (ch_tax.Checked)
                                trans.CREDIT = AMOUNT.Text;
                            else
                                trans.CREDIT = (Convert.ToDecimal(AMOUNT.Text) + Convert.ToDecimal(txt_taxamt.Text)).ToString();

                        }

                        trans.NARRATION = Noteonreciept.Text;
                        //trans.DEBIT = AMOUNT.Text;
                        trans.SYSTEMTIME = DateTime.Now.ToString();
                        trans.BRANCH = "";
                        trans.insertTransaction();

                        trans.PARTICULARS = CASHACC.Text;
                        trans.ACCNAME = PARTYACC.Text;
                        trans.VOUCHERNO = DOC_NO.Text;
                        trans.ACCID = PARTYACC.SelectedValue.ToString();
                        trans.BRANCH = "";
                        //trans.DEBIT = "0";
                        //trans.CREDIT = AMOUNT.Text;
                        if (Type == "Debit Note")
                        {
                            trans.DEBIT = "0";
                            if (ch_tax.Checked)
                                trans.CREDIT = AMOUNT.Text;
                            else
                                trans.CREDIT = (Convert.ToDecimal(AMOUNT.Text) + Convert.ToDecimal(txt_taxamt.Text)).ToString();
                        }
                        else
                        {

                            trans.CREDIT = "0";
                            if (ch_tax.Checked)
                                trans.DEBIT = AMOUNT.Text;
                            else
                                trans.DEBIT = (Convert.ToDecimal(AMOUNT.Text) + Convert.ToDecimal(txt_taxamt.Text)).ToString();

                        }
                        trans.SYSTEMTIME = DateTime.Now.ToString();
                        trans.insertTransaction();
                        TaxTransaction();
                        if (ChekPrint.Checked)
                        {
                            PrintingCreditNOte();
                        }
                        //btnClear.PerformClick();
                        PARTYACC.Text = "";
                        CASHACC.Text = "";
                        AMOUNT.Text = "0.00";
                        //TOTAL_BALANCE.Text = "0.00";
                        DOC_NO.Text = "";
                        NOTES.Text = "";
                        Noteonreciept.Text = "";
                        Edit = false;
                    }
                }
            }
            btnClear.PerformClick();
        }

        public void TaxTransaction()
        {
            if (Convert.ToDouble(txt_taxamt.Text) > 0)
            {
                if (Type == "Debit Note")
                {
                    trans.VOUCHERTYPE = Type;
                }
                else
                {
                    trans.VOUCHERTYPE = Type;
                }
                trans.DATED = DOC_DATE_GRE.Value.ToShortDateString();
                trans.NARRATION = NOTES.Text;
                Login log = (Login)Application.OpenForms["Login"];
                trans.USERID = log.EmpId;
                trans.NARRATION = NOTES.Text;
                if (Type != "Debit Note")
                {
                    //trans.ACCNAME = "PR A/C";
                    //trans.PARTICULARS = "INPUT GST";
                    //trans.ACCID = "84";
               
                    trans.ACCNAME = "OUTPUT GST";
                    trans.PARTICULARS = "SR A/C";
                    trans.ACCID = "83";
                    trans.VOUCHERNO = DOC_NO.Text;

                    trans.CREDIT = "0";
                    trans.BRANCH = lg.Branch;
                    trans.DEBIT = txt_taxamt.Text;
                    trans.SYSTEMTIME = DateTime.Now.ToString();
                    trans.insertTransaction();
                }
               

                if (Type == "Debit Note")
                {
                    trans.PARTICULARS = "PR A/C";
                    trans.ACCNAME = "INPUT GST";
                    trans.ACCID = "66";
                
                    //trans.PARTICULARS = "OUTPUT GST";
                    //trans.ACCNAME = "SR A/C";
                    //trans.ACCID = "105";
                    trans.VOUCHERNO = DOC_NO.Text;
                    trans.BRANCH = lg.Branch;
                    trans.DEBIT = "0";
                    trans.CREDIT = txt_taxamt.Text;
                    trans.SYSTEMTIME = DateTime.Now.ToString();
                    trans.insertTransaction();
                }
              
            }
        }


        public void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        public void PrintingCreditNOte()
        {
            try
            {
                GetCompanyDetails();
                GetBranchDetails();
                try
                {
                    int height = (dgDetail.Rows.Count - 1) * 23;
                    if (PrintPage.SelectedIndex == 0)
                    {
                        PrintDocument printDocumentMediumSize = new PrintDocument();
                        printDocumentMediumSize.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("MediumSize", 820, height + 450);
                        printDialog1.Document = printDocumentMediumSize;
                        printDocumentMediumSize.PrintPage += Print_GSTHALF;
                        printDocumentMediumSize.Print();
                    }
                    if (PrintPage.SelectedIndex == 1)
                    {
                        PrintDocument printDocument = new PrintDocument();
                        printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("smallzize", 360, height + 210);

                        printDialog1.Document = printDocument;


                        printDocument.PrintPage += printDocument_PrintPage;
                        DialogResult result = printDialog1.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            printDocument.Print();
                        }
                    }
                    else if (PrintPage.SelectedIndex == 2)
                    {
                        PrintDocument printdocumentA4 = new PrintDocument();
                        PaperSize ps = new PaperSize();
                        ps.RawKind = (int)PaperKind.A4;
                        printdocumentA4.DefaultPageSettings.PaperSize = ps;
                        printDialog1.Document = printdocumentA4;
                        printdocumentA4.PrintPage += printdocumentA4_PrintPage;
                        // printDocument.Print();
                        DialogResult result = printDialog1.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            printdocumentA4.Print();
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("printing Problem");
                }

            }
            catch
            {
            }

        }
        public void GetBranchDetails()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ComSet.GetCurrentBranchDetails();
                Addres1 = dt.Rows[0][1].ToString();
                Addres2 = dt.Rows[0][2].ToString();
                Phone = dt.Rows[0][3].ToString();
                Email = dt.Rows[0][4].ToString();
                Fax = dt.Rows[0][5].ToString();
            }
            catch
            {
            }
        }
        public void GetCompanyDetails()
        {
            DataTable dt = new DataTable();
            dt = ComSet.getCompanyDetails();
            CompanyName = dt.Rows[0][1].ToString();
            TineNo = dt.Rows[0][8].ToString();
            CUSID = dt.Rows[0][10].ToString();
            Website = dt.Rows[0][11].ToString();
            panno = dt.Rows[0][9].ToString();
            logo = dt.Rows[0][6].ToString();

        }
        void cusomized_print(object sender, PrintPageEventArgs e)
        {

            float xpos;
            int startx = 10;
            int starty = 30;
            int offset = 15;
            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Font printFont = new Font("Courier New", 11);
            var tabDataForeColor = Color.Black;
            int height = 100 + y;
            var txtDataWidth = e.Graphics.MeasureString(CompanyName, printFont).Width;


            using (var sf = new StringFormat())
            {
                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);
                //// Billno = VOUCHNUM.Text;
                e.Graphics.DrawString(CompanyName, printFont, new SolidBrush(tabDataForeColor), xpos, starty, sf);
                e.Graphics.DrawString(Addres1, printFont, new SolidBrush(tabDataForeColor), xpos, starty + offset + 3, sf);
                offset = offset + 24;
                //// e.Graphics.DrawString(Address2, printFont, new SolidBrush(tabDataForeColor), xpos, starty + offset + 3, sf);
                // offset = offset + 24;
                e.Graphics.DrawString("Ph: " + Phone, printFont, new SolidBrush(tabDataForeColor), xpos, starty + offset + 3, sf);


                offset = offset + 24;
                //e.Graphics.DrawString("Credit Note" + DOC_NO.Text, printFont, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                if (Type == "Debit Note")
                    e.Graphics.DrawString("Debit Note", printFont, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                else
                    e.Graphics.DrawString("Credit Note", printFont, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                offset = offset + 24;

                ////e.Graphics.DrawString("---------------------------------------------------------------------------", printFont, new SolidBrush(Color.Black), xpos, starty + offset, sf);
                ////e.Graphics.DrawString("---------------------------------------------------------------------------", printFont, new SolidBrush(Color.Black), xpos, starty + offset + 3, sf);
                ////offset = offset + 14;
                ////DateTime dt = DateTime.ParseExact(Convert.ToDateTime(DOC_DATE_GRE.Text).ToShortDateString(), "dd/MMM/yyyy", CultureInfo.InvariantCulture);
                ////DateTime selectedDate = DateTime.ParseExact(Convert.ToDateTime(DOC_DATE_GRE.Text).ToShortDateString(), "yyyy/MM/dd", CultureInfo.InvariantCulture);
                e.Graphics.DrawString(Convert.ToDateTime(DOC_DATE_GRE.Value).ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), printFont, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                offset = offset + 19;
                ////e.Graphics.DrawString("Tin No:" + TineNo, printFont, new SolidBrush(tabDataForeColor), startx, starty + offset);
                ////offset = offset + 12;
                e.Graphics.DrawString("Customer:" + PARTYACC.Text, printFont, new SolidBrush(tabDataForeColor), startx + 120, starty + offset - 2);
                Font itemhead = new Font("Courier New", 10);
                offset = offset + 15;
                e.Graphics.DrawString("-----------------------------------------------------", printFont, new SolidBrush(Color.Black), startx + 120, starty + offset);
                offset = offset + 14;

                string headtext = "Doc No".PadRight(15) + "Date".PadRight(11) + "Reason".PadRight(25) + "Amt";
                e.Graphics.DrawString(headtext, itemhead, new SolidBrush(Color.Black), startx + 120, starty + offset);
                e.Graphics.DrawString("-----------------------------------------------------", printFont, new SolidBrush(Color.Black), startx + 120, starty + offset + 13);
                offset = offset + 36;
                Font font = new Font("Courier New", 10);
                float fontheight = font.GetHeight();
                try
                {
                    //foreach (DataGridViewRow row in dgDetail.Rows)
                    //{
                    //DataGridViewRow row = (DataGridViewRow)dgDetail.CurrentRow.Clone();
                    string rate = "";
                    //string vno = dgDetail.Rows[0].Cells[0].Value.ToString().PadRight(10);
                    string vno = DOC_NO.Text;
                    string a = vno;
                    ////string name = row.Cells[1].Value.ToString().PadRight(20);
                    //string date = dgDetail.Rows[0].Cells[1].Value.ToString().PadRight(11);
                    string date = DOC_DATE_GRE.Value.ToShortDateString();
                    //if (Type == "Debit Note")
                    // rate = dgDetail.Rows[0].Cells[5].Value.ToString().PadRight(8);
                    //else
                    // rate = dgDetail.Rows[0].Cells[5].Value.ToString().PadRight(8);
                    rate = AMOUNT.Text;
                    //string note = dgDetail.Rows[0].Cells[7].Value.ToString();
                    string note = Noteonreciept.Text;
                    //string productline = name + rate + qty + price;
                    e.Graphics.DrawString(vno, font, new SolidBrush(Color.Black), startx + 120, starty + offset);
                    e.Graphics.DrawString(date, font, new SolidBrush(Color.Black), startx + 230, starty + offset);
                    e.Graphics.DrawString(note, font, new SolidBrush(Color.Black), startx + 340, starty + offset);
                    e.Graphics.DrawString(rate, font, new SolidBrush(Color.Black), startx + 550, starty + offset);

                    offset = offset + (int)fontheight + 7;
                    //}
                }
                catch (Exception ex)
                {

                }

                for (int i = 0; i < 3; i++)
                {
                    e.Graphics.DrawString("", font, new SolidBrush(Color.Black), startx + 120, starty + offset);
                    offset = offset + (int)fontheight + 5;
                }

                e.Graphics.DrawString("-----------------------------------------------------", printFont, new SolidBrush(Color.Black), startx + 120, starty + offset);
                offset = offset + 13;
                string grosstotal = "Gross Total:".PadRight(7) + Spell.SpellAmount.comma(Convert.ToDecimal(AMOUNT.Text));
                ////string vatstring = "Tax Amount:".PadRight(5) + Spell.SpellAmount.comma(Convert.ToDecimal(TAX_TOTAL.Text));
                //string Discountstring = "Discount:".PadRight(13) + Spell.SpellAmount.comma(Convert.ToDecimal(AMOUNT.Text));
                string total = "Total:".PadRight(13) + Spell.SpellAmount.comma(Convert.ToDecimal(AMOUNT.Text));

                ////e.Graphics.DrawString(grosstotal, font, new SolidBrush(Color.Black), startx + 290, starty + offset + 6);
                ////offset = offset + (int)fontheight +3;
                ////e.Graphics.DrawString(vatstring, font, new SolidBrush(Color.Black), startx + 200, starty + offset + 3);
                ////offset = offset + (int)fontheight + 4;
                ////=================
                //if (Convert.ToDecimal(AMOUNT.Text) > 0)
                //==================
                //{
                //  e.Graphics.DrawString(Discountstring, font, new SolidBrush(Color.Black), startx + 290 + 120, starty + offset + 3);
                //  offset = offset + (int)fontheight + 1;
                // }
                ////e.Graphics.DrawString("------------------", font, new SolidBrush(Color.Black), startx + 290, starty + offset + 3);
                ////offset = offset + (int)fontheight + 1;
                e.Graphics.DrawString(total, font, new SolidBrush(Color.Black), startx + 290 + 120 + 40, starty + offset + 3);

                offset = offset + 18;

                e.Graphics.DrawString("-----------------------------------------------------", printFont, new SolidBrush(Color.Black), startx + 120, starty + offset);
                offset = offset + 59;


                ////try
                ////{
                ////    Font amountingeng = new Font("Courier New", 8);
                ////    string test = Spell.SpellAmount.InWrods(Convert.ToDecimal(NET_AMOUNT.Text));

                ////    int index = test.IndexOf("Taka ");
                ////    int l = test.Length;
                ////    test = test.Substring(index + 4);

                ////    e.Graphics.DrawString(test, amountingeng, new SolidBrush(Color.Black), startx, starty + offset + 3);
                ////}
                ////catch
                ////{
                ////}


                try
                {
                    Font amountingeng = new Font("Courier New", 10);

                    //e.Graphics.DrawString("9656198448,9605639467", printFont, new SolidBrush(Color.Black), startx + 120, starty + offset);
                    // e.Graphics.DrawString("9605639467,9633310444", printFont, new SolidBrush(Color.Black), startx + 120, starty + offset + 14);
                    e.Graphics.DrawString("REMARKS:KEEP IT...", amountingeng, new SolidBrush(Color.Black), startx + 120, starty + offset);
                }
                catch
                {
                }

                offset = offset + 15;
                ////if (txtcashrcvd.Text != "")
                ////{
                ////    try
                ////    {
                ////        decimal balance = Convert.ToDecimal(txtcashrcvd.Text) - Convert.ToDecimal(TOTAL_AMOUNT.Text);
                ////        e.Graphics.DrawString("Cash Rcvd:" + Spell.SpellAmount.comma(Convert.ToDecimal(txtcashrcvd.Text)) + "   " + "Balance:" + Spell.SpellAmount.comma(Convert.ToDecimal(balance)).ToString(), font, new SolidBrush(Color.Black), startx, starty + offset);

                ////        offset = offset + 12;

                ////    }
                ////    catch
                ////    {
                ////    }
                ////}
                e.HasMorePages = false;
            }
        }

        void Print_GSTHALF(object sender, PrintPageEventArgs e)
        {
            Company company = Common.getCompany();
            bool PRINTTOTALPAGE = true;
            bool hasmorepages = false;
            float xpos;
            int startx = 50;
            int starty = 20;
            int offset = 15;
            int headerstartposition = 50;
            int startX = 5;
            int startY = 10;
            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Font Headerfont1 = new Font("Calibri", 15, FontStyle.Bold);
            Font dec = new Font("Calibri", 8, FontStyle.Regular);
            Font Headerfont2 = new Font("Calibri", 10, FontStyle.Regular);
            Font number = new Font("Calibri", 8, FontStyle.Regular);
            Font printFont = new Font("Calibri", 10);
            Font printbold = new Font("Calibri", 10, FontStyle.Bold);
            Font printbold1 = new Font("Calibri", 11, FontStyle.Bold);
            string Gst1, Gst2;
            Font printnet = new Font("Calibri", 11, FontStyle.Bold);
            var tabDataForeColor = Color.Black;
            int height = 100 + y;
            decimal tgrossrate, ttaxva, trate;
            decimal tqty;
            tqty = 0; trate = 0; ttaxva = 0; tgrossrate = 0;
            Font FONTHEAD = new Font("Arial Black", 12, FontStyle.Bold);
            Font FONTHEAD1 = new Font("Arial Black", 9, FontStyle.Bold);
            Font FONTHEAD2 = new Font("Arial Black", 8, FontStyle.Bold);
            Font FONTGST = new Font("Arial Unicode MS", 11, FontStyle.Bold);
            Font FONTGST1 = new Font("Arial Unicode MS", 9, FontStyle.Bold);
            var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;
            Gst1 = company.TIN_No.Substring(0, 2);
            Pen blackPen = new Pen(Color.Black, 1);
            using (var sf = new StringFormat())
            {
                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);


                offset = offset + 30;
                // e.Graphics.DrawString(Address1 + "," + Address2, Headerfont2, new SolidBrush(Color.Black), headerstartposition, starty + 20 + offset);
                offset = offset + 12;

                offset = offset + 16;
                offset = 15;

                offset = offset + 16;
                if (Type == "Debit Note")
                {
                    e.Graphics.DrawString("Debit Note", FONTHEAD, new SolidBrush(Color.Black), 360, startY + 15);
                    e.Graphics.DrawString("Debit Note.No", Headerfont2, new SolidBrush(Color.Black), 550, startY + 10);
                }
                else
                {
                    e.Graphics.DrawString("Credit Note", FONTHEAD, new SolidBrush(Color.Black), 360, startY + 15);
                    e.Graphics.DrawString("Credit Note.No", Headerfont2, new SolidBrush(Color.Black), 550, startY + 10);
                }
                e.Graphics.DrawString(company.Name, FONTHEAD1, new SolidBrush(Color.Black), startX, startY);
                e.Graphics.DrawString(company.Address, FONTHEAD2, new SolidBrush(Color.Black), startX, startY + 15);
                e.Graphics.DrawString(company.Phone, FONTHEAD2, new SolidBrush(Color.Black), startX, startY + 30);
                e.Graphics.DrawString("GSTIN:" + company.TIN_No, FONTHEAD1, new SolidBrush(Color.Black), startX, startY + 45);
                
                e.Graphics.DrawString(":" + txt_docid.Text, Headerfont2, new SolidBrush(Color.Black), 628, startY + 10);
                offset = offset + 16;
                e.Graphics.DrawString("Date", Headerfont2, new SolidBrush(Color.Black), 550, startY + 25);
                e.Graphics.DrawString(":" + DOC_DATE_GRE.Value.ToShortDateString(), Headerfont2, new SolidBrush(Color.Black), 620, startY + 25);

                startY += 90;

                e.Graphics.DrawRectangle(blackPen, startX, startY, 780, 450);
                DebitNoteDB.Customer = Convert.ToInt32(PARTYACC.SelectedValue);
                DataTable table = Common.getCustomer(DebitNoteDB.CCodeByLedger());
                Font font12 = new Font("Calibri", 9);
                e.Graphics.DrawString("Name", font12, new SolidBrush(Color.Black), startX, startY + 1);
                e.Graphics.DrawString("Address", font12, new SolidBrush(Color.Black), startX, startY + 12);
                e.Graphics.DrawString("Mob", font12, new SolidBrush(Color.Black), startX, startY + 22);
                e.Graphics.DrawString("GSTIN", font12, new SolidBrush(Color.Black), startX, startY + 32);
                e.Graphics.DrawString("State", font12, new SolidBrush(Color.Black), startX, startY + 42);
                if (table.Rows.Count > 0)
                {
                    e.Graphics.DrawString(":" + table.Rows[0]["DESC_ENG"].ToString(), font12, new SolidBrush(Color.Black), startX + 60, startY + 1);
                    e.Graphics.DrawString(":" + table.Rows[0]["ADDRESS_A"].ToString(), font12, new SolidBrush(Color.Black), startX + 60, startY + 12);
                    e.Graphics.DrawString(":" + table.Rows[0]["MOBILE"].ToString(), font12, new SolidBrush(Color.Black), startX + 60, startY + 22);
                    e.Graphics.DrawString(":" + table.Rows[0]["TIN_NO"].ToString(), font12, new SolidBrush(Color.Black), startX + 60, startY + 32);
                    e.Graphics.DrawString(":" + table.Rows[0]["STATE"].ToString(), font12, new SolidBrush(Color.Black), startX + 60, startY + 42);
                }
       string tin_no="";
                try
                {
                     tin_no= table.Rows[0]["TIN_NO"].ToString();
                }
                catch
                {

                }
                if (tin_no.Length > 0)
                {
                    Gst2 = tin_no.Substring(0, 2);
                }
                else
                {
                    Gst2 = Gst1;
                }
                e.Graphics.DrawString("Delivery Note No & Date", Headerfont2, new SolidBrush(Color.Black), startX + 450, startY + 1);
                e.Graphics.DrawString(":", Headerfont2, new SolidBrush(Color.Black), startX + 610, startY + 1);
                e.Graphics.DrawString("Purchase Order No & Date", Headerfont2, new SolidBrush(Color.Black), startX + 450, startY + 14);
                e.Graphics.DrawString(":", Headerfont2, new SolidBrush(Color.Black), startX + 610, startY + 14);
                e.Graphics.DrawString("Dispatch Doc No & Date", Headerfont2, new SolidBrush(Color.Black), startX + 450, startY + 26);
                e.Graphics.DrawString(":", Headerfont2, new SolidBrush(Color.Black), startX + 610, startY + 26);
                e.Graphics.DrawString("Terms of Delivery if any", Headerfont2, new SolidBrush(Color.Black), startX + 450, startY + 42);
                e.Graphics.DrawString(":", Headerfont2, new SolidBrush(Color.Black), startX + 610, startY + 42);
                startY += 60;
                e.Graphics.DrawRectangle(blackPen, startX, startY, 780, 280);
                e.Graphics.DrawLine(blackPen, startX, startY + 300, startX + 780, startY + 300);
                //   e.Graphics.DrawLine(blackPen, startX, startY + 100, 800 + startX, startY + 100);
                offset = offset + 16;
                //  e.Graphics.DrawString("To                     :", Headerfont2, new SolidBrush(Color.Black), 550, starty + offset);
                // e.Graphics.DrawString(CUSTOMER_NAME.Text, printbold, new SolidBrush(Color.Black), 630, starty + offset);
                offset = offset + 16;
                //   e.Graphics.DrawString("Des.Docu.No & Date:", Headerfont2, new SolidBrush(Color.Black), 580, starty + offset);
                offset = offset + 16;

                //e.Graphics.DrawString("Form No.8", Headerfont2, new SolidBrush(Color.Black), 510, starty + offset);
                offset = offset + 16;
                // e.Graphics.DrawString("[See rule 58(10)]", Headerfont2, new SolidBrush(Color.Black), 510, starty + offset);
                offset = offset + 16;
                // e.Graphics.DrawString("Tax Invoice/Cash/Credit", Headerfont2, new SolidBrush(Color.Black), 510, starty + offset);
                offset = offset + 16;
                offset = offset + 16;
                offset = offset + 16;
                string headtext;
                if (Gst1 == Gst2)
                {
                    headtext = "Sl.No".PadRight(10) + "Item".PadRight(88) + "HSN".PadRight(11) + "Unit".PadRight(8) + "Qty".PadRight(11) + "Rate".PadRight(11) + "Disc".PadRight(13) + "CGST".PadRight(19) + "SGST".PadRight(18) + "Total";
                }
                else
                {
                    headtext = "Sl.No".PadRight(10) + "Item".PadRight(88) + "HSN".PadRight(11) + "Unit".PadRight(8) + "Qty".PadRight(11) + "Rate".PadRight(11) + "Disc".PadRight(13) + "IGST".PadRight(19) + "-----------".PadRight(18) + "Total";
                } 
                e.Graphics.DrawString(headtext, printbold, new SolidBrush(Color.Black), startX, startY);
                startY += 25;
                //Column head Hline
                Point point1 = new Point(startX, startY);
                Point point2 = new Point(startX + 780, startY);
                e.Graphics.DrawLine(blackPen, point1, point2);





                Font itemhead = new Font("Times New Roman", 8);

                //sl_No

                e.Graphics.DrawLine(blackPen, startX + 35, startY - 25, startX + 35, startY + 275);
                //item
                e.Graphics.DrawLine(blackPen, startX + 340, startY - 25, startX + 340, startY + 275);
                //HSN 
                e.Graphics.DrawLine(blackPen, startX + 390, startY - 25, startX + 390, startY + 275);
                //uom
                e.Graphics.DrawLine(blackPen, startX + 420, startY - 25, startX + 420, startY + 275);
                //qty
                e.Graphics.DrawLine(blackPen, startX + 460, startY - 25, startX + 460, startY + 275);
                //RATE
                e.Graphics.DrawLine(blackPen, startX + 520, startY - 25, startX + 520, 550);
                //DISC
                e.Graphics.DrawLine(blackPen, startX + 560, startY - 25, startX + 560, startY + 275);
                //CGST
                e.Graphics.DrawLine(blackPen, startX + 640, startY - 25, startX + 640, startY + 275);
                //SGST
                e.Graphics.DrawLine(blackPen, startX + 720, startY - 25, startX + 720, startY + 275);


                int printpoint = 50; //32
                offset = offset + 35;
                startY += 3;
                Font font = new Font("Calibri", 8);
                float fontheight = font.GetHeight();
                try
                {
                    int j = 1;
                    int nooflines = 0;


                    PRINTTOTALPAGE = false;

                    if (nooflines < 14)
                    {

                        string period, periodtype, tax;
                        int ORGLENGTH = Noteonreciept.Text.Length;
                        string name = Noteonreciept.Text.Length <= 50 ? Noteonreciept.Text : Noteonreciept.Text.Substring(0, 50);
                        string name2 = "";
                        int BALANCELENGH = ORGLENGTH - 50;
                        string qty = "1";
                        string rate, gross, TotalAmt;
                        if (ch_tax.Checked)
                        {
                            rate = (Convert.ToDecimal(AMOUNT.Text) - Convert.ToDecimal(txt_taxamt.Text)).ToString();
                            TotalAmt = AMOUNT.Text;
                        }
                        else
                        {
                            rate = AMOUNT.Text;
                            TotalAmt = (Convert.ToDecimal(AMOUNT.Text) + Convert.ToDecimal(txt_taxamt.Text)).ToString();
                        }

                        string uom = "";
                        string HSN = "";

                        decimal TaxPer = Convert.ToDecimal(TAX.SelectedValue);
                        decimal TaxVal = Convert.ToDecimal(txt_taxamt.Text);
                        string Disc = "0.00";
                        tqty = tqty + Convert.ToDecimal(qty);
                        trate = trate + Convert.ToDecimal(rate);
                        ttaxva += Convert.ToDecimal(qty) * Convert.ToDecimal(rate);
                        //tcdis = tcdis + Convert.ToDecimal(row.Cells["cDisc"].Value.ToString());

                        //to add % in taxpercentage
                        if ((TaxPer / 2) % 1 > 0)
                            tax = (TaxPer / 2).ToString();
                        else
                            tax = Convert.ToInt16(TaxPer / 2).ToString();

                        e.Graphics.DrawString("1.", font, new SolidBrush(Color.Black), startX, startY);
                        e.Graphics.DrawString(name, font, new SolidBrush(Color.Black), startX + 35, startY);
                        e.Graphics.DrawString(HSN, font, new SolidBrush(Color.Black), startX + 340, startY);
                        e.Graphics.DrawString(uom, font, new SolidBrush(Color.Black), startX + 390, startY);


                        StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                        e.Graphics.DrawString(qty, font, new SolidBrush(Color.Black), startX + 460, startY, format);
                        e.Graphics.DrawString(rate, font, new SolidBrush(Color.Black), startX + 520, startY, format);
                        e.Graphics.DrawString(Disc, font, new SolidBrush(Color.Black), startX + 560, startY, format);
                        if (Gst1 == Gst2)
                        {
                            e.Graphics.DrawString(tax + "%", font, new SolidBrush(Color.Black), startX + 560, startY);
                            e.Graphics.DrawString((TaxVal / 2).ToString(), font, new SolidBrush(Color.Black), startX + 640, startY, format);
                            e.Graphics.DrawString(tax + "%", font, new SolidBrush(Color.Black), startX + 640, startY);
                            e.Graphics.DrawString((TaxVal / 2).ToString(), font, new SolidBrush(Color.Black), startX + 720, startY, format);
                        }
                        else
                        {
                            e.Graphics.DrawString(TaxPer + "%", font, new SolidBrush(Color.Black), startX + 560, startY);
                            e.Graphics.DrawString((TaxVal).ToString(), font, new SolidBrush(Color.Black), startX + 640, startY, format);
                        }
                        e.Graphics.DrawString(TotalAmt, font, new SolidBrush(Color.Black), startX + 780, startY, format);
                        
                        
                        offset = offset + (int)fontheight + 2;
                        //if (Serial != "")
                        //{
                        //    string s = Serial;
                        //    string[] values = s.Split(',');
                        //    for (int i = 0; i < (values.Length) && (i < Convert.ToInt64(qty)); i++)
                        //    {
                        //        values[i] = values[i].Trim();
                        //        e.Graphics.DrawString("SN No: " + values[i].ToString(), font, new SolidBrush(Color.Black), startx + 30, starty + offset);

                        //        offset = offset + (int)fontheight + 2;
                        //        nooflines++;
                        //    }

                        //}
                        nooflines++;
                        while (BALANCELENGH > 1)
                        {
                            startY += (int)fontheight;
                            name2 = BALANCELENGH <= 50 ? Noteonreciept.Text.Substring(printpoint, BALANCELENGH) : Noteonreciept.Text.Substring(printpoint, 50);
                            e.Graphics.DrawString(name2, font, new SolidBrush(Color.Black), startX + 35, startY);
                            BALANCELENGH = BALANCELENGH - 50;
                            printpoint = printpoint + 50;
                            // startY = startY + (int)fontheight;
                        }
                        printpoint = 50;
                        j++;
                        startY += (int)fontheight + 2;
                    }
                    else
                    {
                        //printeditems = j - 1;
                        //  e.HasMorePages = true;
                        hasmorepages = true;
                        PRINTTOTALPAGE = true;
                    }
                }
                catch (Exception exc)
                {
                    string s = exc.Message;

                }
            }
            startY = 460;
            float newoffset = 460;



            if (!PRINTTOTALPAGE)
            {
                try
                {
                    StringFormat format1 = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                    e.Graphics.DrawString("Total Amount Before Tax", printbold, new SolidBrush(Color.Black), startX + 520, startY + 3);
                    e.Graphics.DrawString(ttaxva.ToString(), printbold, new SolidBrush(Color.Black), startX + 780, startY + 3, format1);
                    e.Graphics.DrawLine(blackPen, startX + 520, startY + 20, startX + 780, startY + 20);
                    if (Gst1 == Gst2)
                    {
                        e.Graphics.DrawString("Tax Amount : CGST+SGST", printbold, new SolidBrush(Color.Black), startX + 520, startY + 23);
                    }
                    else
                    {
                        e.Graphics.DrawString("Tax Amount : IGST", printbold, new SolidBrush(Color.Black), startX + 520, startY + 23);
                    }
                    e.Graphics.DrawString(txt_taxamt.Text, printbold, new SolidBrush(Color.Black), startX + 780, startY + 23, format1);
                    e.Graphics.DrawLine(blackPen, startX + 520, startY + 40, startX + 780, startY + 40);
                    e.Graphics.DrawString("Total Discount:", printbold, new SolidBrush(Color.Black), startX + 520, startY + 43);
                    e.Graphics.DrawString("0.00", printbold, new SolidBrush(Color.Black), startX + 780, startY + 43, format1);
                    e.Graphics.DrawLine(blackPen, startX + 520, startY + 60, startX + 780, startY + 60);
                    //    e.Graphics.DrawString("Total Amount After Tax", printbold, new SolidBrush(Color.Black), startX + 520, startY + 63);
                    if (ch_tax.Checked)
                        e.Graphics.DrawString(AMOUNT.Text, printbold1, new SolidBrush(Color.Black), startX + 780, startY + 63, format1);
                    else
                        e.Graphics.DrawString((Convert.ToDecimal(AMOUNT.Text) + Convert.ToDecimal(txt_taxamt.Text)).ToString(), printbold1, new SolidBrush(Color.Black), startX + 780, startY + 63, format1);
                    //   e.Graphics.DrawLine(blackPen, startX + 520, startY + 80, startX + 780, startY + 80);

                    e.Graphics.DrawString("Authorized Signatory", number, new SolidBrush(Color.Black), startX + 520, startY + 65);
                    e.Graphics.DrawString("[With Status and Seal]", number, new SolidBrush(Color.Black), startX + 520, startY + 74);

                    Font font11 = new Font("Calibri", 8);
                    StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                    e.Graphics.DrawString("Total", font11, new SolidBrush(Color.Black), startx + 300, 440);
                    e.Graphics.DrawString(tqty.ToString(), font11, new SolidBrush(Color.Black), startX + 460, 440, format);
                    if (Gst1 == Gst2)
                    {
                        e.Graphics.DrawString((Convert.ToDecimal(txt_taxamt.Text) / 2).ToString(), font11, new SolidBrush(Color.Black), startX + 640, 440, format);
                        e.Graphics.DrawString((Convert.ToDecimal(txt_taxamt.Text) / 2).ToString(), font11, new SolidBrush(Color.Black), startX + 720, 440, format);
                    }
                    else
                    {
                        e.Graphics.DrawString((Convert.ToDecimal(txt_taxamt.Text)).ToString(), font11, new SolidBrush(Color.Black), startX + 640, 440, format);
                    } 
                    string test = "";

                    if (ch_tax.Checked)
                    {
                        e.Graphics.DrawString(AMOUNT.Text, font11, new SolidBrush(Color.Black), startX + 780, 440, format);
                        test = Spell.SpellAmount.InWrods(Convert.ToDecimal(AMOUNT.Text));
                    }
                    else
                    {
                        e.Graphics.DrawString((Convert.ToDecimal(AMOUNT.Text) + Convert.ToDecimal(txt_taxamt.Text)).ToString(), font11, new SolidBrush(Color.Black), startX + 780, 440, format);
                        test = Spell.SpellAmount.InWrods(Convert.ToDecimal((Convert.ToDecimal(AMOUNT.Text) + Convert.ToDecimal(txt_taxamt.Text)).ToString()));
                    }


                    int index = test.IndexOf("Rupees");
                    int l = test.Length;
                    test = test.Substring(index + 5);
                    e.Graphics.DrawString("Terms & conditions:", Headerfont2, new SolidBrush(Color.Black), startX, startY + 22);
                    e.Graphics.DrawString("Amount in words:", font11, new SolidBrush(Color.Black), startX, startY + 3);
                    e.Graphics.DrawString(test, new Font("Calibri", 8, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + 14);

                }
                catch
                {
                }

            }



            e.HasMorePages = hasmorepages;


        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            float xpos;
            int startx = 10;
            int starty = 30;
            int offset = 15;

            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Font Headerfont1 = new Font("Courier New", 12);
            Font printFont = new Font("Courier New", 8);
            var tabDataForeColor = Color.Black;
            int height = 100 + y;

            var txtDataWidth = e.Graphics.MeasureString(CompanyName, printFont).Width;
            using (var sf = new StringFormat())
            {
                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);

                e.Graphics.DrawString(CompanyName, printFont, new SolidBrush(tabDataForeColor), xpos, starty, sf);
                e.Graphics.DrawString(Address1, printFont, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                offset = offset + 10;
                e.Graphics.DrawString(Addres2, printFont, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                offset = offset + 10;
                e.Graphics.DrawString("Credit Note" + DOC_NO.Text, printFont, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                offset = offset + 10;

                e.Graphics.DrawString("-------------------------------------------------------------", printFont, new SolidBrush(Color.Black), xpos, starty + offset, sf);
                e.Graphics.DrawString("-------------------------------------------------------------", printFont, new SolidBrush(Color.Black), xpos, starty + offset + 3, sf);

                offset = offset + 10;
                e.Graphics.DrawString("Tin No:" + TineNo, printFont, new SolidBrush(tabDataForeColor), startx, starty + offset);
                offset = offset + 12;
                e.Graphics.DrawString("Date:" + DateTime.Now.ToString(), printFont, new SolidBrush(tabDataForeColor), startx, starty + offset);
                offset = offset + 12;
                e.Graphics.DrawString("Customer:" + PARTYACC.Text, printFont, new SolidBrush(tabDataForeColor), startx, starty + offset);
                Font itemhead = new Font("Courier New", 8);
                offset = offset + 12;
                e.Graphics.DrawString("-----------------------------------------------", printFont, new SolidBrush(Color.Black), startx, starty + offset);
                offset = offset + 12;

                string headtext = "Item".PadRight(20) + "Tax%".PadRight(5) + "Qty".PadRight(5) + "Rate".PadRight(10) + "Total";
                e.Graphics.DrawString(headtext, itemhead, new SolidBrush(Color.Black), startx, starty + offset - 1);
                e.Graphics.DrawString("-----------------------------------------------", printFont, new SolidBrush(Color.Black), startx, starty + offset + 7);
                offset = offset + 15;
                Font font = new Font("Courier New", 8);
                float fontheight = font.GetHeight();
                try
                {
                    foreach (DataGridViewRow row in dgDetail.Rows)
                    {

                        string name = row.Cells[1].Value.ToString().PadRight(20);
                        string tax = row.Cells[7].Value.ToString().PadRight(5);
                        string qty = row.Cells[5].Value.ToString().PadRight(5);
                        string rate = row.Cells[6].Value.ToString().PadRight(10);
                        string price = row.Cells[11].Value.ToString();
                        string productline = name + tax + qty + rate + price;
                        e.Graphics.DrawString(productline, font, new SolidBrush(Color.Black), startx, starty + offset);
                        offset = offset + (int)fontheight + 5;
                    }
                }
                catch
                {

                }

                e.Graphics.DrawString("-----------------------------------------------", printFont, new SolidBrush(Color.Black), startx, starty + offset);
                offset = offset + 12;
                //============
                string vatstring = "Tax Amount:" + AMOUNT.Text.PadRight(30);
                string total = "Total:" + AMOUNT.Text;
                //============
                string endtotal = total;
                e.Graphics.DrawString(endtotal, font, new SolidBrush(Color.Black), startx + 200, starty + offset + 3);

                offset = offset + 15;
                //if (txtcashrcvd.Text != "")
                //{
                //    try
                //    {
                //        decimal balance = Convert.ToDecimal(txtcashrcvd.Text) - Convert.ToDecimal(TOTAL_AMOUNT.Text);
                //        e.Graphics.DrawString("Cash Rcvd:" + txtcashrcvd.Text + "   " + "Balance:" + balance.ToString(), font, new SolidBrush(Color.Black), startx, starty + offset);

                //        offset = offset + 12;

                //    }
                //    catch
                //    {
                //    }
                //}

            }

            e.HasMorePages = false;


        }
        void printdocumentA4_PrintPage(object sender, PrintPageEventArgs e)
        {
            float xpos;
            int startx = 50;
            int starty = 30;
            int offset = 15;
            int headerstartposition = 150;

            int w = e.MarginBounds.Width / 2;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Font Headerfont1 = new Font("Times New Roman", 15, FontStyle.Bold);
            Font Headerfont2 = new Font("Times New Roman", 10, FontStyle.Bold);
            Font printFont = new Font("Times New Roman", 10);
            var tabDataForeColor = Color.Black;
            int height = 100 + y;

            var txtDataWidth = e.Graphics.MeasureString(CompanyName, Headerfont1).Width;

            if (logo != null || logo != "")
            {

                System.Drawing.Image img = System.Drawing.Image.FromFile(logo);

                Point loc = new Point(20, 50);
                e.Graphics.DrawImage(img, new Rectangle(50, 50, 50, 50));
            }


            using (var sf = new StringFormat())
            {
                height += 15;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                xpos = e.MarginBounds.Left + (e.MarginBounds.Width / 2);

                // e.Graphics.DrawString(CompanyName, Headerfont1, new SolidBrush(tabDataForeColor), xpos, starty, sf);
                //   e.Graphics.DrawString(Addres1+", "+Addres2, Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                //  offset = offset + 20;
                //  e.Graphics.DrawString(Phone, Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                //   offset = offset + 20;
                //  e.Graphics.DrawString("Credit Note: " + DOC_NO.Text, Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset, sf);
                //   offset = offset + 20;


                e.Graphics.DrawString(CompanyName, Headerfont1, new SolidBrush(tabDataForeColor), headerstartposition, starty);
                offset = offset + 9;
                e.Graphics.DrawString(Addres1 + ", " + Addres2, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                offset = offset + 20;
                e.Graphics.DrawString("Phone:".PadRight(3) + Phone, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                offset = offset + 20;
                e.Graphics.DrawString("Email:".PadRight(3) + Email, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                offset = offset + 20;
                e.Graphics.DrawString("Website:".PadRight(3) + Website, Headerfont2, new SolidBrush(tabDataForeColor), headerstartposition, starty + offset);
                offset = offset + 20;


                e.Graphics.DrawString("No: " + DOC_NO.Text, Headerfont2, new SolidBrush(tabDataForeColor), 600, starty + offset);
                offset = offset + 16;

                e.Graphics.DrawString("Tin No:" + TineNo, Headerfont2, new SolidBrush(tabDataForeColor), 600, starty + offset);
                offset = offset + 16;
                e.Graphics.DrawString("Date:" + DateTime.Now.ToString(), Headerfont2, new SolidBrush(tabDataForeColor), 600, starty + offset);
                offset = offset + 16;
                e.Graphics.DrawString("Credit Note", Headerfont2, new SolidBrush(tabDataForeColor), xpos, starty + offset - 24, sf);
                offset = offset + 16;
                Pen blackPen = new Pen(Color.Black, 1);
                Point point1 = new Point(0, 185);
                Point point2 = new Point(900, 185);
                e.Graphics.DrawLine(blackPen, point1, point2);

                e.Graphics.DrawString("To:" + PARTYACC.Text, Headerfont2, new SolidBrush(tabDataForeColor), startx, starty + offset - 36);
                Font itemhead = new Font("Times New Roman", 8);
                offset = offset + 2;

                Point point3 = new Point(45, 219);
                Point point4 = new Point(760, 219);
                e.Graphics.DrawLine(blackPen, point3, point4);

                e.Graphics.DrawLine(blackPen, 45, 219, 45, 900);

                e.Graphics.DrawLine(blackPen, 355, 219, 355, 900);
                e.Graphics.DrawLine(blackPen, 450, 219, 450, 900);
                e.Graphics.DrawLine(blackPen, 540, 219, 540, 900);
                e.Graphics.DrawLine(blackPen, 650, 219, 650, 900);
                e.Graphics.DrawLine(blackPen, 760, 219, 760, 900);

                e.Graphics.DrawLine(blackPen, 45, 900, 760, 900);


                string headtext = "Item".PadRight(90) + "".PadRight(22) + "Qty".PadRight(22) + "Rate".PadRight(30) + "Total";
                e.Graphics.DrawString(headtext, Headerfont2, new SolidBrush(Color.Black), startx, starty + offset - 1);

                offset = offset + 40;
                Font font = new Font("Times New Roman", 10);
                float fontheight = font.GetHeight();
                try
                {
                    foreach (DataGridViewRow row in dgDetail.Rows)
                    {

                        string name = row.Cells[1].Value.ToString();
                        string tax = row.Cells[7].Value.ToString();
                        string qty = row.Cells[5].Value.ToString();
                        string rate = row.Cells[6].Value.ToString();
                        string price = row.Cells[11].Value.ToString();
                        string productline = name + tax + qty + rate + price;
                        e.Graphics.DrawString(name, font, new SolidBrush(Color.Black), startx, starty + offset);

                        e.Graphics.DrawString(tax, font, new SolidBrush(Color.Black), startx + 310, starty + offset);
                        e.Graphics.DrawString(qty, font, new SolidBrush(Color.Black), startx + 430, starty + offset);
                        e.Graphics.DrawString(rate, font, new SolidBrush(Color.Black), startx + 525, starty + offset);
                        e.Graphics.DrawString(price, font, new SolidBrush(Color.Black), startx + 630, starty + offset);
                        offset = offset + (int)fontheight + 10;
                    }
                }
                catch
                {

                }
            }

            float newoffset = 900;

            e.Graphics.DrawString(NOTES.Text, Headerfont2, new SolidBrush(Color.Black), startx, starty + newoffset + 3);

            e.Graphics.DrawString("Gross Total", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 3);
            e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(AMOUNT.Text)), Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + newoffset + 3);
            try
            {
                string test = Spell.SpellAmount.InWrods(Convert.ToDecimal(AMOUNT.Text));

                int index = test.IndexOf("Taka");
                int l = test.Length;
                test = test.Substring(index + 4);

                e.Graphics.DrawString(test, Headerfont2, new SolidBrush(Color.Black), startx, starty + newoffset + 3);
            }
            catch
            {
            }


            newoffset = newoffset + 20;
            e.Graphics.DrawString("Discount", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 3);
            e.Graphics.DrawString(AMOUNT.Text, Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + newoffset + 3);

            //  offset = offset + 20;
            //e.Graphics.DrawString("VAT", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + offset + 3);
            //e.Graphics.DrawString(VAT.Text, Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + offset + 3);

            //newoffset = newoffset + 20;

            //e.Graphics.DrawString("Tax Amount", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + offset + 3);
            //e.Graphics.DrawString(TAX_TOTAL.Text, Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + offset + 3);

            offset = offset + 20;
            e.Graphics.DrawString("---------------------------------------", Headerfont2, new SolidBrush(Color.Black), startx + 490, starty + newoffset + 3);
            e.Graphics.DrawString("Authorized Signature", Headerfont2, new SolidBrush(Color.Black), startx, starty + newoffset + 3);
            offset = offset + 25;
            e.Graphics.DrawString("Total", Headerfont2, new SolidBrush(Color.Black), startx + 460, starty + newoffset + 3);


            e.Graphics.DrawString(Spell.SpellAmount.comma(Convert.ToDecimal(AMOUNT.Text)), Headerfont2, new SolidBrush(Color.Black), startx + 610, starty + newoffset + 3);

            newoffset = newoffset + 20;
            e.HasMorePages = false;
        }

        public string generatePayVoucherCode()
        {
            string id = "";
            try
            {
                //cmd.Connection = conn;
                //  conn.Open();
                //  cmd.CommandType = CommandType.Text;
                string value = DateTime.Today.ToString("ddMMyy");
                string testvalue = (Convert.ToInt64(value)).ToString();
                //if (Type == "Debit Note")
                //{
                //    cmd.CommandText = "SELECT MAX(VOUCHERNO) FROM tb_Transactions WHERE VOUCHERTYPE ='Debit Note'";
                //}
                //else
                //{
                //    cmd.CommandText = "SELECT MAX(VOUCHERNO) FROM tb_Transactions WHERE VOUCHERTYPE ='Credit Note'";
                //}
                // id = Convert.ToString(cmd.ExecuteScalar());
                id = Convert.ToString(transObj.isDebitOrCredit(Type));
                //   conn.Close();
                if (id == "")
                {
                    id = testvalue + "0001";
                    //  id = DateTime.Today.ToString("ddMMyy") + "0001";
                }
                else
                {
                    id = (Convert.ToInt64(id) + 1).ToString();
                }

            }
            catch
            {
            }
            return id;
        }

        private void DeleteTransation()
        {
            try
            {
                trans.VOUCHERTYPE = Type;
                trans.VOUCHERNO = DOC_NO.Text;
                trans.DeletePurchaseTransaction();
            }
            catch
            {
            }
        }
        public void modifiedtransaction()
        {
            modtrans.VOUCHERTYPE = Type;
            modtrans.Date = DOC_DATE_GRE.Value.ToString("MM/dd/yyyy");
            modtrans.USERID = lg.EmpId;
            modtrans.VOUCHERNO = DOC_NO.Text;
            modtrans.NARRATION = Noteonreciept.Text;
            modtrans.STATUS = "Update";
            modtrans.MODIFIEDDATE = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss"); ;
            modtrans.BRANCH = "";
            modtrans.insertTransaction();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            TAX.SelectedIndexChanged -= TAX_SelectedIndexChanged;
            GetTaxRates();
            TAX.SelectedIndexChanged += TAX_SelectedIndexChanged;
            ActiveControl = DOC_DATE_GRE;
            //  CASHACC.SelectedValue = "21";
            HasArabic = General.IsEnabled(Settings.Arabic);
            if (!HasArabic)
                DOC_DATE_HIJ.Enabled = false;
            AMOUNT.KeyPress += new KeyPressEventHandler(General.OnlyFloat);
            PrintPage.SelectedIndex = 0;
            if (Type == "Debit Note")
            {
                // BindSupplier();

                cashacclbl.Text = "Dr Acc";
                partyacclbl.Text = "Cr Acc";
                CASHACC.SelectedValue = 32;
            }
            else
            {

                cashacclbl.Text = "Cr Acc";
                partyacclbl.Text = "Dr Acc";
                CASHACC.SelectedValue = 31;
                //BindCustomer();
            }

            if (Type == "Debit Note")
                txt_docid.Text = DebitNoteDB.maxId().ToString();
            else
                txt_docid.Text = CreditNoteDB.maxId().ToString();
            DOC_DATE_GRE.Value = DateTime.Now;
            PARTYACC.Text = "";
            CASHACC.Text = "";
            AMOUNT.Text = "0.00";
            //TOTAL_BALANCE.Text = "0.00";
            DOC_NO.Text = "";
            NOTES.Text = "";
            Noteonreciept.Text = "";
            TAX.SelectedIndex = 0;
            txt_taxamt.Text = "";
            ch_tax.Checked = true;
            doc_reference.Text = "";
            Edit = false;
            //dgDetail.Rows.Clear();
            string query;
            if (Type == "Debit Note")
            {
                query = "SELECT * FROM Tbl_DebitNote";
            }
            else
            {
                query = "SELECT * FROM Tbl_CreditNote";
            }
            this.Text = Type;
            DataTable table = Model.DbFunctions.GetDataTable(query);
            dgDetail.DataSource = table;
        }

        public void TaxAmount()
        {
            if (TAX.SelectedIndex != -1 && TAX.Text != "" && AMOUNT.Text != "")
            {
                if (ch_tax.Checked == true)
                {
                    txt_taxamt.Text = (Convert.ToDecimal(AMOUNT.Text != "" ? AMOUNT.Text : "0") - ((Convert.ToDecimal(AMOUNT.Text != "" ? AMOUNT.Text : "0") / (1 + (Convert.ToDecimal(TAX.SelectedValue) / 100))))).ToString(decimalFormat);
                }
                else
                {
                    txt_taxamt.Text = (Convert.ToDecimal(AMOUNT.Text != "" ? AMOUNT.Text : "0") * (Convert.ToDecimal(TAX.SelectedValue) / 100)).ToString(decimalFormat);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            btnClear.PerformClick();
            Accounts.DebitNoteHelp h = new Accounts.DebitNoteHelp(1, Type);
            h.ShowDialog();
        }

        private void btnDoc_Click(object sender, EventArgs e)
        {
            Accounts.DebitNoteHelp h = new Accounts.DebitNoteHelp(0, Type);
            if (h.ShowDialog() == DialogResult.OK && h.c != null)
            {
                try
                {
                    btnClear.PerformClick();

                    if (Type == "Debit Note")
                    {
                        txt_docid.Text = Convert.ToString(h.c["Id"].Value);
                        DOC_NO.Text = Convert.ToString(h.c["DocNo"].Value);
                        CASHACC.SelectedValue = h.c["CashAccount"].Value;
                        PARTYACC.SelectedValue = h.c["Customer"].Value;
                        doc_reference.Text = Convert.ToString(h.c["ReferenceNo"].Value);
                        AMOUNT.Text = Convert.ToString(h.c["Amount"].Value);
                        DOC_DATE_GRE.Value = Convert.ToDateTime(h.c["Date"].Value.ToString());
                        Noteonreciept.Text = h.c["Note"].Value.ToString();
                        NOTES.Text = h.c["Remark"].Value.ToString();
                        TAX.SelectedValue = h.c["Tax"].Value;
                        if (h.c["TaxInclusive"].Value.ToString() == "True")
                        {
                            ch_tax.Checked = true;
                        }
                        else
                        {
                            ch_tax.Checked = false;
                        }
                        Edit = true;
                    }
                    else
                    {
                        txt_docid.Text = Convert.ToString(h.c["CN_Id"].Value);
                        DOC_NO.Text = Convert.ToString(h.c["CN_Doc_No"].Value);

                        PARTYACC.SelectedValue = h.c["CUSTOMER_CODE"].Value;
                        try
                        {
                            CASHACC.SelectedValue = h.c["CashAccount"].Value;
                        }
                        catch { }
                            doc_reference.Text = Convert.ToString(h.c["CN_Reffrence_No"].Value);
                        AMOUNT.Text = Convert.ToString(h.c["Nett_Amount"].Value);
                        DOC_DATE_GRE.Value = Convert.ToDateTime(h.c["CN_Date"].Value.ToString());
                        Noteonreciept.Text = h.c["NOTES"].Value.ToString();
                        NOTES.Text = h.c["Remarks"].Value.ToString();

                        if (h.c["Tax"].Value != null && h.c["Tax"].Value != "")
                        {
                            TAX.SelectedValue = h.c["Tax"].Value;
                        }
                        else
                        {
                            TAX.SelectedValue = 0;
                        }

                        if (h.c["TaxInclusive"].Value.ToString() == "True")
                        {
                            ch_tax.Checked = true;
                        }
                        else
                        {
                            ch_tax.Checked = false;
                        }
                        Edit = true;
                    }
                }
                catch
                { }
            }
            ActiveControl = PARTYACC;
        }

        private void kryptonLabel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DOC_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                NextFocus(sender, e);
            }
        }

        private void DOC_DATE_GRE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                NextFocus(sender, e);
            }
        }

        private void PARTYACC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                NextFocus(sender, e);
                ActiveControl = CASHACC;
            }
        }

        private void CASHACC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                NextFocus(sender, e);
                ActiveControl = AMOUNT;
            }
        }

        private void AMOUNT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                NextFocus(sender, e);
                ActiveControl = TAX;
            }
        }

        private void kryptonComboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            NextFocus(sender, e);
            ActiveControl = ch_tax;
        }

        private void ch_tax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                NextFocus(sender, e);
                ActiveControl = doc_reference;
            }
        }

        private void doc_reference_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                NextFocus(sender, e);
                ActiveControl = BALANCE;
            }
        }

        private void kryptonTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                NextFocus(sender, e);
                ActiveControl = Noteonreciept;
            }
        }

        private void Noteonreciept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                NextFocus(sender, e);
                ActiveControl = NOTES;
            }
        }

        private void NOTES_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                NextFocus(sender, e);
                ActiveControl = ChekPrint;
            }
        }

        private void ChekPrint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                NextFocus(sender, e);
                ActiveControl = PrintPage;
            }
        }

        private void PrintPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                NextFocus(sender, e);
                ActiveControl = btnSave;
            }
        }

        private void btnSave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                NextFocus(sender, e);
            }
        }

        private void TAX_SelectedIndexChanged(object sender, EventArgs e)
        {
            TaxAmount();
        }

        private void ch_tax_CheckedChanged(object sender, EventArgs e)
        {
            TaxAmount();
        }

        private void AMOUNT_Leave(object sender, EventArgs e)
        {
            TaxAmount();
        }

        private void btn_inv_pick_Click(object sender, EventArgs e)
        {
            if (Type == "Debit Note")
            {
                Accounts.DebitNoteHelp h = new Accounts.DebitNoteHelp(0, "Purchase Invoice");
                if (h.ShowDialog() == DialogResult.OK && h.c != null)
                {
                    try
                    {
                        doc_reference.Text = Convert.ToString(h.c["DOC_ID"].Value) + "/" + Convert.ToString(h.c["SUP_INV_NO"].Value);
                    }
                    catch
                    { }
                }
            }
            else
            {
                Accounts.DebitNoteHelp h = new Accounts.DebitNoteHelp(0, "Sale Invoice");
                if (h.ShowDialog() == DialogResult.OK && h.c != null)
                {
                    try
                    {
                        doc_reference.Text = Convert.ToString(h.c["DOC_ID"].Value);
                    }
                    catch
                    {

                    }
                }
            }
        }


    }
}
