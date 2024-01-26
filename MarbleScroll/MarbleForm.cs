using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarbleScroll
{
    public partial class MarbleForm : Form
    {
        public MarbleForm()
        {
            InitializeComponent();
            InitializeSystray();

            Hide();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Hide();
            base.OnLoad(e);
        }

        private void InitializeSystray()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarbleForm));
            Container components = new Container();


            NotifyIcon trayIcon = new NotifyIcon(components);
            trayIcon.ContextMenuStrip = contextMenuStrip1;
            trayIcon.Icon = (Icon) resources.GetObject("$this.Icon");
            trayIcon.Text = "Scrolly";
            trayIcon.Visible = true;
        }

        private void Exit_Click(object sender, EventArgs e)
        {

        }

        private void MarbleForm_Load(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }

        private void scrollOnMiddleButtonToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Global.marbleScroll.EnableMiddleButtonScroll = scrollOnMiddleButtonToolStripMenuItem.Checked;
        }

        private void scrollOnXButtonToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Global.marbleScroll.EnableXButtonScroll = scrollOnXButtonToolStripMenuItem.Checked;
        }
    }
}
