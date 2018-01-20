namespace Satobot.Forms
{
    partial class MainForm
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


        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        { 
            this.components = new System.ComponentModel.Container();
            this.flowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.addStrategyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromExistingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.browseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startAllStrategiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopAllStrategiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllStrategiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimizeToTrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowPanel
            // 
            this.flowPanel.AutoScroll = true;
            this.flowPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.flowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanel.Location = new System.Drawing.Point(0, 24);
            this.flowPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowPanel.Name = "flowPanel";
            this.flowPanel.Size = new System.Drawing.Size(523, 417);
            this.flowPanel.TabIndex = 2;
            this.flowPanel.MouseEnter += new System.EventHandler(this.FlowPanel_MouseEnter);
            // 
            // trayIcon
            // 
            this.trayIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.trayIcon.BalloonTipText = "Satobot is now running down here, click this icon to bring back the window.";
            this.trayIcon.BalloonTipTitle = "Satobot";
            this.trayIcon.Text = "Satobot";
            this.trayIcon.Click += new System.EventHandler(this.TrayIcon_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addStrategyToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip.Size = new System.Drawing.Size(523, 24);
            this.menuStrip.TabIndex = 3;
            this.menuStrip.Text = "menuStrip1";
            // 
            // addStrategyToolStripMenuItem
            // 
            this.addStrategyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.fromExistingToolStripMenuItem});
            this.addStrategyToolStripMenuItem.Name = "addStrategyToolStripMenuItem";
            this.addStrategyToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.addStrategyToolStripMenuItem.Text = "Add Strategy";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.newToolStripMenuItem.Text = "New...";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.NewStrategyToolStripMenuItem_Click);
            // 
            // fromExistingToolStripMenuItem
            // 
            this.fromExistingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.browseToolStripMenuItem});
            this.fromExistingToolStripMenuItem.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fromExistingToolStripMenuItem.Name = "fromExistingToolStripMenuItem";
            this.fromExistingToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.fromExistingToolStripMenuItem.Text = "Existing";
            // 
            // browseToolStripMenuItem
            // 
            this.browseToolStripMenuItem.Name = "browseToolStripMenuItem";
            this.browseToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.browseToolStripMenuItem.Text = "Browse...";
            this.browseToolStripMenuItem.Click += new System.EventHandler(this.BrowseToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.controlToolStripMenuItem,
            this.minimizeToTrayToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(68, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // controlToolStripMenuItem
            // 
            this.controlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startAllStrategiesToolStripMenuItem,
            this.stopAllStrategiesToolStripMenuItem,
            this.removeAllStrategiesToolStripMenuItem});
            this.controlToolStripMenuItem.Name = "controlToolStripMenuItem";
            this.controlToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.controlToolStripMenuItem.Text = "Control";
            // 
            // startAllStrategiesToolStripMenuItem
            // 
            this.startAllStrategiesToolStripMenuItem.Name = "startAllStrategiesToolStripMenuItem";
            this.startAllStrategiesToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.startAllStrategiesToolStripMenuItem.Text = "Start all strategies";
            this.startAllStrategiesToolStripMenuItem.Click += new System.EventHandler(this.StartAllToolStripMenuItem_Click);
            // 
            // stopAllStrategiesToolStripMenuItem
            // 
            this.stopAllStrategiesToolStripMenuItem.Name = "stopAllStrategiesToolStripMenuItem";
            this.stopAllStrategiesToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.stopAllStrategiesToolStripMenuItem.Text = "Stop all strategies";
            this.stopAllStrategiesToolStripMenuItem.Click += new System.EventHandler(this.StopAllToolStripMenuItem_Click);
            // 
            // removeAllStrategiesToolStripMenuItem
            // 
            this.removeAllStrategiesToolStripMenuItem.Name = "removeAllStrategiesToolStripMenuItem";
            this.removeAllStrategiesToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.removeAllStrategiesToolStripMenuItem.Text = "Remove all strategies";
            this.removeAllStrategiesToolStripMenuItem.Click += new System.EventHandler(this.RemoveAllToolStripMenuItem_Click);
            // 
            // minimizeToTrayToolStripMenuItem
            // 
            this.minimizeToTrayToolStripMenuItem.Checked = true;
            this.minimizeToTrayToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.minimizeToTrayToolStripMenuItem.Name = "minimizeToTrayToolStripMenuItem";
            this.minimizeToTrayToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.minimizeToTrayToolStripMenuItem.Text = "Minimize to tray";
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(523, 441);
            this.Controls.Add(this.flowPanel);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(539, 480);
            this.Name = "MainForm";
            this.Text = "Satobot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.FlowLayoutPanel flowPanel;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem addStrategyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromExistingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem browseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem controlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startAllStrategiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopAllStrategiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeAllStrategiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minimizeToTrayToolStripMenuItem;
    }
}

