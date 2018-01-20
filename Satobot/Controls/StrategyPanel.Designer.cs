namespace Satobot.Controls
{
    partial class StrategyPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toggleButton = new System.Windows.Forms.Button();
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.logGroupBox = new System.Windows.Forms.GroupBox();
            this.removeButton = new System.Windows.Forms.Button();
            this.infoLabel1 = new System.Windows.Forms.Label();
            this.playerHashLabel = new System.Windows.Forms.Label();
            this.infoLabel2 = new System.Windows.Forms.Label();
            this.totalsLabel = new System.Windows.Forms.Label();
            this.infoLabel3 = new System.Windows.Forms.Label();
            this.balanceLabel = new System.Windows.Forms.Label();
            this.gameBoard = new Satobot.Controls.GameBoard();
            this.logGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // toggleButton
            // 
            this.toggleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.toggleButton.Location = new System.Drawing.Point(209, 55);
            this.toggleButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.toggleButton.Name = "toggleButton";
            this.toggleButton.Size = new System.Drawing.Size(75, 25);
            this.toggleButton.TabIndex = 2;
            this.toggleButton.Text = "Start";
            this.toggleButton.UseVisualStyleBackColor = true;
            this.toggleButton.Click += new System.EventHandler(this.ToggleButton_Click);
            // 
            // logBox
            // 
            this.logBox.BackColor = System.Drawing.SystemColors.Control;
            this.logBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.logBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logBox.Location = new System.Drawing.Point(2, 16);
            this.logBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.Size = new System.Drawing.Size(363, 116);
            this.logBox.TabIndex = 3;
            this.logBox.Text = "";
            this.logBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.LogBox_LinkClicked);
            this.logBox.Enter += new System.EventHandler(this.LogBox_Enter);
            // 
            // logGroupBox
            // 
            this.logGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.logGroupBox.Controls.Add(this.logBox);
            this.logGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.logGroupBox.Location = new System.Drawing.Point(0, 79);
            this.logGroupBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.logGroupBox.Name = "logGroupBox";
            this.logGroupBox.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.logGroupBox.Size = new System.Drawing.Size(367, 134);
            this.logGroupBox.TabIndex = 4;
            this.logGroupBox.TabStop = false;
            this.logGroupBox.Text = "Action Log";
            // 
            // removeButton
            // 
            this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeButton.Location = new System.Drawing.Point(288, 55);
            this.removeButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 25);
            this.removeButton.TabIndex = 7;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // infoLabel1
            // 
            this.infoLabel1.AutoSize = true;
            this.infoLabel1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoLabel1.Location = new System.Drawing.Point(79, 2);
            this.infoLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.infoLabel1.Name = "infoLabel1";
            this.infoLabel1.Size = new System.Drawing.Size(85, 13);
            this.infoLabel1.TabIndex = 8;
            this.infoLabel1.Text = "Player Hash";
            // 
            // playerHashLabel
            // 
            this.playerHashLabel.AutoSize = true;
            this.playerHashLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerHashLabel.Location = new System.Drawing.Point(79, 13);
            this.playerHashLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.playerHashLabel.Name = "playerHashLabel";
            this.playerHashLabel.Size = new System.Drawing.Size(82, 13);
            this.playerHashLabel.TabIndex = 9;
            this.playerHashLabel.Text = "---------------";
            // 
            // infoLabel2
            // 
            this.infoLabel2.AutoSize = true;
            this.infoLabel2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoLabel2.Location = new System.Drawing.Point(79, 26);
            this.infoLabel2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.infoLabel2.Name = "infoLabel2";
            this.infoLabel2.Size = new System.Drawing.Size(40, 13);
            this.infoLabel2.TabIndex = 10;
            this.infoLabel2.Text = "Stats";
            // 
            // totalsLabel
            // 
            this.totalsLabel.AutoSize = true;
            this.totalsLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalsLabel.Location = new System.Drawing.Point(79, 37);
            this.totalsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.totalsLabel.Name = "totalsLabel";
            this.totalsLabel.Size = new System.Drawing.Size(65, 13);
            this.totalsLabel.TabIndex = 11;
            this.totalsLabel.Text = "Click start";
            // 
            // infoLabel3
            // 
            this.infoLabel3.AutoSize = true;
            this.infoLabel3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoLabel3.Location = new System.Drawing.Point(79, 50);
            this.infoLabel3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.infoLabel3.Name = "infoLabel3";
            this.infoLabel3.Size = new System.Drawing.Size(58, 13);
            this.infoLabel3.TabIndex = 12;
            this.infoLabel3.Text = "Balance";
            // 
            // balanceLabel
            // 
            this.balanceLabel.AutoSize = true;
            this.balanceLabel.Location = new System.Drawing.Point(79, 61);
            this.balanceLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.balanceLabel.Name = "balanceLabel";
            this.balanceLabel.Size = new System.Drawing.Size(65, 13);
            this.balanceLabel.TabIndex = 13;
            this.balanceLabel.Text = "Click start";
            // 
            // gameBoard
            // 
            this.gameBoard.Location = new System.Drawing.Point(2, 2);
            this.gameBoard.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gameBoard.Name = "gameBoard";
            this.gameBoard.Size = new System.Drawing.Size(73, 73);
            this.gameBoard.TabIndex = 0;
            this.gameBoard.Text = "gameBoard1";
            // 
            // StrategyPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.toggleButton);
            this.Controls.Add(this.infoLabel3);
            this.Controls.Add(this.balanceLabel);
            this.Controls.Add(this.infoLabel2);
            this.Controls.Add(this.totalsLabel);
            this.Controls.Add(this.infoLabel1);
            this.Controls.Add(this.logGroupBox);
            this.Controls.Add(this.gameBoard);
            this.Controls.Add(this.playerHashLabel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(367, 214);
            this.Name = "StrategyPanel";
            this.Size = new System.Drawing.Size(367, 213);
            this.logGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GameBoard gameBoard;
        private System.Windows.Forms.Button toggleButton;
        private System.Windows.Forms.RichTextBox logBox;
        private System.Windows.Forms.GroupBox logGroupBox;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Label infoLabel1;
        private System.Windows.Forms.Label playerHashLabel;
        private System.Windows.Forms.Label infoLabel2;
        private System.Windows.Forms.Label totalsLabel;
        private System.Windows.Forms.Label infoLabel3;
        private System.Windows.Forms.Label balanceLabel;
    }
}
