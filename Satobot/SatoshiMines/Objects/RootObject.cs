using System.Runtime.Serialization;

namespace Satobot.SatoshiMines.Objects
{
    [DataContract]
    public abstract class RootObject
    {
        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }
    }
}
