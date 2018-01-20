using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Satobot.Forms;

namespace Satobot
{
    internal static class Program
    {
        public const string StrategyFileFilter = "Satobot Strategy Files (*.sbg)|*.sbg";
        public const string StrategyFileExtension = "*.sbg";
        public const string StrategyFolder = "Strategies";

        public static MainForm MainForm { get; private set; }
        public static Random Random { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            InitializeDirectories();

            Random = new Random();
            MainForm = new MainForm();

            Application.Run(MainForm);
        }

        private static void InitializeDirectories()
        {
            if (!Directory.Exists(StrategyFolder))
                Directory.CreateDirectory(StrategyFolder);
        }

        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
