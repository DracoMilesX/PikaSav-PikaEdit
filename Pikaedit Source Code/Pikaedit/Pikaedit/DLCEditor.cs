using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Pikaedit
{
    public partial class DLCEditor : Form
    {
        private Musical musical;
        private CgearSkin cgear;
        private PokedexSkin pokedex;
        private SaveFile.Version version;

        public Musical musicalData
        {
            get
            {
                return musical;
            }
            set
            {
                musical = value;
                if (musical.isEmpty())
                {
                    extractMusical.Enabled = false;
                    activeMusical.Enabled = false;
                }
                if (musical.active)
                {
                    activeMusical.Checked = true;
                }
                musicalTitle.Text = musical.title;
            }
        }

        public CgearSkin cgearSkin
        {
            get
            {
                return cgear;
            }
            set
            {
                cgear = value;
                if (cgear.isEmpty())
                {
                    extractCGear.Enabled = false;
                    activeCGear.Enabled = false;
                }
                if (cgear.active)
                {
                    activeCGear.Checked = true;
                }
            }
        }

        public PokedexSkin pokedexSkin
        {
            get
            {
                return pokedex;
            }
            set
            {
                pokedex = value;
                if (pokedex.isEmpty())
                {
                    extractPokedex.Enabled = false;
                    activePokedex.Enabled = false;
                }
                if (pokedex.active)
                {
                    activePokedex.Checked = true;
                }
            }
        }

        public SaveFile.Version Version
        {
            set
            {
                version = value;
            }
        }

        public DLCEditor()
        {
            InitializeComponent();
        }

        private void change(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button a = (Button)sender;
                if (a.Equals(changeCGear))
                {
                    if (version == SaveFile.Version.BW2)
                    {
                        loadDialog.Filter = "Pokemon C-Gear Skin (*.cgb)|*.cgb";
                    }
                    else
                    {
                        loadDialog.Filter = "Pokemon C-Gear Skin (*.psk)|*.psk";
                    }

                }
                if (a.Equals(changePokedex))
                {
                    loadDialog.Filter = "Pokemon Pokedex Skin (*.pds)|*.pds";
                }
                if (a.Equals(changeMusical))
                {
                    loadDialog.Filter = "Pokemon Musical Data (*.pms)|*.pms";
                }
                DialogResult res = loadDialog.ShowDialog();
                if (res == System.Windows.Forms.DialogResult.OK && loadDialog.FileName !="")
                {
                    if (a.Equals(changeCGear))
                    {
                        cgear = new CgearSkin(File.ReadAllBytes(loadDialog.FileName), true);
                        activeCGear.Checked = !cgear.isEmpty();
                    }
                    if (a.Equals(changePokedex))
                    {
                        pokedex = new PokedexSkin(File.ReadAllBytes(loadDialog.FileName), true);
                        activePokedex.Checked = !pokedex.isEmpty();
                    }
                }
                if (a.Equals(changeMusical))
                {
                    musical = new Musical(File.ReadAllBytes(loadDialog.FileName), version, true);
                    activeMusical.Checked = !musical.isEmpty();
                    musicalTitle.Text = musical.title;
                }
            }
        }

        private void extract(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button a = (Button)sender;
                if (a.Equals(extractCGear))
                {
                    if (version == SaveFile.Version.BW2)
                    {
                        saveDialog.Filter = "Pokemon C-Gear Skin (*.cgb)|*.cgb";
                    }
                    else
                    {
                        saveDialog.Filter = "Pokemon C-Gear Skin (*.psk)|*.psk";
                    }

                }
                if (a.Equals(extractPokedex))
                {
                    saveDialog.Filter = "Pokemon Pokedex Skin (*.pds)|*.pds";
                }
                if (a.Equals(extractMusical))
                {
                    saveDialog.Filter = "Pokemon Musical Data (*.pms)|*.pms";
                }
                DialogResult res = saveDialog.ShowDialog();
                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    if (a.Equals(extractCGear))
                    {
                        File.WriteAllBytes(saveDialog.FileName,cgear.data);
                    }
                    if (a.Equals(extractPokedex))
                    {
                        File.WriteAllBytes(saveDialog.FileName, pokedex.data);
                    }
                    if (a.Equals(extractMusical))
                    {
                        File.WriteAllBytes(saveDialog.FileName, musical.data);
                    }
                }
            }
        }

        private void activate(object sender, EventArgs e)
        {
            if (sender is CheckBox)
            {
                CheckBox a = (CheckBox)sender;
                if (a.Equals(changeCGear))
                {
                    cgear.active = a.Checked;
                }
                if (a.Equals(extractPokedex))
                {
                    pokedex.active = a.Checked;
                }
                if (a.Equals(extractMusical))
                {
                    musical.active = a.Checked;
                }
            }
        }

        private void musicalTitle_TextChanged(object sender, EventArgs e)
        {
            musical.title = musicalTitle.Text;
        }
    }
}
