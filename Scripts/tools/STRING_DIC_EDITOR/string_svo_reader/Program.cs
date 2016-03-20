using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace string_svo_reader
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            int argc = args.Length;
            TSSFile TSS;
            switch (argc)
            {
                case 4:
                    TSS = new TSSFile(System.IO.File.ReadAllBytes(args[0]));
                    TSS.ImportText(System.IO.File.ReadAllBytes(args[1]));
                    if (args[3] == "-erase")
                    {
                        foreach (TSSEntry e in TSS.Entries)
                        {
                            if (e.StringENG != null)
                            {
                                e.StringENG = "";
                            }
                        }
                    }
                    System.IO.File.WriteAllBytes(args[2], TSS.Serialize());
                    break;
                case 3:
                    TSS = new TSSFile(System.IO.File.ReadAllBytes(args[0]));
                    TSS.ImportText(System.IO.File.ReadAllBytes(args[1]));
                    System.IO.File.WriteAllBytes(args[2], TSS.Serialize());
                    break;
                default:
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form1());
                    break;
            } 
        }
    }
}
