using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Sys_Sols_Inventory
{
    [RunInstaller(true)]
    public partial class sysbizzInstaller : System.Configuration.Install.Installer
    {
        public sysbizzInstaller()
        {
            InitializeComponent();
        }
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            try
            {
                string ts= this.Context.Parameters["info"].ToString();
                string result = Class.CryptorEngine.EncryptPackage(ts, true);
                byte[] data = new UTF8Encoding(true).GetBytes(result);
               // data = Encrypt(data, new UTF8Encoding(true).GetBytes("SysbizzERP123456"), new UTF8Encoding(true).GetBytes("Sysol12345678901"));
                FileCreation(data);
                // result = System.Text.Encoding.UTF8.GetString(data);
                
                RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\AIN");
                //storing the values  
                key.SetValue("info", result);
                key.Close();
                //  string ts = "sdfsdfsd";
               
            }
            catch 
            {
                
            }
            //Class1.RegisteryUpdate("datagr");

        }
        public void FileCreation(byte[] value)
        {
            string installationPath = this.Context.Parameters["assemblypath"];
            installationPath = installationPath.Substring(0, installationPath.LastIndexOf(@"\"));
            string path1 = installationPath + @"\info.dll";
            string path = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN\info.dll";
          //  MessageBox.Show(path1 +"    "+ value.ToString());
           
            //if (!Directory.Exists(path1))
            //{
            //    Directory.CreateDirectory(path1);
            //    File.Create(path);
            //    TextWriter tw = new StreamWriter(path);
            //    tw.WriteLine(value);
            //    tw.Close();
            //}
            //else if (File.Exists(path))
            //{
            //    TextWriter tw = new StreamWriter(path);
            //    tw.WriteLine(value);
            //    tw.Close();
            //}


            try
            {
                 if (File.Exists(path1))
                {
                    File.Delete(path1);
                }

                // Create a new file 
                using (FileStream fs = File.Create(path1))
                {
                    // Add some text to file                    
                    fs.Write(value, 0, value.Length);
                }
                // Check if file already exists. If yes, delete it. 
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                // Create a new file 
                using (FileStream fs = File.Create(path))
                {
                    // Add some text to file                    
                    fs.Write(value, 0, value.Length);
                }
             
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }
        static byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
        {
            using (Aes algo = Aes.Create())
            {
                using (ICryptoTransform encryptor = algo.CreateEncryptor(key, iv))
                {
                    return Crypt(data, encryptor);
                }
            }
        }

        static byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
        {
            using (Aes algo = Aes.Create())
            {
                using (ICryptoTransform decryptor = algo.CreateDecryptor(key, iv))
                {
                    return Crypt(data, decryptor);
                }
            }
        }

        static byte[] Crypt(byte[] data, ICryptoTransform cryptor)
        {
            var ms = new MemoryStream();
            using (Stream cs = new CryptoStream(ms, cryptor, CryptoStreamMode.Write))
            {
                cs.Write(data, 0, data.Length);
            }
            return ms.ToArray();
        }
    }
}
