using System.Drawing;

namespace Satobot.Objects
{
    internal class LogEntry
    {
        public Color Color { get; }
        public string Message { get; }

        public LogEntry(Color color, string message)
        {
            Color = color;
            Message = message;
        }
    }
}
