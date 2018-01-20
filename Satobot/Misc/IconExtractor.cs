using System.Drawing;
using System.Windows.Forms;

namespace Satobot.Misc
{
    public static class IconExtractor
    {
        public static Icon GetIconFromExe(string path)
        {
            return Icon.ExtractAssociatedIcon(path);
        }

        public static Icon GetIconFromApplication()
        {
            return GetIconFromExe(Application.ExecutablePath);
        }
    }
}
