using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pikaedit_Lib
{
    /// <summary>
    /// Represents a Pokemon save file Box
    /// </summary>
    public class Box
    {
        public string name;
        public byte wallpaper;
        /// <summary>
        /// Contains pokemon slots
        /// </summary>
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

        /// <summary>
        /// Set box properties
        /// </summary>
        /// <param name="name">Box name</param>
        /// <param name="wallpaper">Box wallpaper as a byte</param>
        public void setProperties(string name, byte wallpaper)
        {
            this.name = name;
            this.wallpaper = wallpaper;
        }

        /// <summary>
        /// Get number of non-empty slots in the box
        /// </summary>
        /// <returns>Number of non-empty slots</returns>
        public int getCount()
        {
            int c = 0;
            for (int i = 0; i < 30; i++)
            {
                if (!pkmdata[i].isEmpty)
                {
                    c++;
                }
            }
            return c;
        }

        /// <summary>
        /// Create a byte[] containing box data for a Pikaedit .sfb file
        /// </summary>
        /// <returns>byte[] containing box pokemon data, name and wallpaper</returns>
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

        /// <summary>
        /// Load a byte[] containing box data from a Pikaedit .sfb file
        /// </summary>
        /// <param name="data">byte[] of a .sfb file</param>
        public void loadBoxFile(byte[] data) //No serialization
        {
            byte[] p = new byte[136];
            for (int i = 0; i < 30; i++)
            {
                Array.Copy(data, (i * 136), p, 0, 136);
                pkmdata[i] = new Pokemon(p);
            }
            name = Func.getString(data, 4980, 9);
            wallpaper = data[4998];
        }

        /// <summary>
        /// Tells if the box is empty or not
        /// </summary>
        /// <returns>true if the box is empty, false if there is at least one pokemon</returns>
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
