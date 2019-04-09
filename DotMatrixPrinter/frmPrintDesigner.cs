

using Sys_Sols_Inventory.Class.Printer.Info;
using Sys_Sols_Inventory.Class.Printer.Print;
using Sys_Sols_Inventory.Class.Printer.SP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory
{
    public partial class frmPrintDesigner : Form
    {
        MasterSP spMaster = new MasterSP();
        #region Variables
        private Point offset;
        private bool drag = false;
        private bool isResize = false;
        private Point start_point = new Point(0, 0);
        private Size start_Size = new Size(0, 0);
        public bool isTake = false;
        public bool isLarge = false;
        public DataTable dtbl = new DataTable();
        public int inTextId = 0;
        public static bool isDoubleLineRepeat = false;
        public static bool isDoubleLineNonRepeat = false;
        public static bool isHedderRepeat = false;
        public static int inPageSizeInFirst = 0;
        public static int inMaxLineCountinFirst = 0;
        public static int inMaxLineCountinOther = 0;
        public static int inPageSizeInOther = 0;
        public static int inBlankLineForFooter = 0;
        public static string strFooterLocation = "AfterDetails";
        public static int inLineCountBetweenTwoPages = 0;
        public static int inLineCountAfterPrint = 0;
        public static string strPitch = "12";
        public static string strCondensed = "On";
        public static string strDefaultPass = "cybrotech";
        public TextBox txtCurrent = new TextBox();
        //public TextBox txtTake = new TextBox();
        public static frmPrintDesigner obj = new frmPrintDesigner();
        public int inMasterId = 0;
        #endregion
        #region Functions
        /// <summary>
        /// create an instance of frmPrintDesigner
        /// </summary>
        public frmPrintDesigner()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Function for clear all the fields
        /// </summary>
        public void Clear()
        {
            try
            {
                txtBlankLineCountAfterDetailsForFooter.Text = "";
                rbtnAfterDetailsFooter.Checked = true;
                cmbForm.SelectedItem = "";
                chkLineRepeat.Checked = false;
                chkLineNonRepeat.Checked = false;
                txtBlankLineCountBetweenTwoPages.Text = "";
                txtLineCountForFirstPage.Text = "";
                txtLineCountForOtherPage.Text = "";
                rbtnPitch12.Checked = true;
                rbtnCondensedOn.Checked = true;
                txtLineCountAfterPrint.Text = "";
            }
            catch (Exception ex)
            {
              //  formMDI.infoError.ErrorString = "PD1" + ex.Message;
            }
        }
        /// <summary>
        /// Function for create new print design 
        /// </summary>
        public void New()
        {
            try
            {
                txtCurrent = null;
                ClearDetails();
                frmPrintDesigner obj = new frmPrintDesigner();
                TextBox txt = new TextBox();
                inTextId += 1;
                txt.Name = (inTextId).ToString();
                txt.Multiline = true;
                txt.Size = new Size(5 * 8 + 1, 16 + 1);
                txt.Text = inTextId.ToString();
                txt.ContextMenuStrip = cmsTextBox;
                txt.Enter += txtCurrent_Enter;
                //txt.MouseClick += txtCurrent_MouseClick;
                txt.TextChanged += txtCurrent_TextChanged;
                txt.MouseDown += textBox1_MouseDown;
                txt.MouseUp += textBox1_MouseUp;
                txt.DragOver += textBox1_DragOver;
                txt.MouseMove += textBox1_MouseMove;
                txt.Font = new Font("Lucida Console", 10);
                pnlDesignImage.Controls.Add(txt);
                txtCurrent = txt;
                foreach (Control c in pnlDesignImage.Controls)
                {
                    c.BackColor = System.Drawing.Color.AntiqueWhite;
                }
                txtCurrent.BackColor = System.Drawing.Color.Gainsboro;
                dtbl.Rows.Add();
                int incount = dtbl.Rows.Count - 1;
                dtbl.Rows[incount]["name"] = txt.Name;
                dtbl.Rows[incount]["text"] = txt.Text;
                dtbl.Rows[incount]["row"] = 0;
                dtbl.Rows[incount]["columns"] = 0;
                dtbl.Rows[incount]["width"] = 5;
                dtbl.Rows[incount]["DBF"] = cmbFields.SelectedValue.ToString();
                dtbl.Rows[incount]["dOrH"] = cmbTextType.SelectedItem;
                dtbl.Rows[incount]["Repeat"] = rbtnDetails.Checked ? "true" : rbtnFooter.Checked ? "Footer" : "false";
                dtbl.Rows[incount]["Align"] = cmbAlign.SelectedItem;
                dtbl.Rows[incount]["RepeatAllPage"] = rbtnAllPageHedder.Checked;
                dtbl.Rows[incount]["FooterRepeatAll"] = rbtnInAllPageFooter.Checked;
                dtbl.Rows[incount]["textWrap"] = chkTextWrap.Checked;
                dtbl.Rows[incount]["wrapLineCount"] = cmbWrapLineCount.SelectedItem;
                dtbl.Rows[incount]["fieldsForExtra"] = txtFieldsForExtra.Text;
                dtbl.Rows[incount]["extraFieldName"] = txtExtraFieldName.Text;
            }
            catch (Exception ex)
            {
             //   formMDI.infoError.ErrorString = "PD2" + ex.Message;
            }
        }
        /// <summary>
        /// Function for create new printing forms
        /// </summary>
        public void NewForms()
        {
            try
            {
                frmForms frm = Application.OpenForms["frmForms"] as frmForms;
                if (frm == null)
                {
                    frmForms objfrmForms = new frmForms();
                    objfrmForms.Show();
                    // objfrmForms.MdiParent = this;
                }
                else
                    frm.Activate();
            }
            catch (Exception ex)
            {
              //  formMDI.infoError.ErrorString = "PD3" + ex.Message;
            }
        }
        /// <summary>
        /// Function for delete the print design
        /// </summary>
        public void Delete()
        {
            try
            {
                string str = MessageBox.Show("Are you sure to delete ?", "Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question).ToString();
                if (str == "Yes")
                {
                    if (txtCurrent != null)
                    {
                        DataRow[] drarr = dtbl.Select("name='" + txtCurrent.Name + "'");
                        if (drarr.Length > 0)
                        {
                            dtbl.Rows.Remove(drarr[0]);
                            pnlDesignImage.Controls.Remove(txtCurrent);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
             //   formMDI.infoError.ErrorString = "PD4" + ex.Message;
            }
        }
        /// <summary>
        /// Function for save the print design
        /// </summary>
        public void Save()
        {
            try
            {
                string str = MessageBox.Show("Are you sure to submit ?", "Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question).ToString();
                if (str == "Yes")
                {
                    if (int.Parse(txtLineCountForFirstPage.Text == "" ? "0" : txtLineCountForFirstPage.Text) <= 0)
                    {
                        MessageBox.Show("Line count for first page required", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (int.Parse(txtLineCountForOtherPage.Text == "" ? "0" : txtLineCountForOtherPage.Text) <= 0)
                    {
                        MessageBox.Show("Line count for other page required", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                      //  MasterSPrint spMaster = new MasterSPrint();
                        MasterInfo infoMaster = new MasterInfo();
                        DetailsSP spDetails = new DetailsSP();
                        DetailsInfo infoDetails = new DetailsInfo();
                        infoMaster.BlankLneForFooter = int.Parse(txtBlankLineCountAfterDetailsForFooter.Text == "" ? "0" : txtBlankLineCountAfterDetailsForFooter.Text);
                        infoMaster.FooterLocation = rbtnAfterDetailsFooter.Checked ? "AfterDetails" : "PageEnd";
                        infoMaster.FormName = int.Parse(cmbForm.SelectedValue.ToString());
                        infoMaster.IsTwoLineForDetails = chkLineRepeat.Checked;
                        infoMaster.IsTwoLineForHedder = chkLineNonRepeat.Checked;
                        infoMaster.LineCountBetweenTwo = int.Parse(txtBlankLineCountBetweenTwoPages.Text == "" ? "0" : txtBlankLineCountBetweenTwoPages.Text);
                        infoMaster.PageSize1 = int.Parse(txtLineCountForFirstPage.Text == "" ? "0" : txtLineCountForFirstPage.Text);
                        infoMaster.PageSizeOther = int.Parse(txtLineCountForOtherPage.Text == "" ? "0" : txtLineCountForOtherPage.Text);
                        infoMaster.Condensed = rbtnCondensedOn.Checked ? "On" : "Off";
                        infoMaster.Pitch = rbtnPitch10.Checked ? "10" : "12";
                        infoMaster.LineCountAfterPrint = int.Parse(txtLineCountAfterPrint.Text == "" ? "0" : txtLineCountAfterPrint.Text);
                        if (inMasterId == 0)
                        {
                            inMasterId = spMaster.MasterAdd(infoMaster);
                        }
                        else
                        {
                            infoMaster.MasterId = inMasterId;
                            spMaster.MasterEdit(infoMaster);
                            spDetails.DetailsDelete(inMasterId);
                        }
                        foreach (DataRow dr in dtbl.Rows)
                        {
                            infoDetails.Name = dr["name"].ToString();
                            infoDetails.Text = dr["text"].ToString();
                            infoDetails.Row = int.Parse(dr["row"].ToString());
                            infoDetails.Columns = int.Parse(dr["columns"].ToString());
                            infoDetails.Width = int.Parse(dr["width"].ToString());
                            infoDetails.DBF = dr["DBF"].ToString();
                            infoDetails.DorH = dr["dOrH"].ToString();
                            infoDetails.Repeat = dr["Repeat"].ToString();
                            infoDetails.Align = dr["Align"].ToString();
                            infoDetails.RepeatAll = dr["RepeatAllPage"].ToString();
                            infoDetails.FooterRepeatAll = dr["FooterRepeatAll"].ToString();
                            infoDetails.TextWrap = dr["textWrap"].ToString();
                            infoDetails.WrapLineCount = int.Parse(dr["wrapLineCount"].ToString());
                            infoDetails.FieldsForExtra = dr["fieldsForExtra"].ToString();
                            infoDetails.ExtraFieldName = dr["extraFieldName"].ToString();
                            infoDetails.MasterId = inMasterId;
                            spDetails.DetailsAdd(infoDetails);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
              //  formMDI.infoError.ErrorString = "PD5" + ex.Message;
            }
        }
        /// <summary>
        /// Function for save the default print design
        /// </summary>
        public void SaveDefault()
        {
            try
            {
                if (inMasterId != 0)
                {
                    string str = MessageBox.Show("Default save in process, are you sure to continue ?", "Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question).ToString();
                    if (str == "Yes")
                    {
                        if (int.Parse(txtLineCountForFirstPage.Text == "" ? "0" : txtLineCountForFirstPage.Text) <= 0)
                        {
                            MessageBox.Show("Line count for first page required", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (int.Parse(txtLineCountForOtherPage.Text == "" ? "0" : txtLineCountForOtherPage.Text) <= 0)
                        {
                            MessageBox.Show("Line count for other page required", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {

                         //   MasterSPrint spMaster = new MasterSPrint();
                            MasterInfo infoMaster = new MasterInfo();
                            DetailsSP spDetails = new DetailsSP();
                            DetailsInfo infoDetails = new DetailsInfo();
                            infoMaster.BlankLneForFooter = int.Parse(txtBlankLineCountAfterDetailsForFooter.Text == "" ? "0" : txtBlankLineCountAfterDetailsForFooter.Text);
                            infoMaster.FooterLocation = rbtnAfterDetailsFooter.Checked ? "AfterDetails" : "PageEnd";
                            infoMaster.FormName = int.Parse(cmbForm.SelectedValue.ToString());
                            infoMaster.IsTwoLineForDetails = chkLineRepeat.Checked;
                            infoMaster.IsTwoLineForHedder = chkLineNonRepeat.Checked;
                            infoMaster.LineCountBetweenTwo = int.Parse(txtBlankLineCountBetweenTwoPages.Text == "" ? "0" : txtBlankLineCountBetweenTwoPages.Text);
                            infoMaster.PageSize1 = int.Parse(txtLineCountForFirstPage.Text == "" ? "0" : txtLineCountForFirstPage.Text);
                            infoMaster.PageSizeOther = int.Parse(txtLineCountForOtherPage.Text == "" ? "0" : txtLineCountForOtherPage.Text);
                            infoMaster.Condensed = rbtnCondensedOn.Checked ? "On" : "Off";
                            infoMaster.Pitch = rbtnPitch10.Checked ? "10" : "12";
                            infoMaster.LineCountAfterPrint = int.Parse(txtLineCountAfterPrint.Text == "" ? "0" : txtLineCountAfterPrint.Text);

                            //{
                            //    inMasterId = spMaster.MasterCopyAdd(infoMaster);
                            //}
                            //else
                            //{
                            infoMaster.MasterId = inMasterId;
                            if (spMaster.MasterCopyExistCheck(inMasterId))
                            {
                                spMaster.MasterCopyEdit(infoMaster);
                                spDetails.DetailsCopyDelete(inMasterId);
                            }
                            else
                            {
                                spMaster.MasterCopyAdd(infoMaster);
                            }
                            //}
                            foreach (DataRow dr in dtbl.Rows)
                            {
                                infoDetails.Name = dr["name"].ToString();
                                infoDetails.Text = dr["text"].ToString();
                                infoDetails.Row = int.Parse(dr["row"].ToString());
                                infoDetails.Columns = int.Parse(dr["columns"].ToString());
                                infoDetails.Width = int.Parse(dr["width"].ToString());
                                infoDetails.DBF = dr["DBF"].ToString();
                                infoDetails.DorH = dr["dOrH"].ToString();
                                infoDetails.Repeat = dr["Repeat"].ToString();
                                infoDetails.Align = dr["Align"].ToString();
                                infoDetails.RepeatAll = dr["RepeatAllPage"].ToString();
                                infoDetails.FooterRepeatAll = dr["FooterRepeatAll"].ToString();
                                infoDetails.TextWrap = dr["textWrap"].ToString();
                                infoDetails.WrapLineCount = int.Parse(dr["wrapLineCount"].ToString());
                                infoDetails.FieldsForExtra = dr["fieldsForExtra"].ToString();
                                infoDetails.ExtraFieldName = dr["extraFieldName"].ToString();
                                infoDetails.MasterId = inMasterId;
                                spDetails.DetailsCopyAdd(infoDetails);
                            }
                        }
                    }
                    txtDefaultPass.Text = "";
                }
                else
                {
                    MessageBox.Show("Default not accessible in save.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                //formMDI.infoError.ErrorString = "PD6" + ex.Message;
            }
        }
        /// <summary>
        /// Function to delete all the details
        /// </summary>
        public void ClearAll()
        {
            try
            {
                string str = MessageBox.Show("Are you sure to delete all ?", "Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question).ToString();
                if (str == "Yes")
                {
                    txtDefaultPass.Text = "";
                    Clear();
                    ClearDetails();
                    pnlDesignImage.Controls.Clear();
                    dtbl.Rows.Clear();
                    txtCurrent = null;
                    inTextId = 0;

                    MasterSP spMaster = new MasterSP();
                 //   MasterSPrint spMaster = new MasterSPrint();
                    DetailsSP spDetails = new DetailsSP();
                    spDetails.DetailsDelete(inMasterId);
                    spMaster.MasterDelate(inMasterId);
                }
            }
            catch (Exception ex)
            {
           //     formMDI.infoError.ErrorString = "PD7" + ex.Message;
            }
        }
        /// <summary>
        /// Function for clear all the details
        /// </summary>
        public void ClearDetails()
        {
            try
            {
                txtRow.Text = "";
                txtWidth.Text = "";
                txtColumn.Text = "";
                rbtnHedder.Checked = true;
                cmbFields.SelectedIndex = 0;
                txtFieldsForExtra.Text = "";
                txtExtraFieldName.Text = "";
                cmbTextType.SelectedIndex = 0;
                cmbAlign.SelectedIndex = 0;
                rbtnOnlyInLastPageFooter.Checked = true;
                rbtnOnlyInFirstPAgeHedder.Checked = true;
                rbtnHedder.Checked = true;
                chkTextWrap.Checked = false;
                cmbWrapLineCount.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
              //  formMDI.infoError.ErrorString = "PD8" + ex.Message;
            }
        }
        /// <summary>
        /// Function for fill the ComboBox cmbForm 
        /// </summary>
        public void FormFill()
        {
            try
            {
                MasterSP spMaster = new MasterSP();
             //   MasterSPrint spMaster = new MasterSPrint();
                cmbForm.DisplayMember = "formName";
                cmbForm.ValueMember = "formId";
                cmbForm.DataSource = spMaster.FormViewAll();
            }
            catch (Exception ex)
            {
               // formMDI.infoError.ErrorString = "PD9" + ex.Message;
            }
        }
        /// <summary>
        /// Function for assigning to the DataTable
        /// </summary>
        public void AssignToDtbl()
        {
            try
            {
                if (txtCurrent != null && txtCurrent.Name != "")
                {
                    DataRow[] dr = dtbl.Select("name='" + txtCurrent.Name + "'");
                    if (dr.Length > 0)
                    {
                        dr[0]["name"] = txtCurrent.Name;
                        dr[0]["text"] = txtCurrent.Text;
                        dr[0]["row"] = int.Parse(txtRow.Text == "" ? "0" : txtRow.Text);
                        dr[0]["columns"] = int.Parse(txtColumn.Text == "" ? "0" : txtColumn.Text);
                        dr[0]["width"] = int.Parse(txtWidth.Text == "" ? "5" : txtWidth.Text);
                        dr[0]["DBF"] = cmbFields.SelectedValue.ToString();
                        dr[0]["fieldsForExtra"] = txtFieldsForExtra.Text;
                        dr[0]["extraFieldName"] = txtExtraFieldName.Text;
                        dr[0]["dOrH"] = cmbTextType.SelectedItem;
                        dr[0]["Repeat"] = rbtnDetails.Checked ? "true" : rbtnFooter.Checked ? "Footer" : "false";
                        dr[0]["Align"] = cmbAlign.SelectedItem;
                        dr[0]["RepeatAllPage"] = rbtnAllPageHedder.Checked;
                        dr[0]["FooterRepeatAll"] = rbtnInAllPageFooter.Checked;
                        dr[0]["textWrap"] = chkTextWrap.Checked;
                        dr[0]["wrapLineCount"] = cmbWrapLineCount.SelectedItem.ToString();
                    }
                }
            }
            catch(Exception ex)
                {
                  //  formMDI.infoError.ErrorString = "PD10" + ex.Message;
                }
        }
        #endregion
        #region Events
        /// <summary>
        /// On Button btnNew Click 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                New();
            }
            catch (Exception ex)
            {
                //formMDI.infoError.ErrorString = "PD11" + ex.Message;
            }
        }
        /// <summary>
        /// On TextBx txtCurrent TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCurrent_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataRow[] dr = dtbl.Select("name='" + txtCurrent.Name + "'");
                txtRow.Text = dr[0]["row"].ToString();
                txtColumn.Text = dr[0]["columns"].ToString();
                txtWidth.Text = dr[0]["width"].ToString();
                AssignToDtbl();
            }
            catch (Exception ex)
            {
                //formMDI.infoError.ErrorString = "PD12" + ex.Message;
            }
        }
        /// <summary>
        /// On TextBx txtLocationX TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLocationX_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCurrent != null)
                {
                    txtCurrent.Location = new Point((int.Parse(txtColumn.Text == "" ? "0" : txtColumn.Text) * 8), (int.Parse(txtRow.Text == "" ? "0" : txtRow.Text) * 16));
                    txtCurrent.Size = new Size((int.Parse(txtWidth.Text == "" ? "5" : txtWidth.Text) * 8) + 1, txtCurrent.Height);
                    AssignToDtbl();
                }
            }
            catch (Exception ex)
            {
               // formMDI.infoError.ErrorString = "PD13" + ex.Message;
            }
        }
        /// <summary>
        /// On TextBx txtWidth TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWidth_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCurrent != null)
                {
                    txtCurrent.Location = new Point((int.Parse(txtColumn.Text == "" ? "0" : txtColumn.Text) * 8), (int.Parse(txtRow.Text == "" ? "0" : txtRow.Text) * 16));
                    txtCurrent.Size = new Size((int.Parse(txtWidth.Text == "" ? "5" : txtWidth.Text) * 8) + 1, txtCurrent.Height);
                    AssignToDtbl();
                }
            }
            catch (Exception ex)
            {
              //  formMDI.infoError.ErrorString = "PD14" + ex.Message;
            }
        }
        /// <summary>
        /// On Button btnPrint Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtbl1 = new DataTable();
                dtbl1.Columns.Add("slno");
                dtbl1.Columns.Add("name");
                dtbl1.Columns.Add("rate", typeof(decimal));
                dtbl1.Columns.Add("qty", typeof(int));
                dtbl1.Columns.Add("total", typeof(decimal));
                for (int iii = 1; iii <= 20; iii++)
                {
                    dtbl1.Rows.Add();
                    dtbl1.Rows[iii - 1][0] = iii;
                    dtbl1.Rows[iii - 1][1] = "Name " + iii.ToString();
                    dtbl1.Rows[iii - 1][2] = Math.Sqrt(iii) + iii * 2;
                    dtbl1.Rows[iii - 1][3] = iii + 1;
                    dtbl1.Rows[iii - 1][4] = decimal.Parse(dtbl1.Rows[iii - 1][2].ToString()) * decimal.Parse(dtbl1.Rows[iii - 1][3].ToString());
                }
                DataTable dtblHedder = new DataTable();
                DataTable dtblFooter = new DataTable();
                dtblHedder.Columns.Add("Company");
                dtblHedder.Columns.Add("Date");
                dtblHedder.Columns.Add("Address");
                dtblHedder.Columns.Add("Phone");
                dtblHedder.Columns.Add("name");
                dtblHedder.Rows.Add();
                dtblHedder.Rows[0]["Company"] = "Sys-Sols";
                dtblHedder.Rows[0]["Date"] = "20-July-2015";
                dtblHedder.Rows[0]["Address"] = "Neospace, Calicut Kinfra, Kakancheri, 673634 ";
                dtblHedder.Rows[0]["Phone"] = "0483-2711843";
                dtblHedder.Rows[0]["name"] = "91-9747-80-00-80";
                dtblFooter.Columns.Add("totalAmount");
                dtblFooter.Columns.Add("name");

                dtblFooter.Rows.Add();
                dtblFooter.Rows[0]["TotalAmount"] = Math.Round(decimal.Parse(dtbl1.Compute("Sum(total)", "").ToString()), 2);
                //dtblFooter.Rows[0]["TotalAmount"] = "102564415654.566";
                dtblFooter.Rows[0]["name"] = "91-9747-80-00-80";




             //DotMatrixPrint.PrintDesign(4, dtblHedder, dtbl1, dtblFooter);

                DotMatrixPrint.PrintDesign(int.Parse(cmbForm.SelectedValue.ToString()), dtblHedder, dtbl1, dtblFooter);
            }
            catch (Exception ex)
            {
           //     formMDI.infoError.ErrorString = "PD15" + ex.Message;
            }
        }
        /// <summary>
        /// On Form frmPrintDesigner Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPrintDesigner_Load(object sender, EventArgs e)
        {
            try
            {
                dtbl.Columns.Add("name");
                dtbl.Columns.Add("text");
                dtbl.Columns.Add("row", typeof(decimal));
                dtbl.Columns.Add("columns", typeof(decimal));
                dtbl.Columns.Add("width", typeof(decimal));
                dtbl.Columns.Add("DBF");
                dtbl.Columns.Add("dOrH");
                dtbl.Columns.Add("Repeat");
                dtbl.Columns.Add("Align");
                dtbl.Columns.Add("RepeatAllPage");
                dtbl.Columns.Add("FooterRepeatAll");
                dtbl.Columns.Add("textWrap");
                dtbl.Columns.Add("wrapLineCount");
                dtbl.Columns.Add("fieldsForExtra");
                dtbl.Columns.Add("extraFieldName");
                cmbWrapLineCount.SelectedIndex = 0;
                cmbTextType.SelectedIndex = 0;
                //cmbFields.SelectedIndex = 0;
                cmbAlign.SelectedIndex = 0;
                FormFill();
                obj = this;
            }
            catch (Exception ex)
            {
                //formMDI.infoError.ErrorString = "PD16" + ex.Message;
            }
        }
        /// <summary>
        /// On ComboBox cmbDataBase SelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDataBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                AssignToDtbl();
                if (txtCurrent != null && txtCurrent.Name != "")
                    if (cmbFields.SelectedIndex == 0)
                        txtCurrent.ReadOnly = false;
                    else
                    {
                        txtCurrent.ReadOnly = true;
                        txtCurrent.Text = cmbFields.SelectedValue.ToString();
                    }
            }
            catch (Exception ex)
            {
                //formMDI.infoError.ErrorString = "PD17" + ex.Message;
            }
        }
        /// <summary>
        /// On ComboBox cmbHedding_SelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbHedding_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                AssignToDtbl();
                if (txtCurrent != null)
                {
                    if (cmbTextType.SelectedIndex == 0)
                    {
                        txtCurrent.Font = new Font("Lucida Console", 10, System.Drawing.FontStyle.Regular);
                    }
                    else if (cmbTextType.SelectedIndex == 1)
                    {
                        txtCurrent.Font = new Font("Lucida Console", 11, System.Drawing.FontStyle.Bold);
                    }
                    else if (cmbTextType.SelectedIndex == 2)
                    {
                        txtCurrent.Font = new Font("Lucida Console", 10, System.Drawing.FontStyle.Bold);
                    }
                    else if (cmbTextType.SelectedIndex == 3)
                    {
                        txtCurrent.Font = new Font("Lucida Console", 10, System.Drawing.FontStyle.Italic);
                    }
                }
            }
            catch (Exception ex)
            {
             //   formMDI.infoError.ErrorString = "PD18" + ex.Message;
            }
        }
        /// <summary>
        /// On Button btnDelete Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Delete();
            }
            catch (Exception ex)
            {
                //formMDI.infoError.ErrorString = "PD19" + ex.Message;
            }
        }
        /// <summary>
        /// On RadioButton rbtnNotRepeat CheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnNotRepeat_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                AssignToDtbl();
                if (rbtnHedder.Checked)
                {
                    gbxHedder.Enabled = true;
                    gbxFooter.Enabled = false;
                }
                else if (rbtnDetails.Checked)
                {
                    gbxHedder.Enabled = false;
                    gbxFooter.Enabled = false;
                }
                else if (rbtnFooter.Checked)
                {
                    gbxHedder.Enabled = false;
                    gbxFooter.Enabled = true;
                }
            }
            catch (Exception ex)
            {
              //  formMDI.infoError.ErrorString = "PD20" + ex.Message;
            }
        }
        /// <summary>
        /// On RadioButton rbtnRepeat CheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnRepeat_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                AssignToDtbl();
                if (rbtnHedder.Checked)
                {
                    gbxHedder.Enabled = true;
                    gbxFooter.Enabled = false;
                }
                else if (rbtnDetails.Checked)
                {
                    gbxHedder.Enabled = false;
                    gbxFooter.Enabled = false;
                }
                else if (rbtnFooter.Checked)
                {
                    gbxHedder.Enabled = false;
                    gbxFooter.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                //formMDI.infoError.ErrorString = "PD21" + ex.Message;
            }
        }
        /// <summary>
        /// On ComboBox cmbAlign SelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbAlign_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                AssignToDtbl();
                if (txtCurrent != null)
                {
                    if (cmbAlign.SelectedIndex == 0)
                    {
                        txtCurrent.TextAlign = HorizontalAlignment.Left;
                    }
                    else if (cmbAlign.SelectedIndex == 1)
                    {
                        txtCurrent.TextAlign = HorizontalAlignment.Center;
                    }
                    else
                    {
                        txtCurrent.TextAlign = HorizontalAlignment.Right;
                    }
                }
            }
            catch (Exception ex)
            {
                //formMDI.infoError.ErrorString = "PD22" + ex.Message;
            }
        }
        /// <summary>
        /// On CheckBox chkLine CheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkLine_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkLineNonRepeat.Checked)
                    frmPrintDesigner.isDoubleLineNonRepeat = true;
                else
                    frmPrintDesigner.isDoubleLineNonRepeat = false;
            }
            catch (Exception ex)
            {
               // formMDI.infoError.ErrorString = "PD23" + ex.Message;
            }
        }
        /// <summary>
        /// On TextBox txtCurrent Enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCurrent_Enter(object sender, EventArgs e)
        {
            try
            {
                txtCurrent = sender as TextBox;
                DataRow[] dr = dtbl.Select("name='" + txtCurrent.Name + "'");
                txtCurrent = null;
                foreach (Control c in pnlDesignImage.Controls)
                {
                    c.BackColor = System.Drawing.Color.AntiqueWhite;

                }
                txtRow.Text = dr[0]["row"].ToString();
                txtColumn.Text = dr[0]["columns"].ToString();
                txtWidth.Text = dr[0]["width"].ToString();
                if (dr[0]["Repeat"].ToString() == "false")
                    rbtnHedder.Checked = true;
                else if (dr[0]["Repeat"].ToString() == "true")
                    rbtnDetails.Checked = true;
                else
                    rbtnFooter.Checked = true;
                cmbAlign.SelectedItem = dr[0]["Align"].ToString();
                cmbFields.SelectedValue = dr[0]["DBF"].ToString();
                cmbTextType.SelectedItem = dr[0]["dOrH"].ToString();
                rbtnAllPageHedder.Checked = bool.Parse(dr[0]["RepeatAllPage"].ToString());
                rbtnOnlyInFirstPAgeHedder.Checked = !bool.Parse(dr[0]["RepeatAllPage"].ToString());
                rbtnInAllPageFooter.Checked = bool.Parse(dr[0]["FooterRepeatAll"].ToString());
                rbtnOnlyInLastPageFooter.Checked = !bool.Parse(dr[0]["FooterRepeatAll"].ToString());
                chkTextWrap.Checked = bool.Parse(dr[0]["textWrap"].ToString());
                cmbWrapLineCount.SelectedItem = dr[0]["wrapLineCount"].ToString();
                txtExtraFieldName.Text = dr[0]["extraFieldName"].ToString();
                if (txtExtraFieldName.Text.Trim() != "")
                {
                    txtFieldsForExtra.Text = dr[0]["fieldsForExtra"].ToString();
                    chkExtraField.Checked = true;
                }
                else
                {
                    txtFieldsForExtra.Text = "";
                    chkExtraField.Checked = false;
                }
                txtCurrent = sender as TextBox;
                txtCurrent.BackColor = System.Drawing.Color.Gainsboro;
            }
            catch (Exception ex)
            {
                //formMDI.infoError.ErrorString = "PD24" + ex.Message;
            }
        }
        /// <summary>
        /// On CheckBox chkLineRepeat CheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkLineRepeat_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkLineRepeat.Checked)
                {
                    frmPrintDesigner.isDoubleLineRepeat = true;
                }
                else
                {
                    frmPrintDesigner.isDoubleLineRepeat = false;
                }
            }
            catch (Exception ex)
            {
                //formMDI.infoError.ErrorString = "PD25" + ex.Message;
            }
        }
        /// <summary>
        /// On RadioButton rbtnHedderFirst CheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnHedderFirst_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                AssignToDtbl();
                if (rbtnOnlyInFirstPAgeHedder.Checked)
                    isHedderRepeat = false;
                else
                    isHedderRepeat = true;
            }
            catch (Exception ex)
            {
                //formMDI.infoError.ErrorString = "PD26" + ex.Message;
            }
        }
        /// <summary>
        /// On TextBox txtPageSize TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPageSize_TextChanged(object sender, EventArgs e)
        {
            try
            {
                inPageSizeInFirst = int.Parse(txtLineCountForFirstPage.Text == "" ? "0" : txtLineCountForFirstPage.Text);
            }
            catch (Exception ex)
            {
                //formMDI.infoError.ErrorString = "PD27" + ex.Message;
            }
        }
        /// <summary>
        /// On TextBox txtMaximumLineCount TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMaximumLineCount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                inMaxLineCountinFirst = int.Parse(txtMaximumLineCountInFirst.Text);
            }
            catch (Exception ex)
            {
              //  formMDI.infoError.ErrorString = "PD28" + ex.Message;
            }
        }
        /// <summary>
        /// On TextBox txtPageSizeinOther TextChanged 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPageSizeinOther_TextChanged(object sender, EventArgs e)
        {
            try
            {
                inPageSizeInOther = int.Parse(txtLineCountForOtherPage.Text == "" ? "0" : txtLineCountForOtherPage.Text);
            }
            catch (Exception ex)
            {
                //formMDI.infoError.ErrorString = "PD29" + ex.Message;
            }
        }
        /// <summary>
        /// On TextBox txtMaximumLineCountInOther TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMaximumLineCountInOther_TextChanged(object sender, EventArgs e)
        {
            try
            {
                inMaxLineCountinOther = int.Parse(txtMaximumLineCountInOther.Text);
            }
            catch (Exception ex)
            {
                //formMDI.infoError.ErrorString = "PD30" + ex.Message;
            }
        }
        /// <summary>
        /// On TextBox txtLinecountBetweenTwoPages TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLinecountBetweenTwoPages_TextChanged(object sender, EventArgs e)
        {
            try
            {
                inLineCountBetweenTwoPages = int.Parse(txtBlankLineCountBetweenTwoPages.Text == "" ? "0" : txtBlankLineCountBetweenTwoPages.Text);
            }
            catch (Exception ex)
            {
                //formMDI.infoError.ErrorString = "PD31" + ex.Message;
            }
        }
        /// <summary>
        /// On RadioButton rbtnAfterDetails CheckedChanged 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnAfterDetails_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnAfterDetailsFooter.Checked)
                {
                    strFooterLocation = "AfterDetails";
                    txtBlankLineCountAfterDetailsForFooter.Enabled = true;
                }
                else
                {
                    strFooterLocation = "PageEnd";
                    txtBlankLineCountAfterDetailsForFooter.Enabled = false;
                }
            }
            catch (Exception ex)
            {
             //   formMDI.infoError.ErrorString = "PD32" + ex.Message;
            }
        }
        /// <summary>
        /// On TextBox txtBlankLine TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBlankLine_TextChanged(object sender, EventArgs e)
        {
            try
            {
                inBlankLineForFooter = int.Parse(txtBlankLineCountAfterDetailsForFooter.Text == "" ? "0" : txtBlankLineCountAfterDetailsForFooter.Text);
            }
            catch (Exception ex)
            {
                //formMDI.infoError.ErrorString = "PD33" + ex.Message;
            }
        }
        /// <summary>
        /// On RadioButton rbtnFAllPage CheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnFAllPage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                AssignToDtbl();
            }
            catch (Exception ex)
            {
                //formMDI.infoError.ErrorString = "PD34" + ex.Message;
            }
        }
        /// <summary>
        /// On TextBox txtBlankLine KeyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBlankLine_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8)
                    e.Handled = false;
                else
                    e.Handled = true;
            }
            catch (Exception ex)
            {
              //  formMDI.infoError.ErrorString = "PD35" + ex.Message;
            }
        }
        /// <summary>
        /// On Button btnSave Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
            }
            catch (Exception ex)
            {
              //  formMDI.infoError.ErrorString = "PD36" + ex.Message;
            }
        }
        /// <summary>
        /// On ComboBox cmbForm_SelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pnlDesignImage.Controls.Clear();
                dtbl.Rows.Clear();
                txtCurrent = null;
                inTextId = 0;
                MasterSP spMaster = new MasterSP();
             //   MasterSPrint spMaster = new MasterSPrint();
                MasterInfo infoMaster = new MasterInfo();
                cmbFields.ValueMember = "fieldName";
                cmbFields.DisplayMember = "fieldName";
                DataTable dtblField = spMaster.FieldsViewAll(int.Parse(cmbForm.SelectedValue.ToString()));
                DataRow drnull = dtblField.NewRow();
                drnull["fieldName"] = "";
                dtblField.Rows.InsertAt(drnull, 0);
                cmbFields.DataSource = dtblField;
                infoMaster = spMaster.MasterViewByFormName(int.Parse(cmbForm.SelectedValue.ToString()));
                if (infoMaster.MasterId != 0)
                {
                    inMasterId = infoMaster.MasterId;
                    txtBlankLineCountAfterDetailsForFooter.Text = infoMaster.BlankLneForFooter.ToString();
                    if (infoMaster.FooterLocation == "AfterDetails")
                    {
                        rbtnAfterDetailsFooter.Checked = true;
                    }
                    else
                    {
                        rbtnPageEndFooter.Checked = true;
                    }
                    cmbForm.SelectedItem = infoMaster.FormName;
                    chkLineRepeat.Checked = infoMaster.IsTwoLineForDetails;
                    chkLineNonRepeat.Checked = infoMaster.IsTwoLineForHedder;
                    txtBlankLineCountBetweenTwoPages.Text = infoMaster.LineCountBetweenTwo.ToString();
                    txtLineCountForFirstPage.Text = infoMaster.PageSize1.ToString();
                    txtLineCountForOtherPage.Text = infoMaster.PageSizeOther.ToString();
                    rbtnPitch10.Checked = infoMaster.Pitch == "10" ? true : false;
                    rbtnPitch12.Checked = infoMaster.Pitch == "10" ? false : true;
                    rbtnCondensedOn.Checked = infoMaster.Condensed == "On" ? true : false;
                    rbtnCondensedOff.Checked = infoMaster.Condensed == "On" ? false : true;
                    txtLineCountAfterPrint.Text = infoMaster.LineCountAfterPrint.ToString();
                    DetailsSP spDetails = new DetailsSP();
                    DataTable dtblDetails = new DataTable();
                    dtblDetails = spDetails.DetailsViewAll(inMasterId);
                    dtbl = dtblDetails.Copy();
                    TextBox txtTemp = new TextBox();
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        txtCurrent = null;
                        ClearDetails();
                        frmPrintDesigner obj = new frmPrintDesigner();
                        TextBox txt = new TextBox();
                        if (inTextId < int.Parse(dr["name"].ToString()))
                            inTextId = int.Parse(dr["name"].ToString());
                        txt.Name = dr["name"].ToString();
                        txt.Multiline = true;
                        txt.Size = new Size(int.Parse(dr["width"].ToString()) * 8 + 1, 16 + 1);
                        txt.Text = dr["text"].ToString();
                        txt.Enter += txtCurrent_Enter;
                        //txt.MouseClick += txtCurrent_MouseClick;
                        txt.ContextMenuStrip = cmsTextBox;
                        txt.TextChanged += txtCurrent_TextChanged;
                        txt.MouseDown += textBox1_MouseDown;
                        txt.MouseUp += textBox1_MouseUp;
                        txt.DragOver += textBox1_DragOver;
                        txt.MouseMove += textBox1_MouseMove;
                        txt.Font = new Font("Lucida Console", 10);
                        pnlDesignImage.Controls.Add(txt);
                        txtTemp = txt;
                        foreach (Control c in pnlDesignImage.Controls)
                            c.BackColor = System.Drawing.Color.AntiqueWhite;
                        txtTemp.BackColor = System.Drawing.Color.Gainsboro;

                        txtRow.Text = dr["row"].ToString();
                        txtColumn.Text = dr["columns"].ToString();
                        txtWidth.Text = dr["width"].ToString();
                        if (dr["Repeat"].ToString() == "false")
                            rbtnHedder.Checked = true;
                        else if (dr["Repeat"].ToString() == "true")
                            rbtnDetails.Checked = true;
                        else
                            rbtnFooter.Checked = true;
                        cmbAlign.SelectedItem = dr["Align"].ToString();
                        cmbFields.SelectedValue = dr["DBF"].ToString();
                        cmbTextType.SelectedItem = dr["dOrH"].ToString();
                        rbtnAllPageHedder.Checked = bool.Parse(dr["RepeatAllPage"].ToString());
                        rbtnOnlyInFirstPAgeHedder.Checked = !bool.Parse(dr["RepeatAllPage"].ToString());
                        rbtnInAllPageFooter.Checked = bool.Parse(dr["FooterRepeatAll"].ToString());
                        rbtnOnlyInLastPageFooter.Checked = !bool.Parse(dr["FooterRepeatAll"].ToString());
                        chkTextWrap.Checked = bool.Parse(dr["textWrap"].ToString());
                        cmbWrapLineCount.SelectedItem = dr["wrapLineCount"].ToString();
                        txtExtraFieldName.Text = dr["extraFieldName"].ToString();
                        if (txtExtraFieldName.Text.Trim() != "")
                        {
                            txtFieldsForExtra.Text = dr["fieldsForExtra"].ToString();
                            chkExtraField.Checked = true;
                        }
                        else
                        {
                            txtFieldsForExtra.Text = "";
                            chkExtraField.Checked = false;
                        }
                        if (txtTemp != null)
                            if (cmbTextType.SelectedIndex == 0)
                                txtTemp.Font = new Font("Lucida Console", 10, System.Drawing.FontStyle.Regular);
                            else if (cmbTextType.SelectedIndex == 1)
                                txtTemp.Font = new Font("Lucida Console", 11, System.Drawing.FontStyle.Bold);
                            else if (cmbTextType.SelectedIndex == 2)
                                txtTemp.Font = new Font("Lucida Console", 10, System.Drawing.FontStyle.Bold);
                            else if (cmbTextType.SelectedIndex == 3)
                                txtTemp.Font = new Font("Lucida Console", 10, System.Drawing.FontStyle.Italic);

                        if (txtTemp != null && txtTemp.Name != "")
                            if (cmbFields.SelectedIndex == 0)
                                txtTemp.ReadOnly = false;
                            else
                            {
                                txtTemp.ReadOnly = true;
                                txtTemp.Text = cmbFields.SelectedValue.ToString();
                            }
                        if (txtTemp != null)
                            if (cmbAlign.SelectedIndex == 0)
                                txtTemp.TextAlign = HorizontalAlignment.Left;
                            else if (cmbAlign.SelectedIndex == 1)
                                txtTemp.TextAlign = HorizontalAlignment.Center;
                            else
                                txtTemp.TextAlign = HorizontalAlignment.Right;
                        if (txtTemp != null)
                        {
                            txtTemp.Location = new Point((int.Parse(txtColumn.Text == "" ? "0" : txtColumn.Text) * 8), (int.Parse(txtRow.Text == "" ? "0" : txtRow.Text) * 16));
                            txtTemp.Size = new Size((int.Parse(txtWidth.Text == "" ? "5" : txtWidth.Text) * 8) + 1, txtTemp.Height);
                        }
                    }
                    txtCurrent = txtTemp;
                }
                else
                {
                    inMasterId = 0;
                    Clear();
                    ClearDetails();
                }
            }
            catch (Exception ex)
            {
                //formMDI.infoError.ErrorString = "PD37" + ex.Message;
            }
        }
        /// <summary>
        /// On RadioButton rbtnPitch12 CheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnPitch12_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnPitch10.Checked)
                    strPitch = "10";
                else if (rbtnPitch12.Checked)
                    strPitch = "12";
            }
            catch (Exception ex)
            {
              //  formMDI.infoError.ErrorString = "PD38" + ex.Message;
            }
        }
        /// <summary>
        /// On RadioButton rbtnCondensedOn CheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnCondensedOn_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnCondensedOn.Checked)
                    strCondensed = "On";
                else if (rbtnCondensedOff.Checked)
                    strCondensed = "Off";
            }
            catch (Exception ex)
            {
                //formMDI.infoError.ErrorString = "PD39" + ex.Message;
            }
        }
        /// <summary>
        ///  On Button btnClear Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
            }
            catch (Exception ex)
            {
             //   formMDI.infoError.ErrorString = "PD40" + ex.Message;
            }
        }
        /// <summary>
        /// On CheckBox chkTextWrap CheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkTextWrap_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                AssignToDtbl();
                if (chkTextWrap.Checked)
                    cmbWrapLineCount.Enabled = true;
                else
                    cmbWrapLineCount.Enabled = false;
            }
            catch (Exception ex)
            {
                //formMDI.infoError.ErrorString = "PD41" + ex.Message;
            }
        }
        /// <summary>
        /// On ComboBox cmbWrapLineCount SelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbWrapLineCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                AssignToDtbl();
            }
            catch (Exception ex)
            {
             //   formMDI.infoError.ErrorString = "PD42" + ex.Message;
            }
        }
        /// <summary>
        ///  On Button btnAddForms Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddForms_Click(object sender, EventArgs e)
        {
            try
            {
                NewForms();
            }
            catch (Exception ex)
            {
                //formMDI.infoError.ErrorString = "PD43" + ex.Message;
            }
        }
        /// <summary>
        /// On TextBox txtExtraFieldName TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtExtraFieldName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                AssignToDtbl();
            }
            catch (Exception ex)
            {
              //  formMDI.infoError.ErrorString = "PD44" + ex.Message;
            }
        }
        /// <summary>
        /// On ComboBox cmbFieldsForExtra SelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbFieldsForExtra_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                AssignToDtbl();
            }
            catch (Exception ex)
            {
//formMDI.infoError.ErrorString = "PD45" + ex.Message;
            }
        }
        /// <summary>
        /// On CheckBox chkExtraField CheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkExtraField_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtExtraFieldName.Enabled = chkExtraField.Checked;
                txtFieldsForExtra.Enabled = chkExtraField.Checked;
                cmbFields.Enabled = !chkExtraField.Checked;
                if (!cmbFields.Enabled)
                    cmbFields.SelectedValue = "";
            }
            catch (Exception ex)
            {
               // formMDI.infoError.ErrorString = "PD46" + ex.Message;
            }
        }
        /// <summary>
        /// On Form frmPrintDesigner KeyDown
        /// ShortCutKeys
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPrintDesigner_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Control && e.KeyCode == Keys.S)
                    Save();
                else if (e.Control && e.KeyCode == Keys.N)
                    New();
                else if (e.Control && e.KeyCode == Keys.D)
                    Delete();
                else if (e.Control && e.Shift && e.KeyCode == Keys.C)
                    ClearAll();
                else if (e.Control && e.KeyCode == Keys.F)
                    NewForms();
                else if (e.Alt && e.KeyCode == Keys.M)
                {
                    string str = MessageBox.Show("Are you sure to clear master details ?", "Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question).ToString();
                    if (str == "Yes")
                    {
                        Clear();
                    }
                }
                else if (e.Alt && e.KeyCode == Keys.D)
                {
                    string str = MessageBox.Show("Are you sure to clear this field details ?", "Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question).ToString();
                    if (str == "Yes")
                    {
                        ClearDetails();
                        if (txtCurrent != null)
                            txtCurrent.Text = txtCurrent.Name;
                    }
                }
            }
            catch (Exception ex)
            {
               // formMDI.infoError.ErrorString = "PD47" + ex.Message;
            }
        }
        /// <summary>
        /// On TollStripMenuItem newToolStripMenuItem Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                New();
            }
            catch (Exception ex)
            {
               // formMDI.infoError.ErrorString = "PD48" + ex.Message;
            }
        }
        /// <summary>
        /// On Button btnClearMaster Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearMaster_Click(object sender, EventArgs e)
        {
            try
            {
                string str = MessageBox.Show("Are you sure to clear master details ?", "Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question).ToString();
                if (str == "Yes")
                {
                    Clear();
                }
            }
            catch (Exception ex)
            {
               // formMDI.infoError.ErrorString = "PD49" + ex.Message;
            }
        }
        /// <summary>
        /// On Button btnClearDetails Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearDetails_Click(object sender, EventArgs e)
        {
            try
            {
                string str = MessageBox.Show("Are you sure to clear this field details ?", "Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question).ToString();
                if (str == "Yes")
                {
                    ClearDetails();
                    if (txtCurrent != null)
                        txtCurrent.Text = txtCurrent.Name;
                }
            }
            catch (Exception ex)
            {
               // formMDI.infoError.ErrorString = "PD50" + ex.Message;
            }
        }
        /// <summary>
        /// On ToolStripMenuItem saveToolStripMenuItem Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
            }
            catch (Exception ex)
            {
               // formMDI.infoError.ErrorString = "PD51" + ex.Message;
            }
        }
        /// <summary>
        /// On ToolStripMenuItem deleteToolStripMenuItem Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Delete();
            }
            catch (Exception ex)
            {
              //  formMDI.infoError.ErrorString = "PD52" + ex.Message;
            }
        }
        /// <summary>
        /// On ToolStripMenuItem clearMasterToolStripMenuItem Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                btnClearMaster_Click(sender, e);
            }
            catch (Exception ex)
            {
                //formMDI.infoError.ErrorString = "PD53" + ex.Message;
            }
        }
        /// <summary>
        /// On ToolStripMenuItem clearDetailsToolStripMenuItem Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                btnClearDetails_Click(sender, e);
            }
            catch (Exception ex)
            {
              //  formMDI.infoError.ErrorString = "PD54" + ex.Message;
            }
        }
        /// <summary>
        /// On ToolStripMenuItem clearToolStripMenuItem Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
            }
            catch (Exception ex)
            {
              //  formMDI.infoError.ErrorString = "PD55" + ex.Message;
            }
        }
        /// <summary>
        /// On ToolStripMenuItem newFormToolStripMenuItem Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                NewForms();
            }
            catch (Exception ex)
            {
               // formMDI.infoError.ErrorString = "PD56" + ex.Message;
            }
        }
        /// <summary>
        /// On ToolStripMenuItem takeToolStripMenuItem Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void takeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //TextBox txt = sender as TextBox;
                //isTake = true;
                ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
                if (menuItem != null)
                {
                    ContextMenuStrip calendarMenu = menuItem.Owner as ContextMenuStrip;
                    if (calendarMenu != null)
                    {
                        foreach (Control c in pnlDesignImage.Controls)
                            if (c.BackColor == System.Drawing.Color.Yellow)
                                c.BackColor = System.Drawing.Color.AntiqueWhite;
                        txtCurrent = calendarMenu.SourceControl as TextBox;
                        txtCurrent.BackColor = System.Drawing.Color.Yellow;
                        isLarge = false;
                        isTake = true;
                        if (txtCurrent == null)
                            takeToolStripMenuItem_Click(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
              //  formMDI.infoError.ErrorString = "PD57" + ex.Message;
            }
        }
        /// <summary>
        /// On ToolStripMenuItem pnlDesignImage MouseClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlDesignImage_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (txtCurrent != null && isTake)
                {
                    Point loca = e.Location;
                    int Xvalue = loca.X;
                    int Yvalue = loca.Y;
                    Xvalue -= Xvalue % 8;
                    Yvalue -= Yvalue % 16;
                    loca = new Point(Xvalue, Yvalue);
                    txtWidth.Text = (txtCurrent.Width / 8).ToString();
                    txtColumn.Text = (Xvalue / 8).ToString();
                    txtRow.Text = (Yvalue / 16).ToString();
                    //MessageBox.Show(loca.X.ToString());
                    //txtCurrent.Location = loca;
                    //MessageBox.Show(txtTake.Location.X.ToString());
                    txtCurrent.BackColor = System.Drawing.Color.AntiqueWhite;
                    isTake = false;
                    isResize = false;
                }
            }
            catch (Exception ex)
            {
              //  formMDI.infoError.ErrorString = "PD58" + ex.Message;
            }
            //else if (txtCurrent != null && isLarge)
            //{
            //    Point loca = e.Location;
            //    int Xvalue = loca.X;
            //    int intxtXvalue = txtCurrent.Location.X;
            //    Xvalue -= intxtXvalue;
            //    Xvalue -= Xvalue % 8;
            //    if (Xvalue > 0)
            //    {
            //        txtWidth.Text = (Xvalue / 8 + 1).ToString();
            //        //txtCurrent.Width = Xvalue;
            //    }
            //    txtCurrent.BackColor = System.Drawing.Color.AntiqueWhite;
            //    isLarge = false;
            //}
        }
        /// <summary>
        /// On TextBox textBox1 Mouse Move
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (isResize)
                {
                    Point currentScreenPos = PointToScreen(e.Location);
                    txtCurrent = null;
                    TextBox txt = sender as TextBox;
                    int Xvalue = currentScreenPos.X - pnlDesignImage.Location.X;
                    int Yvalue = currentScreenPos.Y - pnlDesignImage.Location.Y;
                    txt.Width = currentScreenPos.X;
                    if ((txt.Width / 8) < 1)
                    {
                        txtWidth.Text = "1";
                    }
                    else
                        txtWidth.Text = (txt.Width / 8).ToString();
                }
            }
            catch (Exception ex)
            {
               // formMDI.infoError.ErrorString = "PD59" + ex.Message;
            }
        }
        /// <summary>
        /// On TextBox textBox1 Mouse Up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                TextBox txt = sender as TextBox;
                if (isResize)
                {
                    txtCurrent = txt;
                    AssignToDtbl();
                    EventArgs e1 = new EventArgs();
                    txtWidth_TextChanged(sender, e1);
                }
                isResize = false;
                txt.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
               // formMDI.infoError.ErrorString = "PD60" + ex.Message;
            }
        }
        /// <summary>
        /// On TextBox textBox1 Mouse Down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                TextBox txt = sender as TextBox;
                offset = e.Location;
                this.start_point = new Point(txt.Location.X, txt.Location.Y);
                start_Size = txt.Size;
                if (e.X < (txt.Width) && e.X > ((txt.Width) - 8))
                {
                    txt.Cursor = Cursors.SizeWE;
                    isResize = true;
                }
            }
            catch (Exception ex)
            {
              //  formMDI.infoError.ErrorString = "PD61" + ex.Message;
            }
        }
        /// <summary>
        /// On ToolStripMenuItem sizeToolStripMenuItem Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                txtCurrent.BackColor = System.Drawing.Color.AntiqueWhite;
                isTake = false;
                isResize = false;
            }
            catch (Exception ex)
            {
              //  formMDI.infoError.ErrorString = "PD62" + ex.Message;
            }
        }
        /// <summary>
        /// On TextBox textBox1 Drag Over
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_DragOver(object sender, DragEventArgs e)
        {
            try
            {
                TextBox txt = sender as TextBox;
                //offset = e.Location;
                this.start_point = new Point(txt.Location.X, txt.Location.Y);
                start_Size = txt.Size;
                if (e.X < (txt.Width) && e.X > ((txt.Width) - 8))
                {
                    txt.Cursor = Cursors.SizeWE;
                    //isResize = true;
                }
            }
            catch (Exception ex)
            {
              //  formMDI.infoError.ErrorString = "PD63" + ex.Message;
            }
        }
        /// <summary>
        /// On Button btnSaveDefault Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveDefault_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDefaultPass.Text == strDefaultPass)
                {
                    SaveDefault();
                }
                else
                {
                    MessageBox.Show("Invalid password", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
           //     formMDI.infoError.ErrorString = "PD64" + ex.Message;
            }
        }
        /// <summary>
        /// On Button btnSelectDefault Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectDefault_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDefaultPass.Text == strDefaultPass)
                {
                    pnlDesignImage.Controls.Clear();
                    dtbl.Rows.Clear();
                    txtCurrent = null;
                    inTextId = 0;
                    MasterSP spMaster = new MasterSP();
                    //MasterSPrint spMaster = new MasterSPt();
                    MasterInfo infoMaster = new MasterInfo();
                    infoMaster = spMaster.MasterCopyViewByFormName(int.Parse(cmbForm.SelectedValue.ToString()));
                    if (infoMaster.MasterId != 0)
                    {
                        txtBlankLineCountAfterDetailsForFooter.Text = infoMaster.BlankLneForFooter.ToString();
                        if (infoMaster.FooterLocation == "AfterDetails")
                            rbtnAfterDetailsFooter.Checked = true;
                        else
                            rbtnPageEndFooter.Checked = true;
                        cmbForm.SelectedItem = infoMaster.FormName;
                        chkLineRepeat.Checked = infoMaster.IsTwoLineForDetails;
                        chkLineNonRepeat.Checked = infoMaster.IsTwoLineForHedder;
                        txtBlankLineCountBetweenTwoPages.Text = infoMaster.LineCountBetweenTwo.ToString();
                        txtLineCountForFirstPage.Text = infoMaster.PageSize1.ToString();
                        txtLineCountForOtherPage.Text = infoMaster.PageSizeOther.ToString();
                        rbtnPitch10.Checked = infoMaster.Pitch == "10" ? true : false;
                        rbtnPitch12.Checked = infoMaster.Pitch == "10" ? false : true;
                        rbtnCondensedOn.Checked = infoMaster.Condensed == "On" ? true : false;
                        rbtnCondensedOff.Checked = infoMaster.Condensed == "On" ? false : true;
                        txtLineCountAfterPrint.Text = infoMaster.LineCountAfterPrint.ToString();
                        DetailsSP spDetails = new DetailsSP();
                        DataTable dtblDetails = new DataTable();
                        dtblDetails = spDetails.DetailsCopyViewAll(inMasterId);
                        dtbl = dtblDetails.Copy();
                        TextBox txtTemp = new TextBox();
                        foreach (DataRow dr in dtbl.Rows)
                        {
                            txtCurrent = null;
                            ClearDetails();
                            frmPrintDesigner obj = new frmPrintDesigner();
                            TextBox txt = new TextBox();
                            if (inTextId < int.Parse(dr["name"].ToString()))
                                inTextId = int.Parse(dr["name"].ToString());
                            txt.Name = dr["name"].ToString();
                            txt.Multiline = true;
                            txt.Size = new Size(int.Parse(dr["width"].ToString()) * 8 + 1, 16 + 1);
                            txt.Text = dr["text"].ToString();
                            txt.Enter += txtCurrent_Enter;
                            //txt.MouseClick += txtCurrent_MouseClick;
                            txt.ContextMenuStrip = cmsTextBox;
                            txt.TextChanged += txtCurrent_TextChanged;
                            txt.MouseDown += textBox1_MouseDown;
                            txt.MouseUp += textBox1_MouseUp;
                            txt.DragOver += textBox1_DragOver;
                            txt.MouseMove += textBox1_MouseMove;
                            txt.Font = new Font("Lucida Console", 10);
                            pnlDesignImage.Controls.Add(txt);
                            txtTemp = txt;
                            foreach (Control c in pnlDesignImage.Controls)
                                c.BackColor = System.Drawing.Color.AntiqueWhite;
                            txtTemp.BackColor = System.Drawing.Color.Gainsboro;

                            txtRow.Text = dr["row"].ToString();
                            txtColumn.Text = dr["columns"].ToString();
                            txtWidth.Text = dr["width"].ToString();
                            if (dr["Repeat"].ToString() == "false")
                                rbtnHedder.Checked = true;
                            else if (dr["Repeat"].ToString() == "true")
                                rbtnDetails.Checked = true;
                            else
                                rbtnFooter.Checked = true;
                            cmbAlign.SelectedItem = dr["Align"].ToString();
                            cmbFields.SelectedValue = dr["DBF"].ToString();
                            cmbTextType.SelectedItem = dr["dOrH"].ToString();
                            rbtnAllPageHedder.Checked = bool.Parse(dr["RepeatAllPage"].ToString());
                            rbtnOnlyInFirstPAgeHedder.Checked = !bool.Parse(dr["RepeatAllPage"].ToString());
                            rbtnInAllPageFooter.Checked = bool.Parse(dr["FooterRepeatAll"].ToString());
                            rbtnOnlyInLastPageFooter.Checked = !bool.Parse(dr["FooterRepeatAll"].ToString());
                            chkTextWrap.Checked = bool.Parse(dr["textWrap"].ToString());
                            cmbWrapLineCount.SelectedItem = dr["wrapLineCount"].ToString();
                            txtExtraFieldName.Text = dr["extraFieldName"].ToString();
                            if (txtExtraFieldName.Text.Trim() != "")
                            {
                                txtFieldsForExtra.Text = dr["fieldsForExtra"].ToString();
                                chkExtraField.Checked = true;
                            }
                            else
                            {
                                txtFieldsForExtra.Text = "";
                                chkExtraField.Checked = false;
                            }
                            if (txtTemp != null)
                                if (cmbTextType.SelectedIndex == 0)
                                    txtTemp.Font = new Font("Lucida Console", 10, System.Drawing.FontStyle.Regular);
                                else if (cmbTextType.SelectedIndex == 1)
                                    txtTemp.Font = new Font("Lucida Console", 11, System.Drawing.FontStyle.Bold);
                                else if (cmbTextType.SelectedIndex == 2)
                                    txtTemp.Font = new Font("Lucida Console", 10, System.Drawing.FontStyle.Bold);
                                else if (cmbTextType.SelectedIndex == 3)
                                    txtTemp.Font = new Font("Lucida Console", 10, System.Drawing.FontStyle.Italic);

                            if (txtTemp != null && txtTemp.Name != "")
                                if (cmbFields.SelectedIndex == 0)
                                    txtTemp.ReadOnly = false;
                                else
                                {
                                    txtTemp.ReadOnly = true;
                                    txtTemp.Text = cmbFields.SelectedValue.ToString();
                                }
                            if (txtTemp != null)
                                if (cmbAlign.SelectedIndex == 0)
                                    txtTemp.TextAlign = HorizontalAlignment.Left;
                                else if (cmbAlign.SelectedIndex == 1)
                                    txtTemp.TextAlign = HorizontalAlignment.Center;
                                else
                                    txtTemp.TextAlign = HorizontalAlignment.Right;
                            if (txtTemp != null)
                            {
                                txtTemp.Location = new Point((int.Parse(txtColumn.Text == "" ? "0" : txtColumn.Text) * 8), (int.Parse(txtRow.Text == "" ? "0" : txtRow.Text) * 16));
                                txtTemp.Size = new Size((int.Parse(txtWidth.Text == "" ? "5" : txtWidth.Text) * 8) + 1, txtTemp.Height);
                            }
                        }
                        txtCurrent = txtTemp;
                        txtDefaultPass.Text = "";
                    }
                    else
                    {
                        inMasterId = 0;
                        Clear();
                        ClearDetails();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid password", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
               // formMDI.infoError.ErrorString = "PD65" + ex.Message;
            }
        }
        /// <summary>
        /// On GroupBox  groupBox1 Enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        #endregion
    }
}