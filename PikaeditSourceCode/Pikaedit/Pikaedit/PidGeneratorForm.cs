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
    public partial class PidGeneratorForm : Form
    {
        private Pokemon pkm;
        private PidGen.PIDTypes type;
        private PidGen.Shininess shiny;
        private int gender = 0;

        public PidGeneratorForm()
        {
            InitializeComponent();
        }

        public PidGeneratorForm(Pokemon pkm)
        {
            InitializeComponent();
            this.pkm = pkm;
            pidResult.Text = Convert.ToString(pkm.pid);
            PidGen.finalPID = pkm.pid;
            if ((pkm.DW & 1) == 1)
            {
                abilityIndex.SelectedIndex = 2;
            }
            else
            {
                abilityIndex.SelectedIndex = pkm.abilityIndex;
            }
            if (pkm.isShiny)
            {
                isShiny.CheckState = CheckState.Checked;
            }
            else
            {
                isShiny.CheckState = CheckState.Unchecked;
            }
            checkPKM();
        }

        private void checkPKM()
        {
            if (pkm.isHatched)
            {
                eggMethod.Checked = true;
                eggMethod.Text += " (Data determined)";
                //type = PidGen.PIDTypes.Egg;
            }
            else
            {
                if (pkm.locationmet.Equals("Entree Forest") | pkm.locationmet.Equals("Pokémon Dream Radar"))
                {
                    dwMethod.Checked = true;
                    dwMethod.Text += " (Data determined)";
                    //type = PidGen.PIDTypes.Dw;
                }
                else
                {
                    if (!pkm.isFateful & (pkm.species == "Reshiram" | pkm.species == "Zekrom" | pkm.species == "Victini"))
                    {
                        shinyLockedMethod.Checked = true;
                        shinyLockedMethod.Text += " (Data determined)";
                        //type = PidGen.PIDTypes.Shiny_Locked;
                    }
                    else
                    {
                        if (pkm.isFateful & pkm.isShiny)
                        {
                            shinyEventMethod.Checked = true;
                            shinyEventMethod.Text += " (Data determined)";
                            //type = PidGen.PIDTypes.Shiny_Event;
                        }
                        else
                        {
                            if (pkm.isFateful | pkm.species == "Tornadus" | pkm.species == "Thundurus" | pkm.species == "Landorus" | pkm.locationmet == "Nuvema Town" | pkm.locationmet == "Aspertia City")
                            {
                                eventMethod.Checked = true;
                                eventMethod.Text += " (Data determined)";
                                //type = PidGen.PIDTypes.Shiny_Event;
                            }
                            else
                            {
                                wildMethod.Checked = true;
                                wildMethod.Text += " (Data determined)";
                                //type = PidGen.PIDTypes.Wild;
                            }
                        }
                    }
                }
            }
            if (pkm.female == 1)
            {
                femaleButton.Checked = true;
            }
            else
            {
                if (pkm.genderless == 1)
                {
                    genderlessButton.Checked = true;
                }
                else
                {
                    maleButton.Checked = true;
                }
            }
        }

        private void generate_Click(object sender, EventArgs e)
        {
            if (isShiny.CheckState == CheckState.Unchecked)
            {
                shiny = PidGen.Shininess.Normal;
            }
            if (isShiny.CheckState == CheckState.Indeterminate)
            {
                shiny = PidGen.Shininess.Possible;
            }
            if (isShiny.CheckState == CheckState.Checked)
            {
                shiny = PidGen.Shininess.Shiny;
            }
            PidGen.generate(type, shiny, abilityIndex.SelectedIndex, gender, pkm);
            pidResult.Text = Convert.ToString(PidGen.finalPID);
        }

        private void changeMethod(object sender, EventArgs e)
        {
            if (sender is RadioButton)
            {
                RadioButton a = (RadioButton)sender;
                if (a.Name.Equals("eggMethod"))
                {
                    type = PidGen.PIDTypes.Egg;
                    isShiny.Enabled = true;
                }
                if (a.Name.Equals("wildMethod"))
                {
                    type = PidGen.PIDTypes.Wild;
                    isShiny.Enabled = true;
                }
                if (a.Name.Equals("eventMethod"))
                {
                    type = PidGen.PIDTypes.Event;
                    isShiny.Enabled = true;
                }
                if (a.Name.Equals("dwMethod"))
                {
                    type = PidGen.PIDTypes.Dw;
                    isShiny.Enabled = false;
                    isShiny.CheckState = CheckState.Unchecked;
                }
                if (a.Name.Equals("shinyEventMethod"))
                {
                    type = PidGen.PIDTypes.Shiny_Event;
                    isShiny.Enabled = false;
                    isShiny.CheckState = CheckState.Checked;
                }
                if (a.Name.Equals("shinyLockedMethod"))
                {
                    type = PidGen.PIDTypes.Shiny_Locked;
                    isShiny.CheckState = CheckState.Unchecked;
                    isShiny.Enabled = false;
                }
            }
        }

        private void genderChange(object sender, EventArgs e)
        {
            if (sender is RadioButton)
            {
                RadioButton a = (RadioButton)sender;
                if (a.Name.Equals("maleButton"))
                {
                    gender = 0;
                }
                if (a.Name.Equals("femaleButton"))
                {
                    gender = 1;
                }
                if (a.Name.Equals("genderlessButton"))
                {
                    gender = 2;
                }
            }
        }
    }
}
