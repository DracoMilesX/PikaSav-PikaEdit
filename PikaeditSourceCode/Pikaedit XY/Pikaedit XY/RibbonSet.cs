using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pikaedit_XY
{
    public class RibbonSet
    {
        public ushort data = 0;

        public RibbonSet()
        {

        }

        public RibbonSet(ushort data)
        {
            this.data = data;
        }

        public void setChanges(bool[] flags)
        {
            ushort[] c = new ushort[flags.Length];
            for (int i = 0; i < flags.Length; i++)
            {
                c[i] = (flags[i] ? (ushort)1 : (ushort)0);
            }
            for (int i = 0; i < flags.Length; i++)
            {
                this.data = (ushort)(this.data | (c[i] << i));
            }
        }

        public bool[] getFlags()
        {
            bool[] flags = new bool[16];
            for (int i = 0; i < 16; i++)
            {
                flags[i] = ((data >> i) & 1) == 1;
            }
            return flags;
        }
    }
}
