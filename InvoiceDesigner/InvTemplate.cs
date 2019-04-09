using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Diagnostics;
using Microsoft.Win32;
using System.Data.SqlClient;
using MakarovDev.ExpandCollapsePanel;
using System.IO;
namespace Sys_Sols_Inventory
{
    public partial class InvTemplate : Form
    {
        public InvTemplate()
        {
            InitializeComponent();
        }
        public a4design a4design = new a4design();
        Info inf = new Info();


        #region declarations
        public string template_name = "";
        SqlCommand cmd = new SqlCommand();
        private bool _new = false;
        public string datatype = " ";
        public bool ispreprint = false;
        public DataTable company = new DataTable();
        public Stack<String> stack = new Stack<String>();
        public Queue<string> queue = new Queue<string>();
        public bool isok = true;
        public string tail = "";
        public int k = 0;
        //taking min lines
        public string template = "";
        public int morepage; //=; Convert.ToInt32(Properties.Settings.Default.minline.ToString());
        public int maxline; //= ; Convert.ToInt32(Properties.Settings.Default.maxline.ToString());
        public int minline;//= Convert.ToInt32(Properties.Settings.Default.minline.ToString());
        public int addline; //= Convert.ToInt32(Properties.Settings.Default.line_height.ToString());
        public int add_gridheight; //= Convert.ToInt32(Properties.Settings.Default.grid_height.ToString());
        public int add_gridbottom;// = Convert.ToInt32(Properties.Settings.Default.grid_bottom.ToString());

        public int outvalint;
        public int numberofpage = 0;
        public int pageno = 0;
        public int fullpage = 0;
        public int linecount = 0;
        public Font headfont = new Font("Times New Roman", 10);
        public Font detailsfont = new Font("Times New Roman", 8);
        public DataTable sales = new DataTable();
        public DataTable captions = new DataTable();
        public DataTable sales_dtl = new DataTable();
        public DataTable labels = new DataTable();
        public DataTable ogsales = new DataTable();
        public DataTable settings = new DataTable();
        public string language = "";
        public double rowheight;
        public DataTable lines = new DataTable();
        public DataTable rect = new DataTable();
        public int rowcount = 0;
        public int columncount = 0;
        public string selectedtedtext;
        public int rowindex = 0;
        public int columnindex = 0;
        public DataTable gridview = new DataTable();
        #endregion

        public static InvTemplate _invoicedesigner = null;
        List<rectangle> rectList = new List<rectangle>();
        List<line> lineList = new List<line>();
        #region variables
        public bool IsClear = false;
        ExpandCollapsePanel TocloseExp = null;
        InvoiceMaster _invoiceMaster = new InvoiceMaster();
        PrintDocument document;
        PrintDialog dialog = new PrintDialog();
        DataTable ReportHeaders;
        DataTable PageHeaders;
        public string sections = "";
        DataTable Detailss;
        // public DataTable lines = new DataTable();
        //   public DataTable rect = new DataTable();
        Control selectedControl;
        public DataTable indexes = new DataTable();
        DataTable ControlTable;
        PictureBox CureentSection;
        DataTable PageFooters;
        DataTable generalsettings = new DataTable();
        DataTable ReportFooters;
        private int _NoOfLines = 0;
        private int mouseX;
        private int mouseY;
        const int DRAG_HANDLE_SIZE = 7;
        private Control ctrlControl;
        public List<Label> labelcontrols = new List<Label>();

        public List<PictureBox> Pictureboxes = new List<PictureBox>();


        #endregion

        #region line drawing


        Color paintcolor = Color.Black;
        bool choose = false;
        bool draw = false;
        int x, y, lx, ly = 0, height, width;
        string name = "";
        items shapes = new items();
        object copied = null;
        List<Point> pointList = new List<Point>();
        rectangle pre_rect;
        line pre_line;
        float[] dashValues = { 3, 3 };
        Pen dashpen = new Pen(Color.Black, 1);
        bool cpoly_pre = false;
        bool opoly_pre = false;
        bool freehand_pre = false;
        bool pre = false;
        public int selecteddata;




        #endregion
        private void InvTemplate_Load(object sender, EventArgs e)
        {
            loadcombo();
            comboBox1.SelectedItem = "Default";
            cmbAlignmnt.SelectedItem = "Right To Left";
            DataGridViewRow row = dataGridView1.RowTemplate;
            row.DefaultCellStyle.BackColor = Color.SlateGray;
            row.Height = dataGridView1.Height;
            row.MinimumHeight = dataGridView1.Height + 1000;
            sidebar_admin();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //disable selection
            if (listBox1.SelectedIndex != -1)
                listBox1.SetSelected(listBox1.SelectedIndex, false);
            //disable selected item
            e.ClickedItem.Enabled = false;
            for (int i = 0; i < toolStrip1.Items.Count; i++)
            {
                if (!toolStrip1.Items[i].Equals(e.ClickedItem))
                    toolStrip1.Items[i].Enabled = true;
            }
        }
        void loadcombo()
        {
            cmd = new SqlCommand("select template from INVOICE_a4_general");
            DataTable dt = new DataTable();
            dt = inf.get_genaraldata(cmd);
            DataRow row1 = dt.NewRow();
            dt.Rows.InsertAt(row1, 0);
            cmb_template.DataSource = dt;
            cmb_template.DisplayMember = "template";
            cmb_template.ValueMember = "template";
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pointList = new List<Point>();
            if (listBox1.SelectedIndex != -1)
            {
                contextMenuStrip1.Items[0].Enabled = true;
                contextMenuStrip1.Items[1].Enabled = true;
            }
            //enable all buttons
            for (int i = 0; i < toolStrip1.Items.Count; i++)
            {
                toolStrip1.Items[i].Enabled = true;
            }

            selecteddata = listBox1.SelectedIndex;
            selectedtedtext = listBox1.GetItemText(listBox1.SelectedItem);

        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void pcb_main_MouseDown(object sender, MouseEventArgs e)
        {
            if (!(e.Button == System.Windows.Forms.MouseButtons.Left)) return;
            draw = true;
            x = e.X;
            y = e.Y;
        }

        private void pcb_main_MouseMove(object sender, MouseEventArgs e)
        {
            //preview 
            if (draw)
            {
                Graphics box = pcb_main.CreateGraphics();
                int w = Math.Abs(e.X - x);
                int h = Math.Abs(e.Y - y);
                //if shift is enabled make square/cricle
                if (Control.ModifierKeys == Keys.Shift)
                {
                    w = Math.Min(w, h);
                    h = w;
                }

                //free hand drawing
                //if (!penButton.Enabled)
                //{
                //    freehand_pre = true;
                //    pointList.Add(e.Location);
                //    pcb_main.Invalidate();
                //}

                //rectange
                //else if (!rectangleButon.Enabled)
                //{

                //    pre = true;
                //    pre_rect = new rectangle(0, Color.Black, Math.Min(e.X, x), Math.Min(e.Y, y), w, h);
                //    pcb_main.Invalidate();
                //}
                ////ellipse
                //else if (!ellipseButton.Enabled)
                //{
                //    pre = true;
                //    pre_el = new ellipse(0, Color.Black, Math.Min(e.X, x), Math.Min(e.Y, y), w, h);
                //    pcb_main.Invalidate();
                //}
                //line
                else if (!rectangleButon.Enabled)
                {

                    pre = true;
                    pre_rect = new rectangle(0, Color.Black, Math.Min(e.X, x), Math.Min(e.Y, y), w, h);
                    pcb_main.Invalidate();
                }
                if (!V_line.Enabled)
                {
                    pre = true;
                    pre_line = new line(0, Color.Black, x, y, e.X, e.Y, "V");
                    pcb_main.Invalidate();
                }
                else if (!lineButton.Enabled)
                {
                    pre = true;
                    pre_line = new line(0, Color.Black, x, y, e.X, e.Y, "H");
                    pcb_main.Invalidate();
                }

                //move selected item
                else if ((listBox1.SelectedIndex != -1))
                {
                    shapes.moveItem(listBox1.SelectedIndex, selectedtedtext, e.X, e.Y);

                    refreshBox();
                }


            }
        }
        public void refreshBox()
        {
            pcb_main.Invalidate(); //repaint area
        }

        private void pcb_main_MouseUp(object sender, MouseEventArgs e)
        {
            string val = "";
            draw = false;
            lx = e.X;
            ly = e.Y;

            int w = Math.Abs(e.X - x);
            int h = Math.Abs(e.Y - y);
            //if shift is enabled make square
            if (Control.ModifierKeys == Keys.Shift)
            {
                w = Math.Min(w, h);
                h = w;
            }

            if (!(e.Button == System.Windows.Forms.MouseButtons.Left)) return;

            Graphics box = pcb_main.CreateGraphics();
            //Drawing shapes


            if (!rectangleButon.Enabled)//rectangle 
            {
                pre = false;
                val = shapes.drawRect(paintcolor, (Math.Min(e.X, x)), Math.Min(e.Y, y), w, h);
                //   box.DrawRectangle(new Pen(new SolidBrush(paintcolor)), new Point(x, y), new Point(lx, y));
                refreshBox();
                listBox1.Items.Add(val);

            }
            if (!lineButton.Enabled)//line
            {
                pre = false;
                val = shapes.drawLine(paintcolor, new Point(x, y), new Point(lx, y), "H");

                box.DrawLine(new Pen(new SolidBrush(paintcolor)), new Point(x, y), new Point(lx, y));
                refreshBox();
                listBox1.Items.Add(val);
            }
            if (!V_line.Enabled)//line
            {
                pre = false;
                val = shapes.drawLine(paintcolor, new Point(x, y), new Point(x, ly), "V");

                box.DrawLine(new Pen(new SolidBrush(paintcolor)), new Point(x, y), new Point(x, ly));
                refreshBox();
                listBox1.Items.Add(val);
            }


            //else if (!rectangleButon.Enabled)//rectangle 
            //{
            //    pre = false;
            //    shapes.drawRect(paintcolor, (Math.Min(e.X, x)), Math.Min(e.Y, y), w, h);
            //}
            //else if (!ellipseButton.Enabled)//ellipse
            //{
            //    pre = false;
            //    shapes.drawEllipse(paintcolor, (Math.Min(e.X, x)), Math.Min(e.Y, y), w, h);

            //}
            //else if (!polygonButton.Enabled)//closed polygon
            //{
            //    cpoly_pre = true;
            //    pointList.Add(new Point(e.X, e.Y));
            //    pcb_main.Invalidate();

            //}
            //else if (!openPolygonButton.Enabled)//open polygon
            //{
            //    opoly_pre = true;
            //    pointList.Add(new Point(e.X, e.Y));
            //    pcb_main.Invalidate();
            //}


        }

        private void pcb_main_Paint(object sender, PaintEventArgs e)
        {
            
            foreach (rectangle rec in shapes.rectList)
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(rec.Color)), rec.X, rec.Y, rec.Width, rec.Height);
           
            foreach (line line in shapes.lineList)
                e.Graphics.DrawLine(new Pen(new SolidBrush(line.Color)), new Point(line.x, line.y), new Point(line.lx, line.ly));
           

            if ((pre) && (!rectangleButon.Enabled)) //rectangle 
                e.Graphics.DrawRectangle(dashpen, pre_rect.X, pre_rect.Y, pre_rect.Width, pre_rect.Height);


            if ((pre) && (!lineButton.Enabled))//line
                e.Graphics.DrawLine(dashpen, pre_line.x, pre_line.y, pre_line.lx, pre_line.ly);


            if ((pre) && (!V_line.Enabled))//line
                e.Graphics.DrawLine(dashpen, pre_line.x, pre_line.y, pre_line.lx, pre_line.ly);

            if (IsClear)
            {
                e.Graphics.Clear(Color.LightGray);
                IsClear = false;
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pcb_main.Contains(ctrlControl))
            {
                pcb_main.Controls.Remove(ctrlControl);
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Select an item to delete");
                return;
            }
            int idx = listBox1.SelectedIndex;
            selecteddata = listBox1.SelectedIndex;
            selectedtedtext = listBox1.GetItemText(listBox1.SelectedItem);
            shapes.remove(idx, selectedtedtext);
            try
            {
                if (idx != 0)
                {
                    contextMenuStrip1.Items[idx - 1].Enabled = true;
                }
            }
            catch
            {}
            
            listBox1.Items.RemoveAt(idx);
            refreshBox();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cmb_template.SelectedIndex <= 0)
            {
                _new = true;

            }
            else
            {
                _new = false;
            }
            ep1.Clear();

            if (validation())
            {



                if (_new == true)
                {

                    try
                    {
                        InvoiceMaster _invoiceMaster = new InvoiceMaster();
                        _invoiceMaster.PrintName = Microsoft.VisualBasic.Interaction.InputBox("Please enter template name", "Template Name", "", -1, -1);
                        if (_invoiceMaster.PrintName == "")
                        {
                            MessageBox.Show("Pleas Enter Valid Name");
                            return;
                        }
                        cmd = new SqlCommand("select * from INVOICE_TEMPLATE where printname='" + _invoiceMaster.PrintName + "'");
                        DataTable d = inf.get_genaraldata(cmd);
                        if (d.Rows.Count > 0)
                        {
                            MessageBox.Show("Name Already Exsist,Pleas Provide Valid Name");
                            return;

                        }


                        template_name = _invoiceMaster.PrintName;
                        _invoiceMaster.PrintName = template_name;
                        _invoiceMaster.FormName = template_name;
                        _invoiceMaster.PageSize = "";
                      
                        new InvoiceMasterBLL().InsertInvoiceDesign(_invoiceMaster);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);


                    }
                    UpdateDesign();
                    columninsert();
                    savelines();
                    saverect();
                    MessageBox.Show("New Template Added");
                }
                else
                {

                    if (cmb_template.Text == "")
                    {
                          return;
                    }

                    else
                    {
                        if (IsDefault(cmb_template.Text))
                        {
                            if (txtPswd.Text != "IHTPEED")
                            {
                                MessageBox.Show("Incorrect Password");
                                return;
                            }
                        }
                          template = cmb_template.Text;
                          template_name = cmb_template.Text;
                          savelines();
                          saverect();
                          UpdateDesign();
                          columninsert();
                          MessageBox.Show("Design Saved Succesfully");

                    }

                }

                ep1.Clear();
                this.Refresh();

            }
            else
            {
              return;
            }

            loadcombo();
        }
        bool IsDefault(string template)
        {
            cmd = new SqlCommand("SELECT IS_DEFAULT FROM INVOICE_A4_GENERAL WHERE TEMPLATE='"+template+"'");
            bool value = true;
            value=Convert.ToBoolean(inf.get_Scalar(cmd));
            return value;
        }
        public void saverect()
        {
            new invoiceLineBLL().deleterect(template_name);
            int gridypos = dataGridView1.Bottom;
            shapes.saverect(template_name, gridypos);
        }
        public void savelines()
        {
            new invoiceLineBLL().deleteline(template_name);
            int gridypos = dataGridView1.Bottom;
            shapes.savelines(template_name, gridypos);
        }
        public bool validation()
        {
            bool val = true;
            if (!chk_preprinted.Checked)
            {

                if (txt_add_height.Text == "" || txt_add_height.Text == null)
                {

                    val = false;
                    ep1.SetError(txt_add_height, "Pleas Enter Valid Value");
                    txt_add_height.Focus();
                    goto end;

                }
                if (txt_linebottom.Text == "" || txt_linebottom.Text == null)
                {


                    val = false;
                    ep1.SetError(txt_linebottom, "Pleas Enter Valid Value");
                    txt_linebottom.Focus();
                    goto end;

                }
                if (txt_bottom.Text == "" || txt_linebottom.Text == null)
                {

                    val = false;
                    ep1.SetError(txt_bottom, "Pleas Enter Valid Value");
                    txt_bottom.Focus();
                    goto end;

                }


            }
            else
            {




                if (txt_add_height.Text == "" || txt_add_height.Text == null)
                {
                    txt_add_height.Text = "350";


                }
                if (txt_linebottom.Text == "" || txt_linebottom.Text == null)
                {

                    txt_linebottom.Text = "350";



                }
                if (txt_bottom.Text == "" || txt_linebottom.Text == null)
                {
                    txt_bottom.Text = "350";


                }
            }



            if (txt_pheight.Text == "" || txt_pheight.Text == null)
            {
                val = false;
                ep1.SetError(txt_pheight, "Pleas Enter Valid Value");
                txt_pheight.Focus();
                goto end;
            }
            if (txt_pwidth.Text == "" || txt_pwidth.Text == null)
            {

                val = false;
                ep1.SetError(txt_pwidth, "Pleas Enter Valid Value");
                txt_pwidth.Focus();
                goto end;


            }

            if (txt_namelength.Text == "" || txt_namelength.Text == null)
            {



                val = false;
                ep1.SetError(txt_namelength, "Pleas Enter Valid Value");
                txt_namelength.Focus();
                goto end;

            }


            if (txt_rows_height.Text == "" || txt_rows_height.Text == null)
            {

                val = false;
                ep1.SetError(txt_rows_height, "Pleas Enter Valid Value");
                txt_rows_height.Focus();
                goto end;


            }

            if (text_maxline.Text == "" || text_maxline.Text == null)
            {

                val = false;
                ep1.SetError(text_maxline, "Pleas Enter Valid Value");
                text_maxline.Focus();
                goto end;


            }

            if (text_minlines.Text == "" || text_minlines.Text == null)
            {

                val = false;
                ep1.SetError(text_minlines, "Pleas Enter Valid Value");
                text_minlines.Focus();
                goto end;


            }

        end:

            return val;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (cmb_template.SelectedIndex > 0)
            {
                template = (string)(cmb_template.SelectedValue).ToString();
               // print(template, "1");
                PrintDocument printDocument = new PrintDocument();
                laser_print ls = new laser_print();
                ls.template = template;
                string type = getType(template);
                if (type == "Roll")
                {
                    ls.TYPE = "PREVIEW";
                    using (var dlg = new PrintPreviewDialog())
                    {
                        printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Print", getWidth(template), getHeight());
                        printDocument.PrintPage += ls.printPageDynamicThermal;
                        dlg.Document = printDocument;
                        dlg.ShowDialog(this);
                    }
                }
                else
                {
                    using (var dlg = new PrintPreviewDialog())
                    {
                        printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Print", getWidth(template), getHeight(template));
                        printDocument.PrintPage += ls.printPageDynamic;
                        dlg.Document = printDocument;
                        dlg.ShowDialog(this);
                    }
                }
            }
            else
            {
                MessageBox.Show("Pleas Select Template");
            }
        }
        PaperSize ps;
        public void print(string templatename, string entryno)
        {
            template = templatename;

            ps = new PaperSize("ps", 500, 200);


            selection(templatename, entryno);
            int paperwidth;// = Convert.ToInt32(settings.Rows[0]["paper_width"]);
            int paperheight; //= Convert.ToInt32(settings.Rows[0]["paper_height"]);
             // printpages();
            //  fonttype();

            if (settings.Rows.Count > 0)
            {


                paperwidth = Convert.ToInt32(settings.Rows[0]["paper_width"]);
                paperheight = Convert.ToInt32(settings.Rows[0]["paper_height"]);

                // printpages();
                int height = Convert.ToInt32(settings.Rows[0]["details_height"]);

                maxline = Convert.ToInt32(settings.Rows[0]["max_line"]);
                minline = Convert.ToInt32(settings.Rows[0]["min_line"]);
                morepage = Convert.ToInt32(settings.Rows[0]["min_line"]);
                paperheight = add_gridheight + Convert.ToInt32(settings.Rows[0]["details_height"]);

            }
            else
            {

                MessageBox.Show("Pleas set Paper Size");
                printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("ps", 850, 1100);

            }



            #region fullpage
            int length = linecount;//p sales_dtl.Rows.Count;


            double k;

        ss:

            k = (double)length / morepage;

            if (k > 1)
            {

                length = length - maxline;

                fullpage++;

                goto ss;
            }

            #endregion;

            numberofpage = fullpage + 1;



        }
        public void columninsert()
        {


            new invoiceLineBLL().delete_columnslines(template_name);

            //   for (int i = 0; i < dataGridView1.ColumnCount; i++)
            // {
            try
            {

                int i = 0;
                foreach (DataGridViewColumn cl in dataGridView1.Columns)
                {

                    Rectangle CellRectangle1 = dataGridView1.GetCellDisplayRectangle(i, 0, false);
                    var x1 = CellRectangle1.Left;
                    var x2 = CellRectangle1.Right;

                    if (cmb_lang.Text == "English")
                    {
                        x1 = CellRectangle1.Left;
                        x2 = CellRectangle1.Right;

                    }   //  
                    var y1 = dataGridView1.Top;


                    a4design.starty = Convert.ToInt32(y1);
                    a4design.endy = dataGridView1.Bottom + 1;
                    a4design.name = cl.Name.ToString();
                    if (cl.Name.ToString() == "ITEM_NAME")
                    {
                        x1 = CellRectangle1.Left;
                        x2 = CellRectangle1.Right;
                    }
                    if (cl.Name.ToString() == "SL_NO")
                    {
                        x1 = CellRectangle1.Left;
                        x2 = CellRectangle1.Left + CellRectangle1.Width;
                    }

                    a4design.startx = Convert.ToInt32(x1);
                    a4design.endx = Convert.ToInt32(x2);
                    a4design.text = cl.HeaderText.ToString();
                    a4design.index = Convert.ToInt32(cl.DisplayIndex);

                    int val;
                    if (cl.ToolTipText == "" || cl.ToolTipText == null)
                    {


                        cl.ToolTipText = "0";
                    }


                    else if (int.TryParse(cl.ToolTipText, out val))
                    {


                    }
                    else
                    {
                        cl.ToolTipText = "0";

                    }
                    a4design.type = cl.ToolTipText.ToString();
                    a4design.template_name = template_name;
                    a4design.width = dataGridView1.Columns[i].Width;
                    a4design.visibile = Convert.ToBoolean(dataGridView1.Columns[i].Visible);

                    // new invoiceLineBLL().updatecolumnslines(a4design);


                    new invoiceLineBLL().insert_columnslines(a4design);
                    i++;




                }

            }
            catch (Exception k)
            {
                MessageBox.Show(k.Message);


            }



            columnheader();

            updatemargin();

          
        }
        public void columnheader()
        {
           foreach (DataGridViewColumn cl in dataGridView1.Columns)
            {
                a4design.text = cl.HeaderText.ToString();
                a4design.name = cl.Name.ToString();
                new invoiceLineBLL().updatecolumnheader(a4design);
            }

        }

        public void updatemargin()
        {



            new invoiceLineBLL().deletegeneral(template_name);
            a4design.left = Convert.ToInt32(dataGridView1.Left);
            a4design.width = Convert.ToInt32(dataGridView1.Width);
            a4design.top = Convert.ToInt32(dataGridView1.Top);
            a4design.height = Convert.ToInt32(dataGridView1.Height) + 1;
            a4design.id = 1;
            a4design.headerheight = Convert.ToInt32(dataGridView1.ColumnHeadersHeight);
            a4design.isdefault = Convert.ToBoolean(ckDefault.Checked);
            a4design.bottom = Convert.ToInt32(dataGridView1.Bottom);
            a4design.AmountAligmnmnt = cmbAlignmnt.Text;
            a4design.right = Convert.ToInt32(dataGridView1.Right);
            a4design.startx = Convert.ToInt32(dataGridView1.Location.X);
            a4design.starty = Convert.ToInt32(dataGridView1.Location.Y);
            a4design.form_height = this.pcb_main.ClientSize.Height;
            a4design.form_width = this.pcb_main.ClientSize.Width;
            a4design.PaperType = comboBox1.SelectedItem.ToString();
            a4design.h_fontsize = Convert.ToInt32(lbl_header.Font.Size);
            a4design.h_fontstyle = lbl_header.Font.Style.ToString();
            a4design.h_fontname = lbl_header.Font.Name.ToString();
            a4design.d_fontsize = Convert.ToInt32(lbl_details.Font.Size);
            a4design.d_fontstyle = lbl_details.Font.Style.ToString();
            a4design.d_fontname = lbl_details.Font.Name.ToString();
            a4design.template_name = template_name;
            a4design.language = cmb_lang.Text;
            a4design.paper_height = Convert.ToInt32(txt_pheight.Text);
            a4design.paper_width = Convert.ToInt32(txt_pwidth.Text);
            a4design.paper_height_add = Convert.ToInt32(txt_add_height.Text);
            a4design.bottom_minus = Convert.ToInt32(txt_bottom.Text);
            a4design.row_height = Convert.ToInt32(txt_rows_height.Text);
            a4design.line_max = Convert.ToInt32(text_maxline.Text);
            a4design.line_min = Convert.ToInt32(text_minlines.Text);
            a4design.is_preprinted = Convert.ToBoolean(chk_preprinted.CheckState);
            a4design.line_bottom = Convert.ToInt32(txt_linebottom.Text);
            a4design.name_lengyh = Convert.ToInt32(txt_namelength.Text);
            a4design.DrwaLine = Convert.ToBoolean(chk_DrawLine.Checked);
            a4design.DrwaMargin = Convert.ToBoolean(chk_DrawMargin.Checked);
            a4design.printDescription = Convert.ToBoolean(chk_PrintDescription.Checked);
            a4design.printHLINE = Convert.ToBoolean(chk_PrintHline.Checked);
            a4design.printCHeader = Convert.ToBoolean(chk_columnheader.CheckState);
            a4design.HeaderRPT = chkHeader.Checked;
            a4design.FooterRPT = chkFooter.Checked;
            new invoiceLineBLL().updategeneral(a4design);

        }
        public void UpdateDesign()
        {
            



            //    _invoiceMaster.PageSize = comboBox1.Text;
            // _invoiceMaster.width = (float)GlobalFunc.DecimalConversion(txt_namelength.Text, 0, false);
            // _invoiceMaster.height = (float)GlobalFunc.DecimalConversion(txt_rows_height.Text, 0, false);
            //   _invoiceMaster.MgAll = GlobalFunc.IntConversion(txtAll.Text);
            // _invoiceMaster.MgBottom = GlobalFunc.IntConversion(txtBottom.Text);
            //  _invoiceMaster.MgRight = GlobalFunc.IntConversion(txtRight.Text);
            //   _invoiceMaster.MgLeft = GlobalFunc.IntConversion(txtLeft.Text);
            //   _invoiceMaster.MgTop = GlobalFunc.IntConversion(txtTop.Text);
            //   new InvoiceMasterBLL().UpdateInvoiceDesign(_invoiceMaster);
            // new InvoicePrnSectionBLL().deleteInvoicePrnSections(_invoiceMaster.ID);
            new invoiceLineBLL().deleteDesign(template_name);
            int i = 0; int j = 0;
          
            InvoicePrnLine _invoicePrnLine = new InvoicePrnLine();
            var lxx = dataGridView1.Bottom.ToString();
            int lx = Convert.ToInt32(lxx) - 100;
            _invoicePrnLine.templatename = template_name;

            if (pcb_main.Controls.Count > 0)
            {
                foreach (Control ctrl in pcb_main.Controls)
                {
                    if (ctrl is Label)
                    {


                        _invoicePrnLine.PrintName = _invoiceMaster.ID;

                        if (ctrl.Text.Contains("["))
                            _invoicePrnLine.ObjectName = ctrl.Name;

                        else
                            _invoicePrnLine.ObjectName = "Label" + i.ToString();
                        _invoicePrnLine.ObjectType = "Label";
                        _invoicePrnLine.Objectwidth = ctrl.Width;
                        _invoicePrnLine.objectHieght = ctrl.Height;
                        _invoicePrnLine.ObjectX = ctrl.Location.X;
                        _invoicePrnLine.objectY = ctrl.Location.Y;

                        _invoicePrnLine.ObjectSection = pcb_main.Name;
                        if (ctrl.Text.Contains("["))
                            _invoicePrnLine.objectData = 1;
                        else
                            _invoicePrnLine.objectData = 0;
                        _invoicePrnLine.fontname = ctrl.Font.Name;
                        _invoicePrnLine.bold = ctrl.Font.Bold == true ? 1 : 0;
                        _invoicePrnLine.underline = ctrl.Font.Underline == true ? 1 : 0;
                        _invoicePrnLine.strikeout = ctrl.Font.Strikeout == true ? 1 : 0;
                        _invoicePrnLine.fontsize = ctrl.Font.Size;
                        _invoicePrnLine.Text = ctrl.Text;
                        try
                        {
                            _invoicePrnLine.tbl = ctrl.Tag.ToString();
                        }
                        catch (Exception ex)
                        {

                            _invoicePrnLine.tbl = "New";
                        }


                        if (Convert.ToInt32(ctrl.Top.ToString()) > lx)
                        {


                            _invoicePrnLine.section = "footer";
                        }
                        else
                        {

                            _invoicePrnLine.section = "general";
                        }



                        if (ctrl.AccessibleDescription == "" || ctrl.AccessibleDescription == null)
                        {


                            ctrl.AccessibleDescription = "0";


                        }

                        else
                        {

                            if (int.TryParse(ctrl.AccessibleDescription, out outvalint))
                            {

                            }
                            else
                            {

                                ctrl.AccessibleDescription = "0";

                            }


                        }

                        _invoicePrnLine.ValueType = Convert.ToInt32(ctrl.AccessibleDescription);

                        try
                        {

                            // _invoicePrnLine.value_type = Convert.ToInt32(ctrl.AccessibleDescription);
                        }
                        catch
                        {

                            //  _invoicePrnLine.value_type = 0;
                        }
                        new invoiceLineBLL().insertDesignpage(_invoicePrnLine);
                        i++;
                    }

                    else if (ctrl is PictureBox)
                    {


                        _invoicePrnLine.PrintName = _invoiceMaster.ID;

                        if (ctrl.Text.Contains("["))
                            _invoicePrnLine.ObjectName = ctrl.Name;

                        else
                            _invoicePrnLine.ObjectName = "PictureBox" + j.ToString();
                        _invoicePrnLine.ObjectType = "PictureBox";
                        _invoicePrnLine.Objectwidth = ctrl.Width;
                        _invoicePrnLine.objectHieght = ctrl.Height;
                        _invoicePrnLine.ObjectX = ctrl.Location.X;
                        _invoicePrnLine.objectY = ctrl.Location.Y;

                        _invoicePrnLine.ObjectSection = pcb_main.Name;
                        if (ctrl.Text.Contains("["))
                            _invoicePrnLine.objectData = 1;
                        else
                            _invoicePrnLine.objectData = 0;
                        _invoicePrnLine.fontname = ctrl.Font.Name;
                        _invoicePrnLine.bold = ctrl.Font.Bold == true ? 1 : 0;
                        _invoicePrnLine.underline = ctrl.Font.Underline == true ? 1 : 0;
                        _invoicePrnLine.strikeout = ctrl.Font.Strikeout == true ? 1 : 0;
                        _invoicePrnLine.fontsize = ctrl.Font.Size;
                        _invoicePrnLine.Text = ctrl.Text;
                        try
                        {
                            _invoicePrnLine.tbl = ctrl.Tag.ToString();
                        }
                        catch (Exception ex)
                        {

                            _invoicePrnLine.tbl = "new";
                        }


                        if (Convert.ToInt32(ctrl.Top.ToString()) > lx)
                        {


                            _invoicePrnLine.section = "footer";
                        }
                        else
                        {

                            _invoicePrnLine.section = "general";
                        }



                        if (ctrl.AccessibleDescription == "" || ctrl.AccessibleDescription == null)
                        {


                            ctrl.AccessibleDescription = "0";


                        }

                        else
                        {

                            if (int.TryParse(ctrl.AccessibleDescription, out outvalint))
                            {

                            }
                            else
                            {

                                ctrl.AccessibleDescription = "0";

                            }


                        }

                        try
                        {

                            // _invoicePrnLine.value_type = Convert.ToInt32(ctrl.AccessibleDescription);
                        }
                        catch
                        {

                            //  _invoicePrnLine.value_type = 0;
                        }
                        new invoiceLineBLL().insertDesignpage(_invoicePrnLine);
                        j++;

                    }




                }



            }


            i = 0;

        }
    
        public void selection(string templatename, string entryno)
        {

            // cmd = new SqlCommand("select * from company");
            // company = inf.get_genaraldata(cmd);
            cmd = new SqlCommand("select * from  invoiceprnline where template='" + templatename + "' and visible='True' ");
            labels = inf.get_genaraldata(cmd);
            //cmd = new SqlCommand("select   SALES_DTL.*,item.*,item.ITEM_NAME from SALES_DTL inner join ITEM on SALES_DTL.ITEM_CODE =ITEM.ITEM_CODE where SALES_DTL.[status]='True' and SALES_DTL.INVOICE_NO ='" + entryno + "'order by SALES_DTL.SL_NO asc");
            //sales_dtl = inf.get_genaraldata(cmd);

            //cmd = new SqlCommand("select  sales.*,customer.*from sales inner join customer on SALES.CUST_NAME=CUSTOMER.Ledger_id  where sales.invoice_no='" + entryno + "'");
            //sales = inf.get_genaraldata(cmd);
            //cmd = new SqlCommand("select * from a4columns   where template='" + templatename + "' and visible='True'  ORDER BY [INDEX]");
            //gridview = inf.get_genaraldata(cmd);
            //cmd = new SqlCommand("select * from invoice_lines where template='" + templatename + "'");
            //lines = inf.get_genaraldata(cmd);
            //cmd = new SqlCommand("select * from a4_general where template='" + templatename + "'");
            //settings = inf.get_genaraldata(cmd);
            //cmd = new SqlCommand("select * from invoice_rectangle where template='" + templatename + "'");
            //rect = inf.get_genaraldata(cmd);
        }
        int getWidth(string template)
        {
            cmd = new SqlCommand("select PAPER_WIDTH from  INVOICE_a4_general where template='" + template + "'");
            return Convert.ToInt32(inf.get_Scalar(cmd));
        }
        int getHeight(string template)
        {
            cmd = new SqlCommand("select PAPER_HEIGHT from  INVOICE_a4_general where template='" + template + "'");
            return Convert.ToInt32(inf.get_Scalar(cmd));
        }
        string getType(string template)
        {
            cmd = new SqlCommand("select paper_type from  INVOICE_a4_general where template='" + template + "'");
            return Convert.ToString(inf.get_Scalar(cmd));
        }
        int getHeight()
        {
            cmd = new SqlCommand("select * from  INVOICE_a4_general where template='" + template + "'");
            DataTable templateDesign = new DataTable();
            templateDesign =inf.get_genaraldata(cmd);
            int TotalHeight = 0;
            double Header = Convert.ToDouble(templateDesign.Rows[0]["TOP"]);
            double Footer = Convert.ToDouble(templateDesign.Rows[0]["FORM_HEIGHT"]) - Convert.ToDouble(templateDesign.Rows[0]["BOTTOM"]);
            double Rowheight = Convert.ToDouble(templateDesign.Rows[0]["Row_Height"]);
            int GridHeight = Convert.ToInt32(templateDesign.Rows[0]["height"]);
            TotalHeight = Convert.ToInt32(GridHeight + Header + Footer);
            //TotalHeight += Rowheight;
            return TotalHeight;
        }
        private void penButton_Click(object sender, EventArgs e)
        {
            label_static_add("New", "Pleas Enter Caption");
        }
        public void label_static_add(string tag, string text)
        {


            Label lbl = new Label();
            // lbl.Name =
            lbl.Text = text;
            lbl.AutoSize = false;
            lbl.BorderStyle = BorderStyle.FixedSingle;
            lbl.Tag = tag;
            lbl.AccessibleDescription = "0";
            pcb_main.Controls.Add(lbl);

            lbl.MouseDown += new MouseEventHandler(control_MouseDown);
            lbl.BringToFront();
            ControlMoverOrResizer.Init(lbl);


        }
        private void control_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
               
                //Change_Selection(ReportHeader);
                //Change_Selection(PageHeader);
                //Change_Selection(Section);
                //Change_Selection(PageFooter);
                //Change_Selection(ReportFooter);
                selectedControl = (Control)sender;
                Control control = (Control)sender;
                mouseX = -e.X;
                mouseY = -e.Y;
                control.Invalidate();
              

                if (control.GetType() == typeof(Label) || control.GetType() == typeof(DataGridView))
                {
                    propertyGrid1.SelectedObject = selectedControl;
                    control.BackColor = Color.LemonChiffon;
                    browseToolStripMenuItem.Visible = false;
                }

                else
                {
                    browseToolStripMenuItem.Visible = true;
                    propertyGrid1.SelectedObject = null;

                }
            }
            if (e.Button == MouseButtons.Right)
            {
                selectedControl = (Control)sender;
                Control control = (Control)sender;
                ctrlControl = control;
                //foreach (Control ctrl in panel1.Controls)
                //{
                //    ctrl.BackColor = Color.White;
                //}
                //foreach (Control ctrl in panel2.Controls)
                //{
                //    ctrl.BackColor = Color.White;
                //}
                mouseX = -e.X;
                mouseY = -e.Y;
                control.Invalidate();
                control.Update();
                // DrawControlBorder(sender ,(Panel)control.Parent);
                if (control.GetType() == typeof(Label) || control.GetType() == typeof(DataGridView))
                {
                    browseToolStripMenuItem.Visible = false;
                    propertyGrid1.SelectedObject = selectedControl;
                    // control.BackColor = Color.LemonChiffon;
                }

                else
                {


                    browseToolStripMenuItem.Visible = true;

                    propertyGrid1.SelectedObject = null;

                }
                control.BackColor = Color.HotPink;
                Point pn;
                pn = new Point(mouseX + 30, mouseY + 20);
                contextMenuStrip1.Show(control, pn);

            }
        }
        public void Change_Selection(Panel p)
        {
            foreach (Control ctrl in p.Controls)
            {
                ctrl.BackColor = Color.White;
            }
        }
         void exp_ExpandCollapse(object sender, MakarovDev.ExpandCollapsePanel.ExpandCollapseEventArgs e)
        {
            ExpandCollapsePanel Expdem = (ExpandCollapsePanel)sender;

            if (TocloseExp == Expdem)
            {
                TocloseExp = null;
            }
            if (TocloseExp == null)
            {

                TocloseExp = Expdem;
            }
            else
            {
                TocloseExp.IsExpanded = false;
                TocloseExp = Expdem;
            }



        }
        public void sidebar_admin()
        {
            DataTable head = new DataTable();
            DataTable items = new DataTable();
            cmd = new SqlCommand("select * from invoice_heads");
            head = inf.get_genaraldata(cmd);
            
            //  main_tree.

            foreach (DataRow rw in head.Rows)
            {
                string s = rw["name"].ToString();
                string under = rw["table_name"].ToString();
                if (s == "CompanyDetails" || s == "Customer Details" || s == "Other")
                {
                    try
                    {
                        cmd = new SqlCommand("SELECT TEXT AS COLUMN_NAME,[TABLE] AS UNDER FROM INVOICE_DETAILS WHERE UNDER='" + s + "'  ORDER BY TEXT ASC");
                        items = inf.get_genaraldata(cmd);
                    }
                    catch
                    {


                    }
                }

                else if (s == "Sales Header" || s == "Sales Details")
                {

                    cmd = new SqlCommand(@"SELECT COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME ='" + under + "'ORDER BY ORDINAL_POSITION ");

                    try
                    {
                        items = inf.get_genaraldata(cmd);
                    }
                    catch
                    {


                    }
                }
               
                int i = 40;

                ExpandCollapsePanel exp = new ExpandCollapsePanel();
                exp.ButtonStyle = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonStyle.Circle;


                advancedFlowLayoutPanel1.Controls.Add(exp);
                exp.Text = s;
                exp.ForeColor = Color.White;
                exp.IsExpanded = false;
                int j = 0;
                exp.Name = "exp" + j;
                j = j + 1;
                exp.ExpandCollapse += exp_ExpandCollapse;


                //getting size of collapse panel according to menuses count

                if (items.Rows.Count > 0)
                {
                    foreach (DataRow rw1 in items.Rows)
                    {
                        string a = rw1["COLUMN_NAME"].ToString();
                        i = i + 24;

                    }
                }
                else
                {
                    string a = s;
                    if (a.GetType() == typeof(ToolStripSeparator))
                    {
                        continue;
                    }
                    i = i + 24;
                }
                exp.ExpandedHeight = i;

                //  exp.Font = new Font(exp.Font, FontStyle.Bold);

                MakarovDev.ExpandCollapsePanel.AdvancedFlowLayoutPanel adcp = new MakarovDev.ExpandCollapsePanel.AdvancedFlowLayoutPanel();

                //   adcp.BackColor = Color.;

                adcp.Padding = new System.Windows.Forms.Padding(40);

                //  adcp.BackColor = ColorTranslator.FromHtml("#EDEDED");
                exp.Controls.Add(adcp);


                //  adcp.AutoSize = true;

                if (items.Rows.Count > 0)
                {
                    foreach (DataRow rw1 in items.Rows)
                    {
                        string a = rw1["COLUMN_NAME"].ToString();
                        if(s=="CompanyDetails")
                            under = rw1["UNDER"].ToString();
                        Label lb = new Label();
                        //  Button lb = new Button();
                        lb.Text = a;
                        lb.ForeColor = Color.White;
                        //lb.Font = new Font(lb.Font,FontStyle.Bold);
                        lb.Cursor = Cursors.Hand;
                        //   lb.Parent.Text = s;
                        lb.AccessibleName = under;
                        lb.Click += lb_Click;
                        lb.MouseEnter += lb_MouseEnter;
                        lb.MouseLeave += lb_MouseLeave;
                        lb.MouseHover += lb_MouseHover;
                        //  lb.Parent.Name="e"

                        adcp.Controls.Add(lb);
                        //continue;



                    }
                }
                else
                {

                    Label lb = new Label();
                    //  Button lb = new Button();
                    lb.Text = s;
                    lb.ForeColor = Color.White;
                    //lb.Font = new Font(lb.Font,FontStyle.Bold);
                    lb.Cursor = Cursors.Hand;
                    //   lb.Parent.Text = s;
                    lb.AccessibleName = under;
                    lb.Click += lb_Click;
                    lb.MouseEnter += lb_MouseEnter;
                    lb.MouseLeave += lb_MouseLeave;
                    lb.MouseHover += lb_MouseHover;
                    //  lb.Parent.Name="e"

                    adcp.Controls.Add(lb);
                }

                adcp.Height = 2000;

            }
        }
        void lb_MouseLeave(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            lb.ForeColor = Color.White;
            lb.BackColor = Color.Transparent; ;
        }

        void lb_MouseHover(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            lb.BackColor = Color.Orange;
        }

        void lb_MouseEnter(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            lb.ForeColor = Color.WhiteSmoke;

        }
        void lb_Click(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            string k = lb.AccessibleName.ToString();

            string section = lb.AccessibleName.ToString();

            try
            {



                if (section == "")
                {

                    string tbl = lb.Text;
                    
                    if (tbl == "AmtInWords")
                    {
                        
                        tbl = lb.AccessibleName.ToString();
                        labeladd(pcb_main, lb.Text, section);
                    }
                    else if (tbl == "Logo")
                    {

                       string tag = "Logo";
                       pcb_static_add(tag, tag);

                    }
                    else if (tbl == "Sl No" || tbl == "CGST %" || tbl == "SGST %" || tbl == "CGST Amt" || tbl == "SGST Amt" || tbl == "HSN" || tbl == "IGST %" || tbl == "IGST Amt"||tbl=="Amount")
                    {
                        if (dataGridView1.Columns.Contains(lb.Text.ToString()))
                        {
                            dataGridView1.Columns.Remove(lb.Text);
                        }
                        else
                        {
                            dataGridView1.Columns.Add(lb.Text, lb.Text);
                            if (dataGridView1.Rows.Count < 1)
                            {
                                dataGridView1.Rows.Add(30);
                            }
                        }
                    }
                        //code by mayoosha
                    else if (tbl == "IGSTtotal")
                    {
                        
                    }
                    else
                    {
                       label_static_add("New", "Pleas Enter Caption");

                    }
                }
                else
                {
                  if (section == "INV_SALES_DTL")
                    {
                        //To Create Columns
                        if (dataGridView1.Columns.Contains(lb.Text.ToString()))
                        {
                            dataGridView1.Columns.Remove(lb.Text);
                        }
                        else
                        {
                            dataGridView1.Columns.Add(lb.Text, lb.Text);
                            if (dataGridView1.Rows.Count < 1)
                            {
                                dataGridView1.Rows.Add(30);
                            }
                        }
                    }
                    else
                    {
                      //To Create Label
                        string tbl = "";
                        tbl = lb.AccessibleName.ToString();
                        labeladd(pcb_main, lb.Text, section);
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
        void add(Panel P, string text)
        {

            //{
            //    Panel p1 = new Panel();
            //    p1.BackColor = Color.Black;
            //    p1.Width = 5;
            //    p1.Height = Section.Height;
            //    P.Controls.Add(p1);
            //    p1.MouseDown += new MouseEventHandler(control_MouseDown);
            //    ControlMoverOrResizer.Init(p1);
            //    ControlMoverOrResizer.WorkType = ControlMoverOrResizer.MoveOrResize.MoveAndResize;
            //}
        }
        void labeladd(PictureBox p, string Name, string tbl)
        {
            Label lbl = new Label();
            lbl.Name = Name;
            lbl.Text = "[" + Name + "]";
            lbl.AutoSize = false;
            lbl.BorderStyle = BorderStyle.FixedSingle;
            lbl.Tag = tbl;
            p.Controls.Add(lbl);
            lbl.MouseDown += new MouseEventHandler(control_MouseDown);
            // lbl.key

            ControlMoverOrResizer.Init(lbl);
            ControlMoverOrResizer.WorkType = ControlMoverOrResizer.MoveOrResize.MoveAndResize;
            CureentSection = p;

        }
        public void pcb_static_add(string tag, string text)
        {
            try
            {
                string path = getPath();
                if (path != "")
                {
                    PictureBox lbl = new PictureBox();
                    // lbl.Name =
                    lbl.Text = text;
                    lbl.AutoSize = false;
                    lbl.BorderStyle = BorderStyle.FixedSingle;
                    lbl.Tag = tag;
                    lbl.SizeMode = PictureBoxSizeMode.StretchImage;
                    Image im = GetCopyImage(path);
                    lbl.Image = im;
                    pcb_main.Controls.Add(lbl);
                    lbl.MouseDown += new MouseEventHandler(control_MouseDown);
                    lbl.BackgroundImageChanged += new EventHandler(pictureBox1_BackgroundImageChanged);
                    lbl.BringToFront();
                    ControlMoverOrResizer.Init(lbl);
                }
                else
                {
                    MessageBox.Show("Please set logo in company setup");
                    return;
                }
            }
            catch
            {

            }
        }
        private Image GetCopyImage(string path)
        {
            Bitmap bm = null;
            try
            {
                using (Image im = Image.FromFile(path))
                {

                    bm = new Bitmap(im);
                }
            }
            catch (Exception e)
            {
                //TODO: THROW ERROR IF LOGO NOT FOUND IF YOU WANT TO.
            }
            return bm;
        }

        private void pictureBox1_BackgroundImageChanged(object sender, EventArgs e)
        {
            PictureBox lb = sender as PictureBox;
           
        }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControlMoverOrResizer.Init(dataGridView1);
            ControlMoverOrResizer.WorkType = ControlMoverOrResizer.MoveOrResize.MoveAndResize;
        }

        private void deselectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.ActiveControl = pcb_main;
            //pcb_main.Focus();
            ControlMoverOrResizer.UndoInit(dataGridView1);
            ControlMoverOrResizer.WorkType = ControlMoverOrResizer.MoveOrResize.stop;
        }

        private void bringToFrontToolStripMenuItem1_Click(object sender, EventArgs e)
        {
          
        }

        private void sendToBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            propertyGrid1.SelectedObject = dataGridView1.Columns[e.ColumnIndex];
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            propertyGrid1.SelectedObject = dataGridView1.Columns[e.ColumnIndex];
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "A4")
            {
                txt_pheight.Text = "1150";
                txt_pwidth.Text = "820";
            }
            else if (comboBox1.SelectedItem.ToString() == "A5")
            {
                txt_pheight.Text = "580";
                txt_pwidth.Text = "820";
            }
            else if (comboBox1.SelectedItem.ToString() == "Roll")
            {
                txt_pheight.Text = "0";
                txt_pwidth.Text = "300";
            }
            else
            {
                txt_pheight.Text = "1150";
                txt_pwidth.Text = "820";
            }

        }

        private void lbl_header_Click(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = lbl_header;
        }

        private void lbl_details_Click(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = lbl_details;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            clear();
            if (cmb_template.SelectedIndex > 0)
            {
              template_name = cmb_template.SelectedValue.ToString();
              loaddata();
            }
        }
        public void clear()
        {

            chk_PrintHline.Checked = false;
            rectList = null;
            lineList = null;
            shapes = null;
            if (listBox1.Items.Count > 0)
                listBox1.Items.Clear();


            if (dataGridView1.ColumnCount > 0)
                dataGridView1.Columns.Clear();


            foreach (Control ctrl in pcb_main.Controls)
            {
                if (ctrl is Label)
                {

                    pcb_main.Controls.Remove(ctrl);
                }
            }


            if (dataGridView1.ColumnCount > 0)
                dataGridView1.Columns.Clear();


            foreach (Control ctrl in pcb_main.Controls)
            {
                if (ctrl is Label)
                {

                    pcb_main.Controls.Remove(ctrl);
                }
            }

            shapes = new items();

            rectList = new List<rectangle>();
            lineList = new List<line>();
        }
        public void loaddata()
        {

            loadcolumns();
            grid_place();
            Design_load();
          //  ControlMoverOrResizer.Init(pcb_main);
           // ControlMoverOrResizer.WorkType = ControlMoverOrResizer.MoveOrResize.Resize;

            int width = (int)Math.Round(210 / 25.4 * 500);
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TabControl)
                {
                    foreach (Control ctrls in ctrl.Controls)
                    {
                        if (ctrls is TabPage)
                        {

                            foreach (Control labels in ctrls.Controls)
                            {
                                if (labels is Panel)
                                {
                                    labels.Width = width;

                                    ControlMoverOrResizer.Init(labels);
                                    ControlMoverOrResizer.WorkType = ControlMoverOrResizer.MoveOrResize.MoveAndResize;
                                }
                            }
                        }
                    }
                }
            }

            loadline();
            loadrect();
            #region gensettings
            generalsettings = new invoiceLineBLL().gridmargin(template_name);
            try
            {




                if (generalsettings.Rows.Count > 0)
                {


                    try
                    {

                        //  this.dataGridView1.Location = new System.Drawing.Point(Convert.ToInt32(generalsettings.Rows[0]["location_x"]), Convert.ToInt32(generalsettings.Rows[0]["location_x"]));
                        this.dataGridView1.Size = new System.Drawing.Size(Convert.ToInt32(generalsettings.Rows[0]["width"]), Convert.ToInt32(generalsettings.Rows[0]["height"]));
                        // dataGridView1.Size

                        txt_add_height.Text = generalsettings.Rows[0]["details_height"].ToString();
                        txt_pheight.Text = generalsettings.Rows[0]["paper_height"].ToString();
                        txt_pwidth.Text = generalsettings.Rows[0]["paper_width"].ToString();
                        cmbAlignmnt.Text = generalsettings.Rows[0]["AMT_ALGNMNT"].ToString();
                        if (generalsettings.Rows[0]["language"].ToString() != "" || generalsettings.Rows[0]["language"].ToString() != null)
                        {
                            cmb_lang.SelectedIndex = cmb_lang.FindStringExact(generalsettings.Rows[0]["language"].ToString());
                        }
                        ckDefault.Checked = Convert.ToBoolean(generalsettings.Rows[0]["IS_DEFAULT"]);
                        comboBox1.SelectedItem = generalsettings.Rows[0]["Paper_Type"].ToString();
                        txt_bottom.Text = generalsettings.Rows[0]["details_bottom"].ToString();
                        txt_linebottom.Text = generalsettings.Rows[0]["line_bottom"].ToString();
                        text_maxline.Text = generalsettings.Rows[0]["max_line"].ToString();
                        text_minlines.Text = generalsettings.Rows[0]["min_line"].ToString();
                        txt_namelength.Text = generalsettings.Rows[0]["name_length"].ToString();
                        txt_rows_height.Text = generalsettings.Rows[0]["row_height"].ToString();
                        chk_preprinted.Checked = Convert.ToBoolean(generalsettings.Rows[0]["is_preprinted"].ToString());
                        chk_DrawLine.Checked = Convert.ToBoolean(generalsettings.Rows[0]["draw_line"].ToString());
                        chk_DrawMargin.Checked = Convert.ToBoolean(generalsettings.Rows[0]["draw_margin"].ToString());
                        chk_PrintDescription.Checked = Convert.ToBoolean(generalsettings.Rows[0]["is_description"].ToString());
                        chk_columnheader.Checked = Convert.ToBoolean(generalsettings.Rows[0]["is_CHeader"].ToString());
                        chk_PrintHline.Checked = Convert.ToBoolean(generalsettings.Rows[0]["DRAW_HLINE"].ToString());
                        chkFooter.Checked = Convert.ToBoolean(generalsettings.Rows[0]["FOOTER_RPT"].ToString());
                        chkHeader.Checked = Convert.ToBoolean(generalsettings.Rows[0]["Header_RPT"].ToString());
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);


                    }


                }


            }
            catch (Exception ex)
            {


                MessageBox.Show(ex.Message);
            }

            #endregion
         
        }
        public void loadcolumns()
        {
          indexes = new invoiceLineBLL().select_columns();
        }
        public void grid_place()
        {

            int ins;
            DataTable place = new DataTable();
            place = new invoiceLineBLL().getcolumnline(template_name);
           
            if (place.Rows.Count > 0)
            {
                for (int i = 0; i < place.Rows.Count; i++)
                {
                    int s = i + 1;
                    DataGridViewTextBoxColumn cl = new DataGridViewTextBoxColumn();
                    try
                    {
                        try
                        {
                            cl.Visible = Convert.ToBoolean(place.Rows[i]["visible"]);
                            ins = Convert.ToInt32(place.Rows[i]["index"].ToString());
                        }
                        catch
                        {


                            continue;
                        }
                        if (Convert.ToBoolean(place.Rows[i]["visible"]) == true)
                        {
                            dataGridView1.Columns.Insert(ins, cl);

                            cl.Width = Convert.ToInt32(place.Rows[i]["width"]);
                            cl.Name = place.Rows[i]["name"].ToString();
                            cl.HeaderText = place.Rows[i]["text"].ToString();
                            cl.ToolTipText = place.Rows[i]["type"].ToString();

                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }

                }
                //set width

                for (int i = 0; i < place.Rows.Count; i++)
                {
                    dataGridView1.Columns[place.Rows[i]["name"].ToString()].Width = Convert.ToInt32(place.Rows[i]["width"]);
                }
            }
            // to be work
            DataTable place1 = new DataTable();
            place1 = new invoiceLineBLL().getgeneral(template_name);
            if (place1.Rows.Count > 0)
            {

                try
                {
                    dataGridView1.ColumnHeadersHeight = Convert.ToInt32(place1.Rows[0]["rowheader_height"]);

                    //  var p=place.Rows[0][5];
                    //  var q=place.Rows[0][5];
                    dataGridView1.Location = new System.Drawing.Point(Convert.ToInt32(place1.Rows[0]["location_x"]), Convert.ToInt32(place1.Rows[0]["location_y"]));

                    dataGridView1.Height = Convert.ToInt32(place1.Rows[0]["Height"]);
                    dataGridView1.Width = Convert.ToInt32(place1.Rows[0]["width"]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }
        public void loadline()
        {

            cmd = new SqlCommand("select * from invoice_lines where template='" + template_name + "'");
            lines = inf.get_genaraldata(cmd);
            foreach (DataRow row in lines.Rows)
            {
                Graphics box = pcb_main.CreateGraphics();
                x = Convert.ToInt32(row["x1"]);
                y = Convert.ToInt32(row["y1"]);
                lx = Convert.ToInt32(row["x2"]);
                ly = Convert.ToInt32(row["y2"]);
                name = row["name"].ToString();

                // {

                if (name.Contains("H"))
                {
                    pre = false;
                    string val = shapes.drawLine(paintcolor, new Point(lx, ly), new Point(x, ly), "H");

                    box.DrawLine(new Pen(new SolidBrush(paintcolor)), new Point(x, y), new Point(lx, ly));
                    refreshBox();
                    listBox1.Items.Add(val);
                }
                else if (name.Contains("V"))
                {
                    pre = false;
                    string val = shapes.drawLine(paintcolor, new Point(lx, ly), new Point(lx, y), "V");

                    box.DrawLine(new Pen(new SolidBrush(paintcolor)), new Point(x, y), new Point(lx, ly));
                    refreshBox();
                    listBox1.Items.Add(val);

                }
           
            }

        }
        public void Design_load()
        {
            DataTable r_Header = new DataTable();
            r_Header = new invoiceLineBLL().Select_Design_Report_Header(template_name);
            if (r_Header.Rows.Count > 0)
            {
                foreach (DataRow row in r_Header.Rows)
                {
                    load(pcb_main, row);
                }
            }
        }
        public void load(PictureBox p, DataRow row)
        {

            try
            {
                if (row["Text"].ToString().Contains("["))
                {
                    if (row["ObjectType"].ToString() == "Label")
                    {
                        FontStyle fs1 = new FontStyle();
                        if (row["bold"].ToString() == "True")
                            fs1 = FontStyle.Bold;
                        if (row["underline"].ToString() == "True")
                            fs1 = FontStyle.Underline;
                        if (row["strikeout"].ToString() == "True")
                            fs1 = FontStyle.Strikeout;
                        System.Drawing.Font fn = new Font(row["fontname"].ToString(), float.Parse(row["fontsize"].ToString()), fs1, System.Drawing.GraphicsUnit.Point);
                        Label lbl = new Label();
                        lbl.Name = row["ObjectName"].ToString();
                        lbl.Tag = row["tbl"].ToString();
                        lbl.Font = fn;
                        lbl.AccessibleDescription = row["value_type"].ToString();
                        lbl.Location = new Point(Convert.ToInt32(row["ObjectX"].ToString()),
                            Convert.ToInt32(row["ObjectY"].ToString()));
                        lbl.Size = new Size(Convert.ToInt32(row["Objectwidth"].ToString()),
                            Convert.ToInt32(row["objectHieght"].ToString()));
                        lbl.Text = row["Text"].ToString();
                        lbl.AutoSize = false;
                        lbl.BorderStyle = BorderStyle.FixedSingle;
                        p.Controls.Add(lbl);
                        lbl.Click += label_Click;
                        lbl.MouseDown += new MouseEventHandler(control_MouseDown);



                        labelcontrols.Add(lbl);


                        ControlMoverOrResizer.Init(lbl);
                        ControlMoverOrResizer.WorkType = ControlMoverOrResizer.MoveOrResize.MoveAndResize;

                    }

                    else if (row["ObjectType"].ToString() == "PictureBox")
                    {
                        PictureBox lbl = new PictureBox();
                        lbl.Name = row["ObjectName"].ToString();
                        lbl.Tag = row["tbl"].ToString();
                        // lbl.Font = fn;
                        lbl.AccessibleDescription = row["value_type"].ToString();
                        lbl.Location = new Point(Convert.ToInt32(row["ObjectX"].ToString()),
                            Convert.ToInt32(row["ObjectY"].ToString()));
                        lbl.Size = new Size(Convert.ToInt32(row["Objectwidth"].ToString()),
                            Convert.ToInt32(row["objectHieght"].ToString()));
                        lbl.Text = row["Text"].ToString();
                        lbl.AutoSize = false;
                        lbl.SizeMode = PictureBoxSizeMode.StretchImage;
                        try
                        {
                            Image im = GetCopyImage(getPath());
                            lbl.Image = im;
                        }
                        catch
                        {

                        }
                        lbl.BorderStyle = BorderStyle.FixedSingle;
                        p.Controls.Add(lbl);
                        lbl.Click += pic_Click;
                        lbl.MouseDown += new MouseEventHandler(control_MouseDown);



                        Pictureboxes.Add(lbl);


                        ControlMoverOrResizer.Init(lbl);
                        ControlMoverOrResizer.WorkType = ControlMoverOrResizer.MoveOrResize.MoveAndResize;


                    }
                }
                else
                {

                    if (row["ObjectType"].ToString() == "Label")
                    {
                        FontStyle fs1 = new FontStyle();
                        if (row["bold"].ToString() == "True")
                            fs1 = FontStyle.Bold;
                        if (row["underline"].ToString() == "True")
                            fs1 = FontStyle.Underline;
                        if (row["strikeout"].ToString() == "True")
                            fs1 = FontStyle.Strikeout;
                        System.Drawing.Font fn = new Font(row["fontname"].ToString(), float.Parse(row["fontsize"].ToString()), fs1, System.Drawing.GraphicsUnit.Point);
                        Label lbl = new Label();
                        lbl.Font = fn;
                        lbl.Location = new Point(Convert.ToInt32(row["ObjectX"].ToString()),
                            Convert.ToInt32(row["ObjectY"].ToString()));
                        lbl.Size = new Size(Convert.ToInt32(row["Objectwidth"].ToString()),
                            Convert.ToInt32(row["objectHieght"].ToString()));
                        lbl.Text = row["Text"].ToString();
                        lbl.AutoSize = false;
                        lbl.BorderStyle = BorderStyle.FixedSingle;
                        p.Controls.Add(lbl);
                        lbl.MouseDown += new MouseEventHandler(control_MouseDown);

                        ControlMoverOrResizer.Init(lbl);
                        ControlMoverOrResizer.WorkType = ControlMoverOrResizer.MoveOrResize.MoveAndResize;

                    }
                    else if (row["ObjectType"].ToString() == "PictureBox")
                    {
                        PictureBox lbl = new PictureBox();
                        lbl.Name = row["ObjectName"].ToString();
                        lbl.Tag = row["tbl"].ToString();
                        // lbl.Font = fn;
                        lbl.AccessibleDescription = row["value_type"].ToString();
                        lbl.Location = new Point(Convert.ToInt32(row["ObjectX"].ToString()),
                            Convert.ToInt32(row["ObjectY"].ToString()));
                        lbl.Size = new Size(Convert.ToInt32(row["Objectwidth"].ToString()),
                            Convert.ToInt32(row["objectHieght"].ToString()));
                        lbl.Text = row["Text"].ToString();
                        lbl.AutoSize = false;
                        lbl.SizeMode = PictureBoxSizeMode.StretchImage;
                        try
                        {
                            Image im = GetCopyImage(getPath());
                            lbl.Image = im;
                        }
                        catch
                        {

                        }
                        lbl.BorderStyle = BorderStyle.FixedSingle;
                        p.Controls.Add(lbl);
                        lbl.Click += pic_Click;
                        lbl.MouseDown += new MouseEventHandler(control_MouseDown);
                        Pictureboxes.Add(lbl);
                        ControlMoverOrResizer.Init(lbl);
                        ControlMoverOrResizer.WorkType = ControlMoverOrResizer.MoveOrResize.MoveAndResize;


                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
        }
        public void loadrect()
        {
            cmd = new SqlCommand("select * from invoice_rectangle where template='" + template_name + "'");
            rect = inf.get_genaraldata(cmd);

            foreach (DataRow row in rect.Rows)
            {
                string val = "";
                Graphics box = pcb_main.CreateGraphics();
              
                x = Convert.ToInt32(row["x1"]);
                y = Convert.ToInt32(row["y1"]);
                width = Convert.ToInt32(row["width"]);
                height = Convert.ToInt32(row["height"]);
                name = row["name"].ToString();

                val = shapes.drawRect(paintcolor, x, y, width, height);
                //  box.DrawRectangle(new Pen(new SolidBrush(paintcolor)), new Point(x, y), new Point(lx, y));
                refreshBox();
                listBox1.Items.Add(val);

            }
       }
        void label_Click(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            this.ActiveControl = lb;
            browseToolStripMenuItem.Visible = false;
        }
        void pic_Click(object sender, EventArgs e)
        {
            PictureBox lb = sender as PictureBox;
            this.ActiveControl = lb;
            browseToolStripMenuItem.Visible = true;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (cmb_template.SelectedIndex < 1)
            {
                MessageBox.Show("Please select template");
                return;

            }
            else
            {
                cmd = new SqlCommand("Select * from invoice_a4_general where template='"+cmb_template.SelectedValue.ToString()+"'");
                DataTable template = new DataTable();
                template=inf.get_genaraldata(cmd);

                cmd = new SqlCommand("Select * from invoice_a4columns where template='" + cmb_template.SelectedValue.ToString() + "'");
                DataTable columns = new DataTable();
                columns = inf.get_genaraldata(cmd);

                cmd = new SqlCommand("Select * from invoice_lines where template='" + cmb_template.SelectedValue.ToString() + "'");
                DataTable lines = new DataTable();
                lines = inf.get_genaraldata(cmd);

                cmd = new SqlCommand("Select * from invoice_rectangle where template='" + cmb_template.SelectedValue.ToString() + "'");
                DataTable rectangl = new DataTable();
                rectangl = inf.get_genaraldata(cmd);

                cmd = new SqlCommand("Select * from invoiceprnline where template='" + cmb_template.SelectedValue.ToString() + "'");
                DataTable prline = new DataTable();
                prline = inf.get_genaraldata(cmd);


                string fileName = Application.StartupPath +"\\Script of "+cmb_template.SelectedValue.ToString()+".txt";
                FileStream fs1 = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter writer = new StreamWriter(fs1);
                writer.Write(CreateScript(template, "invoice_a4_general") + Environment.NewLine + CreateScript(columns, "invoice_a4columns") + Environment.NewLine + CreateScript(lines, "invoice_lines") + Environment.NewLine + CreateScript(rectangl, "invoice_rectangle") + Environment.NewLine + CreateScript(prline, "invoiceprnline"));
                writer.Close();

                MessageBox.Show("Download Success..Please check application path");
            }
           // clear();
           // IsClear = true;


            //clearAllData();
            //if (dataGridView1.Columns.Count > 0)
            //{
            //    dataGridView1.Columns.Clear();
            //}

        }
        string CreateScript(DataTable dt,string tableName)
        {
            if (dt.Rows.Count < 1)
            {
                return "";
            }
           string scrpt = "insert into "+tableName+"(";
           string column="",values=" values ";
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                //any keyword column names
                if (dt.Columns[i].ColumnName == "LEFT" || dt.Columns[i].ColumnName == "RIGHT" || dt.Columns[i].ColumnName == "TOP" || dt.Columns[i].ColumnName == "LANGUAGE" || dt.Columns[i].ColumnName == "INDEX" || dt.Columns[i].ColumnName == "TYPE" || dt.Columns[i].ColumnName=="TEXT"||dt.Columns[i].ColumnName == "Text" || dt.Columns[i].ColumnName == "type")
                {
                   
                    column +="["+ dt.Columns[i].ColumnName+"]";
                }
                else
                {
                    column += dt.Columns[i].ColumnName;
                }
                    
                if (i + 1 != dt.Columns.Count)
                {
                    column += ",";
                }
                else
                {
                    column += ")";
                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                values += "(";
                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    if (dt.Columns[j].DataType.Equals(typeof(string)) || dt.Columns[j].DataType.Equals(typeof(bool)))
                  {
                      values +="'"+dt.Rows[i][j].ToString()+"'";
                  }
                   else
                    {
                        if (dt.Rows[i][j].ToString() == "")
                        {
                            values += "NULL";
                        }
                        else
                        {
                            values += dt.Rows[i][j].ToString();
                        }
                       
                    }
                    
                    if (j + 1 != dt.Columns.Count)
                    {
                        values += ",";
                    }
                }
               values += ")";
               if (dt.Rows.Count != i + 1)
               {
                   values += ",";
               }
            }
            scrpt+= column + values+";";
         
            return scrpt;
        }
        void clearAllData()
        {
            for (int i = 0; i <= listBox1.Items.Count; i++)
            {
                selecteddata = i;
                selectedtedtext = listBox1.Items[i].ToString();
                shapes.remove(i, selectedtedtext);
                listBox1.Items.RemoveAt(i);
                refreshBox();
            }

        }

        private void browseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //PictureBox PictureBox = (PictureBox)ctrlControl;
            //System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            //ofd.Filter = "images only.|*.jpg; *.jpeg; *.png";
            //DialogResult dr = ofd.ShowDialog();
            //PictureBox.BackgroundImage = Image.FromFile(ofd.FileName);
            //PictureBox.ImageLocation = ofd.FileName;
            

        }
        string getPath()
        {
            cmd = new SqlCommand("Select logo from Tbl_CompanySetup") ;
            string Path = inf.get_Scalar(cmd).ToString();
            return Path;
        }

        private void chkHeader_CheckedChanged(object sender, EventArgs e)
        {
         
        }
        
    }

}

