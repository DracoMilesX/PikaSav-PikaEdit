using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pikaedit
{
    public static class Notification
    {
        public static NotifyIcon notifyIcon;

        public static void Initialize()
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = Properties.Resources.icon;
            notifyIcon.Text = "Pikaedit";
            notifyIcon.Visible = true;
        }

        public static void show(string text, string title="")
        {
            notifyIcon.ShowBalloonTip(3000, title, text, ToolTipIcon.None);
        }

        public static void show(int timeout, string text, string title = "")
        {
            notifyIcon.ShowBalloonTip(timeout, title, text, ToolTipIcon.None);
        }

        public static void showIcon(bool visible=true)
        {
            notifyIcon.Visible = visible;
        }

        public static void kill()
        {
            notifyIcon.Visible = false;
            notifyIcon.Dispose();
            notifyIcon = null;
        }
    }
}
