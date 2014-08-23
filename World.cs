using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;

namespace ld48
{
    class World
    {
        public static int WorldX = 512, WorldY = 64, WorldZ = 2;
        static int[,,] W = new int[WorldX, WorldY, WorldZ];

        public static Vector2f PlayerStartPosition = new Vector2f(0, 0);

        public World()
        {
            /*for (int x = 0; x < WorldX; x++)
            {
                for (int y = 0; y < WorldY; y++)
                {
                    if (y > 32) { W[x, y, 0] = 1; } else { W[x, y, 0] = 0; }
                }
            }

            W[3, 32,0] = 2;*/
        }

        public static int GetBlock(int x, int y, int z)
        {
            if (x >= 0 && x < WorldX && y >= 0 && y < WorldY && z >= 0 && z < WorldZ)
            {
                return W[x, y, z];
            }

            return 0;
        }

        public bool SetBlock(int x, int y, int z , int id)
        {
            if (x >= 0 && x < WorldX && y >= 0 && y < WorldY && z >= 0 && z < WorldZ)
            {
                W[x, y, z] = id;
                return true;
            }

            return false;
        }

        public void FromString(string file)
        {
            string w = System.IO.File.ReadAllText(file);

            string[] p1 = w.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

            if (p1.Length >= 2)
            {
                string[] wd = p1[0].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (wd.Length >= 5)
                {
                    WorldX = Program.ParseInt(wd[0]);
                    WorldY = Program.ParseInt(wd[1]);
                    WorldZ = Program.ParseInt(wd[2]);
                    PlayerStartPosition.X = Program.ParseInt(wd[3]);
                    PlayerStartPosition.Y = Program.ParseInt(wd[4]);

                    string[] wx = p1[1].Split(new string[] { "!" }, StringSplitOptions.RemoveEmptyEntries);
                    if (wx.Length == WorldX)
                    {
                        for (int x = 0; x < WorldX; x++)
                        {
                            string[] wy = wx[x].Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                            if (wy.Length == WorldY)
                            {
                                for (int y = 0; y < WorldY; y++)
                                {
                                    string[] wz = wy[y].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                    if (wz.Length == WorldZ)
                                    {
                                        for (int z = 0; z < WorldZ; z++)
                                        {
                                            W[x, y, z] = Program.ParseInt(wz[z]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(WorldX + "," + WorldY + "," + WorldZ + "," + PlayerStartPosition.X + "," + PlayerStartPosition.Y + "|");

            for (int x = 0; x < WorldX; x++)
            {
                for (int y = 0; y < WorldY; y++)
                {
                    for (int z = 0; z < WorldZ; z++)
                    {
                        sb.Append(W[x, y, z]);
                        if (z != WorldZ - 1) { sb.Append(","); }
                    }

                    if (y != WorldY - 1) { sb.Append("/"); }
                }
                if (x != WorldX - 1) { sb.Append("!"); }
            }

            return sb.ToString();
        }

        public void Save(string path)
        {
            System.IO.File.WriteAllText(path, this.ToString());
        }
    }
}
