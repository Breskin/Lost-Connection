using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;

namespace ld48
{
    class Game
    {
        Program M;
        public World World;
        public Player Player;

        bool EditMode = false, WaitingForSave = false;
        int SelectedBlock = 0, SelectedZ = 0;

        public string UsingWorld = "data/levels/main.lvl";

        int TextNumber = 9, TalkTime = 0, TalkLenght = 0;

        bool WaitForEscape = false;
        bool EndedTutorial = false;

        public Game(Program b)
        {
            M = b;

            Window.ClearColor = new Color(0, 128, 255);

            World = new World();
            Player = new Player();
        }

        public void LoadWorld()
        {
            World.FromString(UsingWorld);

            Player.Position = new Vector2f(World.PlayerStartPosition.X, World.PlayerStartPosition.Y);

            for (int y = (int)Player.Position.Y; y < World.WorldY - 1; y++)
            {
                if (World.GetBlock((int)Player.Position.X, y + 1, 0) != 0) { Player.Position.Y = y; break; }
            }
        }

        public virtual void Render(RenderWindow window)
        {
            if (!Program.GamePaused)
            {
                Player.Update();
                Update();
            }

            int Px = (int)Math.Floor((double)window.Size.X / 48 / 2); int Pwx = (int)Player.Position.X - Px - 2;
            int Py = (int)Math.Floor((double)window.Size.Y / 48 / 2); int Pwy = (int)Player.Position.Y - Py - 2;

            Player.Render(window);

            for (int x = Pwx; x < Player.Position.X + Px + 2; x++)
            {
                for (int y = Pwy; y < Player.Position.Y + Py + 2; y++)
                {
                    for (int z = 0; z < World.WorldZ; z++)
                    {
                        Sprite sp = Textures.GetTexture(World.GetBlock(x, y, z));
                        
                        int Ty = (int)(y * 48 + window.Size.Y / 2 - Player.Position.Y * 48);
                        int Tx = (int)(x * 48 + window.Size.X / 2 - (Player.Position.X - 0.5f) * 48);

                        sp.Position = new Vector2f(Tx, Ty);
                        window.Draw(sp);
                    }
                }
            }

            Player.RenderMessage(window);

            if (UsingWorld != "data/levels/intro.lvl")
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape) && !WaitForEscape) { WaitForEscape = true; }
                else if (!Keyboard.IsKeyPressed(Keyboard.Key.Escape) && WaitForEscape) { Program.GamePaused = !Program.GamePaused; WaitForEscape = false; }
            }
            else { EndedTutorial = true; }

            if (!Program.GamePaused && EndedTutorial) { CheckEvents(window); }
        }

        public virtual void Update()
        {
            if (!EndedTutorial && TalkTime + TalkLenght < Environment.TickCount)
            {
                Player.Say(Language.Text[TextNumber]); TalkTime = Environment.TickCount; TalkLenght = Player.GetSpeakTime(Language.Text[TextNumber]) + 250; TextNumber++;
                if (TextNumber == 11) { EndedTutorial = true; }
            }
            else if ((int)Player.Position.X == 26 && TextNumber == 11)
            {
                Player.Say(Language.Text[TextNumber]); TextNumber++;
            }
            else if ((int)Player.Position.X == 37 && TextNumber == 12)
            {
                Player.Say(Language.Text[TextNumber]); TextNumber++;
            }
            else if ((int)Player.Position.X == 50 && TextNumber == 13)
            {
                Player.Say(Language.Text[TextNumber]); TextNumber++;
            }
            else if ((int)Player.Position.X == 62 && TextNumber == 14)
            {
                Player.Say(Language.Text[TextNumber]); TextNumber++;
            }
            else if ((int)Player.Position.X == 100 && TextNumber == 15)
            {
                Player.Say(Language.Text[TextNumber]); TextNumber++;
            }
            else if ((int)Player.Position.X == 127 && TextNumber == 16)
            {
                Player.Say(Language.Text[TextNumber]); TextNumber++;
            }
            else if ((int)Player.Position.X == 138 && TextNumber == 17)
            {
                Player.Say(Language.Text[TextNumber]); TextNumber++;
            }
            else if ((int)Player.Position.X == 151 && TextNumber == 18)
            {
                Player.Say(Language.Text[TextNumber]); TextNumber++;
                World.SetBlock(152, 51, 0, 9);
            }
        }

        public virtual void CheckEvents(RenderWindow window)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                Player.GoLeft();
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                Player.GoRight();
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                Player.Jump();
            }

            if (EditMode)
            {
                int Mx = (int)((Player.Position.X * 48 + Mouse.GetPosition(window).X - window.Size.X / 2) / 48 - 0.5f);
                int My = (int)((Player.Position.Y * 48 + Mouse.GetPosition(window).Y - window.Size.Y / 2) / 48);

                RectangleShape sb = new RectangleShape(new Vector2f(44, 44)); sb.Origin = new Vector2f(14, 14); sb.FillColor = new Color(255, 255, 255, 128);
                int Ty = (int)(My * 48 + window.Size.Y / 2 - Player.Position.Y * 48 + 16);
                int Tx = (int)(Mx * 48 + window.Size.X / 2 - (Player.Position.X - 0.5f) * 48 + 16);
                sb.Position = new Vector2f(Tx, Ty); sb.OutlineThickness = 2; sb.OutlineColor = new Color(255, 255, 255, 192); window.Draw(sb);

                if (Mouse.IsButtonPressed(Mouse.Button.Left)) { World.SetBlock(Mx, My, SelectedZ, SelectedBlock); }

                if (Keyboard.IsKeyPressed(Keyboard.Key.Return) && !WaitingForSave) { WaitingForSave = true; }
                else if (!Keyboard.IsKeyPressed(Keyboard.Key.Return) && WaitingForSave) { WaitingForSave = false; World.Save(UsingWorld); }

                if (Keyboard.IsKeyPressed(Keyboard.Key.Num0)) { if (Keyboard.IsKeyPressed(Keyboard.Key.LShift)) SelectedZ = 0; else SelectedBlock = 0; }
                if (Keyboard.IsKeyPressed(Keyboard.Key.Num1)) { if (Keyboard.IsKeyPressed(Keyboard.Key.LShift)) SelectedZ = 1; else SelectedBlock = 1; }
                if (Keyboard.IsKeyPressed(Keyboard.Key.Num2)) { if (Keyboard.IsKeyPressed(Keyboard.Key.LShift)) SelectedZ = 2; else SelectedBlock = 2; }
                if (Keyboard.IsKeyPressed(Keyboard.Key.Num3)) { if (Keyboard.IsKeyPressed(Keyboard.Key.LShift)) SelectedZ = 3; else SelectedBlock = 3; }
                if (Keyboard.IsKeyPressed(Keyboard.Key.Num4)) { if (Keyboard.IsKeyPressed(Keyboard.Key.LShift)) SelectedZ = 4; else SelectedBlock = 4; }
                if (Keyboard.IsKeyPressed(Keyboard.Key.Num5)) { if (Keyboard.IsKeyPressed(Keyboard.Key.LShift)) SelectedZ = 5; else SelectedBlock = 5; }
                if (Keyboard.IsKeyPressed(Keyboard.Key.Num6)) { if (Keyboard.IsKeyPressed(Keyboard.Key.LShift)) SelectedZ = 6; else SelectedBlock = 6; }
                if (Keyboard.IsKeyPressed(Keyboard.Key.Num7)) { if (Keyboard.IsKeyPressed(Keyboard.Key.LShift)) SelectedZ = 7; else SelectedBlock = 7; }
                if (Keyboard.IsKeyPressed(Keyboard.Key.Num8)) { if (Keyboard.IsKeyPressed(Keyboard.Key.LShift)) SelectedZ = 8; else SelectedBlock = 8; }
                if (Keyboard.IsKeyPressed(Keyboard.Key.Num9)) { if (Keyboard.IsKeyPressed(Keyboard.Key.LShift)) SelectedZ = 9; else SelectedBlock = 9; }
            }
        }
    }
}
