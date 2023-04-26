using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public Image DarkLogo { get; }
        public Image LightLogo { get; }

        public Setup(bool networkDisabled, string selectedTheme)
        {
            NetworkDisabled = networkDisabled;
            SelectedTheme = selectedTheme;

            string filePath = Path.Combine(TryGetSolutionDirectoryInfo()?.Parent?.FullName, "front-end-component/remake/resources");
            string colorSchemePath = Path.Combine(filePath, "color_scheme.json");
            JObject colorSchemeJson = JObject.Parse(File.ReadAllText(colorSchemePath));

            JObject darkThemeData = colorSchemeJson["themes"]["dark"].ToObject<JObject>();
            DarkTheme = new ColorScheme(
                (string)darkThemeData["form_bg"],
                (string)darkThemeData["form_text"],
                (string)darkThemeData["form_panel_bg"],
                (string)darkThemeData["form_menu_bg"],
                (string)darkThemeData["form_ribbon"]
            );

            JObject lightThemeData = colorSchemeJson["themes"]["light"].ToObject<JObject>();
            LightTheme = new ColorScheme(
                (string)lightThemeData["form_bg"],
                (string)lightThemeData["form_text"],
                (string)lightThemeData["form_panel_bg"],
                (string)lightThemeData["form_menu_bg"],
                (string)lightThemeData["form_ribbon"]
            );

            DarkLock = Image.FromFile(Path.Combine(filePath, "lock_icon_dark.png"));
            LightLock = Image.FromFile(Path.Combine(filePath, "lock_icon_light.png"));
            DarkUser = Image.FromFile(Path.Combine(filePath, "user_icon_dark.png"));
            LightUser = Image.FromFile(Path.Combine(filePath, "user_icon_light.png"));


            DarkLogo = Image.FromFile(Path.Combine(filePath, "dark_logo.png"));
            LightLogo = Image.FromFile(Path.Combine(filePath, "light_logo.png"));

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

    public class ColorSchemeData
    {
        public ColorData DarkTheme { get; set; }
        public ColorData LightTheme { get; set; }
    }

    public class ColorData
    {
        public string form_bg { get; set; }
        public string form_text { get; set; }
        public string form_panel_bg { get; set; }
        public string form_menu_bg { get; set; }
        public string form_ribbon { get; set; }
    }
}
