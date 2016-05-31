using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Pikaedit_Gen4
{
    class Slot : Button
    {
        protected bool selected = false;

        public Slot()
        {
            this.Size = new Size(40, 42);
            this.Text = "";
            this.AllowDrop = true;
            this.selected = false;
            this.Enabled = true;
        }

        public bool Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
            }
        }
    }
}
