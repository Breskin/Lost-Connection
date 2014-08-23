using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;

namespace ld48
{
    class Program
    {
        public static Font Font = new Font("data/font.ttf");
        public static bool IsIntro = true, IsMenu = false, IsGame = false, GamePaused = false;
        public static float P1 = (float)System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / 100f;

        public Window Window;

        public Program()
        {
            Language.Load();

            Window = new Window(this);
            Window.Renderer = new Renderer(this);

            Window.Init();
        }

        static void Main(string[] args)
        {
            new Program();
        }

        public static int ParseInt(string s)
        {
            char[] c = s.ToCharArray(); int y = 0; bool negative = false;
            for (int i = 0; i < s.Length; i++)
            {
                if (i == 0 && s[i] == '-') { negative = true; } else { y = y * 10 + (s[i] - '0'); }
            }
            return (negative) ? y * -1 : y;
        }
    }
}
