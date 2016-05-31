namespace Pikaedit
{
    partial class TrainerEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrainerEditor));
            this.label2 = new System.Windows.Forms.Label();
            this.moneyBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.badgeList = new System.Windows.Forms.CheckedListBox();
            this.girlButton = new System.Windows.Forms.RadioButton();
            this.boyButton = new System.Windows.Forms.RadioButton();
            this.Label50 = new System.Windows.Forms.Label();
            this.sidBox = new System.Windows.Forms.TextBox();
            this.Label49 = new System.Windows.Forms.Label();
            this.idBox = new System.Windows.Forms.TextBox();
            this.Label48 = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // moneyBox
            // 
            resources.ApplyResources(this.moneyBox, "moneyBox");
            this.moneyBox.Name = "moneyBox";
            this.moneyBox.TextChanged += new System.EventHandler(this.moneyBox_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // badgeList
            // 
            this.badgeList.CheckOnClick = true;
            this.badgeList.FormattingEnabled = true;
            resources.ApplyResources(this.badgeList, "badgeList");
            this.badgeList.Name = "badgeList";
            // 
            // girlButton
            // 
            resources.ApplyResources(this.girlButton, "girlButton");
            this.girlButton.Name = "girlButton";
            this.girlButton.UseVisualStyleBackColor = true;
            // 
            // boyButton
            // 
            resources.ApplyResources(this.boyButton, "boyButton");
            this.boyButton.Name = "boyButton";
            this.boyButton.UseVisualStyleBackColor = true;
            // 
            // Label50
            // 
            resources.ApplyResources(this.Label50, "Label50");
            this.Label50.Name = "Label50";
            // 
            // sidBox
            // 
            resources.ApplyResources(this.sidBox, "sidBox");
            this.sidBox.Name = "sidBox";
            this.sidBox.TextChanged += new System.EventHandler(this.ushort_TextChanged);
            // 
            // Label49
            // 
            resources.ApplyResources(this.Label49, "Label49");
            this.Label49.Name = "Label49";
            // 
            // idBox
            // 
            resources.ApplyResources(this.idBox, "idBox");
            this.idBox.Name = "idBox";
            this.idBox.TextChanged += new System.EventHandler(this.ushort_TextChanged);
            // 
            // Label48
            // 
            resources.ApplyResources(this.Label48, "Label48");
            this.Label48.Name = "Label48";
            // 
            // nameBox
            // 
            resources.ApplyResources(this.nameBox, "nameBox");
            this.nameBox.Name = "nameBox";
            // 
            // TrainerEditor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.moneyBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.badgeList);
            this.Controls.Add(this.girlButton);
            this.Controls.Add(this.boyButton);
            this.Controls.Add(this.Label50);
            this.Controls.Add(this.sidBox);
            this.Controls.Add(this.Label49);
            this.Controls.Add(this.idBox);
            this.Controls.Add(this.Label48);
            this.Controls.Add(this.nameBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TrainerEditor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox moneyBox;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox badgeList;
        internal System.Windows.Forms.RadioButton girlButton;
        internal System.Windows.Forms.RadioButton boyButton;
        internal System.Windows.Forms.Label Label50;
        internal System.Windows.Forms.TextBox sidBox;
        internal System.Windows.Forms.Label Label49;
        internal System.Windows.Forms.TextBox idBox;
        internal System.Windows.Forms.Label Label48;
        internal System.Windows.Forms.TextBox nameBox;
    }
}