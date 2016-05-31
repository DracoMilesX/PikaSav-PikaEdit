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
using System.Threading;
using gts;
using System.Globalization;
using System.Reflection;

namespace Pikaedit_Gen4
{
    public partial class Form1 : Form
    {
        public Pokemon pkm;
        public SaveFile savefile;
        public readonly Dns _dns = new Dns();
        public readonly Gts _gts = new Gts();

        private int sSlot = 0;
        private int box = 0;
        private int pIndex = 0;
        private int draggedIndex = 0;
        private int draggedBox = 0;
        private string culture;
        private List<Slot> boxSlots;
        private List<Slot> partySlots;
        private List<Slot> dayCareSlots;
        private List<byte> legalAbilities = new List<byte>();
        private List<Pikaedit_Lib.IPlugin> plugins = new List<Pikaedit_Lib.IPlugin>();
        private List<string> pluginFiles = new List<string>();

        public Form1()
        {
            //Settings.load();
            //string cl = "en";
            //if (PkmLib.lang == 3 | PkmLib.lang == 4)
            //{
            //    if (PkmLib.lang == 3)
            //    {
            //        cl = "fr";
            //    } if (PkmLib.lang == 4)
            //    {
            //        cl = "de";
            //    }
            //    Thread.CurrentThread.CurrentUICulture = new CultureInfo(cl);
            //}
            //if (!Directory.Exists(Application.StartupPath + "\\en"))
            //{
            //    Directory.CreateDirectory("en");
            //    File.WriteAllBytes(Application.StartupPath + "\\en\\Pikaedit.resources.dll", Properties.Resources.Englishresx);
            //}
            //Check Plugin folder
            if (!Directory.Exists(Application.StartupPath + "\\Plugins"))
            {
                Directory.CreateDirectory(Application.StartupPath + "\\Plugins");
            }
            InitializeComponent();
            //if (!File.Exists("PikaeditLib.dll"))
            //{
            //    pluginsMenu.Visible = false;
            //}
            PkmLib.Initialize();
            PkmLib.changeLanguage(PkmLib.lang);
            Notification.Initialize();
            Settings.Initialize();
            _gts.DistributionMode = gts.Operation.SendReceiveGenV;
            Settings.gtsStarted = false;
            Settings.gtsFolderR = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            gtsFolderReceive.Text = Settings.gtsFolderR;
            _gts.GenVGetFolder = Settings.gtsFolderR;
            getIP(localButton, null);
            //culture = cl;
            pkm = new Pokemon(PkmLib.resetpkm);
            fillComboBoxes();
            loadPKM(new Pokemon(PkmLib.resetpkm));
            dateBox.Text = dateEggBox.Text = DateTime.Now.ToString("dd/MM/yy");
            dateEggBox.Text = "";
            savefile = new SaveFile();
            saveFileEditor.Enabled = false;
            partySlots = new List<Slot> { party1, party2, party3, party4, party5, party6 };
            boxSlots = new List<Slot> {slot1, slot2, slot3, slot4, slot5, slot6, slot7, slot8, slot9, slot10, slot11, slot12, slot13, slot14, slot15, slot16, slot17, slot18, slot19,
                             slot20, slot21, slot22, slot23, slot24, slot25, slot26, slot27, slot28, slot29, slot30};
            dayCareSlots = new List<Slot> { daycare1, daycare2 };
            this.Text = "Pikaedit Gen 4 v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().TrimEnd('.', '0');
            string[] dllFileNames = null;
            if (Directory.Exists(Application.StartupPath + "\\Plugins"))
            {
                dllFileNames = Directory.GetFiles(Application.StartupPath + "\\Plugins", "*.dll");
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
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Notification.kill();
            if (Settings.gtsStarted)
            {
                _dns.Stop();
                _gts.Stop();
            }
            Settings.save();
        }

        # region functions

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
            languageBox.Items.Clear();
            versionBox.Items.Clear();
            speciesBox.Items.AddRange(PkmLib.lspecies.ToArray());
            speciesBox.Items.RemoveAt(0);
            itemBox.Items.AddRange(PkmLib.litems.ToArray());
            pokeballBox.Items.Clear();
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
            languageBox.Items.AddRange(PkmLib.llanguages.ToArray());
            versionBox.Items.AddRange(PkmLib.lversions.ToArray());
            pokeballBox.Items.AddRange(PkmLib.pokeballs.ToArray());
        }

        private void combobox_TextChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox)
            {
                ComboBox a = (ComboBox)sender;
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

        private void changeLanguage(object sender, EventArgs e)
        {
            ToolStripMenuItem[] m = { englishLanguage, frenchLanguage, germanLanguage, koreanLanguage };
            if (sender is ToolStripMenuItem)
            {
                Pokemon pkmn = pkm.Clone();
                pkmn.save();
                ToolStripMenuItem a = (ToolStripMenuItem)sender;
                //Get current data to translate
                int species = PkmLib.lspecies.IndexOf(speciesBox.SelectedItem.ToString());
                int ability = PkmLib.labilities.IndexOf(abilityBox.SelectedItem.ToString());
                int nature = PkmLib.lnatures.IndexOf(natureBox.SelectedItem.ToString());
                int item = PkmLib.litems.IndexOf(itemBox.SelectedItem.ToString());
                int[] location = { PkmLib.llocations.IndexOf(locationBox.SelectedItem.ToString()), PkmLib.llocations.IndexOf(locationEggBox.SelectedItem.ToString()) };
                int[] moves = { PkmLib.lmoves.IndexOf(move1Box.SelectedItem.ToString()), PkmLib.lmoves.IndexOf(move2Box.SelectedItem.ToString()), PkmLib.lmoves.IndexOf(move3Box.SelectedItem.ToString()),
                                  PkmLib.lmoves.IndexOf(move4Box.SelectedItem.ToString()) };
                int language = PkmLib.llanguages.IndexOf(languageBox.SelectedItem.ToString());
                int version = PkmLib.lversions.IndexOf(versionBox.SelectedItem.ToString());

                string cl = "en";
                if (a.Name.Equals("englishLanguage"))
                {
                    PkmLib.changeLanguage(0);
                    cl = "en";
                }
                if (a.Name.Equals("frenchLanguage"))
                {
                    PkmLib.changeLanguage(3);
                    cl = "fr";
                }
                if (a.Name.Equals("germanLanguage"))
                {
                    PkmLib.changeLanguage(4);
                    cl = "de";
                }
                if (a.Name.Equals("koreanLanguage"))
                {
                    PkmLib.changeLanguage(6);
                    cl = "en";
                }

                //Fill boxes
                fillComboBoxes();

                if (File.Exists(Application.StartupPath + "\\" + cl + "\\Pikaedit.resources.dll"))
                {
                    if (!cl.Equals(culture))
                    {
                        changeCulture(cl);
                        culture = cl;
                    }
                }

                loadPKM(pkmn);
                dateEggBox.Enabled = pkmn.isHatched;
                locationEggBox.Enabled = pkmn.isHatched;
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
                languageBox.SelectedItem = PkmLib.llanguages[language];
                versionBox.SelectedItem = PkmLib.lversions[version];

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

        public void changeCulture(string cl)
        {
            foreach (Control c in this.Controls)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
                resources.ApplyResources(c, c.Name, new CultureInfo(cl));
                if (c.HasChildren)
                {
                    cultureChildren(c, cl);
                }
            }
            foreach (ToolStripMenuItem c in MenuStrip1.Items)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
                resources.ApplyResources(c, c.Name, new CultureInfo(cl));
                if (c.HasDropDownItems)
                {
                    cultureItems(c, cl);
                }
            }
        }

        private void cultureChildren(Control c, string cl)
        {
            foreach (Control c2 in c.Controls)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
                resources.ApplyResources(c2, c2.Name, new CultureInfo(cl));
                if (c2.HasChildren)
                {
                    cultureChildren(c2, cl);
                }
            }
        }

        private void cultureItems(ToolStripMenuItem c, string cl)
        {
            foreach (ToolStripItem c2 in c.DropDownItems)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
                resources.ApplyResources(c2, c2.Name, new CultureInfo(cl));
            }
        }

        private void getLanguageInterfaces(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem a = (ToolStripMenuItem)sender;
                System.Net.WebClient downloader = new System.Net.WebClient();
                if (a.Name.Equals("frenchInterface"))
                {
                    if (!File.Exists(Application.StartupPath + "\\fr\\Pikaedit.resources.dll"))
                    {

                        try
                        {
                            if (!Directory.Exists(Application.StartupPath + "\\fr"))
                            {
                                Directory.CreateDirectory("fr");
                            }
                            downloader.DownloadFile(Properties.Resources.frenchInterface, Application.StartupPath + "\\fr\\Pikaedit.resources.dll");
                            MessageBox.Show("French Interface Downloaded! Change to French Language to change Pikaedit's interface", "Downloaded!", MessageBoxButtons.OK);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Unable to download\n" + ex.Message.ToString(), "Error", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show("This language interface has been downloaded!", "Error", MessageBoxButtons.OK);
                    }
                }
                if (a.Name.Equals("germanInterface"))
                {
                    if (!File.Exists(Application.StartupPath + "\\de\\Pikaedit.resources.dll"))
                    {

                        try
                        {
                            if (!Directory.Exists(Application.StartupPath + "\\de"))
                            {
                                Directory.CreateDirectory("de");
                            }
                            downloader.DownloadFile(Properties.Resources.germanInterface, Application.StartupPath + "\\de\\Pikaedit.resources.dll");
                            MessageBox.Show("German Interface Downloaded! Change to German Language to change Pikaedit's interface", "Downloaded!", MessageBoxButtons.OK);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Unable to download\n" + ex.Message.ToString(), "Error", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show("This language interface has been downloaded!", "Error", MessageBoxButtons.OK);
                    }
                }
            }
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
                    MessageBox.Show(Properties.Resources.changelog, "Changelog", MessageBoxButtons.OK);
                }
                if (a.Name.Equals("aboutMenu"))
                {
                    MessageBox.Show(Properties.Resources.about, "About Pikaedit XY", MessageBoxButtons.OK);
                }
            }
        }

        #endregion

        # region PKMEditor

        private void loadMovesets()
        {
            if (Settings.legalMode)
            {
                int[] mov;
                try
                {
                    int[] mov2 = { PkmLib.lmoves.IndexOf(move1Box.SelectedItem.ToString()), PkmLib.lmoves.IndexOf(move2Box.SelectedItem.ToString()), PkmLib.lmoves.IndexOf(move3Box.SelectedItem.ToString()),
                                  PkmLib.lmoves.IndexOf(move4Box.SelectedItem.ToString()) };
                    mov = mov2;
                }
                catch (Exception e)
                {
                    mov = new int[] { 0, 0, 0, 0 };
                }
                move1Box.Items.Clear();
                move2Box.Items.Clear();
                move3Box.Items.Clear();
                move4Box.Items.Clear();
                Pikaedit_Lib.Veekun.Initialize();
                //Pikaedit_Lib.MovesetType movesetType = Pikaedit_Lib.MovesetType.All;
                Pikaedit_Lib.GenSearch genType;
                switch (pkm.gen)
                {
                    case 3:
                        genType = Pikaedit_Lib.GenSearch.From_3_to_4;
                        break;
                    case 4:
                        genType = Pikaedit_Lib.GenSearch.Gen_4;
                        break;
                    default:
                        genType = Pikaedit_Lib.GenSearch.Gen_4;
                        break;
                }
                List<ushort> move = Pikaedit_Lib.Veekun.getMoveset(pkm.species, genType, pkm.form, Pikaedit_Lib.MovesetType.Levelup, pkm.level, Pikaedit_Lib.SearchByLevelType.Less_Or_Equal);
                move.AddRange(Pikaedit_Lib.Veekun.getMoveset(pkm.species, genType, pkm.form, Pikaedit_Lib.MovesetType.Form_Exclusive, pkm.level, Pikaedit_Lib.SearchByLevelType.Less_Or_Equal));
                move.AddRange(Pikaedit_Lib.Veekun.getMoveset(pkm.species, genType, pkm.form, Pikaedit_Lib.MovesetType.TMs_HMs, pkm.level, Pikaedit_Lib.SearchByLevelType.Less_Or_Equal));
                move.AddRange(Pikaedit_Lib.Veekun.getMoveset(pkm.species, genType, pkm.form, Pikaedit_Lib.MovesetType.Tutor, pkm.level, Pikaedit_Lib.SearchByLevelType.Less_Or_Equal));
                if (pkm.isHatched)
                {
                    move.AddRange(Pikaedit_Lib.Veekun.getMoveset(pkm.species, genType, pkm.form, Pikaedit_Lib.MovesetType.Egg_moves, pkm.level, Pikaedit_Lib.SearchByLevelType.Less_Or_Equal));
                }
                if (pkm.isFateful)
                {
                    move.AddRange(Pikaedit_Lib.Veekun.getMoveset(pkm.species, genType, pkm.form, Pikaedit_Lib.MovesetType.Event, pkm.level, Pikaedit_Lib.SearchByLevelType.Less_Or_Equal));
                }
                move.Add(0);
                List<string> moves = convertMoveUInt16List(move.Distinct().ToList());
                move1Box.Items.AddRange(moves.ToArray());
                move2Box.Items.AddRange(moves.ToArray());
                move3Box.Items.AddRange(moves.ToArray());
                move4Box.Items.AddRange(moves.ToArray());
                move1Box.SelectedItem = PkmLib.lmoves[mov[0]];
                move2Box.SelectedItem = PkmLib.lmoves[mov[1]];
                move3Box.SelectedItem = PkmLib.lmoves[mov[2]];
                move4Box.SelectedItem = PkmLib.lmoves[mov[3]];
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
                legalAbilities = Pikaedit_Lib.Veekun.getAbilities(pkm.species, pkm.form);
                abilityBox.Items.Clear();
                for (int i = 0; i < legalAbilities.Count; i++)
                {
                    if (i == 2)
                    {
                        legalAbilities[i] = 0;
                    }
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
                    pkm.species = PkmLib.speciesTranslate(a.SelectedItem.ToString());
                    pkm.no = (ushort)PkmLib.species.IndexOf(pkm.species);
                    formBox.Items.Clear();
                    formBox.Items.AddRange(fillForms());
                    formBox.SelectedIndex = 0;
                    nickBox.Text = pkm.species;
                    //pkm.exp = PkmLib.calculateExp(pkm.species, pkm.level);
                    expBox.Text = Convert.ToString(PkmLib.calculateExp(pkm.species, pkm.level));
                    updateStats();
                    loadMovesets();
                    loadAbilities();
                }
                if (a.Name.Equals("itemBox"))
                {
                    pkm.item = PkmLib.itemTranslate(a.SelectedItem.ToString());
                }
                if (a.Name.Equals("abilityBox"))
                {
                    pkm.ability = PkmLib.abilityTranslate(a.SelectedItem.ToString());
                }
                if (a.Name.Equals("natureBox"))
                {
                    pkm.nature = PkmLib.natureTranslate(a.SelectedItem.ToString());
                    updateStats();
                }
                if (a.Name.Equals("versionBox"))
                {
                    pkm.version = PkmLib.versionTranslate(a.SelectedItem.ToString());
                    loadMovesets();
                }
                if (a.Name.Equals("languageBox"))
                {
                    pkm.language = PkmLib.languageTranslate(a.SelectedItem.ToString());
                }
                if (a.Name.Equals("formBox"))
                {
                    pkm.form = a.SelectedItem.ToString();
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
                        pkm.pokerus = 0;
                        pkrsDaysBox.Items.Add("None");
                        pkrsDaysBox.SelectedIndex = 0;
                    }
                    else
                    {
                        pkm.pokerus = (byte)(a.SelectedIndex << 4);
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
                    pkm.pokerus = (byte)((pkm.pokerus & 0xF0) | a.SelectedIndex);
                }
                if (a.Name.Equals("move1Box"))
                {
                    pkm.moveset.move1.move = (ushort)PkmLib.lmoves.IndexOf(a.SelectedItem.ToString());
                    pp1Box.Text = Convert.ToString(PkmLib.pp[pkm.moveset.move1.move]);
                }
                if (a.Name.Equals("move2Box"))
                {
                    pkm.moveset.move2.move = (ushort)PkmLib.lmoves.IndexOf(a.SelectedItem.ToString());
                    pp2Box.Text = Convert.ToString(PkmLib.pp[pkm.moveset.move2.move]);
                }
                if (a.Name.Equals("move3Box"))
                {
                    pkm.moveset.move3.move = (ushort)PkmLib.lmoves.IndexOf(a.SelectedItem.ToString());
                    pp3Box.Text = Convert.ToString(PkmLib.pp[pkm.moveset.move3.move]);
                }
                if (a.Name.Equals("move4Box"))
                {
                    pkm.moveset.move4.move = (ushort)PkmLib.lmoves.IndexOf(a.SelectedItem.ToString());
                    pp4Box.Text = Convert.ToString(PkmLib.pp[pkm.moveset.move4.move]);
                }
                if (a.Name.Equals("locationBox"))
                {
                    pkm.locationmet = PkmLib.locationTranslate(a.SelectedItem.ToString());
                }
                if (a.Name.Equals("locationEggBox"))
                {
                    pkm.eggloc = PkmLib.locationTranslate(a.SelectedItem.ToString());
                }
                if (a.Name.Equals("indexBox"))
                {
                    if (a.SelectedItem.ToString().Equals("DW"))
                    {
                        pkm.DW = 1;
                    }
                    else
                    {
                        pkm.DW = 0;
                    }
                }
            }
        }

        private void nick_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox a = (TextBox)sender;
                if (a.Name.Equals("nickBox"))
                {
                    pkm.nick = nickBox.Text;
                    string tb = pkm.nick;
                    int i = pkm.nick.Length;
                    bool seq = false;
                    for (; i < 11; i++)
                    {
                        if (i == 10)
                        {
                            tb += "\\FFFF";
                        }
                        else
                        {
                            if (seq)
                            {
                                tb += "\\0000";
                            }
                            else
                            {
                                tb += "\\FFFF";
                                seq = true;
                            }
                        }
                    }
                    nicktbBox.Text = tb;
                }
                if (a.Name.Equals("nicktbBox"))
                {
                    pkm.nicktb = nicktbBox.Text;
                }
            }

        }

        private void ot_TextChanged(object sender, EventArgs e)
        {
            pkm.ot = otBox.Text;
            string tb = pkm.ot;
            int i = pkm.ot.Length;
            bool seq = false;
            for (; i < 8; i++)
            {
                if (i == 7)
                {
                    tb += "\\FFFF";
                }
                else
                {
                    if (seq)
                    {
                        tb += "\\0000";
                    }
                    else
                    {
                        tb += "\\0000";
                        seq = true;
                    }
                }
            }
            pkm.ottb = tb;
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
                    pkm.pid = temp;
                    ushort pid1 = 0;
                    ushort pid2 = 0;
                    pid1 = (ushort)(pkm.pid & 0xffff);
                    pid2 = (ushort)((pkm.pid >> 16) & 0xffff);
                    int c = pkm.id ^ pkm.sid;
                    int b = pid1 ^ pid2;
                    if ((c ^ b) < 8)
                    {
                        pkm.isShiny = true;
                        shinyBox.Checked = true;
                    }
                    else
                    {
                        pkm.isShiny = false;
                        shinyBox.Checked = false;
                    }
                    natureBox.SelectedItem = PkmLib.abilityTranslate(PkmLib.natures[(int)(pkm.pid % 25)]);
                }
                if (a.Name.Equals("expBox"))
                {
                    if (temp > PkmLib.maxExp(pkm.species))
                    {
                        temp = PkmLib.maxExp(pkm.species);
                        expBox.Text = Convert.ToString(temp);
                    }
                    pkm.exp = temp;
                    if (pkm.level != PkmLib.calculateLevel(pkm.species, pkm.exp))
                    {
                        pkm.level = PkmLib.calculateLevel(pkm.species, pkm.exp);
                        levelBox.Text = Convert.ToString(PkmLib.calculateLevel(pkm.species, pkm.exp));
                    }
                    updateStats();
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
                if (a.Name.Contains("ppUp"))
                {
                    if (temp > 3)
                    {
                        temp = 3;
                        a.Text = "3";
                    }
                }
                if (a.Name.Equals("levelBox"))
                {
                    pkm.level = temp;
                    if (temp != PkmLib.calculateLevel(pkm.species, pkm.exp))
                    {
                        pkm.exp = PkmLib.calculateExp(pkm.species, temp);
                        expBox.Text = Convert.ToString(PkmLib.calculateExp(pkm.species, temp));
                    }
                    updateStats();
                }
                if (a.Name.Equals("happinessBox"))
                {
                    pkm.happiness = temp;
                }
                if (a.Name.Equals("levelMetBox"))
                {
                    pkm.levelmet = temp;
                }
                if (a.Name.Equals("levelBox"))
                {
                    pkm.level = temp;
                    updateStats();
                }
                if (a.Name.Equals("pp1Box"))
                {
                    pkm.moveset.move1.pp = temp;
                }
                if (a.Name.Equals("pp2Box"))
                {
                    pkm.moveset.move2.pp = temp;
                }
                if (a.Name.Equals("pp3Box"))
                {
                    pkm.moveset.move3.pp = temp;
                }
                if (a.Name.Equals("pp4Box"))
                {
                    pkm.moveset.move4.pp = temp;
                }
                if (a.Name.Equals("ppUp1Box"))
                {
                    pkm.moveset.move1.ppUp = temp;
                    pp1Box.Text = Convert.ToString(PkmLib.pp[pkm.moveset.move1.move] + (PkmLib.pp[pkm.moveset.move1.move] / 5) * temp);
                }
                if (a.Name.Equals("ppUp2Box"))
                {
                    pkm.moveset.move2.ppUp = temp;
                    pp2Box.Text = Convert.ToString(PkmLib.pp[pkm.moveset.move2.move] + (PkmLib.pp[pkm.moveset.move2.move] / 5) * temp);
                }
                if (a.Name.Equals("ppUp3Box"))
                {
                    pkm.moveset.move3.ppUp = temp;
                    pp3Box.Text = Convert.ToString(PkmLib.pp[pkm.moveset.move3.move] + (PkmLib.pp[pkm.moveset.move3.move] / 5) * temp);
                }
                if (a.Name.Equals("ppUp4Box"))
                {
                    pkm.moveset.move4.ppUp = temp;
                    pp4Box.Text = Convert.ToString(PkmLib.pp[pkm.moveset.move4.move] + (PkmLib.pp[pkm.moveset.move4.move] / 5) * temp);
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
                    pkm.hpev = Convert.ToByte(hpEV.Text);
                    pkm.atev = Convert.ToByte(atEV.Text);
                    pkm.dfev = Convert.ToByte(dfEV.Text);
                    pkm.saev = Convert.ToByte(saEV.Text);
                    pkm.sdev = Convert.ToByte(sdEV.Text);
                    pkm.spev = Convert.ToByte(spEV.Text);
                    if ((pkm.hpev + pkm.atev + pkm.dfev + pkm.saev + pkm.sdev + pkm.spev) > 510)
                    {
                        a.Text = "0";
                        pkm.hpev = Convert.ToByte(hpEV.Text);
                        pkm.atev = Convert.ToByte(atEV.Text);
                        pkm.dfev = Convert.ToByte(dfEV.Text);
                        pkm.saev = Convert.ToByte(saEV.Text);
                        pkm.sdev = Convert.ToByte(sdEV.Text);
                        pkm.spev = Convert.ToByte(spEV.Text);
                    }
                    totalLabel.Text = totalLabel.Text.Split('/')[0] + "/EV " + (pkm.hpev + pkm.atev + pkm.dfev + pkm.saev + pkm.sdev + pkm.spev);
                    updateStats();
                }
            }
        }

        private void saveIV()
        {
            pkm.iv.hp = Convert.ToByte(hpIV.Text);
            pkm.iv.atk = Convert.ToByte(atIV.Text);
            pkm.iv.def = Convert.ToByte(dfIV.Text);
            pkm.iv.spa = Convert.ToByte(saIV.Text);
            pkm.iv.spd = Convert.ToByte(sdIV.Text);
            pkm.iv.spe = Convert.ToByte(spIV.Text);
            totalLabel.Text = "Total IV " + (pkm.iv.hp + pkm.iv.atk + pkm.iv.def + pkm.iv.spa + pkm.iv.spd + pkm.iv.spe) + "/" + totalLabel.Text.Split('/')[1];
            pkm.hiddenPower();
            hiddenPowerLabel.Text = PkmLib.moveTranslateTo("Hidden Power") + " " + pkm.hpType;
            updateStats();
        }

        private void updateStats()
        {
            pkm.updateStats();
            maxhpBox.Text = Convert.ToString(pkm.maxhp);
            hpBox.Text = Convert.ToString(pkm.hp);
            attackBox.Text = Convert.ToString(pkm.attack);
            defenseBox.Text = Convert.ToString(pkm.defense);
            spattackBox.Text = Convert.ToString(pkm.spatk);
            spdefenseBox.Text = Convert.ToString(pkm.spdef);
            speedBox.Text = Convert.ToString(pkm.speed);
            double atnature = 1;
            double dfnature = 1;
            double sanature = 1;
            double sdnature = 1;
            double spnature = 1;
            switch (pkm.nature)
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
                    pkm.id = temp;
                }
                if (a.Name.Equals("sidBox"))
                {
                    pkm.sid = temp;
                }
                if (a.Name.Equals("hpBox"))
                {
                    if (temp > pkm.maxhp)
                    {
                        temp = pkm.maxhp;
                        a.Text = Convert.ToString(temp);
                    }
                    pkm.hp = temp;
                    hpBar.Value = temp;
                }
                if (a.Name.Equals("maxhpBox"))
                {
                    pkm.maxhp = temp;
                    hpBar.Maximum = temp;
                    hpBox.Text = Convert.ToString(temp);
                }
                if (a.Name.Equals("attackBox"))
                {
                    pkm.attack = temp;
                }
                if (a.Name.Equals("defenseBox"))
                {
                    pkm.defense = temp;
                }
                if (a.Name.Equals("spattackBox"))
                {
                    pkm.spatk = temp;
                }
                if (a.Name.Equals("spdefenseBox"))
                {
                    pkm.spdef = temp;
                }
                if (a.Name.Equals("speedBox"))
                {
                    pkm.speed = temp;
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
            if (pkm.species == "Unown")
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
            if (pkm.species == "Deoxys")
            {
                forms = new string[]{
				"Normal",
				"Attack",
				"Defense",
				"Speed"
			};
            }
            if (pkm.species == "Burmy")
            {
                forms = new string[]{
				"Plant",
				"Sandy",
				"Thrash"
			};
            }
            if (pkm.species == "Wormadam")
            {
                forms = new string[]{
				"Plant",
				"Sandy",
				"Thrash"
			};
            }
            if (pkm.species == "Shellos")
            {
                forms = new string[]{
				"West",
				"East"
			};
            }
            if (pkm.species == "Gastrodon")
            {
                forms = new string[]{
				"West",
				"East"
			};
            }
            if (pkm.species == "Rotom")
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
            if (pkm.species == "Giratina")
            {
                forms = new string[]{
				"Altered",
				"Origin"
			};
            }
            if (pkm.species == "Shaymin")
            {
                forms = new string[]{
				"Land",
				"Sky"
			};
            }
            if (pkm.species == "Arceus")
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
				"Dark"
			};
            }
            if (pkm.species == "Basculin")
            {
                forms = new string[]{
				"Red Striped",
				"Blue Striped"
			};
            }
            if (pkm.species == "Deerling")
            {
                forms = new string[]{
				"Spring",
				"Summer",
				"Autumn",
				"Winter"
			};
            }
            if (pkm.species == "Sawsbuck")
            {
                forms = new string[]{
				"Spring",
				"Summer",
				"Autumn",
				"Winter"
			};
            }
            if (pkm.species == "Tornadus")
            {
                forms = new string[] {
				"Incarnate",
				"Therian"
			};
            }
            if (pkm.species == "Thundurus")
            {
                forms = new string[]{
				"Incarnate",
				"Therian"
			};
            }
            if (pkm.species == "Landorus")
            {
                forms = new string[]{
				"Incarnate",
				"Therian"
			};
            }
            if (pkm.species == "Kyurem")
            {
                forms = new string[]{
				"Normal",
				"Black",
				"White"
			};
            }
            if (pkm.species == "Keldeo")
            {
                forms = new string[]{
				"Ordinary",
				"Resolute"
			};
            }
            if (pkm.species == "Meloetta")
            {
                forms = new string[]{
				"Aria",
				"Pirouette"
			};
            }
            if (pkm.species == "Genesect")
            {
                forms = new string[]{
				"Normal",
				"Douse",
				"Shock",
				"Burn",
				"Chill"
			};
            }
            return forms;
        }

        private void loadPKM(Pokemon npkm)
        {
            hatchedBox.Checked = npkm.isHatched;
            shinyBox.Checked = npkm.isShiny;
            levelBox.Text = Convert.ToString(npkm.level);
            fatefulBox.Checked = npkm.isFateful;

            itemBox.SelectedItem = PkmLib.itemTranslateTo(npkm.item);
            natureBox.SelectedItem = PkmLib.natureTranslateTo(npkm.nature);
            abilityBox.SelectedItem = PkmLib.abilityTranslateTo(npkm.ability);
            versionBox.SelectedItem = PkmLib.versionTranslateTo(npkm.version);
            strainBox.SelectedIndex = (npkm.pokerus >> 4);
            try
            {
                pkrsDaysBox.SelectedIndex = (npkm.pokerus & 0xF);
            }
            catch (Exception ex)
            {

            }
            languageBox.SelectedItem = PkmLib.languageTranslateTo(npkm.language);
            statusBox.SelectedItem = npkm.status;
            speciesBox.SelectedItem = PkmLib.speciesTranslateTo(npkm.species);
            formBox.SelectedItem = npkm.form;
            move1Box.SelectedItem = PkmLib.lmoves[npkm.moveset.move1.move];
            move2Box.SelectedItem = PkmLib.lmoves[npkm.moveset.move2.move];
            move3Box.SelectedItem = PkmLib.lmoves[npkm.moveset.move3.move];
            move4Box.SelectedItem = PkmLib.lmoves[npkm.moveset.move4.move];
            pokeballBox.SelectedItem = npkm.pokeball;
            locationBox.SelectedItem = PkmLib.locationTranslateTo(npkm.locationmet);
            locationEggBox.SelectedItem = PkmLib.locationTranslateTo(npkm.eggloc);
            if ((npkm.DW & 1) == 1)
            {
                indexBox.SelectedIndex = 2;
            }
            else
            {
                indexBox.SelectedIndex = npkm.abilityIndex;
            }

            checksumBox.Text = Convert.ToString(npkm.checksum);
            expBox.Text = Convert.ToString(npkm.exp);
            happinessBox.Text = Convert.ToString(npkm.happiness);
            hpIV.Text = Convert.ToString(npkm.iv.hp);
            atIV.Text = Convert.ToString(npkm.iv.atk);
            dfIV.Text = Convert.ToString(npkm.iv.def);
            saIV.Text = Convert.ToString(npkm.iv.spa);
            sdIV.Text = Convert.ToString(npkm.iv.spd);
            spIV.Text = Convert.ToString(npkm.iv.spe);
            hpEV.Text = Convert.ToString(npkm.hpev);
            atEV.Text = Convert.ToString(npkm.atev);
            dfEV.Text = Convert.ToString(npkm.dfev);
            saEV.Text = Convert.ToString(npkm.saev);
            sdEV.Text = Convert.ToString(npkm.sdev);
            spEV.Text = Convert.ToString(npkm.spev);
            maxhpBox.Text = Convert.ToString(npkm.maxhp);
            hpBox.Text = Convert.ToString(npkm.hp);
            attackBox.Text = Convert.ToString(npkm.attack);
            defenseBox.Text = Convert.ToString(npkm.defense);
            spattackBox.Text = Convert.ToString(npkm.spatk);
            spdefenseBox.Text = Convert.ToString(npkm.spdef);
            speedBox.Text = Convert.ToString(npkm.speed);
            idBox.Text = Convert.ToString(npkm.id);
            sidBox.Text = Convert.ToString(npkm.sid);
            levelMetBox.Text = Convert.ToString(npkm.levelmet);
            pp1Box.Text = Convert.ToString(npkm.moveset.move1.pp);
            pp2Box.Text = Convert.ToString(npkm.moveset.move2.pp);
            pp3Box.Text = Convert.ToString(npkm.moveset.move3.pp);
            pp4Box.Text = Convert.ToString(npkm.moveset.move4.pp);
            ppUp1Box.Text = Convert.ToString(npkm.moveset.move1.ppUp);
            ppUp2Box.Text = Convert.ToString(npkm.moveset.move2.ppUp);
            ppUp3Box.Text = Convert.ToString(npkm.moveset.move3.ppUp);
            ppUp4Box.Text = Convert.ToString(npkm.moveset.move4.ppUp);

            nickBox.Text = npkm.nick;
            nicktbBox.Text = npkm.nicktb;
            otBox.Text = npkm.ot;
            dateBox.Text = npkm.datemet;
            dateEggBox.Text = npkm.dateegg;

            isNickBox.Checked = npkm.iv.isNick;
            isEggBox.Checked = npkm.iv.isEgg;
            leafA.Checked = (pkm.shinyLeaves & 1) == 1;
            leafB.Checked = ((pkm.shinyLeaves >> 1) & 1) == 1;
            leafC.Checked = ((pkm.shinyLeaves >> 2) & 1) == 1;
            leafD.Checked = ((pkm.shinyLeaves >> 3) & 1) == 1;
            leafE.Checked = ((pkm.shinyLeaves >> 4) & 1) == 1;
            leafCrown.Checked = ((pkm.shinyLeaves >> 5) & 1) == 1;
            if (npkm.genderot == 0)
            {
                maleOTButton.Checked = true;
            }
            else
            {
                femaleOTButton.Checked = true;
            }

            if (npkm.female == 1)
            {
                femaleButton.Checked = true;
            }
            else
            {
                if (npkm.genderless == 1)
                {
                    genderlessButton.Checked = true;
                }
                else
                {
                    maleButton.Checked = true;
                }
            }
            pidBox.Text = Convert.ToString(npkm.pid);
            pkm = new Pokemon(npkm.data);
        }

        private void gender_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton)
            {
                RadioButton a = (RadioButton)sender;
                if (a.Name.Equals("maleButton"))
                {
                    pkm.female = 0;
                    pkm.genderless = 0;
                }
                if (a.Name.Equals("femaleButton"))
                {
                    pkm.female = 1;
                    pkm.genderless = 0;
                }
                if (a.Name.Equals("genderlessButton"))
                {
                    pkm.female = 0;
                    pkm.genderless = 1;
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
                    pkm.genderot = 0;
                }
                if (a.Name.Equals("femaleOTButton"))
                {
                    pkm.genderot = 1;
                }
            }
        }

        private void loadMenu_Click(object sender, EventArgs e)
        {
            loadDialog.Filter = "Pokemon supported files (*.pkm;*.sav;*.dsv)|*.pkm;*.sav;*.dsv";
            loadDialog.FileName = "";
            DialogResult res = loadDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                switch (Path.GetExtension(loadDialog.FileName))
                {
                    case ".pkm":
                        Pokemon npkm = new Pokemon(File.ReadAllBytes(loadDialog.FileName));
                        if (!npkm.isEmpty)
                        {
                            loadPKM(npkm);
                        }
                        break;
                    case ".sav":
                    case ".dsv":
                        savefile = new SaveFile(loadDialog.FileName);
                        loadSAV();
                        break;
                }
            }
        }

        private void saveMenu_Click(object sender, EventArgs e)
        {
            saveDialog.Filter = "Party Pokemon (*.pkm)|*.pkm|PC Pokemon (*.pkm)|*.pkm|Encrypted Party Pokemon (*.pkm)|*.pkm|Encrypted PC Pokemon (*.pkm)|*.pkm";
            pkm.save();
            checksumBox.Text = Convert.ToString(pkm.checksum);
            DialogResult res = saveDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                byte[] p;
                switch (saveDialog.FilterIndex)
                {
                    case 1:
                        File.WriteAllBytes(saveDialog.FileName, pkm.data);
                        break;
                    case 2:
                        File.WriteAllBytes(saveDialog.FileName, pkm.getPC());
                        break;
                    case 3:
                        File.WriteAllBytes(saveDialog.FileName, pkm.getEncrypted());
                        //pkm.decript();
                        break;
                    case 4:
                        File.WriteAllBytes(saveDialog.FileName, pkm.getEncrypted(false));
                        //pkm.decript();
                        break;
                }
                //loadPKM(new Pokemon(File.ReadAllBytes(saveDialog.FileName)));
            }
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox)
            {
                CheckBox a = (CheckBox)sender;
                if (a.Name.Equals("hatchedBox"))
                {
                    pkm.isHatched = a.Checked;
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
                    pkm.isShiny = a.Checked;
                }
                if (a.Name.Equals("fatefulBox"))
                {
                    pkm.isFateful = a.Checked;
                    loadMovesets();
                }
                if (a.Name.Equals("circleMark"))
                {
                    pkm.markings = a.Checked ? (byte)(pkm.markings | 1) : (byte)(pkm.markings & 0xfe);
                }
                if (a.Name.Equals("triangleMark"))
                {
                    pkm.markings = a.Checked ? (byte)(pkm.markings | 2) : (byte)(pkm.markings & 0xfd);
                }
                if (a.Name.Equals("squareMark"))
                {
                    pkm.markings = a.Checked ? (byte)(pkm.markings | 4) : (byte)(pkm.markings & 0xfb);
                }
                if (a.Name.Equals("heartMark"))
                {
                    pkm.markings = a.Checked ? (byte)(pkm.markings | 8) : (byte)(pkm.markings & 0xf7);
                }
                if (a.Name.Equals("starMark"))
                {
                    pkm.markings = a.Checked ? (byte)(pkm.markings | 16) : (byte)(pkm.markings & 0xef);
                }
                if (a.Name.Equals("diamondMark"))
                {
                    pkm.markings = a.Checked ? (byte)(pkm.markings | 32) : (byte)(pkm.markings & 0xdf);
                }
                if (a.Name.Equals("leafA"))
                {
                    pkm.shinyLeaves = a.Checked ? (byte)(pkm.shinyLeaves | 1) : (byte)(pkm.shinyLeaves & 0xfe);
                }
                if (a.Name.Equals("leafB"))
                {
                    pkm.shinyLeaves = a.Checked ? (byte)(pkm.shinyLeaves | 2) : (byte)(pkm.shinyLeaves & 0xfd);
                }
                if (a.Name.Equals("leafC"))
                {
                    pkm.shinyLeaves = a.Checked ? (byte)(pkm.shinyLeaves | 4) : (byte)(pkm.shinyLeaves & 0xfb);
                }
                if (a.Name.Equals("leafD"))
                {
                    pkm.shinyLeaves = a.Checked ? (byte)(pkm.shinyLeaves | 8) : (byte)(pkm.shinyLeaves & 0xf7);
                }
                if (a.Name.Equals("leafE"))
                {
                    pkm.shinyLeaves = a.Checked ? (byte)(pkm.shinyLeaves | 16) : (byte)(pkm.shinyLeaves & 0xef);
                }
                if (a.Name.Equals("leafCrown"))
                {
                    pkm.shinyLeaves = a.Checked ? (byte)(pkm.shinyLeaves | 32) : (byte)(pkm.shinyLeaves & 0xdf);
                }
            }
        }

        private void ribbonListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Unchecked)
            {

            }
            else
            {

            }
        }

        private void generatePID_Click(object sender, EventArgs e)
        {
            pkm.save();
            //PidGeneratorForm pForm = new PidGeneratorForm(pkm);
            //pForm.ShowDialog();
            pidBox.Text = Convert.ToString(PidGen.finalPID);
            pkm.pid = PidGen.finalPID;
        }

        #endregion

        # region gtsSystem

        private void getIP(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button a = (Button)sender;
                string ip = "";
                if (a.Name.Equals("localButton"))
                {
                    string host = System.Net.Dns.GetHostName();
                    ip = System.Net.Dns.GetHostByName(host).AddressList[0].ToString();
                    ipBox.Text = ip;
                }
                else
                {
                    ip = new System.Net.WebClient().DownloadString("http://icanhazip.com/");
                    ipBox.Text = ip;
                }
            }
        }

        private void startGTS_Click(object sender, EventArgs e)
        {
            if (!Settings.gtsStarted)
            {
                startGTS.Text = "Stop GTS System";
                _dns.IP = ipBox.Text;
                _dns.Start();
                if (folderDistribution.Enabled)
                {
                    if (orderDistribution.Checked)
                    {
                        _gts.FolderOptions = gts.FolderOptions.Ordered;
                    }
                    else
                    {
                        _gts.FolderOptions = gts.FolderOptions.Random;
                    }
                }
                _gts.Start();
                if (distributeEditor.Checked)
                {
                    pkm.save();
                    File.WriteAllBytes(Path.GetTempPath() + "\\temp.pkm", pkm.data);
                    Notification.show("GTS System Initialized! Temporary pkm file saved!", "GTS System - Started");
                }
                else
                {
                    Notification.show("GTS System Initialized!", "GTS System - Started");
                }
            }
            else
            {
                startGTS.Text = "Start GTS System";
                _gts.Stop();
                _dns.Stop();
                if (File.Exists(Path.GetTempPath() + "\\temp.pkm"))
                {
                    File.Delete(Path.GetTempPath() + "\\temp.pkm");
                }
                Notification.show("GTS System Stopped", "GTS System - Stopped");
            }
            Settings.gtsStarted = !Settings.gtsStarted;
        }

        private void gtsSend(bool file, string path)
        {
            folderDistribution.Enabled = !file;
            if (file)
            {
                _gts.DistributionOptions = gts.OperationOptions.Individual;
                _gts.GenVFile = path;

            }
            else
            {
                _gts.DistributionOptions = gts.OperationOptions.Folder;
                _gts.GenVGiveFolder = path;
            }
        }

        private void gtsFileSet_Click(object sender, EventArgs e)
        {
            DialogResult res = pkmLoadDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                gtsFile.Items.Add(pkmLoadDialog.FileName);
                gtsFile.Text = pkmLoadDialog.FileName;
                gtsSend(true, pkmLoadDialog.FileName);
            }
        }

        private void gtsFolderSet_Click(object sender, EventArgs e)
        {
            DialogResult res = folderBrowser.ShowDialog();
            if (res == DialogResult.OK)
            {
                Settings.gtsFolderR = folderBrowser.SelectedPath;
                gtsFolderReceive.Text = folderBrowser.SelectedPath;
                _gts.GenVGetFolder = Settings.gtsFolderR;
            }
        }

        private void gtsSetFolder_Click(object sender, EventArgs e)
        {
            DialogResult res = folderBrowser.ShowDialog();
            if (res == DialogResult.OK)
            {
                gtsFolderSend.Text = folderBrowser.SelectedPath;
                gtsSend(false, folderBrowser.SelectedPath);
            }
        }

        private void gtsFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            gtsSend(true, gtsFile.SelectedItem.ToString());
        }

        private void distributeEditor_CheckedChanged(object sender, EventArgs e)
        {
            if (distributeEditor.Checked)
            {
                gtsSend(true, Path.GetTempPath() + "\\temp.pkm");
            }
            else
            {
                if (gtsFile.Items.Count > 0)
                {
                    gtsFile.SelectedIndex = gtsFile.Items.Count - 1;
                }
            }
        }

        private void gtsMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            _gts.DistributionMode = (gts.Operation)gtsMode.SelectedIndex;
        }
        #endregion

        #region savefileEditor

        //private void slotParenting()
        //{
        //    PictureBox[] p = { slot1, slot2, slot3, slot4, slot5, slot6, slot7, slot8, slot9, slot10, slot11, slot12, slot13, slot14, slot15, slot16, slot17, slot18, slot19,
        //                     slot20, slot21, slot22, slot23, slot24, slot25, slot26, slot27, slot28, slot29, slot30};
        //    for (int i = 0; i < 30; i++)
        //    {
        //        p[i].Parent = pictureBox1;
        //    }
        //}

        public void loadSAV()
        {
            saveFileEditor.Enabled = true;
            loadParty(true);
            //boxSelector.Items.Clear();
            //boxSelector.Items.AddRange(savefile.getBoxesNames());
            updateSaveInfo();
            boxSelector.SelectedIndex = 0;
            loadBox(0, true);
            loadDayCare();
        }

        public void loadParty(bool reset = true)
        {
            for (int i = 0; i < 6; i++)
            {
                if (reset)
                {
                    partySlots[i].Selected = false;
                }
                if (!savefile.party[i].isEmpty)
                {
                    partySlots[i].Image = Properties.Resources.ResourceManager.GetObject("_" + savefile.party[i].no) as Image;
                    if (partySlots[i].Selected)
                    {
                        partySlots[i].BackColor = Color.Blue;
                    }
                    else
                    {
                        if (savefile.party[i].isShiny)
                        {
                            partySlots[i].BackColor = Color.Yellow;
                        }
                        else
                        {
                            partySlots[i].BackColor = Color.Transparent;
                        }
                    }
                }
                else
                {
                    partySlots[i].Image = null;
                    if (partySlots[i].Selected)
                    {
                        partySlots[i].BackColor = Color.Blue;
                    }
                    else
                    {
                        partySlots[i].BackColor = Color.Transparent;
                    }
                }
            }
        }

        public void loadBox(int index, bool reset = true)
        {
            for (int i = 0; i < 30; i++)
            {
                if (reset)
                {
                    boxSlots[i].Selected = false;
                }
                if (!savefile.boxes[index].pkmdata[i].isEmpty)
                {
                    boxSlots[i].Image = Properties.Resources.ResourceManager.GetObject("_" + savefile.boxes[index].pkmdata[i].no) as Image;
                    if (boxSlots[i].Selected)
                    {
                        boxSlots[i].BackColor = Color.Blue;
                    }
                    else
                    {
                        if (savefile.boxes[index].pkmdata[i].isShiny)
                        {
                            boxSlots[i].BackColor = Color.Yellow;
                        }
                        else
                        {
                            boxSlots[i].BackColor = Color.Transparent;
                        }
                    }
                }
                else
                {
                    boxSlots[i].Image = null;
                    if (boxSlots[i].Selected)
                    {
                        boxSlots[i].BackColor = Color.Blue;
                    }
                    else
                    {
                        boxSlots[i].BackColor = Color.Transparent;
                    }
                }
            }
            //if (Properties.Resources.ResourceManager.GetObject("box" + savefile.boxes[index].wallpaper + string.Format("{0}", savefile.version == 1 ? "bw2" : "")) as Image == null)
            //{
            //    boxBackground.Image = Properties.Resources.ResourceManager.GetObject("box" + savefile.boxes[index].wallpaper) as Image;
            //}
            //else
            //{
            //    boxBackground.Image = Properties.Resources.ResourceManager.GetObject("box" + savefile.boxes[index].wallpaper + string.Format("{0}", savefile.version == 1 ? "bw2" : "")) as Image;
            //}
        }

        private void boxChange(object sender, EventArgs e)
        {
            loadBoxButton.Visible = (boxSelector.SelectedIndex <= 23);
            saveBoxButton.Visible = (boxSelector.SelectedIndex <= 23);
            changeBoxName.Enabled = (boxSelector.SelectedIndex <= 23);
            if (boxSelector.SelectedIndex <= 23)
            {
                loadBox(boxSelector.SelectedIndex, true);
                box = boxSelector.SelectedIndex;
            }
            else
            {

                //Box and Day-Care
            }
        }

        private void loadDayCare(bool reset = true)
        {
            for (int i = 0; i < 2; i++)
            {
                if (reset)
                {
                    dayCareSlots[i].Selected = false;
                }
                if (!savefile.daycare.pkmdata[i].isEmpty)
                {
                    dayCareSlots[i].Image = Properties.Resources.ResourceManager.GetObject("_" + savefile.daycare.pkmdata[i].no) as Image;
                    if (dayCareSlots[i].Selected)
                    {
                        dayCareSlots[i].BackColor = Color.Blue;
                    }
                    else
                    {
                        if (savefile.daycare.pkmdata[i].isShiny)
                        {
                            dayCareSlots[i].BackColor = Color.Yellow;
                        }
                        else
                        {
                            dayCareSlots[i].BackColor = Color.Transparent;
                        }
                    }
                }
                else
                {
                    dayCareSlots[i].Image = null;
                    if (dayCareSlots[i].Selected)
                    {
                        dayCareSlots[i].BackColor = Color.Blue;
                    }
                    else
                    {
                        dayCareSlots[i].BackColor = Color.Transparent;
                    }
                }
            }
        }

        private void sfmenu_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem a = (ToolStripMenuItem)sender;
                if (a.Name.Equals("loadsfpkm"))
                {
                    loadPKM(getPkm(pIndex, sSlot));
                }
                if (a.Name.Equals("setsfpkm"))
                {
                    pkm.save();
                    setPkm(pkm, pIndex, sSlot);
                    for (int i = 0; i < 6; i++)
                    {
                        if (partySlots[i].Selected)
                        {
                            setPkm(pkm, i, 25);
                        }
                    }
                    for (int i = 0; i < 30; i++)
                    {
                        if (boxSlots[i].Selected)
                        {
                            setPkm(pkm, i, boxSelector.SelectedIndex);
                        }
                    }
                }
                if (a.Name.Equals("clearsfpkm"))
                {
                    setPkm(new Pokemon(), pIndex, sSlot);
                    for (int i = 0; i < 6; i++)
                    {
                        if (partySlots[i].Selected)
                        {
                            setPkm(new Pokemon(), i, 25);
                        }
                    }
                    for (int i = 0; i < 30; i++)
                    {
                        if (boxSlots[i].Selected)
                        {
                            setPkm(new Pokemon(), i, boxSelector.SelectedIndex);
                        }
                    }
                }
            }
        }

        private void sfMenu_Opening(object sender, CancelEventArgs e)
        {
            loadsfpkm.Enabled = true;
            Slot a = (Slot)((ContextMenuStrip)sender).SourceControl;
            if (a.Name.Contains("party"))
            {
                pIndex = partySlots.IndexOf(a);
                sSlot = 25;
                if (savefile.party[pIndex] == null)
                {
                    loadsfpkm.Enabled = false;
                }
                if (savefile.party[pIndex].isEmpty)
                {
                    loadsfpkm.Enabled = false;
                }
            }
            if (a.Name.Contains("slot"))
            {
                sSlot = boxSelector.SelectedIndex;
                pIndex = boxSlots.IndexOf(a);
                if (savefile.boxes[boxSelector.SelectedIndex].pkmdata[pIndex] == null)
                {
                    loadsfpkm.Enabled = false;
                }
                if (savefile.boxes[boxSelector.SelectedIndex].pkmdata[pIndex].isEmpty)
                {
                    loadsfpkm.Enabled = false;
                }
            }
            if (a.Name.Contains("daycare"))
            {
                sSlot = 30;
                pIndex = dayCareSlots.IndexOf(a);
                if (savefile.daycare.pkmdata[pIndex] == null)
                {
                    loadsfpkm.Enabled = false;
                }
                if (savefile.daycare.pkmdata[pIndex].isEmpty)
                {
                    loadsfpkm.Enabled = false;
                }
            }
        }

        private void setPkm(Pokemon pkm, int index, int box = 25)
        {
            if (box == 25)
            {
                savefile.party[index] = pkm.Clone();
                loadParty(false);
            }
            else
            {
                if (box == 30)
                {
                    savefile.daycare.pkmdata[index] = pkm.Clone();
                    loadDayCare(false);
                }
                else
                {
                    savefile.boxes[box].pkmdata[index] = pkm.Clone();
                    loadBox(box, false);
                }
            }
        }

        private Pokemon getPkm(int index, int box = 25, bool clone = true)
        {
            if (box == 25)
            {
                if (clone)
                {
                    return savefile.party[index].Clone();
                }
                else
                {
                    return savefile.party[index];
                }
            }
            else
            {
                if (box == 30)
                {
                    if (clone)
                    {
                        return savefile.daycare.pkmdata[index].Clone();
                    }
                    else
                    {
                        return savefile.daycare.pkmdata[index];
                    }
                }
                else
                {
                    if (clone)
                    {
                        return savefile.boxes[box].pkmdata[index].Clone();
                    }
                    else
                    {
                        return savefile.boxes[box].pkmdata[index];
                    }
                }
            }
        }

        private void saveSavMenu_Click(object sender, EventArgs e)
        {
            saveDialog.Filter = "Pokemon save file (*.sav)|*.sav|Desmume save file (*.dsv)|*.dsv";
            saveDialog.FileName = "";
            DialogResult res = saveDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                savefile.save(saveDialog.FileName, (SaveFile.FileExtension)(saveDialog.FilterIndex - 1));
                //savefile = new SaveFile(saveSavDialog.FileName);
                //loadSAV();
            }
        }

        private void slots_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //Calculate slot
                Slot a = sender as Slot;
                if (a.Name.Contains("party"))
                {
                    pIndex = partySlots.IndexOf(a);
                    sSlot= 25;
                }
                if (a.Name.Contains("slot"))
                {
                    sSlot= boxSelector.SelectedIndex;
                    pIndex = boxSlots.IndexOf(a);
                }
                if (a.Name.Contains("daycare"))
                {
                    sSlot= 30;
                    pIndex = dayCareSlots.IndexOf(a);
                }
                if (e.Clicks > 1)
                {
                    bool b = !a.Selected;
                    a.Selected = b;
                    if (b)
                    {
                        a.BackColor = Color.Blue;
                    }
                    else
                    {
                        if (getPkm(pIndex, sSlot).isShiny)
                        {
                            a.BackColor = Color.Yellow;
                        }
                        else
                        {
                            a.BackColor = Color.Transparent;
                        }
                    }
                }
                if (e.Clicks == 1)
                {
                    draggedBox = sSlot;
                    draggedIndex = pIndex;
                    a.DoDragDrop(getPkm(pIndex, sSlot), DragDropEffects.All);
                }
            }
        }

        private void slots_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Pokemon)))
            {
                //Calculate slot
                Slot a = sender as Slot;
                if (a.Name.Contains("party"))
                {
                    pIndex = partySlots.IndexOf(a);
                    sSlot= 25;
                }
                if (a.Name.Contains("slot"))
                {
                    sSlot= boxSelector.SelectedIndex;
                    pIndex = boxSlots.IndexOf(a);
                }
                if (a.Name.Contains("daycare"))
                {
                    sSlot= 30;
                    pIndex = dayCareSlots.IndexOf(a);
                }
                Pokemon p1 = getPkm(pIndex, sSlot, false);
                Pokemon p2 = e.Data.GetData(typeof(Pokemon)) as Pokemon;
                if (sSlot== draggedBox && pIndex == draggedIndex)
                {
                    e.Effect = DragDropEffects.None;
                }
                else
                {
                    e.Effect = DragDropEffects.All;
                }
            }
            else
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    e.Effect = DragDropEffects.Copy;
                }
            }
        }

        private void slots_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect != DragDropEffects.None)
            {

                //Calculate slot
                Slot a = sender as Slot;
                if (a.Name.Contains("party"))
                {
                    pIndex = partySlots.IndexOf(a);
                    sSlot= 25;
                }
                if (a.Name.Contains("slot"))
                {
                    sSlot= boxSelector.SelectedIndex;
                    pIndex = boxSlots.IndexOf(a);
                }
                if (a.Name.Contains("daycare"))
                {
                    sSlot= 30;
                    pIndex = dayCareSlots.IndexOf(a);
                }
                //Pokemon drop
                if (e.Data.GetDataPresent(typeof(Pokemon)))
                {
                    Pokemon p = e.Data.GetData(typeof(Pokemon)) as Pokemon;
                    Pokemon p2 = getPkm(pIndex, sSlot);
                    setPkm(p, pIndex, sSlot);
                    setPkm(p2, draggedIndex, draggedBox);
                    updateSlots(false);
                }
                //File drop
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
                    string filepath = filePaths[0];
                    if (Path.GetExtension(filepath).Equals("pkm"))
                    {
                        Pokemon p = new Pokemon(File.ReadAllBytes(filepath));
                        setPkm(p, pIndex, sSlot);
                    }
                    updateSlots(false);
                }
            }
        }

        private void boxesFile_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button a = (Button)sender;
                if (a.Name.Equals("loadBoxButton"))
                {
                    loadDialog.Filter = "Save File Box (*.sfb)|*.sfb";
                    loadDialog.FileName = "*.sfb";
                    DialogResult res = loadDialog.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        int i = boxSelector.SelectedIndex;
                        savefile.boxes[boxSelector.SelectedIndex].loadBoxFile(File.ReadAllBytes(loadDialog.FileName));
                        boxSelector.Items.Clear();
                        boxSelector.Items.AddRange(savefile.getBoxesNames());
                        boxSelector.SelectedIndex = i;
                        loadBox(i);
                    }
                }
                if (a.Name.Equals("saveBoxButton"))
                {
                    saveDialog.Filter = "Save File Box (*.sfb)|*.sfb";
                    saveDialog.FileName = savefile.boxes[boxSelector.SelectedIndex].name + ".sfb";
                    DialogResult res = saveDialog.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        File.WriteAllBytes(saveDialog.FileName, savefile.boxes[boxSelector.SelectedIndex].saveBoxFile());
                    }
                }
            }
        }

        private void updateSlots(bool reset = false)
        {
            loadParty(reset);
            loadBox(boxSelector.SelectedIndex, reset);
            loadDayCare(reset);
        }

        private void changeBoxName_Click(object sender, EventArgs e)
        {
            savefile.boxes[box].name = boxSelector.Text;
            boxSelector.Items[box] = boxSelector.Text;
            boxSelector.SelectedIndex = box;
        }

        private void updateSaveInfo()
        {
            saveFileLog.Items.Clear();
            saveFileLog.Items.Add("Version " + string.Format("{0}", savefile.version == SaveFile.Version.HGSS ? "HGSS" : savefile.version == SaveFile.Version.Pt ? "Platinum" : "DP"));
            saveFileLog.Items.Add("Using General Block " + string.Format("{0}", savefile.generalBlock == SaveFile.SelectedBlock.Block1 ? "1" : "2"));
            saveFileLog.Items.Add("Using Storage Block " + string.Format("{0}", savefile.storageBlock == SaveFile.SelectedBlock.Block1 ? "1" : "2"));
            saveFileLog.Items.Add("Trainer Name " + savefile.trainer.name);
            saveFileLog.Items.Add("Trainer ID " + savefile.trainer.id);
            saveFileLog.Items.Add("Trainer Secret ID " + savefile.trainer.sid);
            saveFileLog.Items.Add("Money " + savefile.trainer.money);
            saveFileLog.Items.Add("Gender " + (savefile.trainer.gender == TrainerInfo.TrainerGender.Male ? "Male" : "Female"));
            saveFileLog.Items.Add("Badges " + savefile.trainer.badgeCount());
        }

        private void trainerInfoButton_Click(object sender, EventArgs e)
        {
            TrainerEditor t = new TrainerEditor(savefile.version);
            t.name = savefile.trainer.name;
            t.id = savefile.trainer.id;
            t.sid = savefile.trainer.sid;
            t.money = savefile.trainer.money;
            t.gender = savefile.trainer.gender;
            if (savefile.version == SaveFile.Version.HGSS)
            {
                t.badges = savefile.trainer.getAllBadgesObtained();
            }
            else
            {
                t.badges = savefile.trainer.getBadgesObtained();
            }
            t.ShowDialog();
            savefile.trainer.name = t.name;
            savefile.trainer.id = t.id;
            savefile.trainer.sid = t.sid;
            savefile.trainer.money = t.money;
            savefile.trainer.gender = t.gender;
            savefile.trainer.setBadges(t.badges);
            updateSaveInfo();
        }

        #endregion

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
                    if (plugin.getPluginGen == Pikaedit_Lib.PluginGen.Gen4 || plugin.getPluginGen == Pikaedit_Lib.PluginGen.Gen4_Gen5 ||
                        plugin.getPluginGen == Pikaedit_Lib.PluginGen.Gen4_XY || plugin.getPluginGen == Pikaedit_Lib.PluginGen.All)
                    {
                        try
                        {
                            plugin.getPluginGen = Pikaedit_Lib.PluginGen.Gen4;
                        }
                        catch
                        {

                        }
                        plugins.Add(plugin);
                        pluginFiles.Add(Application.StartupPath + "\\Plugins\\" + Path.GetFileName(file));
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
                        b.Click += new EventHandler(removePlugin);
                        a.DropDownItems.Add(b);
                        string[] dllFileNames = null;
                        if (Directory.Exists(Application.StartupPath + "\\Plugins"))
                        {
                            dllFileNames = Directory.GetFiles(Application.StartupPath + "\\Plugins", "*.dll");
                            if (dllFileNames != null)
                            {
                                for (int i = 0; i < dllFileNames.Length; i++)
                                {
                                    dllFileNames[i] = Path.GetFileName(dllFileNames[i]);
                                }
                                if (!dllFileNames.Contains(Path.GetFileName(file)))
                                {
                                    File.Copy(file, Application.StartupPath + "\\Plugins\\" + Path.GetFileName(file));
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
                    if (plugin.setRequirement == Pikaedit_Lib.Requirement.Loaded_Save_File && savefile.version == SaveFile.Version.Unknown)
                    {
                        MessageBox.Show("This plugin requires save file data to be loaded", "Save File Required", MessageBoxButtons.OK);
                        return;
                    }
                    try
                    {
                        pkm.save();
                        plugin.getGen4Pokemon = new Pikaedit_Lib.PokemonGen4(pkm.data);
                    }
                    catch
                    {

                    }
                    try
                    {
                        plugin.getGen4SaveFile = new Pikaedit_Lib.SaveFileGen4(savefile.getSavedData());
                    }
                    catch
                    {

                    }
                    plugin.Do();
                    try
                    {
                        switch (plugin.setPostProcess)
                        {
                            case Pikaedit_Lib.Process.Load_Pkm:
                                loadPKM(new Pokemon(plugin.getGen4Pokemon.data));
                                break;
                            case Pikaedit_Lib.Process.Update_Save_File:
                                savefile = new SaveFile(plugin.getGen4SaveFile.getSavedData());
                                loadSAV();
                                break;
                        }
                    }
                    catch
                    {

                    }
                }
            }
            if (File.Exists("temp.sav"))
            {
                File.Delete("temp.sav");
            }
        }

        private void removePlugin(object sender, EventArgs e)
        {
            ToolStripMenuItem a = (ToolStripMenuItem)sender;
            foreach (Pikaedit_Lib.IPlugin plugin in plugins)
            {
                if (plugin.getName() == a.Name.Replace("Remove", ""))
                {
                    pluginsMenu.DropDownItems.Remove(a.OwnerItem);
                    int i = plugins.IndexOf(plugin);
                    plugins.Remove(plugin);
                    if (File.Exists(pluginFiles[i]))
                    {
                        File.Delete(pluginFiles[i]);
                    }
                    pluginFiles.RemoveAt(i);
                    break;
                }
            }
        }

        private void updatePlugins_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Application.StartupPath + "\\Plugins"))
            {
                if (Directory.GetFiles(Application.StartupPath + "\\Plugins").Length != 0)
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
                                if (File.Exists(pluginFiles[i]))
                                {
                                    File.Delete(pluginFiles[i]);
                                }
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
                            File.WriteAllBytes(Application.StartupPath + "\\PluginUpdater.exe", Properties.Resources.PluginUpdater);
                            Process process = Process.Start(Application.StartupPath + "\\PluginUpdater.exe", args.TrimEnd(' '));
                            process.WaitForExit();
                            File.Delete(Application.StartupPath + "\\PluginUpdater.exe");
                            string[] dllFileNames = null;
                            if (Directory.Exists(Application.StartupPath + "\\Plugins"))
                            {
                                dllFileNames = Directory.GetFiles(Application.StartupPath + "\\Plugins", "*.dll");
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
                    }
                }
            }
        }

        #endregion

        private void movesetAnalysis_Click(object sender, EventArgs e)
        {
            Pikaedit_Lib.Veekun.Initialize();
            List<ushort> legalMoves = Pikaedit_Lib.Veekun.getMoveset(pkm.species, Pikaedit_Lib.GenSearch.From_3_to_5, pkm.form, Pikaedit_Lib.MovesetType.All);
            string s = "";
            s += (legalMoves.Contains(pkm.moveset.move1.move) || pkm.moveset.move1.move == 0 ? PkmLib.lmoves[pkm.moveset.move1.move] + " - Legal" : PkmLib.lmoves[pkm.moveset.move1.move] + " - Illegal") + "\n";
            s += (legalMoves.Contains(pkm.moveset.move2.move) || pkm.moveset.move2.move == 0 ? PkmLib.lmoves[pkm.moveset.move2.move] + " - Legal" : PkmLib.lmoves[pkm.moveset.move2.move] + " - Illegal") + "\n";
            s += (legalMoves.Contains(pkm.moveset.move3.move) || pkm.moveset.move3.move == 0 ? PkmLib.lmoves[pkm.moveset.move3.move] + " - Legal" : PkmLib.lmoves[pkm.moveset.move3.move] + " - Illegal") + "\n";
            s += (legalMoves.Contains(pkm.moveset.move4.move) || pkm.moveset.move4.move == 0 ? PkmLib.lmoves[pkm.moveset.move4.move] + " - Legal" : PkmLib.lmoves[pkm.moveset.move4.move] + " - Illegal");
            MessageBox.Show(s, "Moveset Check", MessageBoxButtons.OK);
            Pikaedit_Lib.Veekun.Close();
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
                    loadPKM(pkm);
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

        private void randomivButton_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            hpIV.Text = Convert.ToString(r.Next(0, 32));
            atIV.Text = Convert.ToString(r.Next(0, 32));
            dfIV.Text = Convert.ToString(r.Next(0, 32));
            saIV.Text = Convert.ToString(r.Next(0, 32));
            sdIV.Text = Convert.ToString(r.Next(0, 32));
            spIV.Text = Convert.ToString(r.Next(0, 32));
            saveIV();
        }
    }
}
