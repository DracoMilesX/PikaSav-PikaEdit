namespace Pikaedit_Gen4
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
            this.girlButton = new System.Windows.Forms.RadioButton();
            this.boyButton = new System.Windows.Forms.RadioButton();
            this.Label50 = new System.Windows.Forms.Label();
            this.sidBox = new System.Windows.Forms.TextBox();
            this.Label49 = new System.Windows.Forms.Label();
            this.idBox = new System.Windows.Forms.TextBox();
            this.Label48 = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.badgeList = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.moneyBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // girlButton
            // 
            this.girlButton.AutoSize = true;
            this.girlButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.girlButton.Location = new System.Drawing.Point(284, 7);
            this.girlButton.Name = "girlButton";
            this.girlButton.Size = new System.Drawing.Size(40, 17);
            this.girlButton.TabIndex = 111;
            this.girlButton.Text = "Girl";
            this.girlButton.UseVisualStyleBackColor = true;
            // 
            // boyButton
            // 
            this.boyButton.AutoSize = true;
            this.boyButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.boyButton.Location = new System.Drawing.Point(235, 7);
            this.boyButton.Name = "boyButton";
            this.boyButton.Size = new System.Drawing.Size(43, 17);
            this.boyButton.TabIndex = 110;
            this.boyButton.Text = "Boy";
            this.boyButton.UseVisualStyleBackColor = true;
            // 
            // Label50
            // 
            this.Label50.AutoSize = true;
            this.Label50.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label50.Location = new System.Drawing.Point(58, 61);
            this.Label50.Name = "Label50";
            this.Label50.Size = new System.Drawing.Size(25, 13);
            this.Label50.TabIndex = 109;
            this.Label50.Text = "SID";
            // 
            // sidBox
            // 
            this.sidBox.Location = new System.Drawing.Point(89, 58);
            this.sidBox.MaxLength = 5;
            this.sidBox.Name = "sidBox";
            this.sidBox.Size = new System.Drawing.Size(131, 20);
            this.sidBox.TabIndex = 108;
            this.sidBox.TextChanged += new System.EventHandler(this.ushort_TextChanged);
            // 
            // Label49
            // 
            this.Label49.AutoSize = true;
            this.Label49.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label49.Location = new System.Drawing.Point(65, 35);
            this.Label49.Name = "Label49";
            this.Label49.Size = new System.Drawing.Size(18, 13);
            this.Label49.TabIndex = 107;
            this.Label49.Text = "ID";
            // 
            // idBox
            // 
            this.idBox.Location = new System.Drawing.Point(89, 32);
            this.idBox.MaxLength = 5;
            this.idBox.Name = "idBox";
            this.idBox.Size = new System.Drawing.Size(131, 20);
            this.idBox.TabIndex = 106;
            this.idBox.TextChanged += new System.EventHandler(this.ushort_TextChanged);
            // 
            // Label48
            // 
            this.Label48.AutoSize = true;
            this.Label48.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label48.Location = new System.Drawing.Point(12, 9);
            this.Label48.Name = "Label48";
            this.Label48.Size = new System.Drawing.Size(71, 13);
            this.Label48.TabIndex = 105;
            this.Label48.Text = "Trainer Name";
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(89, 6);
            this.nameBox.MaxLength = 7;
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(131, 20);
            this.nameBox.TabIndex = 104;
            // 
            // badgeList
            // 
            this.badgeList.CheckOnClick = true;
            this.badgeList.FormattingEnabled = true;
            this.badgeList.Location = new System.Drawing.Point(15, 132);
            this.badgeList.Name = "badgeList";
            this.badgeList.Size = new System.Drawing.Size(205, 244);
            this.badgeList.TabIndex = 112;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(12, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 113;
            this.label1.Text = "Badges";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(44, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 115;
            this.label2.Text = "Money";
            // 
            // moneyBox
            // 
            this.moneyBox.Location = new System.Drawing.Point(89, 84);
            this.moneyBox.MaxLength = 6;
            this.moneyBox.Name = "moneyBox";
            this.moneyBox.Size = new System.Drawing.Size(131, 20);
            this.moneyBox.TabIndex = 114;
            this.moneyBox.TextChanged += new System.EventHandler(this.moneyBox_TextChanged);
            // 
            // TrainerEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 393);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TrainerEditor";
            this.Text = "Trainer Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.RadioButton girlButton;
        internal System.Windows.Forms.RadioButton boyButton;
        internal System.Windows.Forms.Label Label50;
        internal System.Windows.Forms.TextBox sidBox;
        internal System.Windows.Forms.Label Label49;
        internal System.Windows.Forms.TextBox idBox;
        internal System.Windows.Forms.Label Label48;
        internal System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.CheckedListBox badgeList;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox moneyBox;
    }
}