using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory.Accounts
{
    public partial class Chart_of_Accounts : Form
    {
        //private SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionStrings);
        //private SqlCommand cmd = new SqlCommand();
        Class.ChartOfAccount ca = new Class.ChartOfAccount();
        public Chart_of_Accounts()
        {
            InitializeComponent();
            initTreeView();
        }

        void initTreeView()
        {
            try
            {

                SqlDataReader read;
                //conn.Open();
                //cmd.Parameters.Clear();
                //cmd.CommandText = "Acc_Chart";
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Connection = conn;
                string cmd = "Acc_Chart";
                TreeNode tn=null;
                //cmd.Parameters.AddWithValue("@COMMAND", "S2");
                Dictionary<string, object> parameter = new Dictionary<string, object>();
                parameter.Add("@COMMAND", "S2");
                //read = cmd.ExecuteReader();
                read = Model.DbFunctions.GetDataReaderProcedure(cmd, parameter);
                while (read.Read())
                {
                     tn = new TreeNode(read["Name"].ToString());
                    tn.Nodes.Add(new TreeNode());
                    treeView2.Nodes.Add(tn);
                   
                }
               // conn.Close();
                Model.DbFunctions.CloseConnection();
                treeView2.ExpandAll();
            }
            catch
            { }
                treeView2.BeforeExpand += treeView2_BeforeExpand;
            
        }
   

        private void Chart_of_Accounts_Load(object sender, EventArgs e)
        {
            
        }

        private void treeView2_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {

            TreeNode tNode = e.Node;
            ca.UNDER = tNode.Text;
            tNode.Nodes.Clear();

            try
            {

                //conn.Open();
                //cmd.Parameters.Clear();
                //cmd.CommandText = "Acc_Chart";
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Connection = conn;
                string cmd = "Acc_Chart";
                Dictionary<string, object> parameter = new Dictionary<string, object>();
                parameter.Add("@UNDER", tNode.Text);
                parameter.Add("@COMMAND", "S1");
                //cmd.Parameters.AddWithValue("@UNDER", tNode.Text);
                //cmd.Parameters.AddWithValue("@COMMAND", "S1");
                //SqlDataReader rdr = cmd.ExecuteReader();
                SqlDataReader rdr = Model.DbFunctions.GetDataReaderProcedure(cmd, parameter);

                while (rdr.Read())
                {
                    TreeNode child = new TreeNode(rdr["Name"].ToString());
                    if (haschildnodes(rdr["Name"].ToString()))
                    {
                        child.Nodes.Add(new TreeNode());
                        tNode.Nodes.Add(child);
                       
                    }
                    else
                    {

                        tNode.Nodes.Add(child);
                    }
                }
                //conn.Close();
                Model.DbFunctions.CloseConnection();
            }
            catch { }
        }



       
        

      

     
        public bool haschildnodes(string node)
        {
            bool status;
            DataTable dt = new DataTable();
            ca.UNDER = node;
            dt = ca.SelectUnder();
            if (dt.Rows.Count > 0)
                status = true;
            else
                status = false;
            return status;
        }
    }
}
