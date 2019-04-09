using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Sys_Sols_Inventory
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();       
            Application.SetCompatibleTextRenderingDefault(false);           
           Application.Run(new StartUp());   
           // Application.Run(new PaymentVoucher2(0));   
            //Application.Run(new Manufacture.FrmItemIngredients());
            //HELLO WORLD!!!!!!!!!!!
        }
    }
}
