using System.Runtime.Serialization;

namespace Satobot.SatoshiMines.Objects
{
    [DataContract]
    public class GameObject : RootObject
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "game_hash")]
        public string GameHash { get; set; }

        [DataMember(Name = "secret")]
        public string Secret { get; set; }

        [DataMember(Name = "bet")]
        public double Bet { get; set; }

        [DataMember(Name = "stake")]
        public double Stake { get; set; }

        [DataMember(Name = "next")]
        public double Next { get; set; }

        [DataMember(Name = "betNumber")]
        public string BetNumber { get; set; }

        [DataMember(Name = "gametype")]
        public string Gametype { get; set; }

        [DataMember(Name = "num_mines")]
        public int NumMines { get; set; }
    }
}
