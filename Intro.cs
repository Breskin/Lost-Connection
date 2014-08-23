using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;

namespace ld48
{
    class Intro : Game
    {
        List<Sprite> Bombs = new List<Sprite>();

        public int IntroStart = 0;
        public int GameTime = 0;
        public int TextNumber = 0;
        public int Step = 0;
        public int Alpha = 0;

        public Intro(Program b)
            : base(b)
        {
            Window.ClearColor = new Color(255, 48, 16);

            Sprite s = new Sprite(Textures.Bomb);
            s.Position = new Vector2f(new Random(Environment.TickCount).Next(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width), -new Random(Environment.TickCount).Next(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height));
            Bombs.Add(s);

            IntroStart = Environment.TickCount;
        }

        public override void Update()
        {
            
        }

        public override void Render(RenderWindow window)
        {
            for (int i = 0; i < Bombs.Count; i++)
            {
                window.Draw(Bombs[i]);
                Bombs[i].Position = new Vector2f(Bombs[i].Position.X + 0.035f, Bombs[i].Position.Y + 0.75f);

                if (Bombs[i].Position.Y > window.Size.Y + 5) { Bombs[i].Position = new Vector2f(new Random(Environment.TickCount).Next(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width), -new Random(Environment.TickCount).Next(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height)); }
            }

            if (Bombs.Count < 7)
            {
                Sprite s = new Sprite(Textures.Bomb);
                s.Position = new Vector2f(new Random(Environment.TickCount).Next(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width), -new Random(Environment.TickCount).Next(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height));
                Bombs.Add(s);
            }

            base.Render(window);
        }

        public override void CheckEvents(SFML.Graphics.RenderWindow window)
        {
            int t = Environment.TickCount - IntroStart - 1000;

            if (t == GameTime && Step == 0)
            {
                Player.Say(Language.Text[4 + TextNumber]); GameTime += Player.GetSpeakTime(Language.Text[4 + TextNumber]) + 250; TextNumber++;
                if (TextNumber == 3) { Step++; IntroStart = Environment.TickCount; GameTime = 0; TextNumber = 0; }
            }
            else if (t > 0 && Step == 1 && Player.Position.X < 168.5f)
            {
                if (!Player.CanGoRight()) { Player.Jump(); } Player.GoRight();
                if (Player.Position.X >= 168.5f) { Step++; IntroStart = Environment.TickCount; GameTime = 0; }
            }
            else if (t == GameTime && Step == 2)
            {
                Player.Say(Language.Text[7 + TextNumber]); GameTime += Player.GetSpeakTime(Language.Text[7 + TextNumber]) + 250; TextNumber++;
                if (TextNumber == 2) { Step++; IntroStart = Environment.TickCount; GameTime = 0; TextNumber = 0; }
            }
            else if (t > 0 && Step == 3)
            {
                if (!Player.CanGoRight()) { Player.Jump(); } Player.GoRight();
                if (Player.Position.X >= 170.15f) { Step++; IntroStart = Environment.TickCount + 1000; GameTime = 0; }
            }
            else if (t > 0 && Step == 4)
            {
                RectangleShape r = new RectangleShape(new Vector2f(window.Size.X, window.Size.Y));
                r.Position = new Vector2f(0, 0); r.FillColor = new Color(0, 0, 0, (byte)Alpha); window.Draw(r);

                Alpha += 5; if (Alpha >= 255) { Step++; Program.IsIntro = false; Program.IsMenu = true; }
            }
        }
    }
}
