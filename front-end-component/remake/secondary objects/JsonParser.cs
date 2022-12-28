using Newtonsoft.Json;
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
        private String configPath;

        public JsonParser() {
            this.configPath = Path.Combine(TryGetSolutionDirectoryInfo().Parent.FullName, "front-end-component/config.json");
        }

        public Setup parseData()
        {
            string roughJson = System.IO.File.ReadAllText(this.configPath);

            //add this package to the documentation -> Json.NET
            dynamic jsonData = JsonConvert.DeserializeObject(roughJson);
            bool networking_disabled = jsonData.Program.Config.networking_disabled;
            string selected_color = jsonData.Program.Config.selected_color;

            Setup resultingSetup = new Setup(networking_disabled,selected_color);

            return resultingSetup;
        }

        private static DirectoryInfo TryGetSolutionDirectoryInfo(string currentPath = null)
        {
            var directory = new DirectoryInfo(
                currentPath ?? Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }
            return directory;
        }
        
        public void updateJson(bool networking_disabled, string selected_color)
        {
            //update json data based on current settings from user
            string json = File.ReadAllText(this.configPath);
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            jsonObj["Program"]["Config"]["networking_disabled"] = networking_disabled;
            jsonObj["Program"]["Config"]["selected_color"] = selected_color;
            //another library to add to the documentation
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(configPath, output);

        }


    }
}
