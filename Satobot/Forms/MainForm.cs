using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Satobot.Controls;
using Satobot.Misc;
using Satobot.Objects;

namespace Satobot.Forms
{
    public partial class MainForm : Form
    {
        private bool CloseAccepted;

        public MainForm()
        {
            InitializeComponent();
            InitializeGameFiles();

            Icon = IconExtractor.GetIconFromApplication();
            trayIcon.Icon = Icon;
        }

        public DialogResult ShowMessage(string message, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(this, message, Application.ProductName, buttons, icon);
        }

        private async void InitializeGameFiles()
        {
            fromExistingToolStripMenuItem.DropDownItems.Clear();
            fromExistingToolStripMenuItem.DropDownItems.Add(browseToolStripMenuItem);

            var files = Directory.GetFiles(Program.StrategyFolder, Program.StrategyFileExtension);

            foreach (var file in files)
            {
                using (var reader = new StreamReader(File.OpenRead(file)))
                {
                    var strategy = await StrategySerializer.Read(reader);

                    if (strategy == null)
                        continue;

                    var menuItem = new ToolStripMenuItem(Path.GetFileName(file)) { Tag = strategy };
                    menuItem.Click += FromExistingMenuItem_Click;
                    fromExistingToolStripMenuItem.DropDownItems.Add(menuItem);
                }
            }
        } 

        private void ShowDetailDialogAndAdd(Strategy strategy)
        {
            var finalDetails = StrategyBuilderForm.ShowDetailDialog(strategy);

            if (finalDetails.Item2 == null)
                return;

            for (var i = 0; i < finalDetails.Item1; i++)
                flowPanel.Controls.Add(new StrategyPanel(finalDetails.Item2));

            InitializeGameFiles();
        }

        #region Event Handlers

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized || !minimizeToTrayToolStripMenuItem.Checked)
                return;

            ShowInTaskbar = false;
            trayIcon.Visible = true;
            trayIcon.ShowBalloonTip(3000);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CloseAccepted)
                return;

            foreach (var control in flowPanel.Controls.Cast<StrategyPanel>())
            {
                if (!control.Running)
                    continue;

                var result = ShowMessage(Messages.ExitWarning, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                CloseAccepted = result == DialogResult.Yes;

                if (CloseAccepted)
                    break;

                return;
            }

            Application.Exit();
            Environment.Exit(0);
        }

        private void FlowPanel_MouseEnter(object sender, EventArgs e)
        {
            flowPanel.Focus();
        }

        private void NewStrategyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDetailDialogAndAdd(null);
        }

        private void FromExistingMenuItem_Click(object sender, EventArgs eventArgs)
        {
            ShowDetailDialogAndAdd((Strategy) ((ToolStripMenuItem) sender).Tag);
        }

        private async void BrowseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var openDialog = new OpenFileDialog())
            {
                openDialog.AddExtension = true;
                openDialog.CheckPathExists = true;
                openDialog.ValidateNames = true;
                openDialog.InitialDirectory = Path.Combine(Application.StartupPath, Program.StrategyFolder);
                openDialog.Filter = Program.StrategyFileFilter;
                openDialog.Title = "Open strategy file...";

                var result = openDialog.ShowDialog();

                if (result != DialogResult.OK)
                    return;

                // open here
                using (var file = new StreamReader(openDialog.OpenFile()))
                {
                    var strategy = await StrategySerializer.Read(file);

                    if (strategy == null)
                    {
                        ShowMessage(Messages.OpenStrategyFailure, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    ShowDetailDialogAndAdd(strategy);
                }
            }
        }

        private void StartAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var control in flowPanel.Controls.Cast<StrategyPanel>())
                control.RequestStart();
        }

        private void StopAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var control in flowPanel.Controls.Cast<StrategyPanel>())
                control.RequestStop();
        }

        private void RemoveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            while (true)
            {
                foreach (var control in flowPanel.Controls.Cast<StrategyPanel>())
                    control.RequestRemove();

                if (flowPanel.Controls.Count > 0)
                    continue;

                break;
            }
        }

        private void TrayIcon_Click(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
                return;

            ShowInTaskbar = true;
            trayIcon.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private async void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[]) e.Data.GetData(DataFormats.FileDrop);

            if (files.Length < 1)
                return;

            using (var reader = new StreamReader(File.OpenRead(files[0])))
            {
                var strategy = await StrategySerializer.Read(reader);

                if (strategy == null)
                {
                    ShowMessage(Messages.OpenStrategyFailure, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ShowDetailDialogAndAdd(strategy);
            }
        }

        #endregion
    }
}
