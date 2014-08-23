using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;

namespace ld48
{
    class Textures
    {
        static Sprite nothing      = new Sprite(new Texture(new Image(1, 1, new byte[] {0, 0, 0, 0}))),
                      ground       = new Sprite(new Texture("data/img/ground.png")),
                      grass        = new Sprite(new Texture("data/img/grass.png")),
                      stone        = new Sprite(new Texture("data/img/stone.png")),
                      wood         = new Sprite(new Texture("data/img/wood.png")),
                      leaves       = new Sprite(new Texture("data/img/leaves.png")),
                      portaldown   = new Sprite(new Texture("data/img/portal-down.png")),
                      portalup     = new Sprite(new Texture("data/img/portal-up.png")),
                      off          = new Sprite(new Texture("data/img/off.png")),
                      on           = new Sprite(new Texture("data/img/on.png"));

        public static Sprite Man      = new Sprite(new Texture("data/img/man.png")),
                             Bomb     = new Sprite(new Texture("data/img/bomb.png"));

        public static int Ground      = 1,
                          Grass       = 2,
                          Stone       = 3,
                          Wood        = 4,
                          Leaves      = 5,
                          PortalDown  = 6,
                          PortalUp    = 7,
                          Off         = 8,
                          On          = 9;

        public static Sprite GetTexture(int id)
        {
            if (id == Ground) { return ground; }
            else if (id == Grass) { return grass; }
            else if (id == Stone) { return stone; }
            else if (id == Wood) { return wood; }
            else if (id == Leaves) { return leaves; }
            else if (id == PortalDown) { return portaldown; }
            else if (id == PortalUp) { return portalup; }
            else if (id == Off) { return off; }
            else if (id == On) { return on; }


            return nothing;
        }
    }
}
