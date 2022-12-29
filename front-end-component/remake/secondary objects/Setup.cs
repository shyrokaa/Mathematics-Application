using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathApp.secondary_objects
{
    /// <summary>
    /// Class <c>MainWindow</c> defines an object that stores the application configuration
    /// </summary>
    public class Setup
    {
        public ColorScheme darkTheme;
        public ColorScheme lightTheme;
        public String selectedTheme;
        public bool networkDisabled;
        // Image objects for some of the UI elements 
        public Image darkLock;
        public Image lightLock;
        public Image darkUser;
        public Image lightUser;

        /// <summary>
        /// Constructor that initializes the object using the values gathered from the config.json file
        /// </summary>
        public Setup(bool networkDisabled, String selectedTheme)
        {
            this.networkDisabled = networkDisabled;
            this.selectedTheme= selectedTheme;
            this.darkTheme = new ColorScheme("#0D0C1A", "#CD1748", "#1C172B", "#41314C", "#7A5778");
            this.lightTheme = new ColorScheme("#7DAA92", "#3D2B3D", "#466365", "#AE847E", "#F7F7FF");

            string filePath = Path.Combine(TryGetSolutionDirectoryInfo().Parent.FullName, "front-end-component/remake/resources");
            
            this.darkLock = Image.FromFile(filePath + "/lock_icon_dark.png");
            this.lightLock = Image.FromFile(filePath + "/lock_icon_light.png");
            this.darkUser = Image.FromFile(filePath + "/user_icon_dark.png");
            this.lightUser = Image.FromFile(filePath + "/user_icon_light.png");

        }

        /// <summary>
        /// Function that gets the current work directory from the system path
        /// </summary>
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
    }
}
