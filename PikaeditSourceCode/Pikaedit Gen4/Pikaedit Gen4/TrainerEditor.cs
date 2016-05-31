using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pikaedit_Gen4
{
    public partial class TrainerEditor : Form
    {
        private string[] sinnohBadges = new string[] { "Coal Badge", "Forest Badge", "Cobble Badge", "Fen Badge", "Relic Badge", "Mine Badge", "Icicle Badge", "Beacon Badge" };
        private string[] hgssBadges = new string[] { "Zephyr Badge", "Hive Badge", "Plain Badge", "Fog Badge", "Storm Badge", "Mineral Badge", "Glacier Badge", "Rising Badge", "Boulder Badge", "Cascade Badge", "Thunder Badge", "Rainbow Badge", "Soul Badge", "Marsh Badge", "Volcano Badge", "Earth Badge" };
        public TrainerEditor()
        {
            InitializeComponent();
        }

        public TrainerEditor(SaveFile.Version version)
        {
            InitializeComponent();
            badgeList.Items.Clear();
            if (version == SaveFile.Version.HGSS)
            {
                badgeList.Items.AddRange(hgssBadges);
            }
            else
            {
                badgeList.Items.AddRange(sinnohBadges);
            }
        }

        public string name
        {
            get
            {
                return nameBox.Text;
            }
            set
            {
                nameBox.Text = value;
            }
        }

        public ushort id
        {
            get
            {
                return Convert.ToUInt16(idBox.Text);
            }
            set
            {
                idBox.Text = Convert.ToString(value);
            }
        }

        public ushort sid
        {
            get
            {
                return Convert.ToUInt16(sidBox.Text);
            }
            set
            {
                sidBox.Text = Convert.ToString(value);
            }
        }

        public uint money
        {
            get
            {
                return Convert.ToUInt32(moneyBox.Text);
            }
            set
            {
                moneyBox.Text = Convert.ToString(value);
            }
        }

        public TrainerInfo.TrainerGender gender
        {
            get
            {
                if (boyButton.Checked)
                {
                    return TrainerInfo.TrainerGender.Male;
                }
                else
                {
                    return TrainerInfo.TrainerGender.Female;
                }
            }
            set
            {
                if (value == TrainerInfo.TrainerGender.Male)
                {
                    boyButton.Checked = true;
                }
                else
                {
                    girlButton.Checked = true;
                }
            }
        }

        public bool[] badges
        {
            get
            {
                bool[] b = new bool[badgeList.Items.Count];
                for (int i = 0; i < b.Length; i++)
                {
                    b[i] = badgeList.GetItemChecked(i);
                }
                return b;
            }
            set
            {
                bool[] b = value;
                for (int i = 0; i < b.Length; i++)
                {
                    badgeList.SetItemChecked(i, b[i]);
                }
            }
        }

        private void moneyBox_TextChanged(object sender, EventArgs e)
        {
            uint temp;
            if (!uint.TryParse(moneyBox.Text, out temp))
            {
                moneyBox.Text = "0";
            }
        }

        private void ushort_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox a = (TextBox)sender;
                ushort temp;
                if (!ushort.TryParse(a.Text, out temp))
                {
                    a.Text = "0";
                }
            }
        }
    }
}
