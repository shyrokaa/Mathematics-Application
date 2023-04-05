using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathApp.secondary_objects
{
    public class Setup
    {
        public ColorScheme DarkTheme { get; }
        public ColorScheme LightTheme { get; }
        public string SelectedTheme { get; }
        public bool NetworkDisabled { get; }
        public Image DarkLock { get; }
        public Image LightLock { get; }
        public Image DarkUser { get; }
        public Image LightUser { get; }

        public Setup(bool networkDisabled, string selectedTheme)
        {
            NetworkDisabled = networkDisabled;
            SelectedTheme = selectedTheme;

            string filePath = Path.Combine(TryGetSolutionDirectoryInfo()?.Parent?.FullName, "front-end-component/remake/resources");

            DarkLock = Image.FromFile(Path.Combine(filePath, "lock_icon_dark.png"));
            LightLock = Image.FromFile(Path.Combine(filePath, "lock_icon_light.png"));
            DarkUser = Image.FromFile(Path.Combine(filePath, "user_icon_dark.png"));
            LightUser = Image.FromFile(Path.Combine(filePath, "user_icon_light.png"));

            DarkTheme = new ColorScheme("#0D0C1A", "#CD1748", "#1C172B", "#41314C", "#7A5778");
            LightTheme = new ColorScheme("#7DAA92", "#3D2B3D", "#466365", "#AE847E", "#F7F7FF");
        }

        private static DirectoryInfo TryGetSolutionDirectoryInfo(string currentPath = null)
        {
            DirectoryInfo directory = new DirectoryInfo(currentPath ?? Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }
            return directory;
        }
    }

}
