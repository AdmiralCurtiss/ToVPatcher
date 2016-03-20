using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAPLIST
{
    class Program
    {
        static void Main(string[] args)
        {
            // 0xCB20

            if (args.Length != 1)
            {
                Console.WriteLine("MAPLIST path/to/MAPLIST.DAT");
                return;
            }

            String Path = args[0];

            byte[] Bytes = System.IO.File.ReadAllBytes(Path);


            int i = 0;

            bool print_rename = true;

            foreach (MapName m in new MapList(Bytes).MapNames)
            {
                if (!print_rename)
                {
                    Console.WriteLine(i + " = " + m.ToString());
                }
                else
                {
                    if (m.Name3 != "dummy")
                    {
						//string stmt = "UPDATE descriptions SET desc = '" + m.Name3 + "' WHERE filename = 'VScenario" + i + "';";
						string stmt = "UPDATE descriptions SET shortdesc = '[360] '||shortdesc WHERE desc = '" + m.Name3 + "';";
						Console.WriteLine( stmt );
                    }
                }

                i++;
            }
        }
    }
}
