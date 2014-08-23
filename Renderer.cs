using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace ld48
{
    class Renderer
    {
        public Program M;

        public Menu Menu;
        public Game Game;
        public Intro Intro;

        bool WaitForEscape = false;

        public Renderer(Program b)
        {
            M = b;

            Menu = new Menu(M);
            Game = new Game(M); Game.LoadWorld();
            Intro = new Intro(M); Intro.UsingWorld = "data/levels/intro.lvl"; Intro.LoadWorld();
        }

        public void Render(RenderWindow window)
        {
            if (Program.IsIntro)
            {
                Intro.Render(window);
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape) && !WaitForEscape) { WaitForEscape = true; }
                else if (!Keyboard.IsKeyPressed(Keyboard.Key.Escape) && WaitForEscape) { /*Program.IsIntro = false; Program.IsMenu = true;*/Intro.Step = 4; ; Intro.IntroStart = Environment.TickCount + 1000; Intro.GameTime = 0; WaitForEscape = false; }
            }
            else if (Program.IsMenu)
            {
                Menu.RenderMainMenu(window);
            }
            else if (Program.IsGame)
            {
                Game.Render(window);
                if (Program.GamePaused) { Menu.RenderInGameMenu(window); }
            }
        }
    }
}
