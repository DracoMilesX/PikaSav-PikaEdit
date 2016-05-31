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
    public partial class MysteryGift : Form
    {
        private WonderCard[] wonderCards = new WonderCard[12];

        public MysteryGift()
        {
            InitializeComponent();
            slotSelect.SelectedIndex = 0;
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

        private void slotSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadLog(slotSelect.SelectedIndex);
        }

        public void loadLog(int i){
            wondercardInfo.Items.Clear();
            wondercardInfo.Items.AddRange(wonderCards[i].getLog());
        }

        public WonderCard[] wondercards
        {
            get
            {
                return wonderCards;
            }
            set
            {
                wonderCards = value;
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            WonderCardEditor w = new WonderCardEditor();
            w.Wondercard = wondercards[slotSelect.SelectedIndex];
            w.ShowDialog();
            wondercards[slotSelect.SelectedIndex] = w.Wondercard;
            loadLog(slotSelect.SelectedIndex);
        }
    }
}
