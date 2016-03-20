using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TownMapDisplay
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            byte[] File = System.IO.File.ReadAllBytes(Util.Path);

            TownMapTable t = new TownMapTable(File);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(t));
        }
    }
}
