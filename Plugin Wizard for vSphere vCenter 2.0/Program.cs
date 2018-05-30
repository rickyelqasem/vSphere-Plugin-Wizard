using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Plugin_Wizard_for_vSphere_vCenter_2._0
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        static public Form1 MyForm1; 

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MyForm1 = new Form1();
            Application.Run(new Form1());
        }
    }
}
