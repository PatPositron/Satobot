using System.Runtime.Serialization;

namespace Satobot.SatoshiMines.Objects
{
    [DataContract]
    public class CashoutGameObject : RootObject
    {
        [DataMember(Name = "win")]
        public double Win { get; set; }

        [DataMember(Name = "mines")]
        public string Mines { get; set; }

        [DataMember(Name = "random_string")]
        public string RandomString { get; set; }

        [DataMember(Name = "game_id")]
        public string GameId { get; set; }
    }
}
