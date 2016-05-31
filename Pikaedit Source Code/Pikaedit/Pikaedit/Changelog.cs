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
    public partial class Changelog : Form
    {
        public Changelog()
        {
            InitializeComponent();
            richTextBox1.Text = Properties.Resources.changelog;
        }
    }
}
