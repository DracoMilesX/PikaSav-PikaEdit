namespace Pikaedit
{
    partial class MysteryGift
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MysteryGift));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.addUsedButton = new System.Windows.Forms.Button();
            this.idBox = new System.Windows.Forms.TextBox();
            this.usedIDList = new System.Windows.Forms.ListBox();
            this.wondercardInfo = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.slotSelect = new System.Windows.Forms.ComboBox();
            this.editButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.addUsedButton);
            this.groupBox1.Controls.Add(this.idBox);
            this.groupBox1.Controls.Add(this.usedIDList);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // addUsedButton
            // 
            resources.ApplyResources(this.addUsedButton, "addUsedButton");
            this.addUsedButton.Name = "addUsedButton";
            this.addUsedButton.UseVisualStyleBackColor = true;
            // 
            // idBox
            // 
            resources.ApplyResources(this.idBox, "idBox");
            this.idBox.Name = "idBox";
            this.idBox.TextChanged += new System.EventHandler(this.ushort_TextChanged);
            // 
            // usedIDList
            // 
            this.usedIDList.FormattingEnabled = true;
            resources.ApplyResources(this.usedIDList, "usedIDList");
            this.usedIDList.Name = "usedIDList";
            // 
            // wondercardInfo
            // 
            this.wondercardInfo.FormattingEnabled = true;
            resources.ApplyResources(this.wondercardInfo, "wondercardInfo");
            this.wondercardInfo.Name = "wondercardInfo";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // slotSelect
            // 
            this.slotSelect.FormattingEnabled = true;
            this.slotSelect.Items.AddRange(new object[] {
            resources.GetString("slotSelect.Items"),
            resources.GetString("slotSelect.Items1"),
            resources.GetString("slotSelect.Items2"),
            resources.GetString("slotSelect.Items3"),
            resources.GetString("slotSelect.Items4"),
            resources.GetString("slotSelect.Items5"),
            resources.GetString("slotSelect.Items6"),
            resources.GetString("slotSelect.Items7"),
            resources.GetString("slotSelect.Items8"),
            resources.GetString("slotSelect.Items9"),
            resources.GetString("slotSelect.Items10"),
            resources.GetString("slotSelect.Items11")});
            resources.ApplyResources(this.slotSelect, "slotSelect");
            this.slotSelect.Name = "slotSelect";
            this.slotSelect.SelectedIndexChanged += new System.EventHandler(this.slotSelect_SelectedIndexChanged);
            // 
            // editButton
            // 
            resources.ApplyResources(this.editButton, "editButton");
            this.editButton.Name = "editButton";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // MysteryGift
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.slotSelect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.wondercardInfo);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MysteryGift";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox usedIDList;
        private System.Windows.Forms.TextBox idBox;
        private System.Windows.Forms.Button addUsedButton;
        private System.Windows.Forms.ListBox wondercardInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox slotSelect;
        private System.Windows.Forms.Button editButton;
    }
}