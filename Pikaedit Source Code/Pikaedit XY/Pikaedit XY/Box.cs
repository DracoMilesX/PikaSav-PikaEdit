using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pikaedit_XY
{
    public class Box
    {
        public string name;
        public byte wallpaper;
        public Pokemon[] pkmdata = new Pokemon[30];

        public Box()
        {
            for (int i = 0; i < 30; i++)
            {
                pkmdata[i] = new Pokemon();
                name = "";
                wallpaper = 0;
            }
        }

        public Box(Pokemon[] pkmdata)
        {
            this.pkmdata = pkmdata;
        }

        public Box(string name, byte wallpaper)
        {
            setProperties(name, wallpaper);
        }

        public void setProperties(string name, byte wallpaper)
        {
            this.name = name;
            this.wallpaper = wallpaper;
        }
    }
}
