namespace Satobot.Objects
{
    internal class GameOutcome
    {
        public bool Continue { get; set; }
        public bool Alive { get; set; }
        public bool Error { get; set; }
        public Tile LastTile { get; set; }

        public GameOutcome(bool continueLoop) : this(continueLoop, false) { }

        public GameOutcome(bool continueLoop, bool alive) : this(continueLoop, alive, false) { }

        public GameOutcome(bool continueLoop, bool alive, bool error) : this(continueLoop, alive, error, Tile.Random) { }

        public GameOutcome(bool continueLoop, bool alive, bool error, Tile lastTile)
        {
            Continue = continueLoop;
            Alive = alive;
            Error = error;
            LastTile = lastTile;
        }
    }
}
