using Microsoft.Win32;
using Sys_Sols_Inventory.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Sys_Sols_Inventory.Activation
{
    public partial class Activation : Form
    {
        Model.HardwareInfo hwinfo = new Model.HardwareInfo();
        bool New;
        public Activation()
        {
            InitializeComponent();
        }

        public void focus(TextBox sender)
        {
            if (sender.Text.Length == 4)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txt_akey1_TextChanged(object sender, EventArgs e)
        {
            focus((TextBox)sender);
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (validation())
            {
                if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                    string actkey = txt_akey1.Text + txt_akey2.Text + txt_akey3.Text + txt_akey4.Text;
                    if (KeyCheck(actkey))
                    {
                        if (New)
                        {
                            if (Insertion(actkey) > 0 && UpdateKey(actkey) > 0)
                            {
                                RegistryUpdate();
                                MessageBox.Show("Product Activatated sucessfully..!\nThanks for choosing Sysbizz.", "Sysbizz", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Login login = new Login();
                                login.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Error in Activation..!", "Sysbizz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            RegistryUpdate();
                            MessageBox.Show("Product Activated sucessfully \nThanks for choosing Sysbizz.", "Sysbizz", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Application.Restart();
                            //Login login = new Login();
                            //login.Show();
                            //this.Hide();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Activation key or key allready used\nPlease contact the software vendor..!", "Sysbizz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No Internet. Check your connection Settings.!", "Sysbizz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid Activation Info..!", "Sysbizz", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //this.Close();
        }

        public bool validation()
        {
            if (txt_akey1.Text == "")
            {
                txt_akey1.Focus();
                return false;
            }
            else if (txt_akey2.Text == "")
            {
                txt_akey2.Focus();
                return false;
            }
            else if (txt_akey3.Text == "")
            {
                txt_akey3.Focus();
                return false;
            }
            else if (txt_akey4.Text == "")
            {
                txt_akey4.Focus();
                return false;
            }
            else if (txt_productid.Text == "")
            {
                txt_productid.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        public int Insertion(string ActiveKey)
        {
            //string query = "UPDATE `registration` SET `mac` = '" + Model.HardwareInfo.GetProcessorId() + "',`hdd` = '" + Model.HardwareInfo.GetHDDSerialNo() + "',`mbd` = '" + Model.HardwareInfo.GetProcessorId() + "',`expire_date` = '" + Model.HardwareInfo.GetDateTime() + "' WHERE `registration`.`regkey` = '"+ActiveKey+"';";
            string query = "INSERT INTO `registration` (`id`, `regkey`, `productid`, `mac`, `hdd`, `mbd`, `expire_date`, `users`) VALUES ('" + max_id() + "', '" + ActiveKey + "', '" + txt_productid.Text + "', '" + Model.HardwareInfo.GetMACAddress() + "', '" + Model.HardwareInfo.GetHDDSerialNo() + "', '" + Model.HardwareInfo.GetProcessorId() + "','" + Model.HardwareInfo.GetDateTime().ToString("yyyy-MM-dd") + "', '" + max_count(ActiveKey) + "');";
            int i = DbFunctionsMySQL.InsertUpdate(query);
            return i;
        }

        public int UpdateKey(string ActiveKey)
        {
            string query = "UPDATE ActivationKey SET Remaining=Remaining-1 WHERE RegKey='" + ActiveKey + "'";
            int i = DbFunctionsMySQL.InsertUpdate(query);
            return i;
        }

        public bool KeyCheck(string ActiveKey)
        {
            string query = "SELECT * FROM `ActivationKey` WHERE RegKey='" + ActiveKey + "' AND Remaining>0";
            DataTable dt = DbFunctionsMySQL.GetDataTable(query);
            if (dt.Rows.Count > 0)
            {
                New = true;
                return true;
            }
            else
            {
                if (ExistingClient(ActiveKey))
                {
                    New = false;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool ExistingClient(string ActiveKey)
        {
            string query = "SELECT * FROM `registration` WHERE regkey='" + ActiveKey + "' AND mac='" + HardwareInfo.GetMACAddress() + "' AND mbd='" + Model.HardwareInfo.GetProcessorId() + "' AND expire_date>'" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
            DataTable dt = DbFunctionsMySQL.GetDataTable(query);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public int max_id()
        {
            string query = "SELECT MAX(ID) FROM registration";
            return (Convert.ToInt32(DbFunctionsMySQL.GetAValue(query)) + 1);
        }

        public int max_count(string ActiveKey)
        {
            string query = "SELECT IFNULL(max(users),0) from registration where regkey='" + ActiveKey + "'";
            try
            {
                return (Convert.ToInt32(DbFunctionsMySQL.GetAValue(query)) + 1);
            }
            catch
            {
                return 1;
            }
        }

        private void Activation_Load(object sender, EventArgs e)
        {
            DbFunctionsMySQL.GetConnection();
        }

        public void RegistryUpdate()
        {
            insertPackageType();
            Activation_Data();
            //RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Sysol");
            //Byte[] key1 = new UTF8Encoding(true).GetBytes("SysbizzERP123456");
            //Byte[] iv = new UTF8Encoding(true).GetBytes("Sysol12345678901");
            //Byte[] data = new UTF8Encoding(true).GetBytes("Activated");
            ////fs.Write(title, 0, title.Length);
            //byte[] author = Encrypt(data, key1, iv);

            ////storing the values  
            //key.SetValue("Status", Encoding.UTF8.GetString(author));
            //key.Close();
            //FileCreation(author);
        }

        public static void insertPackageType()
        {
            string fileValue = "2";
            fileValue = Class.CryptorEngine.EncryptPackage(fileValue, true);
            byte[] data = new UTF8Encoding(true).GetBytes(fileValue);


            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\AIN");
            //storing the values  
            key.SetValue("info", fileValue);
            key.Close();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN\info.dll";
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            // Create a new file 
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN");
            }
            using (FileStream fs = File.Create(path))
            {
                // Add some text to file                    
                fs.Write(data, 0, data.Length);
            }                 

        }

        public void Activation_Data()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\AIN");
            //storing the values
            string enc1 = Class.CryptorEngine.Encrypt("365", true);
            key.SetValue("Registry", enc1);
            key.Close();

            RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\AIN");
            //storing the values
            string enc2 = Class.CryptorEngine.Encrypt("Activated", true);
            key1.SetValue("Type", enc2);
            key1.Close();
            FileCreation(enc1, enc2);
            CompanyCreation.CompanyCreation.InsertDate(DateTime.Now);
        }

        public static void FileCreation(string registrty, string type)
        {
            //......................Registry:No.of Days.....................
            string path = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN\Registry.dll";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            byte[] data = new UTF8Encoding(true).GetBytes(registrty);
            // Create a new file 
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN");
            }

            using (FileStream fs = File.Create(path))
            {
                // Add some text to file                    
                fs.Write(data, 0, data.Length);
            }
            //.................Type:Activated or Not.........................
            path = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN\Type.dll";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            data = new UTF8Encoding(true).GetBytes(type);
            // Create a new file 
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN");
            }

            using (FileStream fs = File.Create(path))
            {
                // Add some text to file                    
                fs.Write(data, 0, data.Length);
            }
            //.................Status:Date Last Used................
            path = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN\Status.dll";
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            string enc1 = Class.CryptorEngine.Encrypt(DateTime.Now.ToShortDateString(), true);
            data = new UTF8Encoding(true).GetBytes(enc1);
            // Create a new file 
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\AIN");
            }

            using (FileStream fs = File.Create(path))
            {
                // Add some text to file                    
                fs.Write(data, 0, data.Length);
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

        public void FileCreation(byte[] value)
        {
            string path =  Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\PcInfo\info.dll";
            string path1 = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\PcInfo";
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
    }
}
