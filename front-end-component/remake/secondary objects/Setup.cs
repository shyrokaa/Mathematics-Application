using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathApp.secondary_objects
{
    //object that keeps the data parsed from the xml
    public class Setup
    {
        public bool networkDisabled;
        public String selectedTheme;

        public ColorScheme darkTheme;
        public ColorScheme lightTheme;

        public Setup(bool networkDisabled, String selectedTheme)
        {
            this.networkDisabled = networkDisabled;
            this.selectedTheme= selectedTheme;
            this.darkTheme = new ColorScheme("#0D0C1A", "#CD1748", "#1C172B", "#41314C", "#7A5778");
            this.lightTheme = new ColorScheme("#7DAA92", "#3D2B3D", "#466365", "#AE847E", "#F7F7FF");

        }
        
    }
}
