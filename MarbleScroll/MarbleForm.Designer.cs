namespace MarbleScroll
{
    partial class MarbleForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarbleForm));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scrollOnMiddleButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emulateMiddleButtonOnXClickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scrollOnXButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem,
            this.scrollOnMiddleButtonToolStripMenuItem,
            this.scrollOnXButtonToolStripMenuItem,
            this.emulateMiddleButtonOnXClickToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(347, 165);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(240, 32);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // scrollOnMiddleButtonToolStripMenuItem
            // 
            this.scrollOnMiddleButtonToolStripMenuItem.Checked = true;
            this.scrollOnMiddleButtonToolStripMenuItem.CheckOnClick = true;
            this.scrollOnMiddleButtonToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.scrollOnMiddleButtonToolStripMenuItem.Name = "scrollOnMiddleButtonToolStripMenuItem";
            this.scrollOnMiddleButtonToolStripMenuItem.Size = new System.Drawing.Size(346, 32);
            this.scrollOnMiddleButtonToolStripMenuItem.Text = "Scroll on middle Button";
            this.scrollOnMiddleButtonToolStripMenuItem.CheckedChanged += new System.EventHandler(this.scrollOnMiddleButtonToolStripMenuItem_CheckedChanged);
            // 
            // emulateMiddleButtonOnXClickToolStripMenuItem
            // 
            this.emulateMiddleButtonOnXClickToolStripMenuItem.CheckOnClick = true;
            this.emulateMiddleButtonOnXClickToolStripMenuItem.Name = "emulateMiddleButtonOnXClickToolStripMenuItem";
            this.emulateMiddleButtonOnXClickToolStripMenuItem.Size = new System.Drawing.Size(346, 32);
            this.emulateMiddleButtonOnXClickToolStripMenuItem.Text = "Emulate middle button on X click";
            // 
            // scrollOnXButtonToolStripMenuItem
            // 
            this.scrollOnXButtonToolStripMenuItem.Checked = true;
            this.scrollOnXButtonToolStripMenuItem.CheckOnClick = true;
            this.scrollOnXButtonToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.scrollOnXButtonToolStripMenuItem.Name = "scrollOnXButtonToolStripMenuItem";
            this.scrollOnXButtonToolStripMenuItem.Size = new System.Drawing.Size(346, 32);
            this.scrollOnXButtonToolStripMenuItem.Text = "Scroll on X-Button ";
            this.scrollOnXButtonToolStripMenuItem.CheckedChanged += new System.EventHandler(this.scrollOnXButtonToolStripMenuItem_CheckedChanged);
            // 
            // MarbleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 403);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MarbleForm";
            this.ShowInTaskbar = false;
            this.Text = "MarbleScroll";
            this.Load += new System.EventHandler(this.MarbleForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scrollOnMiddleButtonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emulateMiddleButtonOnXClickToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scrollOnXButtonToolStripMenuItem;
    }
}

