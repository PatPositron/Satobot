using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Satobot.Misc;
using Satobot.Objects;

namespace Satobot.Forms
{
    public partial class StrategyBuilderForm : Form
    {
        public Strategy SelectedStrategy { get; set; }
        public int StrategyAmount { get; set; }

        public StrategyBuilderForm(Strategy strategy)
        {
            InitializeComponent();

            Icon = IconExtractor.GetIconFromApplication();
            StrategyAmount = 1;

            mainGrid.SelectedObject = strategy;
        }

        public static Tuple<int, Strategy> ShowDetailDialog(Strategy details)
        {
            using (var form = new StrategyBuilderForm(details ?? new Strategy()))
            {
                var result = form.ShowDialog();

                return new Tuple<int, Strategy>(form.StrategyAmount, result != DialogResult.OK ? null : form.SelectedStrategy);
            }
        }

        private bool VerifyStrategy()
        {
            var strategy = (Strategy) mainGrid.SelectedObject;

            if (string.IsNullOrWhiteSpace(strategy.PlayerHash))
                return false;

            if (strategy.BalanceLimitMetInstruction == LimitMetInstruction.Withdraw && string.IsNullOrWhiteSpace(strategy.WithdrawalAddress))
                return false;

            if (strategy.Games == null || strategy.Games.Length == 0)
                return false;

            return strategy.Games.All(game => game.Steps != null && game.Steps.Length != 0);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            var result = Program.MainForm.ShowMessage(Messages.CancelConfirmation, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            if (result == DialogResult.No)
                return;

            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (!VerifyStrategy())
            {
                Program.MainForm.ShowMessage(Messages.CheckStrategy, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.AddExtension = true;
                    saveDialog.CheckPathExists = true;
                    saveDialog.ValidateNames = true;
                    saveDialog.InitialDirectory = Path.Combine(Application.StartupPath, Program.StrategyFolder);
                    saveDialog.Filter = Program.StrategyFileFilter;
                    saveDialog.Title = "Save strategy file as...";

                    var result = saveDialog.ShowDialog();

                    if (result != DialogResult.OK)
                        return;

                    // save here
                    using (var file = new StreamWriter(saveDialog.OpenFile()))
                        StrategySerializer.Save(file, (Strategy) mainGrid.SelectedObject);
                }

                Program.MainForm.ShowMessage(Messages.SaveStrategySuccess, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                Program.MainForm.ShowMessage(Messages.SaveStrategyFailure, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            if (!VerifyStrategy())
            {
                Program.MainForm.ShowMessage(Messages.CheckStrategy, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SelectedStrategy = (Strategy) mainGrid.SelectedObject;
            StrategyAmount = (int) amountNumeric.Value;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
