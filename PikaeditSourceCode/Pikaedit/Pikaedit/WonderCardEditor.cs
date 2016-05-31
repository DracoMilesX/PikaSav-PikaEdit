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
    public partial class WonderCardEditor : Form
    {
        public WonderCard wondercard = new WonderCard();
        private Size includepkm = new Size(794, 401);
        private Size normal = new Size(379, 401);

        public WonderCardEditor()
        {
            InitializeComponent();
            fillCombobox();
        }

        private void fillCombobox()
        {
            speciesBox.Items.AddRange(PkmLib.lspecies.ToArray());
            speciesBox.Items.RemoveAt(0);
            itemBox.Items.AddRange(PkmLib.litems.ToArray());
            while (itemBox.Items.Contains("???"))
            {
                itemBox.Items.Remove("???");
            }
            cardItemBox.Items.AddRange(PkmLib.litems.ToArray());
            while (cardItemBox.Items.Contains("???"))
            {
                cardItemBox.Items.Remove("???");
            }
            powerBox.Items.AddRange(PkmLib.passPowers.ToArray());
            natureBox.Items.Add("Random");
            natureBox.Items.AddRange(PkmLib.lnatures.ToArray());
            locationMetBox.Items.AddRange(PkmLib.llocations.ToArray());
            locationHatchedBox.Items.AddRange(PkmLib.llocations.ToArray());
            move1Box.Items.AddRange(PkmLib.lmoves.ToArray());
            move2Box.Items.AddRange(PkmLib.lmoves.ToArray());
            move3Box.Items.AddRange(PkmLib.lmoves.ToArray());
            move4Box.Items.AddRange(PkmLib.lmoves.ToArray());
            languageBox.Items.Add("Game received");
            languageBox.Items.AddRange(PkmLib.llanguages.ToArray());
            versionBox.Items.Add("Game received");
            versionBox.Items.AddRange(PkmLib.lversions.ToArray());
        }

        public WonderCard Wondercard
        {
            get
            {
                return wondercard;
            }
            set
            {
                wondercard = value;
                load();
            }
        }

        private void load()
        {
            cardIDBox.Text = Convert.ToString(wondercard.cardId);
            cardTitleBox.Text = Convert.ToString(wondercard.title);
            dateCardBox.Text = (wondercard.dateDay + "/" + wondercard.dateMonth + "/" +  wondercard.dateYear);
            cardLocationBox.Text = Convert.ToString(wondercard.cardLocation);
            cardUsedBox.Checked = wondercard.cardUsed;

            //Determine type
            setType(wondercard.type);

            if (wondercard.type == WonderCard.CardType.Pokemon)
            {
                //Load Pkm data
                speciesBox.SelectedItem = PkmLib.speciesTranslateTo(wondercard.species);
                formBox.SelectedItem = wondercard.form;
                if (wondercard.staticPID)
                {
                    pidBox.Text = Convert.ToString(wondercard.pid);
                    staticPID.Checked = true;
                }
                else
                {
                    randomPID.Checked = true;
                    pidBox.Text = "0";
                }
                isNickBox.Checked = wondercard.isNicknamed;
                if (wondercard.isNicknamed)
                {
                    nickBox.Text = string.Empty;
                }
                else
                {
                    nickBox.Text = wondercard.nickname;
                }
                useGameOT.Checked = wondercard.ot == string.Empty;
                otBox.Text = wondercard.ot;
                idBox.Text = Convert.ToString(wondercard.id);
                sidBox.Text = Convert.ToString(wondercard.sid);
                switch (wondercard.otGender)
                {
                    case WonderCard.OTGender.Male:
                        maleOT.Checked = true;
                        break;
                    case WonderCard.OTGender.Female:
                        femaleOT.Checked = true;
                        break;
                    case WonderCard.OTGender.Game:
                        gamegenderOT.Checked = true;
                        break;
                }
                switch (wondercard.isShiny)
                {
                    case WonderCard.Shininess.Never:
                        neverShiny.Checked = true;
                        break;
                    case WonderCard.Shininess.Possible:
                        possibleShiny.Checked = true;
                        break;
                    case WonderCard.Shininess.Always:
                        alwaysShiny.Checked = true;
                        break;
                }
                levelBox.Text = Convert.ToString(wondercard.level);
                itemBox.SelectedItem = PkmLib.itemTranslateTo(Convert.ToString(wondercard.pkmItem));
                switch (wondercard.gender)
                {
                    case WonderCard.Gender.Male:
                        genderBox.SelectedItem = "Male";
                        break;
                    case WonderCard.Gender.Female:
                        genderBox.SelectedItem = "Female";
                        break;
                    case WonderCard.Gender.Random_Genderless:
                        genderBox.SelectedItem = "Random";
                        break;
                }
                abilityBox.SelectedIndex = (int)wondercard.ability;
                natureBox.SelectedItem = PkmLib.natureTranslateTo(wondercard.nature);
                languageBox.SelectedItem = PkmLib.languageTranslateTo(wondercard.language);
                versionBox.SelectedItem = PkmLib.versionTranslateTo(wondercard.version);
                isEggBox.Checked = wondercard.isEgg;
                pokeballBox.SelectedItem = wondercard.pokeball;
                locationMetBox.SelectedItem = PkmLib.locationTranslateTo(wondercard.locationMet);
                locationHatchedBox.SelectedItem = PkmLib.locationTranslateTo(wondercard.locationHatched);
                hpBox.Text = Convert.ToString(wondercard.iv.hp);
                atkBox.Text = Convert.ToString(wondercard.iv.atk);
                defBox.Text = Convert.ToString(wondercard.iv.def);
                spaBox.Text = Convert.ToString(wondercard.iv.spa);
                spdBox.Text = Convert.ToString(wondercard.iv.spd);
                speBox.Text = Convert.ToString(wondercard.iv.spe);
                randomHP.Checked = wondercard.iv.hp > 31;
                randomAtk.Checked = wondercard.iv.atk > 31;
                randomDef.Checked = wondercard.iv.def > 31;
                randomSpa.Checked = wondercard.iv.spa > 31;
                randomSpd.Checked = wondercard.iv.spd > 31;
                randomSpe.Checked = wondercard.iv.spe > 31;
                move1Box.SelectedItem = PkmLib.lmoves[wondercard.moveset.move1.move];
                move2Box.SelectedItem = PkmLib.lmoves[wondercard.moveset.move2.move];
                move3Box.SelectedItem = PkmLib.lmoves[wondercard.moveset.move3.move];
                move4Box.SelectedItem = PkmLib.lmoves[wondercard.moveset.move4.move];
                levelupMoves.Checked = wondercard.moveset.isEmpty();

                //Ribbons
                bool[] b = wondercard.ribbons.getFlags();
                for (int i = 0; i < b.Length; i++)
                {
                    ribbonList.SetItemChecked(i, b[i]);
                }
            }
            else
            {
                if (wondercard.type == WonderCard.CardType.Item)
                {
                    cardItemBox.SelectedItem = wondercard.cardItem;
                }
                else
                {
                    powerBox.SelectedItem = wondercard.cardPower;
                }
            }
        }

        private void cardType_Set(object sender, EventArgs e)
        {
            ToolStripMenuItem a = (ToolStripMenuItem)sender;
            if (sender is ToolStripMenuItem)
            {
                if (a.Equals(typePkm))
                {
                    setType(WonderCard.CardType.Pokemon);
                }
                else
                {
                    if (a.Equals(typeItem))
                    {
                        setType(WonderCard.CardType.Item);
                    }
                    else
                    {
                        setType(WonderCard.CardType.Power);
                    }
                }
            }
        }

        private void setType(WonderCard.CardType type)
        {
            ToolStripMenuItem[] p = { typePkm, typeItem, typePower };
            for (int i = 0; i < 3; i++)
            {
                p[i].Checked = false;
            }
            wondercard.type = type;
            itemPanel.Visible = false;
            powerPanel.Visible = false;
            if (type == WonderCard.CardType.Pokemon)
            {
                typePkm.Checked = true;
                this.Size = includepkm;
                //Hide item/power data
                ushort b = 0;
                if (ushort.TryParse(idBox.Text, out b))
                {
                    wondercard.id = b;
                }
            }
            else
            {
                this.Size = normal;
                //Show item/power data
                if (type == WonderCard.CardType.Item)
                {
                    typeItem.Checked = true;
                    itemPanel.Visible = true;
                    if (cardItemBox.SelectedIndex != -1)
                    {
                        wondercard.cardItem = PkmLib.itemTranslate(cardItemBox.SelectedItem.ToString());
                    }
                    else
                    {
                        wondercard.cardItem = PkmLib.itemTranslate("None");
                    }
                }
                else
                {
                    typePower.Checked = true;
                    powerPanel.Visible = true;
                    if (cardItemBox.SelectedIndex != -1)
                    {
                        wondercard.cardPower = powerBox.SelectedItem.ToString();
                    }
                    else
                    {
                        wondercard.cardItem = "None";
                    }
                }
            }
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


        private void combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox)
            {
                ComboBox a = (ComboBox)sender;
                if (a.Equals(speciesBox))
                {
                    wondercard.species = PkmLib.speciesTranslate(a.SelectedItem.ToString());
                    formBox.Items.Clear();
                    formBox.Items.AddRange(PkmLib.getFormList(wondercard.species));
                    formBox.SelectedIndex = 0;
                }
                if (a.Equals(natureBox))
                {
                    wondercard.nature = PkmLib.natureTranslate(a.SelectedItem.ToString());
                }
                if (a.Equals(languageBox))
                {
                    wondercard.language = PkmLib.languageTranslate(a.SelectedItem.ToString());
                }
                if (a.Equals(versionBox))
                {
                    wondercard.version = PkmLib.versionTranslate(a.SelectedItem.ToString());
                }
                if (a.Equals(formBox))
                {
                    wondercard.form = a.SelectedItem.ToString();
                }
                if (a.Equals(itemBox))
                {
                    wondercard.pkmItem = PkmLib.itemTranslate(a.SelectedItem.ToString());
                }
                if (a.Equals(pokeballBox))
                {
                    wondercard.pokeball = a.SelectedItem.ToString();
                }
                if (a.Equals(locationMetBox))
                {
                    wondercard.locationMet = PkmLib.locationTranslate(a.SelectedItem.ToString());
                }
                if (a.Equals(locationHatchedBox))
                {
                    wondercard.locationHatched = PkmLib.locationTranslate(a.SelectedItem.ToString());
                }
                if (a.Equals(cardItemBox))
                {
                    wondercard.cardItem = PkmLib.itemTranslate(a.SelectedItem.ToString());
                }
                if (a.Equals(powerBox))
                {
                    wondercard.cardPower = a.SelectedItem.ToString();
                }
                if (a.Equals(genderBox))
                {
                    switch (a.SelectedItem.ToString())
                    {
                        case "Male":
                            wondercard.gender = WonderCard.Gender.Male;
                            break;
                        case "Female":
                            wondercard.gender = WonderCard.Gender.Female;
                            break;
                        case "Random":
                            wondercard.gender = WonderCard.Gender.Random_Genderless;
                            break;
                    }
                }
                if (a.Equals(abilityBox))
                {
                    wondercard.ability = (WonderCard.Ability)a.SelectedIndex;
                }
                if (a.Equals(move1Box))
                {
                    wondercard.moveset.move1.move = (ushort)PkmLib.lmoves.IndexOf(a.SelectedItem.ToString());
                }
                if (a.Equals(move2Box))
                {
                    wondercard.moveset.move2.move = (ushort)PkmLib.lmoves.IndexOf(a.SelectedItem.ToString());
                }
                if (a.Equals(move3Box))
                {
                    wondercard.moveset.move3.move = (ushort)PkmLib.lmoves.IndexOf(a.SelectedItem.ToString());
                }
                if (a.Equals(move4Box))
                {
                    wondercard.moveset.move4.move = (ushort)PkmLib.lmoves.IndexOf(a.SelectedItem.ToString());
                }
            }
        }

        private void pgfFile(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem a = (ToolStripMenuItem)sender;
                if (a.Equals(loadPGF))
                {
                    DialogResult res = loadFile.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        Wondercard = new WonderCard(File.ReadAllBytes(loadFile.FileName));
                    }
                }
                if (a.Equals(savePGF))
                {
                    DialogResult res = saveFile.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        File.WriteAllBytes(saveFile.FileName, Wondercard.data);
                    }
                }
            }
        }

        private void string_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox a = (TextBox)sender;
                if (a.Equals(dateCardBox))
                {
                    string[] t = a.Text.Split('/');
                    if (t.Length == 2)
                    {
                        try
                        {
                            byte day = Convert.ToByte(t[0]);
                            byte month = Convert.ToByte(t[1]);
                            ushort year = Convert.ToUInt16(t[2]);
                            wondercard.dateDay = day;
                            wondercard.dateMonth = month;
                            wondercard.dateYear = year;
                        }
                        catch
                        {

                        }
                    }
                }
                if (a.Equals(nickBox))
                {
                    wondercard.nickname = a.Text;
                    isNickBox.Checked = wondercard.nickname == string.Empty;
                }
                if (a.Equals(otBox))
                {
                    wondercard.ot = a.Text;
                    useGameOT.Checked = wondercard.ot == string.Empty;
                }
                if (a.Equals(cardTitleBox))
                {
                    wondercard.title = a.Text;
                }
            }
        }

        private void byte_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox a = (TextBox)sender;
                if (a.Equals(cardLocationBox))
                {
                    wondercard.cardLocation = Convert.ToByte(a.Text);
                }
                if (a.Equals(hpBox))
                {
                    wondercard.iv.hp = Convert.ToByte(a.Text);
                }
                if (a.Equals(atkBox))
                {
                    wondercard.iv.atk = Convert.ToByte(a.Text);
                }
                if (a.Equals(defBox))
                {
                    wondercard.iv.def = Convert.ToByte(a.Text);
                }
                if (a.Equals(spaBox))
                {
                    wondercard.iv.spa = Convert.ToByte(a.Text);
                }
                if (a.Equals(spdBox))
                {
                    wondercard.iv.spd = Convert.ToByte(a.Text);
                }
                if (a.Equals(speBox))
                {
                    wondercard.iv.spe = Convert.ToByte(a.Text);
                }
            }
        }

        private void ushort_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox a = (TextBox)sender;
                if (a.Equals(idBox))
                {
                    wondercard.id = Convert.ToUInt16(a.Text);
                }
                if (a.Equals(sidBox))
                {
                    wondercard.sid = Convert.ToUInt16(a.Text);
                }
            }
        }

        private void pidBox_TextChanged(object sender, EventArgs e)
        {
            wondercard.pid = Convert.ToUInt32(pidBox.Text);
            if (wondercard.pid != 0)
            {
                staticPID.Checked = true;
            }
            else
            {
                randomPID.Checked = true;
            }
        }

        private void checkBox_Checked(object sender, EventArgs e)
        {
            if (sender is CheckBox)
            {
                CheckBox a = (CheckBox)sender;
                if (a.Equals(cardUsedBox))
                {
                    wondercard.cardUsed = a.Checked;
                }
                if (a.Equals(isEggBox))
                {
                    wondercard.isEgg = a.Checked;
                }
                if (a.Equals(isNickBox))
                {
                    nickBox.Text = a.Checked ? string.Empty : PkmLib.speciesTranslate(wondercard.species);
                }
                if (a.Equals(useGameOT))
                {
                    otBox.Text = a.Checked ? string.Empty : "Pikaedt";
                }
                if (a.Equals(levelupMoves))
                {
                    if (a.Checked)
                    {
                        move1Box.SelectedItem = PkmLib.moveTranslate("None");
                        move2Box.SelectedItem = PkmLib.moveTranslate("None");
                        move3Box.SelectedItem = PkmLib.moveTranslate("None");
                        move4Box.SelectedItem = PkmLib.moveTranslate("None");
                    }
                }
                if (a.Equals(randomHP))
                {
                    hpBox.Text = (a.Checked ? Convert.ToString("32") : Convert.ToString("31"));
                }
                if (a.Equals(randomAtk))
                {
                    atkBox.Text = (a.Checked ? Convert.ToString("32") : Convert.ToString("31"));
                }
                if (a.Equals(randomDef))
                {
                    defBox.Text = (a.Checked ? Convert.ToString("32") : Convert.ToString("31"));
                }
                if (a.Equals(randomSpa))
                {
                    spaBox.Text = (a.Checked ? Convert.ToString("32") : Convert.ToString("31"));
                }
                if (a.Equals(randomSpd))
                {
                    spdBox.Text = (a.Checked ? Convert.ToString("32") : Convert.ToString("31"));
                }
                if (a.Equals(randomSpe))
                {
                    speBox.Text = (a.Checked ? Convert.ToString("32") : Convert.ToString("31"));
                }
            }
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton)
            {
                RadioButton a = (RadioButton)sender;
                if (a.Equals(staticPID))
                {
                    if (a.Checked)
                    {
                        pidBox.Text = "1";
                    }
                }
                if (a.Equals(randomPID))
                {
                    if (a.Checked)
                    {
                        pidBox.Text = "0";
                    }
                }
                if (a.Equals(neverShiny))
                {
                    wondercard.isShiny = WonderCard.Shininess.Never;
                }
                if (a.Equals(possibleShiny))
                {
                    wondercard.isShiny = WonderCard.Shininess.Possible;
                }
                if (a.Equals(alwaysShiny))
                {
                    wondercard.isShiny = WonderCard.Shininess.Always;
                }
                if (a.Equals(maleOT))
                {
                    wondercard.otGender = WonderCard.OTGender.Male;
                }
                if (a.Equals(femaleOT))
                {
                    wondercard.otGender = WonderCard.OTGender.Female;
                }
                if (a.Equals(gamegenderOT))
                {
                    wondercard.otGender = WonderCard.OTGender.Game;
                }
            }
        }

        private void ribbonList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            bool[] flags = new bool[16];
            RibbonSet r = new RibbonSet(0);
            for (int i = 0; i < flags.Length; i++)
            {
                if (i == e.Index)
                {
                    flags[i] = e.NewValue == CheckState.Checked;
                }
                else
                {
                    flags[i] = ribbonList.GetItemChecked(i);
                }
            }
            r.setChanges(flags);
            wondercard.ribbons = r;
        }
    }
}
