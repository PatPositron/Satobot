using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using Satobot.SatoshiMines.Objects;

namespace Satobot.Objects
{
    public enum Tile : byte
    {
        Random,
        Previous,
        One = 11,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Eleven,
        Twelve,
        Thirteen,
        Fourteen,
        Fifteen,
        Sixteen,
        Seventeen,
        Eighteen,
        Nineteen,
        Twenty,
        TwentyOne,
        TwentyTwo,
        TwentyThree,
        TwentyFour,
        TwentyFive
    }

    public enum PreviousOutcome : byte
    {
        Ignore,
        EnsureSuccess,
        EnsureFailure
    }

    public enum LimitMetInstruction : byte
    {
        Stop,
        Withdraw,
        Ignore
    }

    public class Strategy : Delayable
    {
        private string PlayerHashBacker;
        private double LowerBalanceLimitBacker;
        private double UpperBalanceLimitBacker = 999999999999;
        private string WithdrawalAddressBacker;

        [Description("The unqiue hash of the player to use")]
        public string PlayerHash
        {
            get => PlayerHashBacker;
            set
            {
                if (!Regex.IsMatch(value, "^([a-f0-9]{40})$"))
                    throw new ArgumentOutOfRangeException("The player hash must be a 40 character long hex string", new Exception());

                PlayerHashBacker = value;
            }
        }

        [Description("The password of the player, if you have one")]
        public string Password { get; set; }

        [Description("The bitcoin address to withdraw account funds to")]
        [DataMember(EmitDefaultValue = false)]
        public string WithdrawalAddress
        {
            get => WithdrawalAddressBacker;
            set
            {
                if (value != null && !Regex.IsMatch(value, "^[13][a-km-zA-HJ-NP-Z1-9]{25,34}$"))
                    throw new ArgumentOutOfRangeException("The withdrawal address must be a valid bitcoin address", new Exception());

                WithdrawalAddressBacker = value;
            }
        }

        [Description("The lower limit where the 'balance limit met' option will be run")]
        public double LowerBalanceLimit
        {
            get => LowerBalanceLimitBacker;
            set
            {
                if (value < 0 || value > 999999999999)
                    throw new ArgumentOutOfRangeException("The limit must be inbetween 0 and 999,999,999,999", new Exception());

                LowerBalanceLimitBacker = value;
            }
        }

        [Description("The upper limit where the 'balance limit met' option will be run\n(0 for infinite)")]
        public double UpperBalanceLimit
        {
            get => UpperBalanceLimitBacker;
            set
            {
                if (value < 0 || value > 999999999999)
                    throw new ArgumentOutOfRangeException("The limit must be inbetween 0 and 999,999,999,999", new Exception());

                UpperBalanceLimitBacker = value.Equals(0) ? 999999999999 : value;
            }
        }

        [Description("List of games to be executed")]
        public Game[] Games { get; set; }


        [DisplayName("On Balance Limit Met")]
        [Description("What happens when the balance limit is met")]
        public LimitMetInstruction BalanceLimitMetInstruction { get; set; }
    }

    public class Game : Delayable
    {
        private double BetBacker;

        [Description("The steps to be executed")]
        public Step[] Steps { get; set; }

        [DisplayName("# of Mines")]
        [Description("The number of mines")]
        public MineCount Mines { get; set; }

        [Description("Only execute when the previous outcome matches the specified")]
        public PreviousOutcome PreviousOutcome { get; set; }

        [Description("The amount to bet")]
        public double Bet
        {
            get => BetBacker;
            set
            {
                if (Mines == MineCount.TwentyFour && value > 100000)
                    throw new ArgumentOutOfRangeException("The bet must be 0 or inbetween 30 and 100,000 with 24 mines", new Exception());

                if (value < 30 || value > 1000000 && Mines != MineCount.TwentyFour)
                    if (!value.Equals(0))
                        throw new ArgumentOutOfRangeException("The bet must be 0 or inbetween 30 and 1,000,000", new Exception());
                    
                BetBacker = value;
            }
        }

        public override string ToString()
        {
            return "Game";
        }
    }

    public class Step : Delayable
    {
        [Description("The tile to click")]
        public Tile Tile { get; set; }

        public static int GetIntFromTile(Tile tile)
        {
            return ((int) tile) - 10;
        }

        public static Tile GetTileFromInt(int tile)
        {
            if (tile == 0)
                return Tile.Random;

            return (Tile) (tile + 10);
        }

        public override string ToString()
        {
            return "Step";
        }
    }

    public abstract class Delayable
    {
        private int DelayBacker;

        [DisplayName("Delay (ms)")]
        [Description("The delay in ms")]
        public int Delay
        {
            get => DelayBacker;
            set
            {
                if (value < 0 || value > 10000)
                    throw new ArgumentOutOfRangeException("The delay must be inbetween 0 and 10,000 ms", new Exception());

                DelayBacker = value;
            }
        }
    }
}
