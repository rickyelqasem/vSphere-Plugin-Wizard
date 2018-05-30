using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Plugin_Wizard_for_vSphere_vCenter_2._0
{
    public partial class RemovePlugin : Form
    {
       bool firsttime;

        public delegate void delPassData(string url, string username, string password, bool https, int port);
        public RemovePlugin(string vpassword, bool https, int port, bool ftopenVI)
        {
            firsttime = ftopenVI;
            InitializeComponent();
            
            //this.textBox3.Text = vpassword;
            if (https)
            {
                this.radioButton1.Checked = true;
                this.radioButton2.Checked = false;
            }
            else
            {
                this.radioButton2.Checked = true;
                this.radioButton1.Checked = false;
            }
            this.numericUpDown1.Value = port;

            if (ftopenVI)
            {
                textBox3.Font = new Font(textBox3.Font, FontStyle.Italic);
                textBox3.ForeColor = Color.Gray;
                
            }
            else
            {
                textBox3.PasswordChar = '*';
                
            }
        }



        private void button2_Click_1(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }

 
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                numericUpDown1.Value = 443;
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            Program.MyForm1.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            bool https = true;
            if (radioButton2.Checked)
            {
                https = false;
            }
            int port = (int)numericUpDown1.Value;
            Form1 form1 = new Form1();
            delPassData del = new delPassData(form1.UnRegPlugin);
            del(textBox1.Text, textBox2.Text, textBox3.Text, https, port);
            form1.Show();
            this.Close();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            textBox1.Font = new Font(textBox1.Font, FontStyle.Regular);
            textBox1.ForeColor = Color.Black;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        
                   
        {
            textBox2.Font = new Font(textBox2.Font, FontStyle.Regular);
            textBox2.ForeColor = Color.Black;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
           
            if (firsttime)
            {
                textBox3.Text = "";
                textBox3.Font = new Font(textBox3.Font, FontStyle.Italic);
                textBox3.ForeColor = Color.Gray;
                firsttime = false;
            }
            else
            {
                textBox3.Font = new Font(textBox3.Font, FontStyle.Regular);
                textBox3.ForeColor = Color.Black;
                textBox3.PasswordChar = '*';
            }
       

        }

        private void RemovePlugin_Load(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(37, 64, 97);
            button2.BackColor = Color.FromArgb(37, 64, 97);
            toolTip1.SetToolTip(radioButton1, "Use HTTPS when communicating with VI SDK");
            toolTip1.SetToolTip(radioButton2, "Use HTTP when communicating with VI SDK");
            toolTip1.SetToolTip(numericUpDown1, "The TCP port used when communicating with VI SDK");
            toolTip1.SetToolTip(textBox1, "Hostname or IP Address of your ESX or vCenter server");
            toolTip1.SetToolTip(textBox2, "vSphere User");
            toolTip1.SetToolTip(textBox3, "vSphere User Password");
            
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("Vconnection.xml");
                XmlNode root1 = xmlDoc.SelectSingleNode(@"jobs/Policy/host");
                textBox1.Text = root1.InnerText;
                XmlNode root2 = xmlDoc.SelectSingleNode(@"jobs/Policy/user");
                textBox2.Text = root2.InnerText;
            }
            catch
            {
            }
        }

        private void textBox3_Click_1(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox3.Font = new Font(textBox3.Font, FontStyle.Regular);
            textBox3.ForeColor = Color.Black;
            textBox3.PasswordChar = '*';
        }

        private void textBox2_Click_1(object sender, EventArgs e)
        {
            //textBox2.Text = "";
            textBox2.Font = new Font(textBox2.Font, FontStyle.Regular);
            textBox2.ForeColor = Color.Black;
        }

        private void textBox1_Click_1(object sender, EventArgs e)
        {
            //textBox1.Text = "";
            textBox1.Font = new Font(textBox1.Font, FontStyle.Regular);
            textBox1.ForeColor = Color.Black;
        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                numericUpDown1.Value = 80;
            }
        }

        private void RemovePlugin_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender == this)
            {
                
            }

            if (sender != this)            {
                Program.MyForm1.Show();
                this.Hide();
                
            }
        }

        private void RemovePlugin_FormClosing(object sender, FormClosingEventArgs e)
        {

           
            
        }
        

    }
}
