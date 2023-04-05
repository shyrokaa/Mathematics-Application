using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace MathApp.secondary_objects
{
    internal class JsonParser
    {
        private readonly string configPath;

        public JsonParser()
        {
            var solutionDirectory = TryGetSolutionDirectoryInfo().Parent;
            this.configPath = Path.Combine(solutionDirectory.FullName, "front-end-component/config.json");
        }

        public Setup ParseData()
        {
            var roughJson = File.ReadAllText(this.configPath);

            dynamic jsonData = JObject.Parse(roughJson);
            bool networkingDisabled = jsonData.Program.Config.networking_disabled;
            string selectedColor = jsonData.Program.Config.selected_color;

            return new Setup(networkingDisabled, selectedColor);
        }

        public void UpdateJson(bool networkingDisabled, string selectedColor)
        {
            var json = File.ReadAllText(this.configPath);
            dynamic jsonObj = JObject.Parse(json);
            jsonObj.Program.Config.networking_disabled = networkingDisabled;
            jsonObj.Program.Config.selected_color = selectedColor;

            var output = JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(this.configPath, output);
        }

        private static DirectoryInfo TryGetSolutionDirectoryInfo(string currentPath = null)
        {
            var directory = new DirectoryInfo(currentPath ?? Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }
            return directory;
        }
    }

}
