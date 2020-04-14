using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices; 
using System.Text.RegularExpressions;


namespace Search_DGridView
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        //

        private void EncryptFile(string inputFile, string outputFile)
        {

            try
            {
                string password = @"myKey123"; // Your Key Here
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                string cryptFile = outputFile;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateEncryptor(key, key),
                    CryptoStreamMode.Write);

                FileStream fsIn = new FileStream(inputFile, FileMode.Open);

                int data;
                while ((data = fsIn.ReadByte()) != -1)
                    cs.WriteByte((byte)data);


                fsIn.Close();
                cs.Close();
                fsCrypt.Close();
            }
            catch
            {
                MessageBox.Show("Encryption failed!", "Error");
            }
        }
        ///<summary>
        /// Steve Lydford - 12/05/2008.
        ///
        /// Decrypts a file using Rijndael algorithm.
        ///</summary>
        ///<param name="inputFile"></param>
        ///<param name="outputFile"></param>
        private void DecryptFile(string inputFile, string outputFile)
        {

            {
                string password = @"myKey123"; // Your Key Here

                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateDecryptor(key, key),
                    CryptoStreamMode.Read);

                FileStream fsOut = new FileStream(outputFile, FileMode.Create);

                int data;
                while ((data = cs.ReadByte()) != -1)
                    fsOut.WriteByte((byte)data);

                fsOut.Close();
                cs.Close();
                fsCrypt.Close();

            }
        }



        //
        private void Form2_Load(object sender, EventArgs e)
        {

        }


        private static void EncryptFile_main(string inputFile, string outputFile, string skey)
        {
            try
            {
                using (RijndaelManaged aes = new RijndaelManaged())
                {
                    byte[] key = ASCIIEncoding.UTF8.GetBytes(skey);

                    /* This is for demostrating purposes only.
                     * Ideally you will want the IV key to be different from your key and you should always generate a new one for each encryption in other to achieve maximum security*/
                    byte[] IV = ASCIIEncoding.UTF8.GetBytes(skey);

                    using (FileStream fsCrypt = new FileStream(outputFile, FileMode.Create))
                    {
                        using (ICryptoTransform encryptor = aes.CreateEncryptor(key, IV))
                        {
                            using (CryptoStream cs = new CryptoStream(fsCrypt, encryptor, CryptoStreamMode.Write))
                            {
                                using (FileStream fsIn = new FileStream(inputFile, FileMode.Open))
                                {
                                    int data;
                                    while ((data = fsIn.ReadByte()) != -1)
                                    {
                                        cs.WriteByte((byte)data);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // failed to encrypt file
                MessageBox.Show("Encryption failed!", "Error");
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            String filesource = "C:\\sherif.txt";
            String filedestination = "C:\\coded.txt";
            String key = "7)&awhRzu*ZLobP[&:4~39@+`u?HkKZas0d=p7{>N8-{If4}/H1Lq+dY4Xm[+";
            //EncryptFile DC = new 
            EncryptFile_main(filesource, filedestination,key);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*to get the type of list view used change the view property to details
             * and to add column change the column property to add column
                */
            ListViewItem IT = new ListViewItem();
            IT.Text = "Sherif";
            IT.SubItems.Add("D41, Inike Okene Kogi state");
            IT.SubItems.Add("08164377187");
            listView1.Items.Add(IT);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = listView1.SelectedItems.Count - 1; i >= 0;i-- )
            {
                listView1.Items.RemoveAt(listView1.SelectedIndices[i]);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult st = MessageBox.Show("Do you want to close the form","demo",MessageBoxButtons.YesNo);
            if (st == DialogResult.No) {
                e.Cancel = true;
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show("Form Closed");
        }

    }
}
