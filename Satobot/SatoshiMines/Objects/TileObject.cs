using System.Runtime.Serialization;

namespace Satobot.SatoshiMines.Objects
{
    [DataContract]
    public class TileObject : RootObject
    {
        [DataMember(Name = "guess")]
        public int Guess { get; set; }

        [DataMember(Name = "outcome")]
        public string Outcome { get; set; }

        [DataMember(Name = "stake")]
        public double Stake { get; set; }

        [DataMember(Name = "next")]
        public double Next { get; set; }

        [DataMember(Name = "change")]
        public double Change { get; set; }

        [DataMember(Name = "bombs")]
        public string Bombs { get; set; }

        [DataMember(Name = "random_string")]
        public string RandomString { get; set; }

        [DataMember(Name = "game_id")]
        public string GameId { get; set; }
    }
}
