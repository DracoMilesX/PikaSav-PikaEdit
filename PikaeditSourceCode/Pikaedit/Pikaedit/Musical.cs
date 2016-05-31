using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pikaedit
{
    public class Musical
    {
        public byte[] data;
        public bool active;
        public string title;
        public readonly uint MAXLENGTHBW = 0x1FC00;
        public readonly uint MAXLENGTHBW2 = 0x17C00;

        public Musical()
        {

        }

        public Musical(byte[] data, SaveFile.Version version, bool active = false)
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
            adjustData(version);
            this.title = extractTitle(version);
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

        public string extractTitle(SaveFile.Version version)
        {
            if (data.Length > MAXLENGTHBW2)
            {
                if (data.Length < 0x17D14)
                {
                    return "";
                }
                else
                {
                    return Func.getString(Func.subArray(this.data, 0x17D14, 0x4C), 0, 0x26);
                }
            }
            else
            {
                if (data.Length < 0x1FD14)
                {
                    return "";
                }
                else
                {
                    return Func.getString(Func.subArray(this.data, 0x1FD14, 0x4C), 0, 0x26);
                }
            }
        }

        public byte[] getData(SaveFile.Version version)
        {
            if (version == SaveFile.Version.BW2)
            {
                return Func.subArray(this.data, 0, (int)MAXLENGTHBW2);
            }
            else
            {
                return Func.subArray(this.data, 0, (int)MAXLENGTHBW);
            }
        }

        public void adjustData(SaveFile.Version version)
        {
            if (version == SaveFile.Version.BW && getData(version).Length == MAXLENGTHBW2)
            {
                byte[] d = new byte[0x17D78];
                Array.Copy(getData(version), 0, d, 0, getData(version).Length);
                for (int i = getData(version).Length; i < (int)MAXLENGTHBW; i++)
                {
                    d[i] = 0;
                }
                Array.Copy(data, (int)MAXLENGTHBW2, d, (int)MAXLENGTHBW, 0x178);
                this.data = d;
            }
        }
    }
}
