using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sys_Sols_Inventory.Class
{
    class Print_Direct
    {
        private int m_currentPageIndex;
        private IList<Stream> m_streams;
       

        //private DataTable LoadSalesData()
        //{
        //    // Create a new DataSet and read sales data file 
        //    //    data.xml into the first DataTable.
        //   // DataSet dataSet = new DataSet();
        //   // dataSet.ReadXml(@"..\..\data.xml");
            
        //}
        // Routine to provide to the report renderer, in order to
        //    save an image for each page of the report.
        private Stream CreateStream(string name,string fileNameExtension, Encoding encoding,string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }
        // Export the given report as an EMF (Enhanced Metafile) file.
        private void Export(LocalReport report)
        {
            string deviceInfo =
              @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>21cm</PageWidth>
                <PageHeight>29.7cm</PageHeight>
                <MarginTop>0in</MarginTop>
                <MarginLeft>0in</MarginLeft>
                <MarginRight>0in</MarginRight>
                <MarginBottom>0in</MarginBottom>
            </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream,out warnings);

            //report.Render("Image");
            
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }
        // Handler for PrintPageEvents
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        public void Print()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }
        // Create a local report for Report.rdlc, load the data,
        //    export the report to an .emf file, and print it.
        public void Run(LocalReport report)
        {
           // LocalReport report = new LocalReport();
           // string exeFolder = Application.StartupPath;
            string reportPath = report.ReportPath; //Path.Combine(exeFolder, @"Sales order.rdlc");
            report.ReportPath = reportPath;
            
            //ReportParameter[] parameters = new ReportParameter[15];
            //parameters[0] = new ReportParameter("rdlc_voucherno", vno);
            //parameters[1] = new ReportParameter("rdlc_date", date);
            //parameters[2] = new ReportParameter("rdlc_mode", mode);
            //parameters[3] = new ReportParameter("rdlc_orderno", orderno);
            //parameters[4] = new ReportParameter("rdlc_refno", refno);
            //parameters[5] = new ReportParameter("rdlc_desp", "");
            //parameters[6] = new ReportParameter("rdlc_dest", "");
            //parameters[7] = new ReportParameter("rdlc_despatch", "");
            //parameters[8] = new ReportParameter("rdlc_discharge", "");
            //parameters[9] = new ReportParameter("rdlc_company", company);
            //parameters[10] = new ReportParameter("rdlc_address", address);
            //parameters[11] = new ReportParameter("rdlc_email", "Email:+"+mail);
            //parameters[12] = new ReportParameter("rdlc_despatchadd", "");
            //parameters[13] = new ReportParameter("rdlc_custaddress", custdetails);
            //parameters[14] = new ReportParameter("rdlc_for", "For "+company);
            //report.SetParameters(parameters);
            
            ////SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\JITHIN\Test_Invoice\Test_Invoice\SYSBIZZ.mdf;Integrated Security=True;Connect Timeout=30");
            ////con.Open();
            ////SqlDataAdapter adapt = new SqlDataAdapter("select CODE AS CONTAINER_NO,CODE AS KIND_OF_PKGS,DESC_ENG AS DESCRIPTION_GOODS,MINIMUM_QTY AS DUE_ON,MINIMUM_QTY AS QTY,MINIMUM_QTY AS QTY,MINIMUM_QTY AS RATE,MINIMUM_QTY AS PER,MINIMUM_QTY AS AMOUNT from INV_ITEM_DIRECTORY", con);
          
            ////adapt.Fill(dt);
            ////con.Close();
            ////Providing DataSource for the Report
            //report.DataSources.Clear();
            
            //ReportDataSource rds = new ReportDataSource("Sales_order", dt);
            
            ////Add ReportDataSource  
            //report.DataSources.Add(rds);
            //report.Refresh();
            Export(report);
            Print();
        }

        public void Dispose()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }

    }
}
