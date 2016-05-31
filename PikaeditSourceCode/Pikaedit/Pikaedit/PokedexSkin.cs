using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pikaedit
{
    public class PokedexSkin
    {
        public byte[] data = new byte[0x6200];
        public bool active;
        public readonly uint MAXLENGTH = 0x6200;

        public PokedexSkin()
        {

        }

        public PokedexSkin(byte[] data, bool active = false)
        {
            this.data = data;
            if (active)
            {
                this.active = !isEmpty();
            }
            else
            {
                this.active = active;
            }
        }

        public bool isEmpty()
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] != 0xFF)
                {
                    return false;
                }
            }
            //active = false;
            return true;
        }
    }
}
