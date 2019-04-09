using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.IO;
using System.Diagnostics;
using System.Data.SqlClient;
namespace Sys_Sols_Inventory
{

    public class rectangle
    {
        public Color Color { get; set; }
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }


        public rectangle(int id, Color color, int x, int y, int width, int hight)
        {
            this.Color = color;
            this.Id = id;
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = hight;




        }
        public override string ToString()
        {
            return "rectangle " + this.Id;
        }
    }
    public    class line
    {
      

        public Color Color { get; set; }
        public int Id { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int lx { get; set; }
        public int ly { get; set; }
        public string type { get; set; }

        public line(int Id, Color paintColor, int x, int y, int lx, int ly,string type)
        {
            this.Id = Id;
            this.Color = paintColor;
            this.x = x;
            this.y = y;
            this.lx = lx;
            this.ly = ly;
            this.type = type;
        }

        public override string ToString( )
        {
            return type+"line " + this.Id;
        }

    }








 public class items
    {
       a4design a4design = new a4design();
       InvoiceMaster _invoiceMaster = new InvoiceMaster();

         public List<rectangle> rectList { get; set; }
        public List<line> lineList { get; set; }
       // public List<freehand> freehandList { get; set; }
     //   public List<ellipse> ellipseList { get; set; }
      //public List<polygon> polyList { get; set; }
        public int selectedRect { get; set; }
        public object selectedItem { get; set; }

       


        public items()
        {
          rectList = new List<rectangle>();
          lineList = new List<line>();
            

            // ellipseList = new List<ellipse>();
            //freehandList = new List<freehand>();
            //polyList = new List<polygon>();

        }
        //moving
        public void selectItem(object item)
        {
           // string a="";
            this.selectedItem = item;
          //  a = item;
           // return a;

        }


        public void savelines(string tempname ,int gridpos)
        {
           

            foreach (var items in lineList)
            {
                
                a4design.x1  = items.lx;
                a4design.x2 = items.x;
                a4design.y1 = items.y;
                a4design.y2 = items.ly;
                a4design.template_name = tempname;
                if (items.ly > gridpos-10)
                {
                    a4design.type = "footer";
                }
                else
                {
                    a4design.type = "general";

                }
                a4design.name = items.ToString();


                new invoiceLineBLL().insert_lines(a4design);
               // i++;





            }


        }




        public void saverect(string tempname, int gridpos)
        {


            foreach (var items in rectList)
            {

               a4design.x1 = items.X;
                a4design.y1 = items.Y;
                a4design.height = items.Height;
                a4design.width = items.Width;
                a4design.template_name = tempname;
                if (items.Y > gridpos - 10)
                {
                    a4design.type = "footer";
                }
                else
                {
                    a4design.type = "general";

                }
                a4design.name = items.ToString();


               new invoiceLineBLL().insert_rect(a4design);
                // i++;





            }


        }
        public void moveItem(int selecteddata,string selectedtext, int x,int y)
        {
            int index = 0;
            string val = ""; 
            try
            {

                val= selectedtext.Substring(selectedtext.Length - 2);
                try
                {

                    index = Convert.ToInt32(val);
                }
                catch
                {

                    val = selectedtext.Substring(selectedtext.Length - 1);
                    index = Convert.ToInt32(val);

                }
                
              
            }
            catch
            {


            }
        

            if (selectedtext.StartsWith("r"))
            {
                // int a = rectList. Count;
                //   int dx = Convert.ToInt32((rectList[selecteddata].X) - (lineList[selecteddata].x));

               
              


               rectList[index].X = x;
                rectList[index].Y = y;
               // lineList[selecteddata].lx = x + dx;
                //lineList[selecteddata].ly = y + dy;
            }
           else if(selectedtext.StartsWith("Hl")) 
            {

       
                int a = lineList.Count;
                int dx = Convert.ToInt32((lineList[index].lx) - (lineList[index].x));
                int dy = Convert.ToInt32((lineList[index].ly) - (lineList[index].y));

                ///  ((line)selectedItem).x = x;
                //  ((line)selectedItem).y = y;
                //((line)selectedItem).lx = x + dx;
                // ((line)selectedItem).ly = y + dy;


                lineList[index].x = x;
                lineList[index].y = y;
                lineList[index].lx = x + dx;
                lineList[index].ly = y + dy;
            }

            else if (selectedtext.StartsWith("Vl"))
            {

         
                int a = lineList.Count;
                int dx = Convert.ToInt32((lineList[index].lx) - (lineList[index].x));
                int dy = Convert.ToInt32((lineList[index].ly) - (lineList[index].y));

                ///  ((line)selectedItem).x = x;
                //  ((line)selectedItem).y = y;
                //((line)selectedItem).lx = x + dx;
                // ((line)selectedItem).ly = y + dy;


                lineList[index].x = x;
                lineList[index].y = y;
                lineList[index].lx = x + dx;
                lineList[index].ly = y + dy;
            }
            //refreshBox();
            //}
            //else if (selectedItem.GetType().ToString().StartsWith("drawing.ellipse"))
            //{
            //    ((ellipse)selectedItem).X = x;
            //    ((ellipse)selectedItem).Y = y;
            //    refreshBox();
            //}

            //else if (selectedItem.GetType().ToString().StartsWith("drawing.polygon"))
            //{
            //    int x1 = ((polygon)selectedItem).pointList[0].X;
            //    int y1 = ((polygon)selectedItem).pointList[0].Y;
            //    //offset from mouse to first point
            //    int OffsetX = x1- x;
            //    int OffsetY = y1- y;

            //    ((polygon)selectedItem).pointList[0] = new Point(x, y);
            //    for (int i = 1; i < ((polygon)selectedItem).pointList.Count; i++)
            //    {
            //        int dx = ((polygon)selectedItem).pointList[i].X - x1;
            //        int dy = ((polygon)selectedItem).pointList[i].Y - y1;

            //        ((polygon)selectedItem).pointList[i] = new Point(x+dx, y+dy);
            //    }
            //    refreshBox();
            //}
            //else if (selectedItem.GetType().ToString().StartsWith("drawing.freehand"))
            //{


            //    int x1 = ((freehand)selectedItem).pointList[0].X;
            //    int y1 = ((freehand)selectedItem).pointList[0].Y;
            //    //offset from mouse to first point
            //    int OffsetX = x1 - x;
            //    int OffsetY = y1 - y;

            //    ((freehand)selectedItem).pointList[0] = new Point(x, y);
            //    for (int i = 1; i < ((freehand)selectedItem).pointList.Count; i++)
            //    {
            //        int dx = ((freehand)selectedItem).pointList[i].X - x1;
            //        int dy = ((freehand)selectedItem).pointList[i].Y - y1;

            //        ((freehand)selectedItem).pointList[i] = new Point(x + dx, y + dy);
            //    }

            //     refreshBox();
            //  }


        }
        /*public void paste(object item)
         {
             Point offset = new Point(5,2);
             if (item.GetType().ToString().StartsWith("drawing.rectangle"))
             {
                 rectangle c = ((rectangle)item);
                 int id = rectList.Max(e => e.Id)+1;
                 Debug.WriteLine(id+" ID");

                 rectList.Add(new rectangle(id, c.Color, c.X + offset.X, c.Y + offset.Y, c.Width, c.Height)); //make rectangle item
             WindowsFormsApplication16.Form1.form.listBox1.Items.Add(rectList[rectList.Count - 1]); //added it to layer listbox
                 refreshBox();
             }
             else if (item.GetType().ToString().StartsWith("drawing.line"))
             {
                 line l = ((line)item);

                 lineList.Add(new line(lineList.Max(e => e.Id)+1, l.Color, l.x + offset.X, l.y + offset.Y, l.lx + offset.X, l.ly + offset.Y));
                 WindowsFormsApplication16.Form1.form.listBox1.Items.Add(lineList[lineList.Count - 1]);
                 refreshBox();
             }
             else if (item.GetType().ToString().StartsWith("drawing.ellipse"))
             {
                 ellipse el = ((ellipse)item);
                 ellipseList.Add(new ellipse(ellipseList.Max(e => e.Id) + 1, el.Color, el.X + offset.X, el.Y + offset.Y, el.Width, el.Height));
                 WindowsFormsApplication16.Form1.form.listBox1.Items.Add(ellipseList[ellipseList.Count - 1]);
                 refreshBox();
             }

             else if (item.GetType().ToString().StartsWith("drawing.polygon"))
             {
                 polygon p = ((polygon)item);
                 List<Point> clone = p.pointList.ConvertAll(point => new Point(point.X,point.Y));

                 for (int i = 0; i < clone.Count; i++)
                 {
                    clone[i] = new Point(clone[i].X + offset.X, clone[i].Y + offset.Y);
                 }
                 polyList.Add(new polygon(polyList.Max(e => e.Id) + 1, p.Color, clone, p.closed));
               WindowsFormsApplication16.Form1.form.listBox1.Items.Add(polyList[polyList.Count - 1]);
                 refreshBox();
             }
             else if (item.GetType().ToString().StartsWith("drawing.freehand"))
             {
                 freehand f = ((freehand)item);
                 List<Point> clone = f.pointList.ConvertAll(point => new Point(point.X, point.Y));
                 for (int i = 0; i < clone.Count; i++)
                 {
                     clone[i] = new Point(clone[i].X + offset.X, clone[i].Y + offset.Y);
                 }
                 freehandList.Add(new freehand(freehandList.Max(e => e.Id) + 1, f.Color, clone));
                 WindowsFormsApplication16.Form1.form.listBox1.Items.Add(freehandList[freehandList.Count - 1]);
                 refreshBox();

             }
         }
         */

   
        public void remove(int idx,string type)
        {
            int mes = 0;
         
            type = type.Replace(" ", "");
            string index = type.Substring(type.Length - 2);


        
            if (int.TryParse(index, out mes))
            {


                idx = Convert.ToInt32(index);
            }

            else
            {

                index = type.Substring(type.Length - 1);
                idx = Convert.ToInt32(index);

            }


            if (type.StartsWith("r"))
            {

            

                rectList.RemoveAt(idx);
              
            }
            else
            {

                lineList.RemoveAt(idx);

            }

        }

        public string drawRect(Color paintcolor, int x, int y, int width, int hight)
        {
            string val = "";
            int id = 0;
            if (rectList.Count != 0) id = rectList.Max(e => e.Id) + 1;

            rectList.Add(new rectangle(id, paintcolor, x, y, width, hight)); //make rectangle item
               int a=rectList.Count;                                                              // Form1.form.listBox1.Items.Add(rectList[rectList.Count - 1]); //added it to layer listbox
            val = rectList[rectList.Count - 1].ToString();
            return val;
            //refreshBox();
        }
        public string drawLine(Color paintcolor, Point start, Point end,string type)
         {
             string val = "";
             try {
                 int id = 0;
                 if (lineList.Count != 0) id = lineList.Max(e => e.Id) + 1;

                 lineList.Add(new line(id, paintcolor, start.X, start.Y, end.X, end.Y,type ));
                val = lineList[lineList.Count - 1].ToString();
               // invoicedesigner._invoicedesigner.listBox1.Items.Add(val);
               
                 // refreshBox();
             }
             catch (Exception ex)
             {


             }
             return val;
         }

        //}


        public void refreshBox()
        {
           
            InvTemplate._invoicedesigner.pcb_main.Invalidate(); //repaint area
        }
    }
}
//}
