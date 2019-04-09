using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using MakarovDev.ExpandCollapsePanel;
namespace Sys_Sols_Inventory
{
   


    public class InvoiceMaster
    {
        public int ID { get; set; }
        public string PrintName { get; set; }
        public string FormName { get; set; }
        public string PageSize { get; set; }
        public int MgAll { get; set; }
        public int MgRight { get; set; }
        public int MgLeft { get; set; }
        public int MgBottom { get; set; }
        public int MgTop { get; set; }
        public float width { get; set; }
        public float height { get; set; }
        public int SetAsDefault { get; set; }


    }
    public class InvoicePrnSections
    {
        public int ID { get; set; }
        public int PrintNameId { get; set; }
        public string Section { get; set; }
        public int Objectx { get; set; }
        public int objectY { get; set; }
        public int Width { get; set; }
        public int Hieght { get; set; }

    }


    public class a4design
    {
        public int id { get; set; }
        public string name { get; set; }
        public string text { get; set; }
        public string header { get; set; }
        public int startx { get; set; }
        public int endx { get; set; }
        public int starty { get; set; }
        public int endy { get; set; }
        public int index { get; set; }
        public int width { get; set; }
        public string type { get; set; }
        public string template_name { get; set; }
        public string language { get; set; }

        public int form_height { get; set; }
        public int form_width { get; set; }

        public int h_fontsize { get; set; }
        public int d_fontsize { get; set; }

        public string h_fontstyle { get; set; }
        public string d_fontstyle { get; set; }
        public string h_fontname { get; set; }
        public string d_fontname { get; set; }
        public int x1 { get; set; }
        public int x2 { get; set; }
        public int y1 { get; set; }
        public int y2 { get; set; }
        public bool HeaderRPT { get; set; }
        public bool FooterRPT { get; set; }

        public int paper_height { get; set; }
        public int paper_width { get; set; }
        public int paper_height_add { get; set; }
        public int bottom_minus { get; set; }
        //public int row_height { get; set; }


        public int line_min { get; set; }
        public bool is_preprinted { get; set; }
        public int line_bottom { get; set; }
        public int line_max { get; set; }
        public int name_lengyh { get; set; }

        public int row_height { get; set; }

        public byte[] img { get; set; }
        public int left { get; set; }
        public int right { get; set; }
        public int top { get; set; }
        public int bottom { get; set; }
        public int height { get; set; }
        public int headerheight { get; set; }
        public bool visibile { get; set; }
        public bool isdefault { get; set; }
        public bool printDescription { get; set; }
        public bool DrwaLine { get; set; }
        public bool DrwaMargin { get; set; }
        public bool printCHeader { get; set; }
        public bool printHLINE { get; set; }
        public string AmountAligmnmnt { get; set; }
        public string PaperType { get; set; }


    }

    public class InvoicePrnLine
    {
        public int ID { get; set; }
        public int PrintName { get; set; }
        public string ObjectName { get; set; }
        public string ObjectType { get; set; }
        public int Objectwidth { get; set; }
        public int objectHieght { get; set; }
        public int ObjectX { get; set; }
        public int objectY { get; set; }
        public string ObjectSection { get; set; }
        public int objectData { get; set; }
        public int bold { get; set; }
        public int underline { get; set; }
        public int strikeout { get; set; }
        public float fontsize { get; set; }
        public string fontname { get; set; }
        public string Text { get; set; }
        public string templatename { get; set; }
        public int ValueType { get; set; }
        public string type { get; set; }
        public string tbl { get; set; }
        public string section { get; set; }










    }







    public class InvoiceMasterBLL
    {

        public object InsertInvoiceDesign(InvoiceMaster _invoiceMaster)
        {
            return new InvoiceMasterDAL().InsertInvoiceDesign(_invoiceMaster);

        }

        public int UpdateInvoiceDesign(InvoiceMaster _invoiceMaster)
        {
            return new InvoiceMasterDAL().UpdateInvoiceDesign(_invoiceMaster);
        }

        public DataTable selectDesignAll(string FormName)
        {
            return new InvoiceMasterDAL().selectDesignAll(FormName);
        }

        public DataTable selectDesignAll(int id)
        {
            return new InvoiceMasterDAL().selectDesignAll(id);
        }

        public int DeleteDesign(int id)
        {
            return new InvoiceMasterDAL().DeleteDesign(id);
        }

        public int updateSetasDefault(string PrintName, int val)
        {
            return new InvoiceMasterDAL().updateSetasDefault(PrintName, val);
        }
    }

    public class InvoicePrnSectionBLL
    {
        public int insertDesign(InvoicePrnSections _invoicePrnSections)
        {
            return new IvoicePrnSectionDal().insertDesign(_invoicePrnSections);
        }


      

    }

    public class invoiceLineBLL
    {
        public int insertDesignpage(InvoicePrnLine _invoicePrnLine)
        {
            return new invoiceLineDal().insertDesignpage(_invoicePrnLine);

        }
        public int updatecolumnslines(a4design _a4design)
        {
            return new invoiceLineDal().Updatecolumns(_a4design);

        }
        public int insert_columnslines(a4design _a4design)
        {
            return new invoiceLineDal().insertcolumns(_a4design);

        }

        public int insert_lines(a4design _a4design)
        {
            return new invoiceLineDal().insertlines(_a4design);

        }

        public int insert_rect(a4design _a4design)
        {
            return new invoiceLineDal().insert_rect(_a4design);

        }
        public int delete_columnslines(string printNameId)
        {
            return new invoiceLineDal().delete_columnDesign(printNameId);

        }


        public int updatecolumnheader(a4design _a4design)
        {
            return new invoiceLineDal().update_columnheader(_a4design);

        }

        public int updategeneral(a4design _a4design)
        {
            return new invoiceLineDal().update_general(_a4design);

        }


        public int insert_image(a4design _a4design)
        {
            return new invoiceLineDal().insert_image(_a4design);

        }
        public int deletegeneral(string template)
        {
            return new invoiceLineDal().delete_general(template);

        }


        public DataTable Select_Design_Page_Header(int PrintNameId)
        {
            return new invoiceLineDal().Select_Design_Page_Header(PrintNameId);
        }

        public DataTable Select_Design_Section(int PrintNameId)
        {
            return new invoiceLineDal().Select_Design_Section(PrintNameId);
        }

        public DataTable Select_Design_Page_Footer(int PrintNameId)
        {
            return new invoiceLineDal().Select_Design_Page_Footer(PrintNameId);
        }

        public DataTable Select_Design_Report_Footer(int PrintNameId)
        {
            return new invoiceLineDal().Select_Design_Report_Footer(PrintNameId);
        }

        public int deleteDesign(string template)
        {
            return new invoiceLineDal().deleteDesign(template);
        }
        public int deleteline(string template)
        {
            return new invoiceLineDal().deleteline(template);
        }

        public int deleterect(string template)
        {
            return new invoiceLineDal().deleterect(template);
        }

        public DataTable Select_Design_Report_Header(string template)
        {
            return new invoiceLineDal().Select_Design_Report_Header(template);
        }



        public DataTable getcolumnline(string template)
        {
            return new invoiceLineDal().Select_columnline(template);
        }

        public DataTable gridmargin(string template)
        {
            return new invoiceLineDal().selectgrid_margin(template);
        }



        public DataTable selectcolumn_details()
        {
            return new invoiceLineDal().Select_columndetails();
        }




        public DataTable getgeneral(string template)
        {
            return new invoiceLineDal().Select_general(template);
        }
        public DataTable select_columns()
        {
            return new invoiceLineDal().Select_columns_design();
        }

    }




    public class InvoiceMasterDAL
    {


        public SqlConnection connection = new SqlConnection(Sys_Sols_Inventory.Properties.Settings.Default.ConnectionStrings.ToString());

        public SqlDataAdapter sda = null;
        public object InsertInvoiceDesign(InvoiceMaster _invoiceMaster)
            
        {

         
            string Query = @"INSERT INTO [INVOICE_TEMPLATE]
                                         ([PrintName]
                                         ,[FormName]
                                         ,[PageSize]
                                         ,[width]
                                         ,[hieght]
                                         ,[MgAll]
                                         ,[MgRight]
                                         ,[MgLeft]
                                         ,[MgBottom]
                                         ,[MgTop])
                                        VALUES
                                         (@PrintName, 
                                          @FormName,
                                          @PageSize,
                                          @width,
                                          @hieght,
                                          @MgAll,
                                          @MgRight,
                                          @MgLeft,
                                          @MgBottom,
                                          @MgTop)
                         Select ident_current('INVOICE_TEMPLATE')";
            SqlCommand cmd = new SqlCommand(Query);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@PrintName", SqlDbType = SqlDbType.NVarChar, Value = _invoiceMaster.PrintName });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@FormName", SqlDbType = SqlDbType.NVarChar, Value = _invoiceMaster.FormName });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@PageSize", SqlDbType = SqlDbType.NVarChar, Value = _invoiceMaster.PageSize });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@width", SqlDbType = SqlDbType.Float, Value = _invoiceMaster.width });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@hieght", SqlDbType = SqlDbType.Float, Value = _invoiceMaster.height });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@MgAll", SqlDbType = SqlDbType.Float, Value = _invoiceMaster.MgAll });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@MgRight", SqlDbType = SqlDbType.Float, Value = _invoiceMaster.MgRight });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@MgLeft", SqlDbType = SqlDbType.Float, Value = _invoiceMaster.MgLeft });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@MgBottom", SqlDbType = SqlDbType.Float, Value = _invoiceMaster.MgBottom });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@MgTop", SqlDbType = SqlDbType.Float, Value = _invoiceMaster.MgTop });
            return SqlCommands(cmd);
        }


        public int  SqlCommands(SqlCommand cmd)
        {
            int rowsaffected = 0;
           cmd.Connection= connection;
            if (connection.State == ConnectionState.Closed)
            {

                connection.Open();
            }
            rowsaffected= cmd.ExecuteNonQuery();


            if (connection.State == ConnectionState.Open)
            {

                connection.Close();
            }


            return rowsaffected;
        }


  
        public int updateSetasDefault(string PrintName, int val)
        {
            string Query = @"UPDATE [INVOICE_TEMPLATE]
                                    SET [SetAsDefault] = @SetAsDefault where [PrintName] = @PrintName ";
            SqlCommand cmd = new SqlCommand(Query);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@PrintName", SqlDbType = SqlDbType.NVarChar, Value = PrintName });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@SetAsDefault", SqlDbType = SqlDbType.Bit, Value = val });
            return    SqlCommands(cmd);
        }


        public int UpdateInvoiceDesign(InvoiceMaster _invoiceMaster)
        {
            string Query = @"UPDATE [INVOICE_TEMPLATE]
                                    SET [PrintName] = @PrintName
                                       ,[FormName] = @FormName
                                       ,[width] = @width
                                       ,[hieght] = @hieght
                                       ,[MgAll] = @MgAll
                                       ,[MgRight] = @MgRight
                                       ,[MgLeft] = @MgLeft
                                       ,[MgBottom] = @MgBottom
                                       ,[MgTop] = @MgTop
                                   WHERE ID =@ID";
            SqlCommand cmd = new SqlCommand(Query);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@PrintName", SqlDbType = SqlDbType.NVarChar, Value = _invoiceMaster.PrintName });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@FormName", SqlDbType = SqlDbType.NVarChar, Value = _invoiceMaster.FormName });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@width", SqlDbType = SqlDbType.Float, Value = _invoiceMaster.width });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@hieght", SqlDbType = SqlDbType.Float, Value = _invoiceMaster.height });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@MgAll", SqlDbType = SqlDbType.Float, Value = _invoiceMaster.MgAll });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@MgRight", SqlDbType = SqlDbType.Float, Value = _invoiceMaster.MgRight });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@MgLeft", SqlDbType = SqlDbType.Float, Value = _invoiceMaster.MgLeft });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@MgBottom", SqlDbType = SqlDbType.Float, Value = _invoiceMaster.MgBottom });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@MgTop", SqlDbType = SqlDbType.Float, Value = _invoiceMaster.MgTop });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@ID", SqlDbType = SqlDbType.Float, Value = _invoiceMaster.ID });
            return SqlCommands(cmd);
        }








        public DataTable selectDesignAll(string FormName)
        {



            string Query = "select * from INVOICE_TEMPLATE where setAsDefault=1 and  FormName=@formName";
            SqlCommand cmd = new SqlCommand(Query);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@FormName", SqlDbType = SqlDbType.NVarChar, Value = FormName });

            cmd.Connection = connection;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
         
            sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

            return dt;
        }
        public DataTable selectDesignAll(int id)
        {
            string Query = "select * from INVOICE_TEMPLATE where Id=@Id";
            SqlCommand cmd = new SqlCommand(Query);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id });
            cmd.Connection = connection;

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
          
            sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

            return dt;
        }
        public int DeleteDesign(int id)
        {
            string Query = "Delete from INVOICE_TEMPLATE where Id=@Id";
            SqlCommand cmd = new SqlCommand(Query);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id });
          return SqlCommands(cmd);
        }
    }

    public class IvoicePrnSectionDal
    {


        public SqlConnection connection = new SqlConnection(Sys_Sols_Inventory.Properties.Settings.Default.ConnectionStrings.ToString());
        public int SqlCommands(SqlCommand cmd)
        {
            int rowsaffected = 0;
            cmd.Connection = connection;
            if (connection.State == ConnectionState.Closed)
            {

                connection.Open();
            }
           
            rowsaffected = cmd.ExecuteNonQuery();


            if (connection.State == ConnectionState.Open)
            {

                connection.Close();
            }


            return rowsaffected;
        }
        public int insertDesign(InvoicePrnSections _invoicePrnSections)
        {
            int rowsaffected = 0;
            string Query = "INSERT INTO InvoicePrnSections" +
                               "([PrintNameId]," +
                           "[Section]" +
                               ",[Objectx]" +
                               ",[objectY]" +
                               ",[Width]" +
                               ",[Hieght])" +
                               "VALUES" +
                               "(@PrintNameId," +
                           "@Section," +
                               "@Objectx," +
                               "@objectY," +
                               "@Width," +
                               "@Hieght)";
            SqlCommand cmd = new SqlCommand(Query);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@PrintNameId", SqlDbType = SqlDbType.Int, Value = _invoicePrnSections.PrintNameId });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@Section", SqlDbType = SqlDbType.NVarChar, Value = _invoicePrnSections.Section });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@Objectx", SqlDbType = SqlDbType.NVarChar, Value = _invoicePrnSections.Objectx });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@ObjectY", SqlDbType = SqlDbType.NVarChar, Value = _invoicePrnSections.objectY });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@Width", SqlDbType = SqlDbType.NVarChar, Value = _invoicePrnSections.Width });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@Hieght", SqlDbType = SqlDbType.NVarChar, Value = _invoicePrnSections.Hieght });
            rowsaffected = new invoiceLineDal().SqlCommands(cmd);
            return rowsaffected;
        }



     

    }



    public class invoiceLineDal
    {
        public SqlConnection connection = new SqlConnection(Sys_Sols_Inventory.Properties.Settings.Default.ConnectionStrings.ToString());
        public SqlDataAdapter sda = null;

        public int SqlCommands(SqlCommand cmd)
        {
            int rowsaffected = 0;cmd.Connection = connection;
            cmd.Connection = connection;
            if (connection.State == ConnectionState.Closed)
            {

                connection.Open();
            }
           
            rowsaffected = cmd.ExecuteNonQuery();


            if (connection.State == ConnectionState.Open)
            {

                connection.Close();
            }
           return rowsaffected;
        }


        public void select_SqlCommands(SqlCommand cmd)
        {

            cmd.Connection = connection;
            if (connection.State == ConnectionState.Closed)
            {

                connection.Open();
            }

            cmd.ExecuteNonQuery();


            if (connection.State == ConnectionState.Open)
            {

                connection.Close();
            }


          
        }

        public int update_columnheader(a4design a4design)
        {

            SqlCommand cmd = new SqlCommand("update INVOICE_a4columns set text=@t  where name=@name");

            cmd.Parameters.Add(new SqlParameter("@t", SqlDbType.NVarChar)).Value = a4design.text;

            cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar)).Value = a4design.name;

           return SqlCommands(cmd);



        }




        public int insert_image(a4design a4design)
        {


            SqlCommand cmd = new SqlCommand(" delete from invoice_image where template=@template insert into invoice_image (image,template)values(@template,@image)");

            //   cmd.Parameters.Add(new SqlParameter("@i", SqlDbType.Int)).Value = a4design.id;

        
            cmd.Parameters.Add(new SqlParameter("@template", SqlDbType.NVarChar)).Value = a4design.template_name;
            cmd.Parameters.Add(new SqlParameter("@image", SqlDbType.VarBinary)).Value = a4design.img;

            return SqlCommands(cmd);



        }

        public int update_general(a4design a4design)
        {


            SqlCommand cmd = new SqlCommand(@"insert into INVOICE_a4_general (height,width,location_x,location_y
,rowheader_height,[left],[right],[top],[bottom],form_height,form_width,heading_fontsize,heading_fontstyle,heading_fontname
,details_fontsize,details_fontstyle,details_fontname,template,[language],paper_height,paper_width,details_height,details_bottom,
min_line,max_line,name_length,row_height,is_preprinted,line_bottom,draw_margin,draw_line,is_description,IS_CHEADER,DRAW_HLINE,AMT_ALGNMNT,paper_type,HEADER_RPT,FOOTER_RPT,is_default)values
(@height,@width,@location_x,@location_y,@rowheader_height,@left,@right,@top,@bottom,@form_height,@form_width,@heading_fontsize
,@heading_fontstyle,@heading_fontname,@details_fontsize,@details_fontstyle,@details_fontname,@template,@language,@paper_height
,@paper_width,@details_height,@details_bottom,@min_line,@max_line,@name_length,@row_height,@is_preprinted,@line_bottom,
@draw_margin,@draw_line,@is_description,@is_cheader,@DRAW_HLINE,@Alignmnt,@PaperType,@hdr_rpt,@ftr_rpt,@is_default)");

            //   cmd.Parameters.Add(new SqlParameter("@i", SqlDbType.Int)).Value = a4design.id;

            cmd.Parameters.Add(new SqlParameter("@height", SqlDbType.Int)).Value = a4design.height;

            cmd.Parameters.Add(new SqlParameter("@width", SqlDbType.Int)).Value = a4design.width;
            cmd.Parameters.Add(new SqlParameter("@location_x", SqlDbType.Int)).Value = a4design.startx;
            cmd.Parameters.Add(new SqlParameter("@location_y", SqlDbType.Int)).Value = a4design.starty;
            cmd.Parameters.Add(new SqlParameter("@rowheader_height", SqlDbType.Int)).Value = a4design.headerheight;
            cmd.Parameters.Add(new SqlParameter("@left", SqlDbType.Int)).Value = a4design.left;
            cmd.Parameters.Add(new SqlParameter("@right", SqlDbType.Int)).Value = a4design.right;
            cmd.Parameters.Add(new SqlParameter("@top", SqlDbType.Int)).Value = a4design.top;
            cmd.Parameters.Add(new SqlParameter("@bottom", SqlDbType.Int)).Value = a4design.bottom;
            cmd.Parameters.Add(new SqlParameter("@heading_fontsize", SqlDbType.Int)).Value = a4design.h_fontsize;
            cmd.Parameters.Add(new SqlParameter("@form_height", SqlDbType.Int)).Value = a4design.form_height;
            cmd.Parameters.Add(new SqlParameter("@form_width", SqlDbType.Int)).Value = a4design.form_width;
            cmd.Parameters.Add(new SqlParameter("@details_fontsize", SqlDbType.Int)).Value = a4design.d_fontsize;
            //  cmd.Parameters.Add(new SqlParameter { ParameterName = "@heading_fontstyle", SqlDbType = SqlDbType.NVarChar, Value = a4design.h_fontstyle });
            cmd.Parameters.Add(new SqlParameter("@heading_fontstyle", SqlDbType.NVarChar)).Value = a4design.h_fontstyle;
            cmd.Parameters.Add(new SqlParameter("@heading_fontname", SqlDbType.NVarChar)).Value = a4design.h_fontname;
            cmd.Parameters.Add(new SqlParameter("@details_fontstyle", SqlDbType.NVarChar)).Value = a4design.d_fontstyle;
            cmd.Parameters.Add(new SqlParameter("@details_fontname", SqlDbType.NVarChar)).Value = a4design.d_fontname;
            cmd.Parameters.Add(new SqlParameter("@template", SqlDbType.NVarChar)).Value = a4design.template_name;
            cmd.Parameters.Add(new SqlParameter("@language", SqlDbType.NVarChar)).Value = a4design.language;
            cmd.Parameters.Add(new SqlParameter("@paper_height", SqlDbType.Int)).Value = a4design.paper_height;
            cmd.Parameters.Add(new SqlParameter("@paper_width", SqlDbType.Int)).Value = a4design.paper_width;
            cmd.Parameters.Add(new SqlParameter("@details_height", SqlDbType.Int)).Value = a4design.paper_height_add;
            cmd.Parameters.Add(new SqlParameter("@details_bottom", SqlDbType.Int)).Value = a4design.bottom_minus;
            cmd.Parameters.Add(new SqlParameter("@min_line", SqlDbType.Int)).Value = a4design.line_min;
            cmd.Parameters.Add(new SqlParameter("@max_line", SqlDbType.Int)).Value = a4design.line_max;
            cmd.Parameters.Add(new SqlParameter("@name_length", SqlDbType.Int)).Value = a4design.name_lengyh;
            cmd.Parameters.Add(new SqlParameter("@row_height", SqlDbType.Int)).Value = a4design.row_height;
            cmd.Parameters.Add(new SqlParameter("@is_preprinted", SqlDbType.Bit)).Value = a4design.is_preprinted;
            cmd.Parameters.Add(new SqlParameter("@line_bottom", SqlDbType.Bit)).Value = a4design.line_max;
            cmd.Parameters.Add(new SqlParameter("@draw_margin", SqlDbType.Bit)).Value = a4design.DrwaMargin;
            cmd.Parameters.Add(new SqlParameter("@draw_line", SqlDbType.Bit)).Value = a4design.DrwaLine;
            cmd.Parameters.Add(new SqlParameter("@is_description", SqlDbType.Bit)).Value = a4design.printDescription;
            cmd.Parameters.Add(new SqlParameter("@is_cheader", SqlDbType.Bit)).Value = a4design.printCHeader;
            cmd.Parameters.Add(new SqlParameter("@DRAW_HLINE", SqlDbType.Bit)).Value = a4design.printHLINE;
            cmd.Parameters.Add(new SqlParameter("@Alignmnt", SqlDbType.VarChar)).Value = a4design.AmountAligmnmnt;
            cmd.Parameters.Add(new SqlParameter("@PaperType", SqlDbType.VarChar)).Value = a4design.PaperType;
            cmd.Parameters.Add(new SqlParameter("@hdr_rpt", SqlDbType.Bit)).Value = a4design.HeaderRPT;
            cmd.Parameters.Add(new SqlParameter("@ftr_rpt", SqlDbType.Bit)).Value = a4design.FooterRPT;
            cmd.Parameters.Add(new SqlParameter("@is_default", SqlDbType.Bit)).Value = a4design.isdefault;
            return SqlCommands(cmd);



        }


        public int delete_general(string temp)
        {
            string Query = "delete  from INVOICE_a4_general where template=@temp";
            SqlCommand cmd = new SqlCommand(Query);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@temp", SqlDbType = SqlDbType.NVarChar, Value = temp });
            return SqlCommands(cmd);

        }


        public int delete_columnDesign(string temp)
        {
            string Query = "delete  from INVOICE_A4columns where template=@temp";
            SqlCommand cmd = new SqlCommand(Query);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@temp", SqlDbType = SqlDbType.NVarChar, Value = temp });
            return SqlCommands(cmd);

        }

        public int Updatecolumns(a4design a4design)
        {
            string Query = @"UPDATE [A4columns]
                                    SET [text]=@text
                                       ,[startx] = @startx
                                       ,[starty] = @starty
                                       ,[endx] = @endx
                                       ,[endy] = @endy
                                       ,[index] = @index
                                       ,[type] = @type
                                       ,[width] = @width
                                        ,[visible]=@visible
                                        WHERE  name =@name";
            SqlCommand cmd = new SqlCommand(Query);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@name", SqlDbType = SqlDbType.NVarChar, Value = a4design.name });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@text", SqlDbType = SqlDbType.NVarChar, Value = a4design.text });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@startx", SqlDbType = SqlDbType.Int, Value = a4design.startx });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@starty", SqlDbType = SqlDbType.Int, Value = a4design.starty });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@endx", SqlDbType = SqlDbType.Int, Value = a4design.endx });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@endy", SqlDbType = SqlDbType.Int, Value = a4design.endy });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@index", SqlDbType = SqlDbType.Int, Value = a4design.index });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@type", SqlDbType = SqlDbType.NVarChar, Value = a4design.type });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@width", SqlDbType = SqlDbType.Int, Value = a4design.width });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@visible", SqlDbType = SqlDbType.Bit, Value = Convert.ToInt32(a4design.visibile) });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@ID", SqlDbType = SqlDbType.Int, Value = a4design.id });

          return SqlCommands(cmd);
        }







        public int insertcolumns(a4design a4design)
        {
            string Query = @"insert into [INVOICE_A4columns]
                                        ([name],[text],
                                           [startx]  ,
                                           [starty],
                                           [endx] ,
                                           [endy] , 
                                           [index] ,
                                           [type] ,
                                           [width],
                                           [visible],[template])
                                         values(@name,@text
                                       ,@startx
                                     ,@starty
                                       ,@endx
                                     ,@endy
                                     ,@index
                                   ,@type
                                       ,@width
                                        ,@visible
                                          ,@temp)";
            SqlCommand cmd = new SqlCommand(Query);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@name", SqlDbType = SqlDbType.NVarChar, Value = a4design.name });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@text", SqlDbType = SqlDbType.NVarChar, Value = a4design.text });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@startx", SqlDbType = SqlDbType.Int, Value = a4design.startx });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@starty", SqlDbType = SqlDbType.Int, Value = a4design.starty });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@endx", SqlDbType = SqlDbType.Int, Value = a4design.endx });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@endy", SqlDbType = SqlDbType.Int, Value = a4design.endy });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@index", SqlDbType = SqlDbType.Int, Value = a4design.index });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@type", SqlDbType = SqlDbType.NVarChar, Value = a4design.type });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@width", SqlDbType = SqlDbType.Int, Value = a4design.width });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@visible", SqlDbType = SqlDbType.Bit, Value = Convert.ToInt32(a4design.visibile) });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@ID", SqlDbType = SqlDbType.Int, Value = a4design.id });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@temp", SqlDbType = SqlDbType.NVarChar, Value = a4design.template_name });

           return SqlCommands(cmd);
        }
        public int insertlines(a4design a4design)
        {
            string Query = @"insert into [invoice_lines]
                                        ([x1],[x2],
                                           [y1]  ,
                                           [y2],[template],[type],[name])
                                         values(@x1,@x2,@y1,@y2,@temp,@type,@nm)";
            SqlCommand cmd = new SqlCommand(Query);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@x1", SqlDbType = SqlDbType.Int, Value = a4design.x1 });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@x2", SqlDbType = SqlDbType.Int, Value = a4design.x2 });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@y1", SqlDbType = SqlDbType.Int, Value = a4design.y1 });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@y2", SqlDbType = SqlDbType.Int, Value = a4design.y2 });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@temp", SqlDbType = SqlDbType.NVarChar, Value = a4design.template_name });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@type", SqlDbType = SqlDbType.NVarChar, Value = a4design.type });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@nm", SqlDbType = SqlDbType.NVarChar, Value = a4design.name });


           return SqlCommands(cmd);
        }

        public int insert_rect(a4design a4design)
        {
            string Query = @"insert into [invoice_rectangle]
                                        ([x1],
                                           [y1]  ,
                                           [height],[width],[template],[type],[name])
                                         values(@x1,@y1,@height,@width,@template,@type,@nm)";
            SqlCommand cmd = new SqlCommand(Query);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@x1", SqlDbType = SqlDbType.Int, Value = a4design.x1 });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@y1", SqlDbType = SqlDbType.Int, Value = a4design.y1 });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@height", SqlDbType = SqlDbType.Int, Value = a4design.height });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@width", SqlDbType = SqlDbType.Int, Value = a4design.width });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@template", SqlDbType = SqlDbType.NVarChar, Value = a4design.template_name });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@type", SqlDbType = SqlDbType.NVarChar, Value = a4design.type });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@nm", SqlDbType = SqlDbType.NVarChar, Value = a4design.name });


            return SqlCommands(cmd);
        }
        public int insertDesignpage(InvoicePrnLine _invoicePrnLine)
        {
            int rowsaffected = 0;
            string Query = "INSERT INTO [InvoicePrnLine]" +
           "([PrintName]" +
                           ",[ObjectName]" +
           ",[ObjectType]" +
           ",[Objectwidth]" +
           ",[objectHieght]" +
           ",[ObjectX]" +
           ",[objectY]" +
           ",[ObjectSection]" +
           ",[objectData]" +
           ",[bold]" +
           ",[underline]" +
           ",[strikeout]" +
           ",[fontsize]" +
           ",[fontname]" +
            ",[Text]" +
             ",[visible]" +
            ",[tbl]" +
           ",[type],[template],value_type)" +

            "VALUES" +
           "(@PrintName," +
            "@ObjectName," +
            "@ObjectType, " +
            "@Objectwidth," +
            "@objectHieght, " +
            "@ObjectX," +
            "@objectY," +
            "@ObjectSection," +
            "@objectData," +
            "@bold," +
            "@underline," +
            "@strikeout," +
            "@fontsize," +
            "@fontname," +
            "@Text," +
             "@visible," +
             "@tbl," +
             "@type,@temp,@vtype)";
            SqlCommand cmd = new SqlCommand(Query);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@PrintName", SqlDbType = SqlDbType.NVarChar, Value = _invoicePrnLine.PrintName });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@ObjectName", SqlDbType = SqlDbType.NVarChar, Value = _invoicePrnLine.ObjectName });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@ObjectType", SqlDbType = SqlDbType.NVarChar, Value = _invoicePrnLine.ObjectType });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@Objectwidth", SqlDbType = SqlDbType.Int, Value = _invoicePrnLine.Objectwidth });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@objectHieght", SqlDbType = SqlDbType.Int, Value = _invoicePrnLine.objectHieght });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@ObjectX", SqlDbType = SqlDbType.Int, Value = _invoicePrnLine.ObjectX });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@ObjectY", SqlDbType = SqlDbType.Int, Value = _invoicePrnLine.objectY });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@ObjectSection", SqlDbType = SqlDbType.NVarChar, Value = _invoicePrnLine.ObjectSection });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@ObjectData", SqlDbType = SqlDbType.Int, Value = _invoicePrnLine.objectData });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@bold", SqlDbType = SqlDbType.Int, Value = _invoicePrnLine.bold });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@underline", SqlDbType = SqlDbType.Int, Value = _invoicePrnLine.underline });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@strikeout", SqlDbType = SqlDbType.Int, Value = _invoicePrnLine.strikeout });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@fontsize", SqlDbType = SqlDbType.Float, Value = _invoicePrnLine.fontsize });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@fontname", SqlDbType = SqlDbType.NVarChar, Value = _invoicePrnLine.fontname });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@text", SqlDbType = SqlDbType.NVarChar, Value = _invoicePrnLine.Text });

            cmd.Parameters.Add(new SqlParameter { ParameterName = "@visible", SqlDbType = SqlDbType.Bit, Value = Convert.ToInt32(true) });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@tbl", SqlDbType = SqlDbType.NVarChar, Value = _invoicePrnLine.tbl });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@type", SqlDbType = SqlDbType.NVarChar, Value = _invoicePrnLine.section });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@temp", SqlDbType = SqlDbType.NVarChar, Value = _invoicePrnLine.templatename });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@vtype", SqlDbType = SqlDbType.Int, Value = _invoicePrnLine.ValueType });



            return SqlCommands(cmd);

          
        }



     
        public DataTable Select_Design_Page_Header(int PrintNameId)
        {
            DataTable dt = new DataTable();
            string Query = "select * from InvoicePrnLine where ObjectSection ='main_pnl' and printname=@printnameId";
            SqlCommand cmd = new SqlCommand(Query);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@PrintNameId", SqlDbType = SqlDbType.Int, Value = PrintNameId });
            cmd.Connection = connection;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            sda = new SqlDataAdapter(cmd);
         
            sda.Fill(dt);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

            return dt;
        }
        public DataTable Select_Design_Section(int PrintNameId)
        {
            DataTable dt = new DataTable();
            string Query = "select * from InvoicePrnLine where ObjectSection ='main_pnl' and printname=@printnameId";
            SqlCommand cmd = new SqlCommand(Query);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@PrintNameId", SqlDbType = SqlDbType.Int, Value = PrintNameId });
             cmd.Connection = connection;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
           
            sda = new SqlDataAdapter(cmd);
           
            sda.Fill(dt);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

            return dt;
        }
        public DataTable Select_Design_Page_Footer(int PrintNameId)
        {
            DataTable dt = new DataTable();
            string Query = "select * from InvoicePrnLine where ObjectSection ='main_pnl' and printname=@printnameId";
            SqlCommand cmd = new SqlCommand(Query);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@PrintNameId", SqlDbType = SqlDbType.Int, Value = PrintNameId });
            cmd.Connection = connection;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        
            sda = new SqlDataAdapter(cmd);
         
            sda.Fill(dt);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

            return dt;
        }
        public DataTable Select_Design_Report_Footer(int PrintNameId)
        {
            DataTable dt = new DataTable();
            string Query = "select * from InvoicePrnLine where ObjectSection ='main_pnl' and printname=@printnameId";
            SqlCommand cmd = new SqlCommand(Query);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@PrintNameId", SqlDbType = SqlDbType.Int, Value = PrintNameId });
            cmd.Connection = connection;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
       
            sda = new SqlDataAdapter(cmd);
           
            sda.Fill(dt);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

            return dt;
        }
        public DataTable Select_Design_Report_Header(string template)
        {
            DataTable dt = new DataTable();
            string Query = "select * from InvoicePrnLine where visible='True' and template=@temp";
            SqlCommand cmd = new SqlCommand(Query);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@temp", SqlDbType = SqlDbType.NVarChar, Value = template });
            cmd.Connection = connection;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        
            sda = new SqlDataAdapter(cmd);
        
            sda.Fill(dt);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

            return dt;
        }

        public DataTable Select_columns_design()
        {
            DataTable dt = new DataTable();
            string Query = "select * from INVOICE_A4columns  order by [index]";
            SqlCommand cmd = new SqlCommand(Query);
            cmd.CommandType = CommandType.Text;
            //  cmd.Parameters.Add(new SqlParameter { ParameterName = "@PrintNameId", SqlDbType = SqlDbType.Int, Value = PrintNameId });
            cmd.Connection = connection;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
      
            sda = new SqlDataAdapter(cmd);
            
            sda.Fill(dt);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

            return dt;
        }

        public DataTable Select_columnline(string template)
        {
            DataTable dt = new DataTable();
            string Query = "select * from INVOICE_A4columns where template='" + template + "'order by [index] asc";
            SqlCommand cmd = new SqlCommand(Query);
            cmd.CommandType = CommandType.Text;
            //  cmd.Parameters.Add(new SqlParameter { ParameterName = "@PrintNameId", SqlDbType = SqlDbType.Int, Value = PrintNameId });
            cmd.Connection = connection;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
           
            sda = new SqlDataAdapter(cmd);
          
            sda.Fill(dt);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

            return dt;
        }
        public DataTable selectgrid_margin(string template)
        {
            DataTable dt = new DataTable();
            string Query = "select * from INVOICE_a4_general where template='" + template + "'";
            SqlCommand cmd = new SqlCommand(Query);
            cmd.CommandType = CommandType.Text;
            //  cmd.Parameters.Add(new SqlParameter { ParameterName = "@PrintNameId", SqlDbType = SqlDbType.Int, Value = PrintNameId });
            cmd.Connection = connection;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            sda = new SqlDataAdapter(cmd);
          
            sda.Fill(dt);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

            return dt;
        }


        public DataTable Select_columndetails()
        {
            DataTable dt = new DataTable();
            string Query = "select * from INVOICE_A4columns   where visible='True'  order by [INDEX]";
            SqlCommand cmd = new SqlCommand(Query);
            cmd.CommandType = CommandType.Text;
                //  cmd.Parameters.Add(new SqlParameter { ParameterName = "@PrintNameId", SqlDbType = SqlDbType.Int, Value = PrintNameId });
                cmd.Connection = connection;
                if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        
            sda = new SqlDataAdapter(cmd);
        
            sda.Fill(dt);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

            return dt;
        }


        public DataTable Select_general(string template)
        {
            DataTable dt = new DataTable();
            string Query = "select * from INVOICE_a4_general where template='" + template + "'";
            SqlCommand cmd = new SqlCommand(Query);
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add(new SqlParameter { ParameterName = "@PrintNameId", SqlDbType = SqlDbType.Int, Value = PrintNameId });
            cmd.Connection = connection;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
           
            sda = new SqlDataAdapter(cmd);
           
            sda.Fill(dt);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

            return dt;
        }






        public int deleteDesign(string template)
        {
            string Query = "delete  from InvoicePrnLine where template=@temp";
            SqlCommand cmd = new SqlCommand(Query);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@temp", SqlDbType = SqlDbType.NVarChar, Value = template });
            return SqlCommands(cmd);

        }
        public int deleteline(string template)
        {
            string Query = "delete  from invoice_lines where template=@temp";
            SqlCommand cmd = new SqlCommand(Query);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@temp", SqlDbType = SqlDbType.NVarChar, Value = template });
            return SqlCommands(cmd);

        }

        public int deleterect(string template)
        {
            string Query = "delete  from invoice_rectangle where template=@temp";
            SqlCommand cmd = new SqlCommand(Query);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@temp", SqlDbType = SqlDbType.NVarChar, Value = template });
            return SqlCommands(cmd);

        }
    }

}
