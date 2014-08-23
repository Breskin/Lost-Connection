using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ld48
{
    class Language
    {
        public static string Name = "English";
        public static List<string> Text = new List<string>();

        public static void Load()
        {
            if (File.Exists("data/langs/" + Name + ".txt"))
            {
                string[] lines = File.ReadAllLines("data/langs/" + Name + ".txt");
                for (int i = 0; i < lines.Length; i++)
                {
                    Text.Add(lines[i]);
                }
            }
        }
    }
}
