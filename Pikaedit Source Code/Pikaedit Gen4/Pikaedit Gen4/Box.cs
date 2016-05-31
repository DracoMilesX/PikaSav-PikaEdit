using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pikaedit_Gen4
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

        public byte[] saveBoxFile() //No serialization
        {
            byte[] data = new byte[4999];
            for (int i = 0; i < 30; i++)
            {
                Array.Copy(pkmdata[i].data, 0, data, (i * 136), 136);
            }
            Array.Copy(Func.getfromString(this.name, 9), 0, data, 4980, 18);
            data[4998] = wallpaper;
            return data;
        }

        public byte[] loadBoxFile(byte[] data) //No serialization
        {
            byte[] p = new byte[136];
            for (int i = 0; i < 30; i++)
            {
                Array.Copy(data, (i * 136), p, 0, 136);
                pkmdata[i] = new Pokemon(p);
            }
            name = Func.getString(data, 4980, 9);
            wallpaper = data[4998];
            return data;
        }

        public bool isEmpty()
        {
            for (int i = 0; i < 30; i++)
            {
                if (!pkmdata[i].isEmpty)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
