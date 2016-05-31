namespace Pikaedit
{
    partial class PidGeneratorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PidGeneratorForm));
            this.methodGroup = new System.Windows.Forms.GroupBox();
            this.gen5Group = new System.Windows.Forms.GroupBox();
            this.shinyLockedMethod = new System.Windows.Forms.RadioButton();
            this.eggMethod = new System.Windows.Forms.RadioButton();
            this.shinyEventMethod = new System.Windows.Forms.RadioButton();
            this.wildMethod = new System.Windows.Forms.RadioButton();
            this.eventMethod = new System.Windows.Forms.RadioButton();
            this.dwMethod = new System.Windows.Forms.RadioButton();
            this.generateButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.abilityIndex = new System.Windows.Forms.ComboBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.genderlessButton = new System.Windows.Forms.RadioButton();
            this.femaleButton = new System.Windows.Forms.RadioButton();
            this.maleButton = new System.Windows.Forms.RadioButton();
            this.isShiny = new System.Windows.Forms.CheckBox();
            this.pidResult = new System.Windows.Forms.TextBox();
            this.methodGroup.SuspendLayout();
            this.gen5Group.SuspendLayout();
            this.SuspendLayout();
            // 
            // methodGroup
            // 
            resources.ApplyResources(this.methodGroup, "methodGroup");
            this.methodGroup.Controls.Add(this.gen5Group);
            this.methodGroup.Name = "methodGroup";
            this.methodGroup.TabStop = false;
            // 
            // gen5Group
            // 
            resources.ApplyResources(this.gen5Group, "gen5Group");
            this.gen5Group.Controls.Add(this.shinyLockedMethod);
            this.gen5Group.Controls.Add(this.eggMethod);
            this.gen5Group.Controls.Add(this.shinyEventMethod);
            this.gen5Group.Controls.Add(this.wildMethod);
            this.gen5Group.Controls.Add(this.eventMethod);
            this.gen5Group.Controls.Add(this.dwMethod);
            this.gen5Group.Name = "gen5Group";
            this.gen5Group.TabStop = false;
            // 
            // shinyLockedMethod
            // 
            resources.ApplyResources(this.shinyLockedMethod, "shinyLockedMethod");
            this.shinyLockedMethod.Name = "shinyLockedMethod";
            this.shinyLockedMethod.UseVisualStyleBackColor = true;
            this.shinyLockedMethod.CheckedChanged += new System.EventHandler(this.changeMethod);
            // 
            // eggMethod
            // 
            resources.ApplyResources(this.eggMethod, "eggMethod");
            this.eggMethod.Checked = true;
            this.eggMethod.Name = "eggMethod";
            this.eggMethod.TabStop = true;
            this.eggMethod.UseVisualStyleBackColor = true;
            this.eggMethod.CheckedChanged += new System.EventHandler(this.changeMethod);
            // 
            // shinyEventMethod
            // 
            resources.ApplyResources(this.shinyEventMethod, "shinyEventMethod");
            this.shinyEventMethod.Name = "shinyEventMethod";
            this.shinyEventMethod.UseVisualStyleBackColor = true;
            this.shinyEventMethod.CheckedChanged += new System.EventHandler(this.changeMethod);
            // 
            // wildMethod
            // 
            resources.ApplyResources(this.wildMethod, "wildMethod");
            this.wildMethod.Name = "wildMethod";
            this.wildMethod.UseVisualStyleBackColor = true;
            this.wildMethod.CheckedChanged += new System.EventHandler(this.changeMethod);
            // 
            // eventMethod
            // 
            resources.ApplyResources(this.eventMethod, "eventMethod");
            this.eventMethod.Name = "eventMethod";
            this.eventMethod.UseVisualStyleBackColor = true;
            this.eventMethod.CheckedChanged += new System.EventHandler(this.changeMethod);
            // 
            // dwMethod
            // 
            resources.ApplyResources(this.dwMethod, "dwMethod");
            this.dwMethod.Name = "dwMethod";
            this.dwMethod.UseVisualStyleBackColor = true;
            this.dwMethod.CheckedChanged += new System.EventHandler(this.changeMethod);
            // 
            // generateButton
            // 
            resources.ApplyResources(this.generateButton, "generateButton");
            this.generateButton.Name = "generateButton";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.generate_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // abilityIndex
            // 
            resources.ApplyResources(this.abilityIndex, "abilityIndex");
            this.abilityIndex.FormattingEnabled = true;
            this.abilityIndex.Items.AddRange(new object[] {
            resources.GetString("abilityIndex.Items"),
            resources.GetString("abilityIndex.Items1"),
            resources.GetString("abilityIndex.Items2")});
            this.abilityIndex.Name = "abilityIndex";
            // 
            // Label11
            // 
            resources.ApplyResources(this.Label11, "Label11");
            this.Label11.Name = "Label11";
            // 
            // genderlessButton
            // 
            resources.ApplyResources(this.genderlessButton, "genderlessButton");
            this.genderlessButton.Name = "genderlessButton";
            this.genderlessButton.TabStop = true;
            this.genderlessButton.UseVisualStyleBackColor = true;
            this.genderlessButton.CheckedChanged += new System.EventHandler(this.genderChange);
            // 
            // femaleButton
            // 
            resources.ApplyResources(this.femaleButton, "femaleButton");
            this.femaleButton.Name = "femaleButton";
            this.femaleButton.TabStop = true;
            this.femaleButton.UseVisualStyleBackColor = true;
            this.femaleButton.CheckedChanged += new System.EventHandler(this.genderChange);
            // 
            // maleButton
            // 
            resources.ApplyResources(this.maleButton, "maleButton");
            this.maleButton.Name = "maleButton";
            this.maleButton.TabStop = true;
            this.maleButton.UseVisualStyleBackColor = true;
            this.maleButton.CheckedChanged += new System.EventHandler(this.genderChange);
            // 
            // isShiny
            // 
            resources.ApplyResources(this.isShiny, "isShiny");
            this.isShiny.Name = "isShiny";
            this.isShiny.ThreeState = true;
            this.isShiny.UseVisualStyleBackColor = true;
            // 
            // pidResult
            // 
            resources.ApplyResources(this.pidResult, "pidResult");
            this.pidResult.Name = "pidResult";
            this.pidResult.ReadOnly = true;
            // 
            // PidGeneratorForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pidResult);
            this.Controls.Add(this.isShiny);
            this.Controls.Add(this.Label11);
            this.Controls.Add(this.genderlessButton);
            this.Controls.Add(this.femaleButton);
            this.Controls.Add(this.maleButton);
            this.Controls.Add(this.abilityIndex);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.methodGroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PidGeneratorForm";
            this.methodGroup.ResumeLayout(false);
            this.gen5Group.ResumeLayout(false);
            this.gen5Group.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox methodGroup;
        private System.Windows.Forms.RadioButton eggMethod;
        private System.Windows.Forms.RadioButton shinyLockedMethod;
        private System.Windows.Forms.RadioButton shinyEventMethod;
        private System.Windows.Forms.RadioButton eventMethod;
        private System.Windows.Forms.RadioButton dwMethod;
        private System.Windows.Forms.RadioButton wildMethod;
        private System.Windows.Forms.GroupBox gen5Group;
        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox abilityIndex;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.RadioButton genderlessButton;
        internal System.Windows.Forms.RadioButton femaleButton;
        internal System.Windows.Forms.RadioButton maleButton;
        private System.Windows.Forms.CheckBox isShiny;
        private System.Windows.Forms.TextBox pidResult;
    }
}