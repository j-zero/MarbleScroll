using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarbleScroll
{
    static class Program
    {

        

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Global.marbleScroll = new MarbleScroll();
            Global.marbleScroll.FocusWindow = false;
            Global.marbleScroll.EnableMiddleButtonScroll = true;
            Global.marbleScroll.EnableXButtonScroll = true;
            Global.marbleScroll.Start();
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += new EventHandler(Exit);
            MarbleForm form = new MarbleForm();
            Application.Run(); 
        }

        static void Exit(object sender, EventArgs e)
        {
            Global.marbleScroll.Stop();
        }
    }
}
