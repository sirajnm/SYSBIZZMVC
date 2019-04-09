using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;

namespace Sys_Sols_Inventory.Class
{
    class Translation
    {
        public string TranslateText(string input, string languagePair)
        {
            try
            {
                string url = String.Format("http://www.google.com/translate_t?h1=en&ie=UTF8&text={0}&langpair={1}", input, languagePair);
                WebClient webClient = new WebClient();
                webClient.Encoding = System.Text.Encoding.GetEncoding("windows-1256");
                  string result="";
                try
                {
                    result = webClient.DownloadString(url);
               
                }
                catch
                {
                 
                }
                int len = result.Length;
                result = result.Remove(0, result.IndexOf("id=result_box"));

                int len2 = result.Length;
                result = result.Remove(0, result.IndexOf("title="));
                result = result.Remove(0, result.IndexOf(">"));
                result = result.Remove(0, 1);
                result = result.Remove(result.IndexOf("</span>"));

                //   return "<span>" + result + "</span>";
                return result;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                return "";
                
            }
        }
    }
}
