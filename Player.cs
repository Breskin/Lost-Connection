using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace ld48
{
    class Player
    {
        public Vector2f Position = new Vector2f(0, 0);
        public float Speed = 0.075f;

        bool LookingRight = true;

        bool Jumping = false, Falling = false;
        float JumpSpeed = 0.075f, FallSpeed = 0.075f, JumpDistance = 1.15f;

        Text Text = new Text("", Program.Font, (uint)(Program.P1 * 2));

        string Message = "";
        int MessageTime = 0;
        bool DisplayMessage = false;

        public void Render(RenderWindow window)
        {
            Sprite s = Textures.Man;
            if (LookingRight) { s.Scale = new Vector2f(1, 1); } else { s.Scale = new Vector2f(-1, 1); }
            s.Position = new Vector2f((int)((window.Size.X - s.Texture.Size.X) / 2 + s.Texture.Size.X * ((LookingRight) ? 1.5f : 2.5f)), (int)((window.Size.Y - s.Texture.Size.Y) / 2 + s.Texture.Size.Y / 2));
            window.Draw(s);
        }

        public void RenderMessage(RenderWindow window)
        {
            if (DisplayMessage)
            {
                Text.DisplayedString = Message;

                RectangleShape r = new RectangleShape(new Vector2f((int)(Text.GetLocalBounds().Width + 50), (int)(Text.CharacterSize * 1.2f)));
                r.OutlineThickness = 1; r.FillColor = new Color(0, 0, 0, 192); r.OutlineColor = new Color(0, 0, 0, 96);
                r.Position = new Vector2f((int)((window.Size.X - r.Size.X) / 2 + Textures.Man.Texture.Size.X * 1.5f), (int)(window.Size.Y - r.Size.Y) / 2 - Textures.Man.Texture.Size.Y / 2);
                window.Draw(r);

                Text.Position = new Vector2f((int)((window.Size.X - Text.GetLocalBounds().Width) / 2 + Textures.Man.Texture.Size.X * 1.5f), (int)(window.Size.Y - r.Size.Y) / 2 - Textures.Man.Texture.Size.Y / 2);

                window.Draw(Text);
            }
        }

        public static int GetSpeakTime(string s) { return s.Length * 125; }

        public void Say(string s)
        {
            Message = s;
            MessageTime = Environment.TickCount + GetSpeakTime(s);
            DisplayMessage = true;
        }

        public void Update()
        {
            if (DisplayMessage == true && Environment.TickCount > MessageTime)
            {
                Message = "";
                DisplayMessage = false;
            }

            if (Jumping)
            {
                if (JumpDistance > 0)
                {
                    Position.Y -= JumpSpeed;
                    JumpDistance -= JumpSpeed;
                    JumpSpeed = JumpSpeed * 0.98f;
                }
                else { JumpDistance = 1.15f; FallSpeed = JumpSpeed; JumpSpeed = Speed; Jumping = false; Falling = true; }
            }
            else if (Falling)
            {
                if (World.GetBlock((int)Position.X, (int)Position.Y + 1, 0) == 0)
                {
                    Position.Y += FallSpeed;
                    if (FallSpeed < 0.5f) { FallSpeed = FallSpeed * 1.05f; }
                }
                else { Falling = false; FallSpeed = 0.075f; Position.Y = (int)Position.Y; }
            }
            else
            {
                Position.Y = (int)Position.Y;
                if (World.GetBlock((int)(Position.X + ((LookingRight) ? 0 : 0.5f)), (int)Position.Y + 1, 0) == 0)
                {
                    Falling = true;
                }
            }
        }

        public void GoLeft()
        {
            if (CanGoLeft())
            {
                Position.X -= Speed;
            }

            LookingRight = false;
        }

        public void GoRight()
        {
            if (CanGoRight())
            {
                Position.X += Speed;
            }
            
            LookingRight = true;
        }

        public bool CanGoRight()
        {
            return World.GetBlock((int)(Math.Floor(Position.X + 0.65f)), (int)Math.Floor(Position.Y + 0.75f), 0) == 0;
        }

        public bool CanGoLeft()
        {
            return World.GetBlock((int)(Math.Floor(Position.X - 0.15f)), (int)Math.Floor(Position.Y + 0.75f), 0) == 0;
        }

        public void Jump()
        {
            if (!Jumping && !Falling && World.GetBlock((int)Position.X, (int)Position.Y - 1, 0) == 0) { Jumping = true; }
        }
    }
}
