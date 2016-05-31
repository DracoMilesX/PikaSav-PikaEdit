using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pikaedit
{
    public partial class TrainerEditor : Form
    {
        private string[] bwBadges = new string[] { "Trio Badge", "Basic Badge", "Insect Badge", "Bolt Badge", "Quake Badge", "Jet Badge", "Icicle Badge", "Legend Badge" };
        private string[] bw2Badges = new string[] { "Basic Badge", "Toxic Badge", "Insect Badge", "Bolt Badge", "Quake Badge", "Jet Badge", "Legend Badge", "Wave Badge" };
        private string[] bwBadgesfr = new string[] { "Badge Triple", "Badge Basique", "Badge Élytre", "Badge Volt", "Badge Sismique", "Badge Jet", "Badge Stalactite", "Badge Mythe" };
        private string[] bw2Badgesfr = new string[] { "Badge Basique", "Badge Toxique", "Badge Élytre", "Badge Volt", "Badge Sismique", "Badge Jet", "Badge Mythe", "Badge Vague" };
        public TrainerEditor()
        {
            InitializeComponent();
        }

        public TrainerEditor(SaveFile.Version version)
        {
            InitializeComponent();
            badgeList.Items.Clear();
            if (version == SaveFile.Version.BW2)
            {
                switch(PkmLib.lang){
                    case 3:
                        badgeList.Items.AddRange(bw2Badgesfr);
                        break;
                    default:
                badgeList.Items.AddRange(bw2Badges);
                break;
                }
            }
            else
            {
                switch (PkmLib.lang)
                {
                    case 3:
                        badgeList.Items.AddRange(bwBadgesfr);
                        break;
                    default:
                        badgeList.Items.AddRange(bwBadges);
                        break;
                }
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
