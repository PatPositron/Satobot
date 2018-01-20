using Satobot.Misc;
using Satobot.Objects;
using Satobot.SatoshiMines;
using Satobot.SatoshiMines.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Satobot.Controls
{
    public partial class StrategyPanel : UserControl
    {
        public bool Running { get; set; }

        private readonly Strategy Strategy;
        private readonly SatoshiMinesSession Session;

        private bool Remove;
        private double CurrentBalance, ProfitStat;
        private int WinsStat, LossesStat;

        public StrategyPanel(Strategy strategy)
        {
            Strategy = strategy;

            Session = new SatoshiMinesSession(Strategy.PlayerHash);

            InitializeComponent();
            UpdateLabels();
        }

        public void RequestStart()
        {
            if (!Running)
                StartStrategy();
        }

        public void RequestStop()
        {
            if (Running)
                StopStrategy(false);
        }

        public void RequestRemove()
        {
            removeButton.PerformClick();
        }

        private void AddLog(Color color, string format, params object[] args)
        {
            const int maxLines = 1000;

            logBox.SelectionColor = color;
            logBox.AppendText(string.Concat(string.Format(format, args), Environment.NewLine));

            if (logBox.Lines.Length > maxLines)
            {
                logBox.SelectionStart = 0;
                logBox.SelectionLength = logBox.GetFirstCharIndexFromLine(logBox.Lines.Length - maxLines);
                logBox.SelectedText = string.Concat(new string('.', 20), Environment.NewLine);
            }

            logBox.SelectionStart = logBox.Text.Length;
            logBox.ScrollToCaret();
        }

        private void UpdateLabels()
        {
            balanceLabel.Text = string.Concat(CurrentBalance, " bits");
            playerHashLabel.Text = Session.PlayerHash;

            if (WinsStat + LossesStat > 0)
            {
                totalsLabel.Text = string.Format("{0}% wins ({1}W / {2}L / {3}T) | {4} Profit",
                    WinsStat * 100 / (WinsStat + LossesStat), WinsStat, LossesStat, WinsStat + LossesStat,
                    SatoshiMinesSession.BtcToBits(ProfitStat));
            }
        }

        private void StartStrategy()
        {
            toggleButton.Text = "Stop";

            Running = true;
            WinsStat = LossesStat = 0;
            UpdateLabels();

            AddLog(Color.Black, "Strategy started");

            try
            {
                LoopStrategy(Strategy);
            }
            catch (Exception ex)
            {
                AddLog(Color.Crimson, "An error has occurred:");
                AddLog(Color.Crimson, "{0}", ex.GetType());
                AddLog(Color.Crimson, ex.Message);

                StopStrategy(true);
            }
        }

        private void StopStrategy(bool stopped)
        {
            if (stopped)
            {
                // actually stopped
                removeButton.Enabled = true;
                toggleButton.Text = "Start";
                toggleButton.Enabled = true;

                gameBoard.NewTiles();

                AddLog(Color.Black, "Strategy stopped");

                if (Remove)
                    RemoveFromParent();
            }
            else
            {
                removeButton.Enabled = false;
                toggleButton.Text = "Stopping";
                toggleButton.Enabled = false;

                AddLog(Color.Black, "Stopping strategy...");
            }

            Running = false;
        }

        private void RemoveFromParent()
        {
            Session.Dispose();
            Parent.Controls.Remove(this);
        }

        private async Task AuthenticateSession()
        {
            if (Session.Authenticated)
                return;

            AddLog(Color.Black, "Authenticating player {0}", Strategy.PlayerHash);

            Session.Authenticated = await Session.Login(Strategy.Password);

            if (Session.Authenticated)
            {
                AddLog(Color.DarkGreen, "Session authenticated {0}", string.IsNullOrWhiteSpace(Strategy.Password) ? string.Empty : "with password");
            }
            else
            {
                AddLog(Color.DarkRed, "Could not authenticate player hash (incorrect password?)");
                StopStrategy(true);
            }
        }

        private async void LoopStrategy(Strategy strategy)
        {
            await AuthenticateSession();

            while (Session.Authenticated && Running)
            {
                var outcome = new GameOutcome(true, true);
                var balanceLimitMet = false;

                foreach (var game in strategy.Games)
                {
                    if (Running && !balanceLimitMet && outcome.Continue && !outcome.Error)
                    {
                        outcome = await PerformGame(outcome, game);

                        balanceLimitMet = CurrentBalance < strategy.LowerBalanceLimit ||
                                          CurrentBalance > strategy.UpperBalanceLimit;
                    }
                    else
                    {
                        break;
                    }
                }

                if (!Running || outcome.Error || !balanceLimitMet)
                    continue;

                AddLog(Color.DarkRed, "Balance limit met");

                if (strategy.BalanceLimitMetInstruction == LimitMetInstruction.Ignore)
                {
                    AddLog(Color.DarkRed, "Ignoring balance limit...");
                    continue;
                }

                if (strategy.BalanceLimitMetInstruction == LimitMetInstruction.Withdraw)
                {
                    if (CurrentBalance < 1)
                    {
                        AddLog(Color.Fuchsia, "Cannot withdraw 0 bits");
                        break;
                    }

                    AddLog(Color.Fuchsia, "Withdrawing {0} bits to {1}...", CurrentBalance, strategy.WithdrawalAddress);

                    var cashout = await Session.FullCashout(strategy.WithdrawalAddress, (int) CurrentBalance);
                    var success = Session.Successful(cashout);

                    AddLog(success ? Color.DarkGreen : Color.DarkRed, HtmlStripper.StripTags(cashout.Message));
                    AddLog(Color.DarkGoldenrod, "You have {0} bits left", cashout.BalanceBits);
                }

                StopStrategy(true);
                break;
            }

            StopStrategy(true);
        }

        private async Task<GameOutcome> PerformGame(GameOutcome previousOutcome, Game game)
        {
            if (game.PreviousOutcome == PreviousOutcome.EnsureSuccess && !previousOutcome.Alive)
                return new GameOutcome(false, false);

            if (game.PreviousOutcome == PreviousOutcome.EnsureFailure && previousOutcome.Alive)
                return new GameOutcome(false, false);

            if (game.Delay > 0)
                AddLog(Color.DarkBlue, "Waiting {0}ms before next game...", game.Delay);

            await Task.Delay(game.Delay);

            var gameObject = await Session.NewGame((int) game.Bet, game.Mines);

            if (!Session.Successful(gameObject))
            {
                AddLog(Color.DarkRed, gameObject.Message);
                return new GameOutcome(false, false, true);
            }

            UpdateLabels();

            AddLog(Color.Black, string.Empty);
            AddLog(Color.DarkMagenta, "Game {0}", gameObject.GameHash);

            var tile = new TileObject();
            var guessedTiles = new List<int>();
            var firstStep = true;
            var alive = true;
            var guessCount = 0;
            var stake = 0D;
            var loseStake = SatoshiMinesSession.BitsToBtc((int) game.Bet);

            foreach (var step in game.Steps)
            {
                if (!firstStep && (!tile.Outcome.Equals("bitcoins") || guessCount + (int) game.Mines >= 25))
                    break;

                if (step.Delay > 0)
                    AddLog(Color.DarkBlue, "Waiting {0}ms before next step...", step.Delay);

                await Task.Delay(step.Delay);

                firstStep = false;
                tile = await GuessTile(gameObject, step.Tile == Tile.Previous ? previousOutcome.LastTile : step.Tile, guessedTiles);
                guessCount++;

                if (tile.Outcome.Equals("bitcoins"))
                {
                    loseStake = tile.Stake;
                    stake += tile.Change;

                    AddLog(Color.Green, "({0}) Found {1} bits in tile {2}", guessCount, SatoshiMinesSession.BtcToBits(tile.Change), tile.Guess);

                    gameBoard.SetTile(tile.Guess, DrawTileStyle.Clicked);
                }
                else
                {
                    alive = false;
                    var bombs = tile.Bombs.Split('-');

                    foreach (var bomb in bombs.Select(int.Parse))
                        gameBoard.SetTile(bomb, bomb == tile.Guess ? DrawTileStyle.ClickedBomb : DrawTileStyle.Bomb);
                }
            }

            var fairGame = true;

            if (alive)
            {
                WinsStat++;
                ProfitStat += stake;

                var cashout = await Session.CashoutGame(gameObject);

                AddLog(Color.DarkCyan, cashout.Message);

                if (Session.Successful(cashout))
                    AddLog(Color.DarkGreen, Session.GetGameUrl(cashout));
            }
            else
            {
                LossesStat++;
                ProfitStat -= loseStake;

                AddLog(Color.Red, tile.Message);
                AddLog(Color.DarkGreen, Session.GetGameUrl(tile));

                fairGame = Session.CheckFairGame(tile.Bombs, tile.RandomString, gameObject.Secret);

                AddLog(fairGame ? Color.Tomato : Color.DarkRed, "You lost to a{0} {1} game{2}",
                    fairGame ? string.Empty : "n", fairGame ? "fair" : "unfair", fairGame ? string.Empty : "!");
            }

            var balance = await Session.GetBalance();

            CurrentBalance = balance.BalanceBits;

            UpdateLabels();

            AddLog(Color.DarkGoldenrod, "You have {0} bits", balance.BalanceBits);

            gameBoard.NewTiles();

            return new GameOutcome(fairGame, alive, !fairGame, Step.GetTileFromInt(tile.Guess));
        }

        private async Task<TileObject> GuessTile(GameObject game, Tile tile, ICollection<int> guessedTiles)
        {
            int selectedTile;

            if (tile == Tile.Random)
            {
                do
                {
                    selectedTile = Program.Random.Next(1, 26);
                }
                while (guessedTiles.Contains(selectedTile));
            }
            else
            {
                selectedTile = (int) tile - 10;
            }

            guessedTiles.Add(selectedTile);

            return await Session.GuessTile(game, selectedTile);
        }

        #region Event Handlers

        private void ToggleButton_Click(object sender, EventArgs e)
        {
            if (Running)
                StopStrategy(false);
            else
                StartStrategy();
        }

        private void LogBox_Enter(object sender, EventArgs e)
        {
            if (Running)
                toggleButton.Focus();
        }

        private void LogBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            if (!Running)
                Process.Start(e.LinkText);
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            removeButton.Text = "Removing";
            removeButton.Enabled = false;

            if (Running)
            {
                Remove = true;
                StopStrategy(false);
            }
            else
            {
                RemoveFromParent();
            }
        }

        #endregion
    }
}
