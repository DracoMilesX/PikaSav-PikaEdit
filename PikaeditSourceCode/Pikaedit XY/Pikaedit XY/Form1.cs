using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace Pikaedit_XY
{
    public partial class Form1 : Form
    {
        public Pokemon pkx;
        public SaveFile savefile;
        private List<byte> legalAbilities = new List<byte>();
        private List<Pikaedit_Lib.IPlugin> plugins = new List<Pikaedit_Lib.IPlugin>();
        private List<string> pluginFiles = new List<string>();


        public Form1()
        {
            Settings.load();
            //Check Plugin folder
            if (!Directory.Exists(Application.StartupPath + Path.DirectorySeparatorChar + "Plugins"))
            {
                Directory.CreateDirectory(Application.StartupPath + Path.DirectorySeparatorChar + "Plugins");
            }
            InitializeComponent();
            //if (!File.Exists("PikaeditLib.dll"))
            //{
            //    pluginsMenu.Visible = false;
            //}
            PkmLib.Initialize();
            pkx = new Pokemon(PkmLib.resetpkx);
            fillComboBoxes();
            loadPKX(pkx);
            dateBox.Text = dateEggBox.Text = DateTime.Now.ToString("dd/MM/yy");
            dateEggBox.Text = "";
            this.Text = "Pikaedit XY v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().TrimEnd('.', '0');
            locationBox.SelectedIndex = 0;
            locationEggBox.SelectedIndex = 0;
            string[] dllFileNames = null;
            if (Directory.Exists(Application.StartupPath + Path.DirectorySeparatorChar + "Plugins"))
            {
                dllFileNames = Directory.GetFiles(Application.StartupPath + Path.DirectorySeparatorChar + "Plugins", "*.dll");
                if (dllFileNames != null)
                {
                    foreach (string file in dllFileNames)
                    {
                        addPluginFrom(file);
                    }
                }

            }
            try
            {
                if (plugins.Count != 0)
                {
                    Updater.Initialize(plugins, pluginFiles);
                }
                else
                {
                    Updater.Initialize();
                }
            }
            catch (Exception ex)
            {
                //Do something with exception message... not here XD
            }
            //Verify if there is new pkx injection method
            string url = "";
            bool conSuc = false;
            try
            {
                System.Net.WebClient wc = new System.Net.WebClient();
                url = wc.DownloadString("https://dl.dropboxusercontent.com/s/mwcit4zldnkzpb5/PKXSendStatus.txt?dl=1");
                conSuc = true;
                if (url != null)
                {
                    url = url.Trim();
                }
                Settings.injectionP = !string.IsNullOrEmpty(url);
            }
            catch (Exception e)
            {
                url = "";
                conSuc = false;
            }
            ToolStripMenuItem a = new ToolStripMenuItem();
            if (conSuc)
            {
                if (Settings.injectionP && !string.IsNullOrEmpty(url))
                {
                    a.Text = "Sending pkx is now possible";
                    a.Click += delegate { Process.Start(url); };
                }
                else
                {
                    a.Text = "Sending pkx is currently impossible";
                    a.Click += delegate { Process.Start("http://pikaedit.wordpress.com/2014/05/18/im-back-and-an-answer-for-most-comments-and-future-ones/"); };
                }
            }
            else
            {
                if (Settings.injectionP)
                {
                    a.Text = "Sending pkx is now possible";
                }
                else
                {
                    a.Text = "Unable to check pkx send method status";
                }
            }
            MenuStrip1.Items.Add(a);
        }

        private void fillComboBoxes()
        {
            speciesBox.Items.Clear();
            itemBox.Items.Clear();
            natureBox.Items.Clear();
            locationBox.Items.Clear();
            locationEggBox.Items.Clear();
            abilityBox.Items.Clear();
            move1Box.Items.Clear();
            move2Box.Items.Clear();
            move3Box.Items.Clear();
            move4Box.Items.Clear();
            eggMove1Box.Items.Clear();
            eggMove2Box.Items.Clear();
            eggMove3Box.Items.Clear();
            eggMove4Box.Items.Clear();
            speciesBox.Items.AddRange(PkmLib.lspecies.ToArray());
            speciesBox.Items.RemoveAt(0);
            itemBox.Items.AddRange(PkmLib.litems.ToArray());
            while (itemBox.Items.Contains("???"))
            {
                itemBox.Items.Remove("???");
            }
            natureBox.Items.AddRange(PkmLib.lnatures.ToArray());
            locationBox.Items.AddRange(PkmLib.llocations.ToArray());
            locationEggBox.Items.AddRange(PkmLib.llocations.ToArray());
            abilityBox.Items.AddRange(PkmLib.labilities.ToArray());
            move1Box.Items.AddRange(PkmLib.lmoves.ToArray());
            move2Box.Items.AddRange(PkmLib.lmoves.ToArray());
            move3Box.Items.AddRange(PkmLib.lmoves.ToArray());
            move4Box.Items.AddRange(PkmLib.lmoves.ToArray());
            eggMove1Box.Items.AddRange(PkmLib.lmoves.ToArray());
            eggMove2Box.Items.AddRange(PkmLib.lmoves.ToArray());
            eggMove3Box.Items.AddRange(PkmLib.lmoves.ToArray());
            eggMove4Box.Items.AddRange(PkmLib.lmoves.ToArray());
            pokeballBox.Items.AddRange(PkmLib.pokeballs.ToArray());
        }

        private void combobox_TextChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox)
            {
                ComboBox a = (ComboBox)sender;
                ushort numericValue = 0;
                if ((a.Name == "locationBox" || a.Name == "locationEggBox") && ushort.TryParse(a.Text, out numericValue))
                {
                    a.BackColor = Color.White;
                    if (a.Name == "locationBox")
                    {
                        pkx.locationmet = a.Text;
                    }
                    else
                    {
                        pkx.eggloc = a.Text;
                    }
                }
                else
                {
                    if (a.Text != "")
                    {
                        string p = "";
                        string t = "";
                        string temp = a.Text.ToLower();
                        for (int i = 0; i < a.Items.Count; i++)
                        {
                            p = a.Items[i].ToString();
                            t = a.Items[i].ToString().ToLower();
                            if (t.Equals(temp))
                            {
                                a.SelectedItem = p;
                                a.BackColor = Color.White;
                                a.Select(a.Text.Length, 0);
                                return;
                            }
                            else
                            {
                                a.BackColor = Color.Yellow;
                            }
                        }
                    }
                    else
                    {
                        a.BackColor = Color.Yellow;
                    }
                }
            }
        }

        private void uintBox_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox a = (TextBox)sender;
                uint temp;
                if (!uint.TryParse(a.Text, out temp))
                {
                    a.Text = "0";
                }
                if (a.Name.Equals("pidBox"))
                {
                    pkx.pid = temp;
                    ushort pid1 = 0;
                    ushort pid2 = 0;
                    pid1 = (ushort)(pkx.pid & 0xffff);
                    pid2 = (ushort)((pkx.pid >> 16) & 0xffff);
                    int c = pkx.id ^ pkx.sid;
                    int b = pid1 ^ pid2;
                    if ((c ^ b) < 16)
                    {
                        pkx.isShiny = true;
                        shinyBox.Checked = true;
                    }
                    else
                    {
                        pkx.isShiny = false;
                        shinyBox.Checked = false;
                    }
                }
                if (a.Name.Equals("expBox"))
                {
                    if (temp > PkmLib.maxExp(pkx.species))
                    {
                        temp = PkmLib.maxExp(pkx.species);
                        expBox.Text = Convert.ToString(temp);
                    }
                    pkx.exp = temp;
                    if (pkx.level != PkmLib.calculateLevel(pkx.species, pkx.exp))
                    {
                        pkx.level = PkmLib.calculateLevel(pkx.species, pkx.exp);
                        levelBox.Text = Convert.ToString(PkmLib.calculateLevel(pkx.species, pkx.exp));
                    }
                    updateStats();
                }
                if (a.Name.Equals("encryptionBox"))
                {
                    pkx.encryptionKey = temp;
                }
            }
        }

        private void byteBox_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox a = (TextBox)sender;
                byte temp;
                if (!byte.TryParse(a.Text, out temp))
                {
                    if (a.Name.Equals("levelBox"))
                    {
                        a.Text = "1";
                    }
                    else
                    {
                        a.Text = "0";
                    }
                }
                if (a.Name.Equals("levelBox"))
                {
                    if (temp > 100)
                    {
                        temp = 100;
                        a.Text = "100";
                    }
                }
                if (a.Name.Equals("levelBox"))
                {
                    pkx.level = temp;
                    if (temp != PkmLib.calculateLevel(pkx.species,pkx.exp)){
                        pkx.exp = PkmLib.calculateExp(pkx.species, temp);
                        expBox.Text = Convert.ToString(PkmLib.calculateExp(pkx.species, temp));
                    }
                    loadMovesets();
                }
                if (a.Name.Equals("happinessBox"))
                {
                    pkx.happiness = temp;
                }
                if (a.Name.Equals("affectionBox"))
                {
                    pkx.amieAffection = temp;
                }
                if (a.Name.Equals("fullnessBox"))
                {
                    pkx.amieFullness = temp;
                }
                if (a.Name.Equals("enjoymentBox"))
                {
                    pkx.amieEnjoyment = temp;
                }
                if (a.Name.Equals("levelMetBox"))
                {
                    pkx.levelmet = temp;
                }
                if (a.Name.Equals("levelBox"))
                {
                    pkx.level = temp;
                    updateStats();
                }
                if (a.Name.Equals("countryBox"))
                {
                    pkx.countryID = temp;
                }
                if (a.Name.Equals("regionBox"))
                {
                    pkx.regionID = temp;
                }
                if (a.Name.Equals("region3dsBox"))
                {
                    pkx.region3dsID = temp;
                }
                if (a.Name.Equals("pp1Box"))
                {
                    pkx.moveset.move1.pp = temp;
                }
                if (a.Name.Equals("pp2Box"))
                {
                    pkx.moveset.move2.pp = temp;
                }
                if (a.Name.Equals("pp3Box"))
                {
                    pkx.moveset.move3.pp = temp;
                }
                if (a.Name.Equals("pp4Box"))
                {
                    pkx.moveset.move4.pp = temp;
                }
                if (a.Name.Equals("ppUp1Box"))
                {
                    pkx.moveset.move1.ppUp = temp;
                    pp1Box.Text = Convert.ToString(PkmLib.pp[pkx.moveset.move1.move] + (PkmLib.pp[pkx.moveset.move1.move] / 5) * temp);
                }
                if (a.Name.Equals("ppUp2Box"))
                {
                    pkx.moveset.move2.ppUp = temp;
                    pp2Box.Text = Convert.ToString(PkmLib.pp[pkx.moveset.move2.move] + (PkmLib.pp[pkx.moveset.move2.move] / 5) * temp);
                }
                if (a.Name.Equals("ppUp3Box"))
                {
                    pkx.moveset.move3.ppUp = temp;
                    pp3Box.Text = Convert.ToString(PkmLib.pp[pkx.moveset.move3.move] + (PkmLib.pp[pkx.moveset.move3.move] / 5) * temp);
                }
                if (a.Name.Equals("ppUp4Box"))
                {
                    pkx.moveset.move4.ppUp = temp;
                    pp4Box.Text = Convert.ToString(PkmLib.pp[pkx.moveset.move4.move] + (PkmLib.pp[pkx.moveset.move4.move] / 5) * temp);
                }
                if (a.Name.Contains("IV"))
                {
                    if (temp > 31)
                    {
                        a.Text = "31";
                    }
                    saveIV();
                }
                if (a.Name.Contains("EV"))
                {
                    pkx.hpev = Convert.ToByte(hpEV.Text);
                    pkx.atev = Convert.ToByte(atEV.Text);
                    pkx.dfev = Convert.ToByte(dfEV.Text);
                    pkx.saev = Convert.ToByte(saEV.Text);
                    pkx.sdev = Convert.ToByte(sdEV.Text);
                    pkx.spev = Convert.ToByte(spEV.Text);
                    if ((pkx.hpev + pkx.atev + pkx.dfev + pkx.saev + pkx.sdev + pkx.spev) > 510)
                    {
                        a.Text = "0";
                        pkx.hpev = Convert.ToByte(hpEV.Text);
                        pkx.atev = Convert.ToByte(atEV.Text);
                        pkx.dfev = Convert.ToByte(dfEV.Text);
                        pkx.saev = Convert.ToByte(saEV.Text);
                        pkx.sdev = Convert.ToByte(sdEV.Text);
                        pkx.spev = Convert.ToByte(spEV.Text);
                    }
                    Label43.Text = Label43.Text.Split('/')[0] + "/EV " + (pkx.hpev + pkx.atev + pkx.dfev + pkx.saev + pkx.sdev + pkx.spev);
                    updateStats();
                }
            }
        }

        private void saveIV()
        {
            pkx.iv.hp = Convert.ToByte(hpIV.Text);
            pkx.iv.atk = Convert.ToByte(atIV.Text);
            pkx.iv.def = Convert.ToByte(dfIV.Text);
            pkx.iv.spa = Convert.ToByte(saIV.Text);
            pkx.iv.spd = Convert.ToByte(sdIV.Text);
            pkx.iv.spe = Convert.ToByte(spIV.Text);
            Label43.Text = "Total IV " + (pkx.iv.hp + pkx.iv.atk + pkx.iv.def + pkx.iv.spa + pkx.iv.spd + pkx.iv.spe) + "/" + Label43.Text.Split('/')[1];
            pkx.hiddenPower();
            Label17.Text = PkmLib.moveTranslateTo("Hidden Power") + " " + pkx.hpType;
            updateStats();
        }

        private void updateStats()
        {
            pkx.updateStats();
            maxhpBox.Text = Convert.ToString(pkx.maxhp);
            attackBox.Text = Convert.ToString(pkx.attack);
            defenseBox.Text = Convert.ToString(pkx.defense);
            spattackBox.Text = Convert.ToString(pkx.spatk);
            spdefenseBox.Text = Convert.ToString(pkx.spdef);
            speedBox.Text = Convert.ToString(pkx.speed);
            double atnature = 1;
            double dfnature = 1;
            double sanature = 1;
            double sdnature = 1;
            double spnature = 1;
            switch (pkx.nature)
            {
                case "Adamant":
                    atnature = 1.1;
                    sanature = 0.9;
                    break;
                case "Bashful":

                    break;
                case "Bold":
                    atnature = 0.9;
                    dfnature = 1.1;
                    break;
                case "Brave":
                    atnature = 1.1;
                    spnature = 0.9;
                    break;
                case "Calm":
                    sdnature = 1.1;
                    atnature = 0.9;
                    break;
                case "Careful":
                    sdnature = 1.1;
                    sanature = 0.9;
                    break;
                case "Docile":

                    break;
                case "Gentle":
                    sdnature = 1.1;
                    dfnature = 0.9;
                    break;
                case "Hardy":

                    break;
                case "Hasty":
                    spnature = 1.1;
                    dfnature = 0.9;
                    break;
                case "Impish":
                    dfnature = 1.1;
                    sanature = 0.9;
                    break;
                case "Jolly":
                    spnature = 1.1;
                    sanature = 0.9;
                    break;
                case "Lax":
                    dfnature = 1.1;
                    sdnature = 0.9;
                    break;
                case "Lonely":
                    atnature = 1.1;
                    dfnature = 0.9;
                    break;
                case "Mild":
                    sanature = 1.1;
                    dfnature = 0.9;
                    break;
                case "Modest":
                    sanature = 1.1;
                    atnature = 0.9;
                    break;
                case "Naive":
                    spnature = 1.1;
                    sdnature = 0.9;
                    break;
                case "Naughty":
                    atnature = 1.1;
                    sdnature = 0.9;
                    break;
                case "Quiet":
                    sanature = 1.1;
                    spnature = 0.9;
                    break;
                case "Quirky":

                    break;
                case "Rash":
                    sanature = 1.1;
                    sdnature = 0.9;
                    break;
                case "Relaxed":
                    dfnature = 1.1;
                    spnature = 0.9;
                    break;
                case "Sassy":
                    sdnature = 1.1;
                    spnature = 0.9;
                    break;
                case "Serious":

                    break;
                case "Timid":
                    spnature = 1.1;
                    atnature = 0.9;
                    break;
            }
            attackBox.BackColor = Color.White;
            defenseBox.BackColor = Color.White;
            spattackBox.BackColor = Color.White;
            spdefenseBox.BackColor = Color.White;
            speedBox.BackColor = Color.White;
            if (atnature == 1.1)
            {
                attackBox.BackColor = Color.Orange;
            }
            else
            {
                if (atnature == 0.9)
                {
                    attackBox.BackColor = Color.SkyBlue;
                }
            }
            if (dfnature == 1.1)
            {
                defenseBox.BackColor = Color.Orange;
            }
            else
            {
                if (dfnature == 0.9)
                {
                    defenseBox.BackColor = Color.SkyBlue;
                }
            }
            if (sanature == 1.1)
            {
                spattackBox.BackColor = Color.Orange;
            }
            else
            {
                if (sanature == 0.9)
                {
                    spattackBox.BackColor = Color.SkyBlue;
                }
            }
            if (sdnature == 1.1)
            {
                spdefenseBox.BackColor = Color.Orange;
            }
            else
            {
                if (sdnature == 0.9)
                {
                    spdefenseBox.BackColor = Color.SkyBlue;
                }
            }
            if (spnature == 1.1)
            {
                speedBox.BackColor = Color.Orange;
            }
            else
            {
                if (spnature == 0.9)
                {
                    speedBox.BackColor = Color.SkyBlue;
                }
            }
        }

        private void ushortBox_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox a = (TextBox)sender;
                ushort temp;
                if (!ushort.TryParse(a.Text, out temp))
                {
                    a.Text = "0";
                }
                if (a.Name.Equals("idBox"))
                {
                    pkx.id = temp;
                }
                if (a.Name.Equals("sidBox"))
                {
                    pkx.sid = temp;
                }
                if (a.Name.Equals("hpBox"))
                {
                    if (temp > pkx.maxhp)
                    {
                        temp = pkx.maxhp;
                        a.Text = Convert.ToString(temp);
                    }
                    pkx.hp = temp;
                    hpBar.Value = temp;
                }
                if (a.Name.Equals("maxhpBox"))
                {
                    pkx.maxhp = temp;
                    hpBar.Maximum = temp;
                    hpBox.Text = Convert.ToString(temp);
                }
                if (a.Name.Equals("attackBox"))
                {
                    pkx.attack = temp;
                }
                if (a.Name.Equals("defenseBox"))
                {
                    pkx.defense = temp;
                }
                if (a.Name.Equals("spattackBox"))
                {
                    pkx.spatk = temp;
                }
                if (a.Name.Equals("spdefenseBox"))
                {
                    pkx.spdef = temp;
                }
                if (a.Name.Equals("speedBox"))
                {
                    pkx.speed = temp;
                }
            }
        }

        private void resetEVs(object sender, EventArgs e)
        {
            hpEV.Text = "0";
            atEV.Text = "0";
            dfEV.Text = "0";
            saEV.Text = "0";
            sdEV.Text = "0";
            spEV.Text = "0";
        }

        private void maxIV(object sender, EventArgs e)
        {
            hpIV.Text = "31";
            atIV.Text = "31";
            dfIV.Text = "31";
            saIV.Text = "31";
            sdIV.Text = "31";
            spIV.Text = "31";
        }

        public string[] fillForms()
        {
            string[] forms = new string[] { "None" };
            if (pkx.species == "Unown")
            {
                forms = new string[]{
				"A",
				"B",
				"C",
				"D",
				"E",
				"F",
				"G",
				"H",
				"I",
				"J",
				"K",
				"L",
				"M",
				"N",
				"O",
				"P",
				"Q",
				"R",
				"S",
				"T",
				"U",
				"V",
				"W",
				"X",
				"Y",
				"Z",
				"?",
				"!"
			};
            }
            if (pkx.species == "Deoxys")
            {
                forms = new string[]{
				"Normal",
				"Attack",
				"Defense",
				"Speed"
			};
            }
            if (pkx.species == "Burmy")
            {
                forms = new string[]{
				"Plant",
				"Sandy",
				"Thrash"
			};
            }
            if (pkx.species == "Wormadam")
            {
                forms = new string[]{
				"Plant",
				"Sandy",
				"Thrash"
			};
            }
            if (pkx.species == "Shellos")
            {
                forms = new string[]{
				"West",
				"East"
			};
            }
            if (pkx.species == "Gastrodon")
            {
                forms = new string[]{
				"West",
				"East"
			};
            }
            if (pkx.species == "Rotom")
            {
                forms = new string[]{
				"Normal",
				"Heat",
				"Wash",
				"Frost",
				"Fan",
				"Mow"
			};
            }
            if (pkx.species == "Giratina")
            {
                forms = new string[]{
				"Altered",
				"Origin"
			};
            }
            if (pkx.species == "Shaymin")
            {
                forms = new string[]{
				"Land",
				"Sky"
			};
            }
            if (pkx.species == "Arceus")
            {
                forms = new string[]{
				"Normal",
				"Fighting",
				"Flying",
				"Poison",
				"Ground",
				"Rock",
				"Bug",
				"Ghost",
				"Steel",
				"Fire",
				"Water",
				"Grass",
				"Electric",
				"Psychic",
				"Ice",
				"Dragon",
				"Dark",
                "Fairy"
			};
            }
            if (pkx.species == "Basculin")
            {
                forms = new string[]{
				"Red Striped",
				"Blue Striped"
			};
            }
            if (pkx.species == "Deerling")
            {
                forms = new string[]{
				"Spring",
				"Summer",
				"Autumn",
				"Winter"
			};
            }
            if (pkx.species == "Sawsbuck")
            {
                forms = new string[]{
				"Spring",
				"Summer",
				"Autumn",
				"Winter"
			};
            }
            if (pkx.species == "Tornadus")
            {
                forms = new string[] {
				"Incarnate",
				"Therian"
			};
            }
            if (pkx.species == "Thundurus")
            {
                forms = new string[]{
				"Incarnate",
				"Therian"
			};
            }
            if (pkx.species == "Landorus")
            {
                forms = new string[]{
				"Incarnate",
				"Therian"
			};
            }
            if (pkx.species == "Kyurem")
            {
                forms = new string[]{
				"Normal",
				"Black",
				"White"
			};
            }
            if (pkx.species == "Keldeo")
            {
                forms = new string[]{
				"Ordinary",
				"Resolute"
			};
            }
            if (pkx.species == "Meloetta")
            {
                forms = new string[]{
				"Aria",
				"Pirouette"
			};
            }
            if (pkx.species == "Genesect")
            {
                forms = new string[]{
				"Normal",
				"Douse",
				"Shock",
				"Burn",
				"Chill"
			};
            }
            if (pkx.species == "Vivillon" | pkx.species == "Scatterbug" | pkx.species == "Spewpa")
            {
                forms = new string[]{
				"Icy-snow",
				"Polar",
				"Tundra",
				"Continental",
				"Garden",
				"Elegant",
				"Meadow",
				"Modern",
				"Marine",
				"Archipelago",
				"High-plains",
				"Sandstorm",
				"River",
				"Monsoon",
				"Savanna",
				"Sun",
				"Ocean",
				"Jungle"
			};
            }
            if (pkx.species == "Flabébé" | pkx.species == "Floette" | pkx.species == "Florges")
            {
                if (pkx.species == "Floette")
                {
                    forms = new string[]{
				"Red",
				"Yellow",
				"Orange",
				"Blue",
				"White",
                "Eternal Flower"
			};
                }
                else
                {
                    forms = new string[]{
				"Red",
				"Yellow",
				"Orange",
				"Blue",
				"White"
			};
                }
            }
            if (pkx.species == "Furfrou")
            {
                forms = new string[]{
				"Natural",
				"Heart",
				"Star",
				"Diamond",
				"Deputante",
				"Matron",
				"Dandy",
				"La-reine",
				"Kabuki",
				"Pharoah"
			};
            }
            if (pkx.species == "Meowstic")
            {
                forms = new string[]{
				"Male",
				"Female"
			};
            }
            if (pkx.species == "Aegislash")
            {
                forms = new string[]{
				"Shield",
				"Blade"
			};
            }
            if (pkx.species == "Pumpkaboo" | pkx.species == "Gourgeist")
            {
                forms = new string[]{
				"Average",
				"Small",
				"Large",
				"Super"
			};
            }
            if (pkx.species == "Venusaur" | pkx.species == "Blastoise" | pkx.species == "Alakazam" | pkx.species == "Gengar" | pkx.species == "Kangaskhan" | pkx.species == "Pinsir" | pkx.species == "Gyarados" | pkx.species == "Aerodactyl" | pkx.species == "Ampharos" | pkx.species == "Scizor" | pkx.species == "Heracross" | pkx.species == "Houndoom" | pkx.species == "Tyranitar" | pkx.species == "Blaziken" | pkx.species == "Gardevoir" | pkx.species == "Mawile" | pkx.species == "Aggron" | pkx.species == "Medicham" | pkx.species == "Manectric" | pkx.species == "Banette" | pkx.species == "Absol" | pkx.species == "Garchomp" | pkx.species == "Lucario" | pkx.species == "Abomasnow" | pkx.species == "Latias" | pkx.species == "Latios")
            {
                forms = new string[]{
				"Normal",
				"Mega"
			};
            }
            if (pkx.species == "Charizard" | pkx.species == "Mewtwo")
            {
                forms = new string[]{
				"Normal",
				"Mega X",
				"Mega Y"
			};
            }
            return forms;
        }

        private void loadMovesets()
        {
            if (Settings.legalMode)
            {
                int[] mov;
                try
                {
                    int[] mov2 = { PkmLib.lmoves.IndexOf(move1Box.SelectedItem.ToString()), PkmLib.lmoves.IndexOf(move2Box.SelectedItem.ToString()), PkmLib.lmoves.IndexOf(move3Box.SelectedItem.ToString()),
                                  PkmLib.lmoves.IndexOf(move4Box.SelectedItem.ToString()), PkmLib.lmoves.IndexOf(eggMove1Box.SelectedItem.ToString()),
                                  PkmLib.lmoves.IndexOf(eggMove2Box.SelectedItem.ToString()), PkmLib.lmoves.IndexOf(eggMove3Box.SelectedItem.ToString()), PkmLib.lmoves.IndexOf(eggMove4Box.SelectedItem.ToString()) };
                    mov = mov2;
                }
                catch (Exception e)
                {
                    mov = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
                }
                move1Box.Items.Clear();
                move2Box.Items.Clear();
                move3Box.Items.Clear();
                move4Box.Items.Clear();
                eggMove1Box.Items.Clear();
                eggMove2Box.Items.Clear();
                eggMove3Box.Items.Clear();
                eggMove4Box.Items.Clear();
                Pikaedit_Lib.Veekun.Initialize();
                //Pikaedit_Lib.MovesetType movesetType = Pikaedit_Lib.MovesetType.All;
                Pikaedit_Lib.GenSearch genType;
                switch (pkx.gen)
                {
                    case 3:
                        genType = Pikaedit_Lib.GenSearch.From_3_to_6;
                        break;
                    case 4:
                        genType = Pikaedit_Lib.GenSearch.From_4_to_6;
                        break;
                    case 5:
                        genType = Pikaedit_Lib.GenSearch.From_5_to_6;
                        break;
                    case 6:
                        genType = Pikaedit_Lib.GenSearch.Gen_6;
                        break;
                    default:
                        genType = Pikaedit_Lib.GenSearch.Gen_6;
                        break;
                }
                List<ushort> eggmoves = Pikaedit_Lib.Veekun.getMoveset(pkx.species, genType, pkx.form, Pikaedit_Lib.MovesetType.Levelup);
                List<ushort> move = Pikaedit_Lib.Veekun.getMoveset(pkx.species, genType, pkx.form, Pikaedit_Lib.MovesetType.Levelup, pkx.level, Pikaedit_Lib.SearchByLevelType.Less_Or_Equal);
                move.AddRange(Pikaedit_Lib.Veekun.getMoveset(pkx.species, genType, pkx.form, Pikaedit_Lib.MovesetType.Form_Exclusive, pkx.level, Pikaedit_Lib.SearchByLevelType.Less_Or_Equal));
                move.AddRange(Pikaedit_Lib.Veekun.getMoveset(pkx.species, genType, pkx.form, Pikaedit_Lib.MovesetType.TMs_HMs, pkx.level, Pikaedit_Lib.SearchByLevelType.Less_Or_Equal));
                move.AddRange(Pikaedit_Lib.Veekun.getMoveset(pkx.species, genType, pkx.form, Pikaedit_Lib.MovesetType.Tutor, pkx.level, Pikaedit_Lib.SearchByLevelType.Less_Or_Equal));
                if (pkx.isHatched)
                {
                    move.AddRange(Pikaedit_Lib.Veekun.getMoveset(pkx.species, genType, pkx.form, Pikaedit_Lib.MovesetType.Egg_moves, pkx.level, Pikaedit_Lib.SearchByLevelType.Less_Or_Equal));
                    eggmoves.AddRange(Pikaedit_Lib.Veekun.getMoveset(pkx.species, genType, pkx.form, Pikaedit_Lib.MovesetType.Egg_moves, pkx.level, Pikaedit_Lib.SearchByLevelType.Less_Or_Equal));
                }
                if (pkx.isFateful)
                {
                    move.AddRange(Pikaedit_Lib.Veekun.getMoveset(pkx.species, genType, pkx.form, Pikaedit_Lib.MovesetType.Event, pkx.level, Pikaedit_Lib.SearchByLevelType.Less_Or_Equal));
                    eggmoves.AddRange(Pikaedit_Lib.Veekun.getMoveset(pkx.species, genType, pkx.form, Pikaedit_Lib.MovesetType.Event, pkx.level, Pikaedit_Lib.SearchByLevelType.Less_Or_Equal));
                }
                move.Add(0);
                eggmoves.Add(0);
                List<string> moves = convertMoveUInt16List(move.Distinct().ToList());
                List<string> eggmove = convertMoveUInt16List(eggmoves.Distinct().ToList());
                move1Box.Items.AddRange(moves.ToArray());
                move2Box.Items.AddRange(moves.ToArray());
                move3Box.Items.AddRange(moves.ToArray());
                move4Box.Items.AddRange(moves.ToArray());
                move1Box.SelectedItem = PkmLib.lmoves[mov[0]];
                move2Box.SelectedItem = PkmLib.lmoves[mov[1]];
                move3Box.SelectedItem = PkmLib.lmoves[mov[2]];
                move4Box.SelectedItem = PkmLib.lmoves[mov[3]];
                eggMove1Box.Items.AddRange(eggmove.ToArray());
                eggMove2Box.Items.AddRange(eggmove.ToArray());
                eggMove3Box.Items.AddRange(eggmove.ToArray());
                eggMove4Box.Items.AddRange(eggmove.ToArray());
                eggMove1Box.SelectedItem = PkmLib.lmoves[mov[4]];
                eggMove2Box.SelectedItem = PkmLib.lmoves[mov[5]];
                eggMove3Box.SelectedItem = PkmLib.lmoves[mov[6]];
                eggMove4Box.SelectedItem = PkmLib.lmoves[mov[7]];
                Pikaedit_Lib.Veekun.Close();
            }
        }

        private List<string> convertMoveUInt16List(List<ushort> list)
        {
            List<string> moves = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                if (PkmLib.moves.Count >= list[i])
                {
                    moves.Add(PkmLib.lmoves[list[i]]);
                }
            }
            return moves;
        }

        private void loadAbilities()
        {
            if (Settings.legalMode)
            {
                Pikaedit_Lib.Veekun.Initialize();
                legalAbilities = Pikaedit_Lib.Veekun.getAbilities(pkx.species, pkx.form);
                abilityBox.Items.Clear();
                for (int i = 0; i < legalAbilities.Count; i++)
                {
                    if (legalAbilities[i] != 0)
                    {
                        abilityBox.Items.Add(PkmLib.labilities[legalAbilities[i]]);
                    }
                }
                int a = indexBox.SelectedIndex;
                while (a != 0 && legalAbilities[a] == 0)
                {
                    a--;
                }
                abilityBox.SelectedItem = PkmLib.labilities[legalAbilities[a]];
                Pikaedit_Lib.Veekun.Close();
            }
            //indexBox.SelectedIndex = a;
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox)
            {
                ComboBox a = (ComboBox)sender;
                if (a.Name.Equals("speciesBox"))
                {
                    pkx.species = PkmLib.speciesTranslate(a.SelectedItem.ToString());
                    pkx.no = (ushort)PkmLib.species.IndexOf(pkx.species);
                    formBox.Items.Clear();
                    formBox.Items.AddRange(fillForms());
                    formBox.SelectedIndex = 0;
                    nickBox.Text = pkx.species;
                    //pkx.exp = PkmLib.calculateExp(pkx.species, pkx.level);
                    expBox.Text = Convert.ToString(PkmLib.calculateExp(pkx.species, pkx.level));
                    updateStats();
                    loadMovesets();
                    loadAbilities();
                }
                if (a.Name.Equals("itemBox"))
                {
                    pkx.item = PkmLib.itemTranslate(a.SelectedItem.ToString());
                }
                if (a.Name.Equals("abilityBox"))
                {
                    pkx.ability = PkmLib.abilityTranslate(a.SelectedItem.ToString());
                }
                if (a.Name.Equals("indexBox"))
                {
                    pkx.abilityIndex = (Pokemon.AbilityIndex)Convert.ToByte(a.SelectedItem.ToString());
                    int b = indexBox.SelectedIndex;
                    while (b != 0 && legalAbilities[b] == 0)
                    {
                        b--;
                    }
                    abilityBox.SelectedItem = PkmLib.labilities[legalAbilities[b]];
                }
                if (a.Name.Equals("natureBox"))
                {
                    pkx.nature = PkmLib.natureTranslate(a.SelectedItem.ToString());
                    updateStats();
                }
                if (a.Name.Equals("versionBox"))
                {
                    pkx.version = a.SelectedItem.ToString();
                    loadMovesets();
                }
                if (a.Name.Equals("languageBox"))
                {
                    pkx.language = a.SelectedItem.ToString();
                }
                if (a.Name.Equals("formBox"))
                {
                    pkx.form = a.SelectedItem.ToString();
                    updateStats();
                    loadMovesets();
                    loadAbilities();
                }
                if (a.Name.Equals("strainBox"))
                {
                    pkrsDaysBox.Items.Clear();
                    pkrsDaysBox.Enabled = true;
                    if (a.SelectedIndex == 0)
                    {
                        pkrsDaysBox.Enabled = false;
                        pkx.pokerus = 0;
                        pkrsDaysBox.Items.Add("None");
                        pkrsDaysBox.SelectedIndex = 0;
                    }
                    else
                    {
                        pkx.pokerus = (byte)(a.SelectedIndex << 4);
                        pkrsDaysBox.Items.Add("Cured");
                        for (int i = 1; i <= (a.SelectedIndex % 4) + 1; i++)
                        {
                            pkrsDaysBox.Items.Add(i);
                        }
                        pkrsDaysBox.SelectedIndex = (a.SelectedIndex % 4) + 1;
                    }
                }
                if (a.Name.Equals("pkrsDaysBox"))
                {
                    pkx.pokerus = (byte)((pkx.pokerus & 0xF0) | a.SelectedIndex);
                }
                if (a.Name.Equals("move1Box"))
                {
                    pkx.moveset.move1.move = (ushort)PkmLib.lmoves.IndexOf(a.SelectedItem.ToString());
                    pp1Box.Text = Convert.ToString(PkmLib.pp[pkx.moveset.move1.move]);
                }
                if (a.Name.Equals("move2Box"))
                {
                    pkx.moveset.move2.move = (ushort)PkmLib.lmoves.IndexOf(a.SelectedItem.ToString());
                    pp2Box.Text = Convert.ToString(PkmLib.pp[pkx.moveset.move2.move]);
                }
                if (a.Name.Equals("move3Box"))
                {
                    pkx.moveset.move3.move = (ushort)PkmLib.lmoves.IndexOf(a.SelectedItem.ToString());
                    pp3Box.Text = Convert.ToString(PkmLib.pp[pkx.moveset.move3.move]);
                }
                if (a.Name.Equals("move4Box"))
                {
                    pkx.moveset.move4.move = (ushort)PkmLib.lmoves.IndexOf(a.SelectedItem.ToString());
                    pp4Box.Text = Convert.ToString(PkmLib.pp[pkx.moveset.move4.move]);
                }
                if (a.Name.Equals("eggMove1Box"))
                {
                    pkx.eggmoves.move1.move = (ushort)PkmLib.lmoves.IndexOf(a.SelectedItem.ToString());
                }
                if (a.Name.Equals("eggMove2Box"))
                {
                    pkx.eggmoves.move2.move = (ushort)PkmLib.lmoves.IndexOf(a.SelectedItem.ToString());
                }
                if (a.Name.Equals("eggMove3Box"))
                {
                    pkx.eggmoves.move3.move = (ushort)PkmLib.lmoves.IndexOf(a.SelectedItem.ToString());
                }
                if (a.Name.Equals("eggMove4Box"))
                {
                    pkx.eggmoves.move4.move = (ushort)PkmLib.lmoves.IndexOf(a.SelectedItem.ToString());
                }
                if (a.Name.Equals("locationBox"))
                {
                    pkx.locationmet = PkmLib.locationTranslate(a.SelectedItem.ToString());
                }
                if (a.Name.Equals("locationEggBox"))
                {
                    pkx.eggloc = PkmLib.locationTranslate(a.SelectedItem.ToString());
                }
            }
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox)
            {
                CheckBox a = (CheckBox)sender;
                if (a.Name.Equals("hatchedBox"))
                {
                    pkx.isHatched = a.Checked;
                    dateEggBox.Enabled = a.Checked;
                    locationEggBox.Enabled = a.Checked;
                    locationEggBox.SelectedItem = "";
                    if (a.Checked)
                    {
                        dateEggBox.Text = DateTime.Now.ToString("dd/MM/yy");
                    }
                    else
                    {
                        dateEggBox.Text = "";
                    }
                    loadMovesets();
                }
                if (a.Name.Equals("shinyBox"))
                {
                    pkx.isShiny = a.Checked;
                }
                if (a.Name.Equals("fatefulBox"))
                {
                    pkx.isFateful = a.Checked;
                    loadMovesets();
                }
                if (a.Name.Equals("circleMark"))
                {
                    pkx.markings = a.Checked ? (byte)(pkx.markings | 1) : (byte)(pkx.markings & 0xfe);
                }
                if (a.Name.Equals("triangleMark"))
                {
                    pkx.markings = a.Checked ? (byte)(pkx.markings | 2) : (byte)(pkx.markings & 0xfd);
                }
                if (a.Name.Equals("squareMark"))
                {
                    pkx.markings = a.Checked ? (byte)(pkx.markings | 4) : (byte)(pkx.markings & 0xfb);
                }
                if (a.Name.Equals("heartMark"))
                {
                    pkx.markings = a.Checked ? (byte)(pkx.markings | 8) : (byte)(pkx.markings & 0xf7);
                }
                if (a.Name.Equals("starMark"))
                {
                    pkx.markings = a.Checked ? (byte)(pkx.markings | 16) : (byte)(pkx.markings & 0xef);
                }
                if (a.Name.Equals("diamondMark"))
                {
                    pkx.markings = a.Checked ? (byte)(pkx.markings | 32) : (byte)(pkx.markings & 0xdf);
                }
            }
        }

        private void loadMenu_Click(object sender, EventArgs e)
        {
            loadDialog.Filter = "XY supported files (*.pkx;*.ekx;*.bin;*.pk6;*.pkm)|*.pkx;*.ekx;*.bin;*.pk6;*.pkm";
            DialogResult res = loadDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                Pokemon npkx=new Pokemon();
                switch (Path.GetExtension(loadDialog.FileName))
                {
                    case ".pkm":
                        Pikaedit_Lib.Pokemon pokemon = new Pikaedit_Lib.Pokemon(File.ReadAllBytes(loadDialog.FileName));
                        npkx.supertraining = new SuperTraining();
                        npkx.ribbon1 = new RibbonSet();
                        npkx.ribbon2 = new RibbonSet();
                        npkx.ribbon3 = new RibbonSet();
                        npkx.species = pokemon.species;
                        npkx.ability = pokemon.ability;
                        npkx.form = pokemon.form;
                        Pikaedit_Lib.Veekun.Initialize();
                        List<byte> abilities = Pikaedit_Lib.Veekun.getAbilities(npkx.species, npkx.form);
                        try
                        {
                            Pikaedit_Lib.Veekun.Close();
                        }
                        catch (Exception ex)
                        {

                        }
                        byte ab = (byte)PkmLib.abilities.IndexOf(npkx.ability);
                        if (abilities.IndexOf(ab) != -1)
                        {
                            npkx.abilityIndex = (Pokemon.AbilityIndex)(1 << (abilities.IndexOf(ab)));
                        }
                        npkx.iv = new IVSet(pokemon.iv.getIV());
                        npkx.hpev = pokemon.hpev;
                        npkx.atev = pokemon.atev;
                        npkx.dfev = pokemon.dfev;
                        npkx.saev = pokemon.saev;
                        npkx.sdev = pokemon.sdev;
                        npkx.spev = pokemon.spev;
                        npkx.happiness = pokemon.happiness;
                        npkx.pokeball = pokemon.pokeball;
                        npkx.pid = pokemon.pid;
                        npkx.encryptionKey = pokemon.pid;
                        npkx.moveset = new MoveSet(new Move(pokemon.moveset.move1.move, pokemon.moveset.move1.pp, pokemon.moveset.move1.ppUp),
                            new Move(pokemon.moveset.move2.move, pokemon.moveset.move2.pp, pokemon.moveset.move2.ppUp), new Move(pokemon.moveset.move3.move, pokemon.moveset.move3.pp, pokemon.moveset.move3.ppUp),
                            new Move(pokemon.moveset.move4.move, pokemon.moveset.move4.pp, pokemon.moveset.move4.ppUp));
                        npkx.eggmoves = new MoveSet();
                        npkx.exp = pokemon.exp;
                        npkx.level = pokemon.level;
                        npkx.nature = pokemon.nature;
                        npkx.language = pokemon.language;
                        npkx.version = pokemon.version;
                        npkx.item = pokemon.item;
                        npkx.nick = pokemon.nick;
                        npkx.ot = pokemon.ot;
                        npkx.OTtraded = "";
                        npkx.gen = pokemon.gen;
                        npkx.genderless = pokemon.genderless;
                        npkx.female = pokemon.female;
                        npkx.isEmpty = pokemon.isEmpty;
                        npkx.isFateful = pokemon.isFateful;
                        npkx.isHatched = pokemon.isHatched;
                        npkx.isShiny = pokemon.isShiny;
                        npkx.id = pokemon.id;
                        npkx.sid = pokemon.sid;
                        npkx.dateegg = pokemon.dateegg;
                        npkx.datemet = pokemon.datemet;
                        npkx.genderRatio = (PkmLib.GenderRatios)pokemon.genderRatio;
                        npkx.markings = pokemon.markings;
                        npkx.sprite = pokemon.sprite;
                        npkx.status = pokemon.status;
                        npkx.locationmet = Convert.ToString(Pikaedit_Lib.PkmLib.locationValues[Pikaedit_Lib.PkmLib.locations.IndexOf(pokemon.locationmet)]);
                        npkx.eggloc = Convert.ToString(Pikaedit_Lib.PkmLib.locationValues[Pikaedit_Lib.PkmLib.locations.IndexOf(pokemon.eggloc)]);
                        npkx.nicktb = pokemon.nick;
                        npkx.ottb = pokemon.ottb;
                        npkx.levelmet = pokemon.levelmet;
                        npkx.updateStats();
                        npkx.save();
                        npkx = new Pokemon(npkx.data);
                        loadPKX(npkx);
                        break;
                    default:
                        npkx = new Pokemon(File.ReadAllBytes(loadDialog.FileName));
                        if (!npkx.isEmpty)
                        {
                            loadPKX(npkx);
                        }
                        break;
                }
                //switch (Path.GetExtension(loadDialog.FileName))
                //{
                //    case ".pkx":
                //        Pokemon npkx = new Pokemon(File.ReadAllBytes(loadDialog.FileName));
                //        if (!npkx.isEmpty)
                //        {
                //            loadPKX(npkx);
                //        }
                //        break;
                //    //case ".sav":
                //    //    savefile = new SaveFile(loadDialog.FileName);
                //    //    //loadSAV();
                //    //    break;
                //}
            }
        }

        private void loadPKX(Pokemon npkx)
        {
            hatchedBox.Checked = npkx.isHatched;

            speciesBox.SelectedItem = PkmLib.speciesTranslateTo(npkx.species);
            itemBox.SelectedItem = PkmLib.itemTranslateTo(npkx.item);
            natureBox.SelectedItem = PkmLib.natureTranslateTo(npkx.nature);
            abilityBox.SelectedItem = PkmLib.abilityTranslateTo(npkx.ability);
            switch (npkx.abilityIndex)
            {
                case Pokemon.AbilityIndex.Ability_0:
                    indexBox.SelectedIndex = 0;
                    break;
                case Pokemon.AbilityIndex.Ability_1:
                    indexBox.SelectedIndex = 1;
                    break;
                case Pokemon.AbilityIndex.HA_Ability:
                    indexBox.SelectedIndex = 2;
                    break;
            }
            versionBox.SelectedItem = npkx.version;
            formBox.SelectedItem = npkx.form;
            strainBox.SelectedIndex = (npkx.pokerus >> 4);
            try
            {
                pkrsDaysBox.SelectedIndex = (npkx.pokerus & 0xF);
            }
            catch (Exception ex)
            {

            }
            languageBox.SelectedItem = npkx.language;
            statusBox.SelectedItem = npkx.status;

            pokeballBox.SelectedItem = npkx.pokeball;
            if (PkmLib.locations.Contains(npkx.locationmet))
            {
                locationBox.SelectedItem = PkmLib.locationTranslateTo(npkx.locationmet);
            }
            else
            {
                locationBox.Text = npkx.locationmet;
            }
            if (PkmLib.locations.Contains(npkx.eggloc))
            {
                locationEggBox.SelectedItem = PkmLib.locationTranslateTo(npkx.eggloc);
            }
            else
            {
                locationEggBox.Text = npkx.eggloc;
            }
            

            encryptionBox.Text = Convert.ToString(npkx.encryptionKey);
            checksumBox.Text = Convert.ToString(npkx.checksum);
            levelBox.Text = Convert.ToString(npkx.level);
            expBox.Text = Convert.ToString(npkx.exp);
            happinessBox.Text = Convert.ToString(npkx.happiness);
            hpIV.Text = Convert.ToString(npkx.iv.hp);
            atIV.Text = Convert.ToString(npkx.iv.atk);
            dfIV.Text = Convert.ToString(npkx.iv.def);
            saIV.Text = Convert.ToString(npkx.iv.spa);
            sdIV.Text = Convert.ToString(npkx.iv.spd);
            spIV.Text = Convert.ToString(npkx.iv.spe);
            hpEV.Text = Convert.ToString(npkx.hpev);
            atEV.Text = Convert.ToString(npkx.atev);
            dfEV.Text = Convert.ToString(npkx.dfev);
            saEV.Text = Convert.ToString(npkx.saev);
            sdEV.Text = Convert.ToString(npkx.sdev);
            spEV.Text = Convert.ToString(npkx.spev);
            hpBox.Text = Convert.ToString(npkx.hp);
            maxhpBox.Text = Convert.ToString(npkx.maxhp);
            attackBox.Text = Convert.ToString(npkx.attack);
            defenseBox.Text = Convert.ToString(npkx.defense);
            spattackBox.Text = Convert.ToString(npkx.spatk);
            spdefenseBox.Text = Convert.ToString(npkx.spdef);
            speedBox.Text = Convert.ToString(npkx.speed);
            idBox.Text = Convert.ToString(npkx.id);
            sidBox.Text = Convert.ToString(npkx.sid);
            levelMetBox.Text = Convert.ToString(npkx.levelmet);
            countryBox.Text = Convert.ToString(npkx.countryID);
            regionBox.Text = Convert.ToString(npkx.regionID);
            region3dsBox.Text = Convert.ToString(npkx.region3dsID);
            pp1Box.Text = Convert.ToString(npkx.moveset.move1.pp);
            pp2Box.Text = Convert.ToString(npkx.moveset.move2.pp);
            pp3Box.Text = Convert.ToString(npkx.moveset.move3.pp);
            pp4Box.Text = Convert.ToString(npkx.moveset.move4.pp);
            ppUp1Box.Text = Convert.ToString(npkx.moveset.move1.ppUp);
            ppUp2Box.Text = Convert.ToString(npkx.moveset.move2.ppUp);
            ppUp3Box.Text = Convert.ToString(npkx.moveset.move3.ppUp);
            ppUp4Box.Text = Convert.ToString(npkx.moveset.move4.ppUp);
            affectionBox.Text = Convert.ToString(npkx.amieAffection);
            fullnessBox.Text = Convert.ToString(npkx.amieFullness);
            enjoymentBox.Text = Convert.ToString(npkx.amieEnjoyment);

            nickBox.Text = npkx.nick;
            otBox.Text = npkx.ot;
            otTradedBox.Text = npkx.OTtraded;
            dateBox.Text = npkx.datemet;
            dateEggBox.Text = npkx.dateegg;

            isNickBox.Checked = npkx.iv.isNick;
            isEggBox.Checked = npkx.iv.isEgg;
            if (npkx.genderot == 0)
            {
                maleOTButton.Checked = true;
            }
            else
            {
                femaleOTButton.Checked = true;
            }

            if (npkx.female == 1)
            {
                femaleButton.Checked = true;
            }
            else
            {
                if (npkx.genderless == 1)
                {
                    genderlessButton.Checked = true;
                }
                else
                {
                    maleButton.Checked = true;
                }
            }
            move1Box.SelectedItem = PkmLib.lmoves[npkx.moveset.move1.move];
            move2Box.SelectedItem = PkmLib.lmoves[npkx.moveset.move2.move];
            move3Box.SelectedItem = PkmLib.lmoves[npkx.moveset.move3.move];
            move4Box.SelectedItem = PkmLib.lmoves[npkx.moveset.move4.move];
            eggMove1Box.SelectedItem = PkmLib.lmoves[npkx.eggmoves.move1.move];
            eggMove2Box.SelectedItem = PkmLib.lmoves[npkx.eggmoves.move2.move];
            eggMove3Box.SelectedItem = PkmLib.lmoves[npkx.eggmoves.move3.move];
            eggMove4Box.SelectedItem = PkmLib.lmoves[npkx.eggmoves.move4.move];
            circleMark.Checked = (npkx.markings & 1) == 1;
            TriangleMark.Checked = ((npkx.markings >> 1) & 1) == 1;
            SquareMark.Checked = ((npkx.markings >> 2) & 1) == 1;
            HeartMark.Checked = ((npkx.markings >> 3) & 1) == 1;
            StarMark.Checked = ((npkx.markings >> 4) & 1) == 1;
            DiamondMark.Checked = ((npkx.markings >> 5) & 1) == 1;
            pidBox.Text = Convert.ToString(npkx.pid);
            //Lists
            bool[] b = npkx.supertraining.getFlags();
            for (int i = 0; i < b.Length; i++)
            {
                stFlags.SetItemChecked(i, b[i]);
            }
            b = npkx.ribbon1.getFlags();
            for (int i = 0; i < b.Length; i++)
            {
                ribbonList1.SetItemChecked(i, b[i]);
            }
            b = npkx.ribbon2.getFlags();
            for (int i = 0; i < b.Length; i++)
            {
                ribbonList2.SetItemChecked(i, b[i]);
            }
            b = npkx.ribbon3.getFlags();
            for (int i = 0; i < ribbonList3.Items.Count; i++)
            {
                ribbonList3.SetItemChecked(i, b[i]);
            }
            pkx = new Pokemon(npkx.data);
        }

        private void gender_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton)
            {
                RadioButton a = (RadioButton)sender;
                if (a.Name.Equals("maleButton"))
                {
                    pkx.female = 0;
                    pkx.genderless = 0;
                }
                if (a.Name.Equals("femaleButton"))
                {
                    pkx.female = 1;
                    pkx.genderless = 0;
                }
                if (a.Name.Equals("genderlessButton"))
                {
                    pkx.female = 0;
                    pkx.genderless = 1;
                }
            }
        }

        private void genderOT_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton)
            {
                RadioButton a = (RadioButton)sender;
                if (a.Name.Equals("maleOTButton"))
                {
                    pkx.genderot = 0;
                }
                if (a.Name.Equals("femaleOTButton"))
                {
                    pkx.genderot = 1;
                }
            }
        }

        private void saveMenu_Click(object sender, EventArgs e)
        {
            saveDialog.Filter = "Party XY file (*.pkx;*.pk6)|*.pkx;*.pk6|PC XY file (*.pkx;*.pk6)|*.pkx;*.pk6|Encrypted Party XY file (*.ekx;*.bin)|*.ekx;*.bin|Encrypted PC XY file (*.ekx;*.bin)|*.ekx;*.bin";
            pkx.save();
            checksumBox.Text = Convert.ToString(pkx.checksum);
            DialogResult res = saveDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                byte[] p;
                switch (saveDialog.FilterIndex)
                {
                    case 1:
                        File.WriteAllBytes(saveDialog.FileName, pkx.data);
                        break;
                    case 2:
                        File.WriteAllBytes(saveDialog.FileName, pkx.getPC());
                        break;
                    case 3:
                        File.WriteAllBytes(saveDialog.FileName, pkx.getEncrypted());
                        break;
                    case 4:
                        File.WriteAllBytes(saveDialog.FileName, pkx.getEncrypted(false));
                        break;
                }
            }
            //loadPKX(new Pokemon(File.ReadAllBytes(saveDialog.FileName)));
        }

        private void changeLanguage(object sender, EventArgs e)
        {
            ToolStripMenuItem[] m = { englishLanguage, frenchLanguage };
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem a = (ToolStripMenuItem)sender;
                //Get current data to translate
                int species = PkmLib.lspecies.IndexOf(speciesBox.SelectedItem.ToString());
                int ability = PkmLib.labilities.IndexOf(abilityBox.SelectedItem.ToString());
                int nature = PkmLib.lnatures.IndexOf(natureBox.SelectedItem.ToString());
                int item = PkmLib.litems.IndexOf(itemBox.SelectedItem.ToString());
                int[] location = { PkmLib.llocations.IndexOf(locationBox.SelectedItem.ToString()), PkmLib.llocations.IndexOf(locationEggBox.SelectedItem.ToString()) };
                int[] moves = { PkmLib.lmoves.IndexOf(move1Box.SelectedItem.ToString()), PkmLib.lmoves.IndexOf(move2Box.SelectedItem.ToString()), PkmLib.lmoves.IndexOf(move3Box.SelectedItem.ToString()),
                                  PkmLib.lmoves.IndexOf(move4Box.SelectedItem.ToString()), PkmLib.lmoves.IndexOf(eggMove1Box.SelectedItem.ToString()),
                                  PkmLib.lmoves.IndexOf(eggMove2Box.SelectedItem.ToString()), PkmLib.lmoves.IndexOf(eggMove3Box.SelectedItem.ToString()), PkmLib.lmoves.IndexOf(eggMove4Box.SelectedItem.ToString()) };

                if(a.Name.Equals("englishLanguage"))
                {
                    PkmLib.changeLanguage(0);
                }
                if (a.Name.Equals("frenchLanguage"))
                {
                    PkmLib.changeLanguage(3);
                }

                //Fill boxes
                fillComboBoxes();

                //Reinsert data translated
                speciesBox.SelectedItem = PkmLib.lspecies[species];
                abilityBox.SelectedItem = PkmLib.labilities[ability];
                itemBox.SelectedItem = PkmLib.litems[item];
                natureBox.SelectedItem = PkmLib.lnatures[nature];
                locationBox.SelectedItem = PkmLib.llocations[location[0]];
                locationEggBox.SelectedItem = PkmLib.llocations[location[1]];
                move1Box.SelectedItem = PkmLib.lmoves[moves[0]];
                move2Box.SelectedItem = PkmLib.lmoves[moves[1]];
                move3Box.SelectedItem = PkmLib.lmoves[moves[2]];
                move4Box.SelectedItem = PkmLib.lmoves[moves[3]];
                eggMove1Box.SelectedItem = PkmLib.lmoves[moves[4]];
                eggMove2Box.SelectedItem = PkmLib.lmoves[moves[5]];
                eggMove3Box.SelectedItem = PkmLib.lmoves[moves[6]];
                eggMove4Box.SelectedItem = PkmLib.lmoves[moves[7]];

                for (int i = 0; i < m.Length; i++)
                {
                    if (m[i].Name.Equals(a.Name))
                    {
                        m[i].Checked = true;
                    }
                    else
                    {
                        m[i].Checked = false;
                    }
                }
            }
        }

        private void nick_TextChanged(object sender, EventArgs e)
        {
            pkx.nick = nickBox.Text;
        }

        private void ot_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox a = (TextBox)sender;
                if(a.Name.Equals("otBox"))
                {
                    pkx.ot = a.Text;
                }
                if (a.Name.Equals("otTradedBox"))
                {
                    pkx.OTtraded = a.Text;
                }
            }
        }

        private void resetMenu_Click(object sender, EventArgs e)
        {
            loadPKX(new Pokemon(PkmLib.resetpkx));
        }

        private void aboutMenu_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem a = (ToolStripMenuItem)sender;
                if (a.Name.Equals("webPageMenu"))
                {
                    Process.Start("http://pikaedit.wordpress.com/");
                }
                if (a.Name.Equals("facebookMenu"))
                {
                    Process.Start("http://www.facebook.com/Pikaedit");
                }
                if (a.Name.Equals("changelogMenu"))
                {
                    //MessageBox.Show(Properties.Resources.changelog, "Changelog", MessageBoxButtons.OK);
                    Changelog c = new Changelog();
                    c.Show();
                }
                if (a.Name.Equals("aboutMenu"))
                {
                    MessageBox.Show(Properties.Resources.about,"About Pikaedit XY",MessageBoxButtons.OK);
                }
            }
        }

        private void randomivButton_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            hpIV.Text = Convert.ToString(r.Next(0, 32));
            atIV.Text = Convert.ToString(r.Next(0, 32));
            dfIV.Text = Convert.ToString(r.Next(0, 32));
            saIV.Text = Convert.ToString(r.Next(0, 32));
            sdIV.Text = Convert.ToString(r.Next(0, 32));
            spIV.Text = Convert.ToString(r.Next(0, 32));
            updateStats();
        }

        private void stFlags_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            bool[] flags = new bool[30];
            for (int i = 0; i < 30; i++)
            {
                if (i == e.Index)
                {
                    flags[i] = e.NewValue == CheckState.Checked;
                }
                else
                {
                    flags[i] = stFlags.GetItemChecked(i);
                }
            }
            pkx.supertraining.setChanges(flags);
        }

        private void ribbons_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (sender is CheckedListBox)
            {
                CheckedListBox a = sender as CheckedListBox;
                bool[] flags = new bool[a.Items.Count];
                for (int i = 0; i < flags.Length; i++)
                {
                    if (i == e.Index)
                    {
                        flags[i] = e.NewValue == CheckState.Checked;
                    }
                    else
                    {
                        flags[i] = a.GetItemChecked(i);
                    }
                }
                if (a.Name == "ribbonList1")
                {
                    pkx.ribbon1.setChanges(flags);
                }
                if (a.Name == "ribbonList2")
                {
                    pkx.ribbon2.setChanges(flags);
                }
                if (a.Name == "ribbonList3")
                {
                    pkx.ribbon3.setChanges(flags);
                }
            }
        }

        #region plugin

        private void addPlugin_Click(object sender, EventArgs e)
        {
            loadDialog.Filter = "Plugin (*.dll)|*.dll";
            loadDialog.FileName = "*.dll";
            DialogResult res = loadDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                addPluginFrom(loadDialog.FileName);
            }
        }

        private void addPluginFrom(string file)
        {
            //Type[] pluginTypes = Assembly.UnsafeLoadFrom(loadDialog.FileName).GetTypes();
            Type[] pluginTypes;
            try
            {
                pluginTypes = Assembly.Load(File.ReadAllBytes(file)).GetTypes();
            }
            catch (Exception e)
            {
                return;
            }
            foreach (Type t in pluginTypes)
            {
                if (t.GetInterfaces().Contains(typeof(Pikaedit_Lib.IPlugin)))
                {
                    Pikaedit_Lib.IPlugin plugin = Activator.CreateInstance(t) as Pikaedit_Lib.IPlugin;
                    if (plugin.getPluginGen == Pikaedit_Lib.PluginGen.XY || plugin.getPluginGen == Pikaedit_Lib.PluginGen.Gen4_XY ||
                        plugin.getPluginGen == Pikaedit_Lib.PluginGen.Gen5_XY || plugin.getPluginGen == Pikaedit_Lib.PluginGen.All)
                    {
                        try
                        {
                            plugin.getPluginGen = Pikaedit_Lib.PluginGen.XY;
                        }
                        catch
                        {

                        }
                        plugins.Add(plugin);
                        pluginFiles.Add(Application.StartupPath + Path.DirectorySeparatorChar + "Plugins" + Path.DirectorySeparatorChar + Path.GetFileName(file));
                        ToolStripMenuItem a = new ToolStripMenuItem();
                        a.Name = plugin.getName();
                        a.Click += new EventHandler(loadPlugin);
                        a.Text = plugin.getName() + " [" + plugin.getAuthor() + "] v" + plugin.getVersion();
                        a.AutoToolTip = true;
                        a.ToolTipText = plugin.getPluginInfo();
                        pluginsMenu.DropDownItems.Add(a);
                        ToolStripMenuItem b = new ToolStripMenuItem();
                        b.Name = plugin.getName() + "Remove";
                        b.Text = "Remove Plugin";
                        //b.Click += new EventHandler(removePlugin);
                        b.Click += delegate { removePlugin(plugin.getName()); };
                        a.DropDownItems.Add(b);
                        ToolStripMenuItem c = new ToolStripMenuItem();
                        c.Name = plugin.getName() + "Update";
                        c.Text = "Update Plugin";
                        c.Click += delegate { callPluginUpdater(plugin.getName()); };
                        a.DropDownItems.Add(c);
                        string[] dllFileNames = null;
                        if (Directory.Exists(Application.StartupPath + Path.DirectorySeparatorChar + "Plugins"))
                        {
                            dllFileNames = Directory.GetFiles(Application.StartupPath + Path.DirectorySeparatorChar + "Plugins", "*.dll");
                            if (dllFileNames != null)
                            {
                                for (int i = 0; i < dllFileNames.Length; i++)
                                {
                                    dllFileNames[i] = Path.GetFileName(dllFileNames[i]);
                                }
                                if (!dllFileNames.Contains(Path.GetFileName(file)))
                                {
                                    File.Copy(file, Application.StartupPath + Path.DirectorySeparatorChar + "Plugins" + Path.DirectorySeparatorChar + Path.GetFileName(file));
                                }
                            }
                        }
                    }
                }
            }
        }

        private void loadPlugin(object sender, EventArgs e)
        {
            ToolStripMenuItem a = (ToolStripMenuItem)sender;
            foreach (Pikaedit_Lib.IPlugin plugin in plugins)
            {
                if (plugin.getName() == a.Name)
                {
                    if (plugin.setRequirement == Pikaedit_Lib.Requirement.Loaded_Save_File)
                    {
                        MessageBox.Show("This plugin uses Gen 5 or Gen 4 save file data, Pikaedit XY doesn't support save file editing", "Plugin requires Gen 4/5 Save File", MessageBoxButtons.OK);
                        return;
                    }
                    try
                    {
                        pkx.save();
                        plugin.getXYPokemon = new Pikaedit_Lib.PokemonXY(pkx.data);
                    }
                    catch
                    {

                    }
                    //try
                    //{
                    //    plugin.getSaveFile = new Pikaedit_Lib.SaveFile(savefile.getSavedData());
                    //}
                    //catch
                    //{

                    //}
                    plugin.Do();
                    try
                    {
                        switch (plugin.setPostProcess)
                        {
                            case Pikaedit_Lib.Process.Load_Pkm:
                                loadPKX(new Pokemon(plugin.getXYPokemon.data));
                                break;
                            //case Pikaedit_Lib.Process.Update_Save_File:
                            //    savefile = new SaveFile(plugin.getSaveFile.getSavedData());
                            //    loadSAV();
                            //    break;
                            case Pikaedit_Lib.Process.Update_Plugin:
                                callPluginUpdater(plugin.getName());
                                break;
                        }
                    }
                    catch
                    {

                    }
                }
            }
            //if (File.Exists("temp.sav"))
            //{
            //    File.Delete("temp.sav");
            //}
        }

        private void removePlugin(string p,bool removeFile = true)
        {
            foreach (Pikaedit_Lib.IPlugin plugin in plugins)
            {
                if (plugin.getName() == p)
                {
                    pluginsMenu.DropDownItems.RemoveByKey(p);
                    int i = plugins.IndexOf(plugin);
                    plugins.Remove(plugin);
                    if (removeFile)
                    {
                        if (File.Exists(pluginFiles[i]))
                        {
                            File.Delete(pluginFiles[i]);
                        }
                    }
                    pluginFiles.RemoveAt(i);
                    break;
                }
            }
        }

        private void updatePlugins_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Application.StartupPath + Path.DirectorySeparatorChar + "Plugins"))
            {
                if (Directory.GetFiles(Application.StartupPath + Path.DirectorySeparatorChar + "Plugins").Length != 0)
                {
                    System.Net.WebClient downloader = new System.Net.WebClient();
                    string extraPlugins = downloader.DownloadString("https://dl.dropboxusercontent.com/u/87538979/Pikaedit/Plugins/PluginUpdater.txt");
                    Dictionary<string, string> pluginslist = new Dictionary<string, string>();
                    string[] readlist = extraPlugins.Split('\n');
                    for (int i = 0; i < readlist.Length; i++)
                    {
                        readlist[i] = readlist[i].TrimEnd('\r');
                        if (!string.IsNullOrEmpty(readlist[i]))
                        {
                            pluginslist.Add(readlist[i].Split('|')[0], readlist[i].Split('|')[1]);
                        }
                    }
                    //Remove all plugins from Pikaedit
                    List<string> pluginsName = new List<string>();
                    int s = plugins.Count;
                    if (s != 0)
                    {
                        List<Pikaedit_Lib.IPlugin> temp = new List<Pikaedit_Lib.IPlugin>();
                        temp.AddRange(plugins.ToArray());
                        foreach (Pikaedit_Lib.IPlugin plugin in plugins)
                        {
                            if (pluginslist.Keys.Contains(plugin.getName()))
                            {
                                pluginsMenu.DropDownItems.RemoveAt(temp.IndexOf(plugin) + 2);
                                pluginsName.Add(plugin.getName());
                                int i = temp.IndexOf(plugin);
                                temp.Remove(plugin);
                                //if (File.Exists(pluginFiles[i]))
                                //{
                                //    File.Delete(pluginFiles[i]);
                                //}
                                pluginFiles.RemoveAt(i);
                            }
                        }

                        plugins = temp;
                        string args = "";
                        foreach (string name in pluginsName)
                        {
                            args += "\"" + name + "\" ";
                        }
                        if (args != string.Empty)
                        {
                            callPluginUpdater(args);
                        }
                    }
                }
            }
        }

        private void callPluginUpdater(string args)
        {
            if (!args.Contains("\""))
            {
                removePlugin(args,false);
                args = "\"" + args + "\"";
            }
            File.WriteAllBytes(Application.StartupPath + Path.DirectorySeparatorChar + "PluginUpdater.exe", Properties.Resources.PluginUpdater);
            Process process = Process.Start(Application.StartupPath + Path.DirectorySeparatorChar + "PluginUpdater.exe", args.TrimEnd(' '));
            process.WaitForExit();
            File.Delete(Application.StartupPath + Path.DirectorySeparatorChar + "PluginUpdater.exe");
            string[] dllFileNames = null;
            if (Directory.Exists(Application.StartupPath + Path.DirectorySeparatorChar + "Plugins"))
            {
                dllFileNames = Directory.GetFiles(Application.StartupPath + Path.DirectorySeparatorChar + "Plugins", "*.dll");
                if (dllFileNames != null)
                {
                    for (int i = 0; i < dllFileNames.Length; i++)
                    {
                        if (!pluginFiles.Contains(dllFileNames[i]))
                        {
                            addPluginFrom(dllFileNames[i]);
                        }
                    }
                }
            }
        }
        #endregion

        private void movesetAnalysis_Click(object sender, EventArgs e)
        {
            Pikaedit_Lib.Veekun.Initialize();
            List<ushort> legalMoves = Pikaedit_Lib.Veekun.getMoveset(pkx.species, Pikaedit_Lib.GenSearch.From_3_to_6, pkx.form, Pikaedit_Lib.MovesetType.All);
            string s = "";
            s += (legalMoves.Contains(pkx.moveset.move1.move) || pkx.moveset.move1.move==0 ? PkmLib.lmoves[pkx.moveset.move1.move] + " - Legal" : PkmLib.lmoves[pkx.moveset.move1.move] + " - Illegal") + "\n";
            s += (legalMoves.Contains(pkx.moveset.move2.move) || pkx.moveset.move2.move == 0 ? PkmLib.lmoves[pkx.moveset.move2.move] + " - Legal" : PkmLib.lmoves[pkx.moveset.move2.move] + " - Illegal") + "\n";
            s += (legalMoves.Contains(pkx.moveset.move3.move) || pkx.moveset.move3.move == 0 ? PkmLib.lmoves[pkx.moveset.move3.move] + " - Legal" : PkmLib.lmoves[pkx.moveset.move3.move] + " - Illegal") + "\n";
            s += (legalMoves.Contains(pkx.moveset.move4.move) || pkx.moveset.move4.move == 0 ? PkmLib.lmoves[pkx.moveset.move4.move] + " - Legal" : PkmLib.lmoves[pkx.moveset.move4.move] + " - Illegal") + "\n\nHatched Moves:\n";
            s += (legalMoves.Contains(pkx.eggmoves.move1.move) || pkx.eggmoves.move1.move == 0? PkmLib.lmoves[pkx.eggmoves.move1.move] + " - Legal" : PkmLib.lmoves[pkx.eggmoves.move1.move] + " - Illegal") + "\n";
            s += (legalMoves.Contains(pkx.eggmoves.move2.move) || pkx.eggmoves.move2.move == 0 ? PkmLib.lmoves[pkx.eggmoves.move2.move] + " - Legal" : PkmLib.lmoves[pkx.eggmoves.move2.move] + " - Illegal") + "\n";
            s += (legalMoves.Contains(pkx.eggmoves.move3.move) || pkx.eggmoves.move3.move == 0 ? PkmLib.lmoves[pkx.eggmoves.move3.move] + " - Legal" : PkmLib.lmoves[pkx.eggmoves.move3.move] + " - Illegal") + "\n";
            s += (legalMoves.Contains(pkx.eggmoves.move4.move) || pkx.eggmoves.move4.move == 0 ? PkmLib.lmoves[pkx.eggmoves.move4.move] + " - Legal" : PkmLib.lmoves[pkx.eggmoves.move4.move] + " - Illegal");
            MessageBox.Show(s, "Moveset Check", MessageBoxButtons.OK);
        }

        private void editorMode_Switch(object sender, EventArgs e)
        {
            ToolStripMenuItem[] m = { legalSwitch, hackSwitch };
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem a = (ToolStripMenuItem)sender;
                for (int i = 0; i < m.Length; i++)
                {
                    if (m[i].Name.Equals(a.Name))
                    {
                        m[i].Checked = true;
                    }
                    else
                    {
                        m[i].Checked = false;
                    }
                }
                if (a.Equals(legalSwitch))
                {
                    Settings.legalMode = true;
                    loadAbilities();
                    loadMovesets();
                }
                else
                {
                    Settings.legalMode = false;
                    fillComboBoxes();
                    loadPKX(pkx);
                }
            }
        }

        private void maxPPButton_Click(object sender, EventArgs e)
        {
            ppUp1Box.Text = "3";
            ppUp2Box.Text = "3";
            ppUp3Box.Text = "3";
            ppUp4Box.Text = "3";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.save();
        }
    }
}
