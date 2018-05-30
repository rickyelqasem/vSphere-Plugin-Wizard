using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VimApi;
using System.Web.Services;
using System.Net;
using System.Xml;
using Microsoft.Win32;
using System.Threading;
using System.Collections;
using System.IO;

namespace Plugin_Wizard_for_vSphere_vCenter_2._0
{
    public partial class Form1 : Form
    {
        string vhostname = "";
        string vusername = "";
        string vpassword = "";
        string vurl = "";
        string pluginUrl = "";
        bool vhttps = true;
        int vport = 443;
        bool firsttimeOpenReg = true;

        protected VimService _service;
        protected ServiceContent _sic;
        protected ManagedObjectReference _svcRef;
        protected VimService _service2;
        protected ServiceContent _sic2;
        protected ManagedObjectReference _svcRef2;
        protected ManagedObjectReference _propCol;
        protected ManagedObjectReference _rootFolder;
        protected ManagedObjectReference _propCol2;
        protected ManagedObjectReference _rootFolder2;
        protected ServiceContent extlist;
        protected Extension ext;

        public Form1()
        {
            InitializeComponent();
            comboBox1.Text = "InventoryView.Datacenter";
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(37, 64, 97);
            
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("vxml.xml");
                XmlNode root1 = xmlDoc.SelectSingleNode("//url");
                textBox1.Text = root1.InnerText;
                //MessageBox.Show(root1.InnerText);
                XmlNode root2 = xmlDoc.SelectSingleNode("//title");
                textBox2.Text = root2.InnerText;
                XmlNode root3 = xmlDoc.SelectSingleNode("//description");
                textBox3.Text = root3.InnerText;
                XmlNode root4 = xmlDoc.SelectSingleNode("//vendor");
                textBox4.Text = root4.InnerText;
                XmlNode root5 = xmlDoc.SelectSingleNode("//@parent");
                comboBox1.Text = root5.InnerText;
            }
            catch
            {
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {

                       
            RemovePlugin removeplugin = new RemovePlugin(vpassword, vhttps, vport, firsttimeOpenReg);
            removeplugin.Show();
            this.Hide();
            

        }



        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;
            comboBox1.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            toolStripButton2.Enabled = true;
            toolStripButton3.Enabled = true;

            comboBox2.Visible = false;
            label7.Visible = false;
            button1.Visible = false;

            groupBox1.Size = new Size(402, 151);
            groupBox2.Visible = true;

             OpenFileDialog openFileDialog1 = new OpenFileDialog();

             openFileDialog1.InitialDirectory = "c:\\" ;
             openFileDialog1.Filter = "xml Files (*.xml)|*.xml|All Files (*.*)|*.*";
             openFileDialog1.FilterIndex = 1 ;
             openFileDialog1.RestoreDirectory = true ;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(openFileDialog1.OpenFile());
                    XmlNode root1 = xmlDoc.SelectSingleNode("//url");
                    textBox1.Text = root1.InnerText;
                    //MessageBox.Show(root1.InnerText);
                    XmlNode root2 = xmlDoc.SelectSingleNode("//title");
                    textBox2.Text = root2.InnerText;
                    XmlNode root3 = xmlDoc.SelectSingleNode("//description");
                    textBox3.Text = root3.InnerText;
                    XmlNode root4 = xmlDoc.SelectSingleNode("//vendor");
                    textBox4.Text = root4.InnerText;
                    XmlNode root5 = xmlDoc.SelectSingleNode("//@parent");
                    comboBox1.Text = root5.InnerText;
                    string host = Dns.GetHostName();
                    IPHostEntry he = Dns.GetHostEntry(host);
                    string fqdn = he.HostName;

                    textBox5.Text = "https://" + fqdn + "/ui/plugin/" + (System.IO.Path.GetFileNameWithoutExtension(openFileDialog1.FileName)) + ".xml";
                }
                catch
                {
                    MessageBox.Show("Error opening file");
                }
                                               
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            toolStripButton2.Enabled = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            toolStripButton2.Enabled = true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            toolStripButton2.Enabled = true;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            toolStripButton2.Enabled = true;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            toolStripButton3.Enabled = true;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            About_and_Help about = new About_and_Help();
           about.ShowDialog();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                XmlTextWriter textwriter = new XmlTextWriter("vxml.xml", null);
                textwriter.Close();
                StreamWriter sw = new StreamWriter("vxml.xml");

                sw.WriteLine("<scriptConfiguration  version=\"4.0\">");
                sw.WriteLine("<key>" + "com.vmware.pubs.sdkteam" + "</key>");
                sw.WriteLine("<description>" + textBox3.Text + "</description>");
                sw.WriteLine("<vendor>" + textBox4.Text + "</vendor>");
                sw.WriteLine("<multiVCsupported>false</multiVCsupported>");
                sw.WriteLine("<extension parent=\"" + comboBox1.SelectedItem + "\">");
                sw.WriteLine("<title locale=\"en\">" + textBox2.Text + "</title>");
                sw.WriteLine("<url  display=\"window\">" + textBox1.Text + "</url>");
                sw.WriteLine("</extension>");
                sw.WriteLine("</scriptConfiguration>");
                sw.Close();
            }
            catch
            {
            }
            
            
            this.Hide();
            RegPlugin regplugin = new RegPlugin(textBox5.Text, vpassword, vhttps, vport, firsttimeOpenReg);
            regplugin.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = "C:\\";
            saveFileDialog1.Filter = "xml Files (*.xml)|*.xml|All Files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            //read and filter the raw data 
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XmlTextWriter textwriter = new XmlTextWriter(saveFileDialog1.FileName, null);
                    textwriter.Close();
                    StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);

                    sw.WriteLine("<scriptConfiguration  version=\"4.0\">");
                    sw.WriteLine("<key>" + "com.vmware.pubs.sdkteam" + "</key>");
                    sw.WriteLine("<description>" + textBox3.Text + "</description>");
                    sw.WriteLine("<vendor>" + textBox4.Text + "</vendor>");
                    sw.WriteLine("<multiVCsupported>false</multiVCsupported>");
                    sw.WriteLine("<extension parent=\"" + comboBox1.SelectedItem + "\">");
                    sw.WriteLine("<title locale=\"en\">" + textBox2.Text + "</title>");
                    sw.WriteLine("<url  display=\"window\">" + textBox1.Text + "</url>");
                    sw.WriteLine("</extension>");
                    sw.WriteLine("</scriptConfiguration>");
                    sw.Close();
                    //get FQDN
                    string host = Dns.GetHostName();
                    IPHostEntry he = Dns.GetHostEntry(host);
                    string fqdn = he.HostName;

                    textBox5.Text = "https://" + fqdn + "/ui/plugin/" + (System.IO.Path.GetFileNameWithoutExtension(saveFileDialog1.FileName)) + ".xml";
                }
                catch
                {
                    MessageBox.Show("Error saving file");
                }
            }


        }
        public object CreateServiceRef(string svcRefVal)
        {
            System.Net.ServicePointManager.CertificatePolicy = new CertPolicy();
            ManagedObjectReference _svcRef = new ManagedObjectReference();
            _svcRef.type = "ServiceInstance";

            _svcRef.Value = svcRefVal;
            return _svcRef;
        }
        public object CreateServiceRef2(string svcRefVal)
        {
            System.Net.ServicePointManager.CertificatePolicy = new CertPolicy();
            ManagedObjectReference _svcRef2 = new ManagedObjectReference();
            _svcRef2.type = "ServiceInstance";

            _svcRef2.Value = svcRefVal;
            return _svcRef2;
        }
        public class CertPolicy : System.Net.ICertificatePolicy
        {


            public bool CheckValidationResult(System.Net.ServicePoint srvPoint, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Net.WebRequest request, int certificateProblem)
            {


                return true;
            }

        }


        public void RegPlugin(string exturl, string host, string username, string password, bool https, int port)
        {
            vhostname = host;
            vport = port;
            vhttps = https;
            vusername = username;
            vpassword = password;
            if (https)
            {
                if (port != 443)
                {
                    vurl = "https://" + host + ":" + port.ToString() + "/sdk";
                }
                else
                {
                    vurl = "https://" + host + "/sdk";
                }
            }
            else
            {
                if (port != 80)
                {
                    vurl = "http://" + host + ":" + port.ToString() + "/sdk";
                }
                else
                {
                    vurl = "http://" + host + "/sdk";
                }
            }
            pictureBox1.Visible = true;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            comboBox1.Enabled = false;
            pluginUrl = exturl;
            textBox5.Text = exturl;
            registerPluginWorker.RunWorkerAsync();
        }

        public void UnRegPlugin(string host, string username, string password, bool https, int port)
        {
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            comboBox1.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            toolStripButton2.Enabled = false;
            toolStripButton3.Enabled = false;

            comboBox2.Visible = true;
            label7.Visible = true;
            button1.Visible = true;
            groupBox2.Visible = false;
            comboBox2.Text = "Building Plugin List - Please Wait";

            label7.Location = new Point(6, 23);
            comboBox2.Location = new Point(195, 23);
            button1.Location = new Point(195, 50);
            groupBox1.Size = new Size(402, 100);
            vhostname = host;
            vport = port;
            vhttps = https;
            vusername = username;
            vpassword = password;
            if (https)
            {
                if (port != 443)
                {
                    vurl = "https://" + host + ":" + port.ToString() + "/sdk";
                }
                else
                {
                    vurl = "https://" + host + "/sdk";
                }
            }
            else
            {
                if (port != 80)
                {
                    vurl = "http://" + host + ":" + port.ToString() + "/sdk";
                }
                else
                {
                    vurl = "http://" + host + "/sdk";
                }
            }
            pictureBox1.Visible = true;
            comboBox2.Enabled = false;
            button1.Enabled = false;
            removePluginWorker.RunWorkerAsync();
            
        }

        private void registerPluginWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                
                 _service = new VimService();
                _service.Url = vurl;
                _svcRef = new ManagedObjectReference();
                _svcRef.type = "ServiceInstance";
                _svcRef.Value = "ServiceInstance";
                _service.CookieContainer = new System.Net.CookieContainer();
                CreateServiceRef("ServiceInstance");
                _sic = _service.RetrieveServiceContent(_svcRef);
                _propCol = _sic.propertyCollector;
                _rootFolder = _sic.rootFolder;



                string userName = vusername;
                string password = vpassword;

                string companyStr = textBox4.Text;
                string descStr = textBox3.Text;
                string keyStr = "com.virtualizeplanet." + textBox2.Text;
                string ext_url = pluginUrl;
                string adminEmail = "admin@company.local";
                string versionStr = "4.0";


                Description description = new Description();
                description.label = textBox3.Text;
                description.summary = descStr;
                ExtensionServerInfo esi = new ExtensionServerInfo();

                esi.url = ext_url;
                esi.description = description;
                esi.company = companyStr;
                esi.type = "com.vmware.vim.viClientScripts"; //do not change;
                esi.adminEmail = new String[] { adminEmail };
                ExtensionClientInfo eci = new ExtensionClientInfo();

                eci.version = versionStr;
                eci.description = description;
                eci.company = companyStr;
                eci.type = "com.vmware.vim.viClientScripts";
                eci.url = ext_url;
                Extension ext = new Extension();
                ext.description = description;
                ext.key = keyStr;
                ext.version = versionStr;
                ext.subjectName = "blank";
                ext.server = new ExtensionServerInfo[] { esi };
                ext.client = new ExtensionClientInfo[] { eci };

                ext.lastHeartbeatTime = DateTime.Now;




                if (_sic.sessionManager != null)
                {
                    _service.Login(_sic.sessionManager, userName, password, null);
                    ManagedObjectReference extMgrMof = _sic.extensionManager;
                    _service.RegisterExtension(extMgrMof, ext);
                }
                
                MessageBox.Show("Finished", "Completed");
                

            }
            catch
            {
                MessageBox.Show("Review your settings or plugin already exists");
            }
        }

        private void registerPluginWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pictureBox1.Visible = false;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            comboBox1.Enabled = true;

            try
            {
                XmlTextWriter textwriter = new XmlTextWriter("Vconnection.xml", null);
                textwriter.WriteStartDocument();
                textwriter.WriteStartElement("jobs");
                textwriter.WriteEndElement();
                textwriter.WriteEndDocument();
                textwriter.Close();

                XmlDocument np = new XmlDocument();

                np.Load("Vconnection.xml");
                XmlElement el = np.CreateElement("Policy");
                string Policy = "<host>" + vhostname + "</host>" +
                        "<user>" + vusername + "</user>";
                el.InnerXml = Policy;
                np.DocumentElement.AppendChild(el);
                np.Save("Vconnection.xml");
            }
            catch
            {
                MessageBox.Show("Cannot write vSphere connection file", "Error with file");

            }
        }

        private void removePluginWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string hostName = vhostname;
                //#########################setup plugin in a xml file###################


                XmlDocument xmldoc = new XmlDocument();
                XmlNode xmlnode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
                xmldoc.AppendChild(xmlnode);
                //add root element
                XmlElement xmlroot = xmldoc.CreateElement("root");
                xmldoc.AppendChild(xmlroot);

                //######################################################################


                _service2 = new VimService();
                _service2.Url = vurl;
                _svcRef2 = new ManagedObjectReference();
                _svcRef2.type = "ServiceInstance";
                _svcRef2.Value = "ServiceInstance";
                _service2.CookieContainer = new System.Net.CookieContainer();
                CreateServiceRef2("ServiceInstance");
                _sic2 = _service2.RetrieveServiceContent(_svcRef2);
                _propCol2 = _sic2.propertyCollector;
                _rootFolder2 = _sic2.rootFolder;



                String userName = vusername;
                String password = vpassword;



                if (_sic2.sessionManager != null)
                {
                    _service2.Login(_sic2.sessionManager, userName, password, null);
                    ManagedObjectReference extMOREF = _sic2.extensionManager;



                    PropertyFilterSpec spec = new PropertyFilterSpec();

                    spec.propSet = new PropertySpec[] { new PropertySpec() };
                    spec.propSet[0].all = false;
                    spec.propSet[0].allSpecified = spec.propSet[0].all;
                    spec.propSet[0].type = extMOREF.type;
                    spec.propSet[0].pathSet = new string[] { "extensionList" };
                    spec.objectSet = new ObjectSpec[] { new ObjectSpec() };
                    spec.objectSet[0].obj = extMOREF;
                    spec.objectSet[0].skip = false;




                    ObjectContent[] ocary = _service2.RetrieveProperties(_sic2.propertyCollector, new PropertyFilterSpec[] { spec });

                    


                    ArrayList ObjectList = new ArrayList();


                    if (ocary != null)
                    {

                        ObjectContent oc = null;
                        ManagedObjectReference mor = null;
                        DynamicProperty[] pcary = null;
                        DynamicProperty pc = null;


                        for (int oci = 0; oci <= ocary.Length - 1; oci++)
                        {
                            oc = ocary[oci];
                            mor = oc.obj;
                            pcary = oc.propSet;

                            for (int propi = 0; propi <= pcary.Length - 1; propi++)
                            {
                                pc = pcary[propi];
                                if ((pc.name.Equals("extensionList")))
                                {
                                    try
                                    {
                                        for (int pci = 0; ; pci++)
                                        {

                                            ObjectList.Add(((VimApi.Extension[])(pc.val))[pci].key);

                                        }
                                    }
                                    catch
                                    {
                                        //
                                    }
                                    string leftcharObjL;
                                    for (int olc = 0; olc < ObjectList.Count; olc++)
                                    {

                                        leftcharObjL = ObjectList[olc].ToString();
                                        if (leftcharObjL.StartsWith("com.virtualizeplanet."))
                                        {
                                            leftcharObjL = leftcharObjL.Remove(0, 21);
                                            //comboBox2.Items.Add(leftcharObjL);
                                            //comboBox2.SelectedIndex = 0;

                                            XmlElement xmldcmoref = xmldoc.CreateElement("", "PluginList", "");

                                            XmlAttribute newAtt = xmldoc.CreateAttribute("Position");
                                            newAtt.Value = leftcharObjL;
                                            xmldcmoref.Attributes.Append(newAtt);
                                            xmldoc.ChildNodes.Item(1).AppendChild(xmldcmoref);
                                            
                                        }
                                    }


                                }

                            }
                        }

                    }


                }

                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Indent = true;
                xmlWriterSettings.NewLineChars = Environment.NewLine + Environment.NewLine;
                XmlWriter xmlwrite = XmlWriter.Create("plugin.xml", xmlWriterSettings);
                xmldoc.Save(xmlwrite);
                xmlwrite.Close();
                removePluginWorker.CancelAsync();
            }

            catch
            {
                MessageBox.Show("Something maybe wrong with your settings");
            }
            
        }

        private void removePluginWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            comboBox2.Enabled = true;
            button1.Enabled = true;
            XmlDocument dom = new XmlDocument();
            try
            {

                dom.Load("plugin.xml");
                XmlNodeList xmlnode3 = dom.SelectNodes("//PluginList[@Position]");
                foreach (XmlNode node in xmlnode3)
                {
                    comboBox2.Items.Add(node.Attributes.Item(0).Value);
                    comboBox2.SelectedIndex = 0;
                }

            }
            catch
            {
                MessageBox.Show("Cannot load plugin list", "Error with Plugin List");
            }

           

            try
            {
                XmlTextWriter textwriter = new XmlTextWriter("Vconnection.xml", null);
                textwriter.WriteStartDocument();
                textwriter.WriteStartElement("jobs");
                textwriter.WriteEndElement();
                textwriter.WriteEndDocument();
                textwriter.Close();

                XmlDocument np = new XmlDocument();

                np.Load("Vconnection.xml");
                XmlElement el = np.CreateElement("Policy");
                string Policy = "<host>" + vhostname + "</host>" +
                        "<user>" + vusername + "</user>";
                el.InnerXml = Policy;
                np.DocumentElement.AppendChild(el);
                np.Save("Vconnection.xml");
            }
            catch
            {
                MessageBox.Show("Cannot write vSphere connection file", "Error with file");

            }
            pictureBox1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try 
            {
                _service = new VimService();
                _service.Url = vurl;
                _svcRef = new ManagedObjectReference();
                _svcRef.type = "ServiceInstance";
                _svcRef.Value = "ServiceInstance";
                _service.CookieContainer = new System.Net.CookieContainer();
                CreateServiceRef("ServiceInstance");
                _sic = _service.RetrieveServiceContent(_svcRef);
                _propCol = _sic.propertyCollector;
                _rootFolder = _sic.rootFolder;

                Extension ext = new Extension();
                ext.key = "com.virtualizeplanet." + comboBox2.SelectedItem.ToString();
                //sdsd

                if (_sic.sessionManager != null)
                {
                    _service.Login(_sic.sessionManager, vusername, vpassword, null);
                    ManagedObjectReference extMgrMof = _sic.extensionManager;
                    _service.UnregisterExtension(extMgrMof, ext.key);
                }
                int c2index = comboBox2.SelectedIndex;
                comboBox2.Items.RemoveAt(c2index);
                comboBox2.Text = "";
                MessageBox.Show("Done");
            }
            catch
            {
                MessageBox.Show("Something may have gone wrong");
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
