using System.Runtime.Serialization;

namespace Satobot.SatoshiMines.Objects
{
    [DataContract]
    public class BalanceObject : RootObject
    {
        [DataMember(Name = "balance")]
        public string Balance { get; set; }

        public double BalanceBits => double.TryParse(Balance, out var value) ? SatoshiMinesSession.BtcToBits(value) : 0;
    }
}
