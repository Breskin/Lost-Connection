using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace ld48
{
    class Menu
    {
        Program M;
        Sprite Background = new Sprite(new Texture("data/menu-background.png"));
        Sprite Logo = new Sprite(new Texture("data/logo.png"));

        Text Text = new Text("", Program.Font, (uint)(Program.P1 * 3.5f));
        List<Text> Texts = new List<Text>();
        List<Text> InGameTexts = new List<Text>();

        bool WaitingForMouse = false;

        public Menu(Program b)
        {
            M = b;

            Background.Position = new Vector2f(0, 0);

            Text.DisplayedString = Language.Text[0];
            Text.Position = new Vector2f(15, (int)(Logo.Texture.Size.Y + Program.P1 * 10f));
            Texts.Add(new Text(Text));

            Text.DisplayedString = Language.Text[1];
            Text.Position = new Vector2f(15, (int)(Text.Position.Y + Program.P1 * 7f));
            Texts.Add(new Text(Text));


            Text.DisplayedString = Language.Text[2];
            Text.Position = new Vector2f(15, (int)(Program.P1 * 10f));
            InGameTexts.Add(new Text(Text));

            Text.DisplayedString = Language.Text[3];
            Text.Position = new Vector2f(15, (int)(Text.Position.Y + Program.P1 * 7f));
            InGameTexts.Add(new Text(Text));

            Text.DisplayedString = Language.Text[1];
            Text.Position = new Vector2f(15, (int)(Text.Position.Y + Program.P1 * 7f));
            InGameTexts.Add(new Text(Text));
        }

        public void RenderMainMenu(RenderWindow window)
        {
            Background.Scale = new Vector2f(((float)M.Window.window.Size.X / 1280f), ((float)M.Window.window.Size.Y / 720f));
            window.Draw(Background);

            Logo.Position = new Vector2f((int)(M.Window.window.Size.X - Logo.Texture.Size.X) / 2, 25);
            window.Draw(Logo);

            for (int i = 0; i < Texts.Count; i++) { window.Draw(Texts[i]); }

            CheckMainMenuEvents(window);
        }

        public void RenderInGameMenu(RenderWindow window)
        {
            RectangleShape r = new RectangleShape(new Vector2f(window.Size.X, window.Size.Y));
            r.FillColor = new Color(0, 0, 0, 128); r.Position = new Vector2f(0, 0);
            window.Draw(r);

            r.Size = new Vector2f((int)(window.Size.X * 30 / 100), window.Size.Y);
            r.FillColor = Color.Black;
            window.Draw(r);

            for (int i = 0; i < InGameTexts.Count; i++) { window.Draw(InGameTexts[i]); }

            CheckInGameMenuEvents(window);
        }

        void CheckInGameMenuEvents(RenderWindow window)
        {
            Vector2i m = Mouse.GetPosition(window);
            for (int i = 0; i < InGameTexts.Count; i++)
            {
                if (m.X >= InGameTexts[i].Position.X && m.X <= InGameTexts[i].Position.X + InGameTexts[i].GetLocalBounds().Width && m.Y >= InGameTexts[i].Position.Y && m.Y <= InGameTexts[i].Position.Y + InGameTexts[i].CharacterSize * 1.1f)
                {
                    InGameTexts[i].Color = new Color(255, 255, 255, 160);

                    if (Mouse.IsButtonPressed(Mouse.Button.Left) && !WaitingForMouse) { WaitingForMouse = true; }
                    else if (!Mouse.IsButtonPressed(Mouse.Button.Left) && WaitingForMouse)
                    {
                        InGameButtonClick(i);
                        WaitingForMouse = false;
                    }
                }
                else { InGameTexts[i].Color = new Color(255, 255, 255, 255); }
            }
        }

        void CheckMainMenuEvents(RenderWindow window)
        {
            Vector2i m = Mouse.GetPosition(window);
            for (int i = 0; i < Texts.Count; i++)
            {
                if (m.X >= Texts[i].Position.X && m.X <= Texts[i].Position.X + Texts[i].GetLocalBounds().Width && m.Y >= Texts[i].Position.Y && m.Y <= Texts[i].Position.Y + Texts[i].CharacterSize * 1.1f)
                {
                    Texts[i].Color = new Color(255, 255, 255, 160);

                    if (Mouse.IsButtonPressed(Mouse.Button.Left) && !WaitingForMouse) { WaitingForMouse = true; }
                    else if (!Mouse.IsButtonPressed(Mouse.Button.Left) && WaitingForMouse)
                    {
                        ButtonClick(i);
                        WaitingForMouse = false;
                    }
                }
                else { Texts[i].Color = new Color(255, 255, 255, 255); }
            }
        }

        void ButtonClick(int button)
        {
            switch (button)
            {
                case 0: Program.IsMenu = false; Program.IsGame = true; Window.ClearColor = new Color(0, 128, 255); M.Window.Renderer.Game.LoadWorld(); break;
                case 1: Environment.Exit(0); break;
            }
        }

        void InGameButtonClick(int button)
        {
            switch (button)
            {
                case 0: Program.GamePaused = false; break;
                case 1: Program.IsGame = false; Program.IsMenu = true; Program.GamePaused = false; M.Window.Renderer.Game = new Game(M); M.Window.Renderer.Game.LoadWorld(); break;
                case 2: Environment.Exit(0); break;
            }
        }
    }
}
