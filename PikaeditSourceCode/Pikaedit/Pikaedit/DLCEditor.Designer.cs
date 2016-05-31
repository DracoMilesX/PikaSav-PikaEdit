namespace Pikaedit
{
    partial class DLCEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DLCEditor));
            this.loadDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.extractMusical = new System.Windows.Forms.Button();
            this.activeMusical = new System.Windows.Forms.CheckBox();
            this.changeMusical = new System.Windows.Forms.Button();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.extractPokedex = new System.Windows.Forms.Button();
            this.activePokedex = new System.Windows.Forms.CheckBox();
            this.changePokedex = new System.Windows.Forms.Button();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.extractCGear = new System.Windows.Forms.Button();
            this.changeCGear = new System.Windows.Forms.Button();
            this.activeCGear = new System.Windows.Forms.CheckBox();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.musicalTitle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GroupBox3.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // extractMusical
            // 
            this.extractMusical.Location = new System.Drawing.Point(6, 71);
            this.extractMusical.Name = "extractMusical";
            this.extractMusical.Size = new System.Drawing.Size(75, 23);
            this.extractMusical.TabIndex = 8;
            this.extractMusical.Text = "Extract";
            this.extractMusical.UseVisualStyleBackColor = true;
            this.extractMusical.Click += new System.EventHandler(this.extract);
            // 
            // activeMusical
            // 
            this.activeMusical.AutoSize = true;
            this.activeMusical.Location = new System.Drawing.Point(6, 19);
            this.activeMusical.Name = "activeMusical";
            this.activeMusical.Size = new System.Drawing.Size(56, 17);
            this.activeMusical.TabIndex = 6;
            this.activeMusical.Text = "Active";
            this.activeMusical.UseVisualStyleBackColor = true;
            this.activeMusical.CheckedChanged += new System.EventHandler(this.activate);
            // 
            // changeMusical
            // 
            this.changeMusical.Location = new System.Drawing.Point(6, 42);
            this.changeMusical.Name = "changeMusical";
            this.changeMusical.Size = new System.Drawing.Size(75, 23);
            this.changeMusical.TabIndex = 7;
            this.changeMusical.Text = "Change";
            this.changeMusical.UseVisualStyleBackColor = true;
            this.changeMusical.Click += new System.EventHandler(this.change);
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.label1);
            this.GroupBox3.Controls.Add(this.musicalTitle);
            this.GroupBox3.Controls.Add(this.extractMusical);
            this.GroupBox3.Controls.Add(this.activeMusical);
            this.GroupBox3.Controls.Add(this.changeMusical);
            this.GroupBox3.Location = new System.Drawing.Point(312, 2);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(263, 110);
            this.GroupBox3.TabIndex = 12;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "Musical Data";
            // 
            // extractPokedex
            // 
            this.extractPokedex.Location = new System.Drawing.Point(6, 71);
            this.extractPokedex.Name = "extractPokedex";
            this.extractPokedex.Size = new System.Drawing.Size(75, 23);
            this.extractPokedex.TabIndex = 8;
            this.extractPokedex.Text = "Extract";
            this.extractPokedex.UseVisualStyleBackColor = true;
            this.extractPokedex.Click += new System.EventHandler(this.extract);
            // 
            // activePokedex
            // 
            this.activePokedex.AutoSize = true;
            this.activePokedex.Location = new System.Drawing.Point(6, 19);
            this.activePokedex.Name = "activePokedex";
            this.activePokedex.Size = new System.Drawing.Size(56, 17);
            this.activePokedex.TabIndex = 6;
            this.activePokedex.Text = "Active";
            this.activePokedex.UseVisualStyleBackColor = true;
            this.activePokedex.CheckedChanged += new System.EventHandler(this.activate);
            // 
            // changePokedex
            // 
            this.changePokedex.Location = new System.Drawing.Point(6, 42);
            this.changePokedex.Name = "changePokedex";
            this.changePokedex.Size = new System.Drawing.Size(75, 23);
            this.changePokedex.TabIndex = 7;
            this.changePokedex.Text = "Change";
            this.changePokedex.UseVisualStyleBackColor = true;
            this.changePokedex.Click += new System.EventHandler(this.change);
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.extractPokedex);
            this.GroupBox2.Controls.Add(this.activePokedex);
            this.GroupBox2.Controls.Add(this.changePokedex);
            this.GroupBox2.Location = new System.Drawing.Point(162, 2);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(144, 110);
            this.GroupBox2.TabIndex = 11;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Pokedex Skin";
            // 
            // extractCGear
            // 
            this.extractCGear.Location = new System.Drawing.Point(6, 71);
            this.extractCGear.Name = "extractCGear";
            this.extractCGear.Size = new System.Drawing.Size(75, 23);
            this.extractCGear.TabIndex = 5;
            this.extractCGear.Text = "Extract";
            this.extractCGear.UseVisualStyleBackColor = true;
            this.extractCGear.Click += new System.EventHandler(this.extract);
            // 
            // changeCGear
            // 
            this.changeCGear.Location = new System.Drawing.Point(6, 42);
            this.changeCGear.Name = "changeCGear";
            this.changeCGear.Size = new System.Drawing.Size(75, 23);
            this.changeCGear.TabIndex = 4;
            this.changeCGear.Text = "Change";
            this.changeCGear.UseVisualStyleBackColor = true;
            this.changeCGear.Click += new System.EventHandler(this.change);
            // 
            // activeCGear
            // 
            this.activeCGear.AutoSize = true;
            this.activeCGear.Location = new System.Drawing.Point(6, 19);
            this.activeCGear.Name = "activeCGear";
            this.activeCGear.Size = new System.Drawing.Size(56, 17);
            this.activeCGear.TabIndex = 3;
            this.activeCGear.Text = "Active";
            this.activeCGear.UseVisualStyleBackColor = true;
            this.activeCGear.CheckedChanged += new System.EventHandler(this.activate);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.extractCGear);
            this.GroupBox1.Controls.Add(this.changeCGear);
            this.GroupBox1.Controls.Add(this.activeCGear);
            this.GroupBox1.Location = new System.Drawing.Point(3, 2);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(153, 110);
            this.GroupBox1.TabIndex = 10;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "C-Gear Skin";
            // 
            // musicalTitle
            // 
            this.musicalTitle.Location = new System.Drawing.Point(92, 42);
            this.musicalTitle.MaxLength = 14;
            this.musicalTitle.Name = "musicalTitle";
            this.musicalTitle.Size = new System.Drawing.Size(163, 20);
            this.musicalTitle.TabIndex = 13;
            this.musicalTitle.TextChanged += new System.EventHandler(this.musicalTitle_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Musical Title";
            // 
            // DLCEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 116);
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.GroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DLCEditor";
            this.Text = "DLC";
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox3.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog loadDialog;
        private System.Windows.Forms.SaveFileDialog saveDialog;
        internal System.Windows.Forms.Button extractMusical;
        internal System.Windows.Forms.CheckBox activeMusical;
        internal System.Windows.Forms.Button changeMusical;
        internal System.Windows.Forms.GroupBox GroupBox3;
        internal System.Windows.Forms.Button extractPokedex;
        internal System.Windows.Forms.CheckBox activePokedex;
        internal System.Windows.Forms.Button changePokedex;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.Button extractCGear;
        internal System.Windows.Forms.Button changeCGear;
        internal System.Windows.Forms.CheckBox activeCGear;
        internal System.Windows.Forms.GroupBox GroupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox musicalTitle;
    }
}