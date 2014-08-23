using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;

namespace ld48
{
    class Window
    {
        public Program M;
        public Renderer Renderer;
        public RenderWindow window;

        public static Color ClearColor = Color.Black;

        public Window(Program b)
        {
            M = b;
        }

        public void Init()
        {
            ContextSettings ctx = new ContextSettings(); ctx.AntialiasingLevel = 8;
            window = new RenderWindow(new VideoMode(960, 540), "Lost connection", Styles.Default, ctx);
            window.SetFramerateLimit(65);

            window.Closed += new EventHandler(OnWindowClose);
            window.Resized += new EventHandler<SizeEventArgs>(OnWindowResize);
            window.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyDown);
            window.MouseButtonPressed += new EventHandler<MouseButtonEventArgs>(OnMouseClick);
            window.MouseButtonReleased += new EventHandler<MouseButtonEventArgs>(OnMouseUp);
            window.MouseMoved += new EventHandler<MouseMoveEventArgs>(OnMouseMove);
            window.MouseWheelMoved += new EventHandler<MouseWheelEventArgs>(OnMouseScroll);

            while (window.IsOpen())
            {
                window.DispatchEvents();
                window.Clear(ClearColor);

                Renderer.Render(window);

                window.Display();
            }
        }

        void OnMouseScroll(object sender, MouseWheelEventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
        }

        void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            int x = e.X, y = e.Y;
        }

        void OnMouseClick(object sender, MouseButtonEventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            int x = e.X, y = e.Y;
        }

        void OnMouseMove(object sender, MouseMoveEventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            int x = e.X, y = e.Y;
        }

        void OnKeyDown(object sender, KeyEventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;

            string key = GetKeyEvent(e);
        }

        void OnWindowClose(object sender, EventArgs e) { RenderWindow window = (RenderWindow)sender; window.Close(); }

        void OnWindowResize(object sender, SizeEventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            FloatRect f = new FloatRect(0f, 0f, window.Size.X, window.Size.Y);
            View v = new View(f);
            window.SetView(v);
        }

        public string GetKeyEvent(KeyEventArgs e)
        {
            string key = e.ToString().Substring(e.ToString().IndexOf("Code(") + 5, e.ToString().IndexOf(")", e.ToString().IndexOf("Code(") + 5) - (e.ToString().IndexOf("Code(") + 5));

            if (key.IndexOf("Num") != -1) { key = key.Substring(key.Length - 1); }
            if (e.Shift && !e.Alt && !e.Control) { key = key.ToUpper(); } else { key = key.ToLower(); }

            switch (key)
            {
                case "period": key = "."; break;
                case "PERIOD": key = ">"; break;
                case "comma": key = ","; break;
                case "COMMA": key = "<"; break;
                case "backslash": key = "\\"; break;
                case "BACKSLASH": key = "|"; break;
                case "divide": key = "/"; break;
                case "subtract": key = "-"; break;
                case "dash": key = "-"; break;
                case "DASH": key = "_"; break;
                case "tilde": key = "`"; break;
                case "TILDE": key = "~"; break;
                case "multiply": key = "*"; break;
                case "add": key = "+"; break;
                case "equal": key = "="; break;
                case "EQUAL": key = "+"; break;
                case "semicolon": key = ";"; break;
                case "SEMICOLON": key = ":"; break;
                case "slash": key = "/"; break;
                case "SLASH": key = "?"; break;
                case "quote": key = "'"; break;
                case "QUOTE": key = "\""; break;
                case "lbracket": key = "["; break;
                case "LBRACKET": key = "{"; break;
                case "rbracket": key = "]"; break;
                case "RBRACKET": key = "}"; break;
                case "space": key = " "; break;
                case "SPACE": key = " "; break;
                case "return":
                case "RETURN": key = "return"; break;
                case "1": if (e.Shift) key = "!"; break;
                case "2": if (e.Shift) key = "@"; break;
                case "3": if (e.Shift) key = "#"; break;
                case "4": if (e.Shift) key = "$"; break;
                case "5": if (e.Shift) key = "%"; break;
                case "6": if (e.Shift) key = "^"; break;
                case "7": if (e.Shift) key = "&"; break;
                case "8": if (e.Shift) key = "*"; break;
                case "9": if (e.Shift) key = "("; break;
                case "0": if (e.Shift) key = ")"; break;

                case "left": key = "left"; break;
                case "up": key = "up"; break;
                case "down": key = "down"; break;
                case "right": key = "right"; break;

                case "a": if (e.Alt && e.Control) if (!e.Shift) key = "ą"; else key = "Ą"; break;
                case "e": if (e.Alt && e.Control) if (!e.Shift) key = "ę"; else key = "Ę"; break;
                case "s": if (e.Alt && e.Control) if (!e.Shift) key = "ś"; else key = "Ś"; break;
                case "c": if (e.Alt && e.Control) if (!e.Shift) key = "ć"; else key = "Ć"; break;
                case "z": if (e.Alt && e.Control) if (!e.Shift) key = "ż"; else key = "Ż"; break;
                case "x": if (e.Alt && e.Control) if (!e.Shift) key = "ź"; else key = "Ź"; break;
                case "n": if (e.Alt && e.Control) if (!e.Shift) key = "ń"; else key = "Ń"; break;
                case "o": if (e.Alt && e.Control) if (!e.Shift) key = "ó"; else key = "Ó"; break;
                case "l": if (e.Alt && e.Control) if (!e.Shift) key = "ł"; else key = "Ł"; break;
                case "u": if (e.Alt && e.Control) key = "€"; break;

                default: if (key.ToLower() == "back") { key = key.ToLower(); } else if (key.Length > 1) key = ""; break;
            }

            return key;
        }

        public byte[] IconToByteArray(System.Drawing.Icon i)
        {
            byte[] b = new byte[i.Width * i.Height * 4];
            System.Drawing.Bitmap bm = i.ToBitmap();
            for (int x = 0; x < i.Width; x++)
            {
                for (int y = 0; y < i.Height; y++)
                {
                    int p = (y * i.Height + x) * 4;
                    b[p] = bm.GetPixel(x, y).R;
                    b[p + 1] = bm.GetPixel(x, y).G;
                    b[p + 2] = bm.GetPixel(x, y).B;
                    b[p + 3] = bm.GetPixel(x, y).A;

                    if (p == i.Width * i.Height * 4 - 4) { return b; }
                }
            }
            return b;
        }
    }
}
