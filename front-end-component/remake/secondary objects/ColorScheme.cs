using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathApp.secondary_objects
{
    //object that holds the hex values of the colors needed in the ui
    public class ColorScheme
    {
        public String form_bg;
        public String form_text;
        public String form_panel_bg;
        public String form_menu_bg;
        public String form_ribbon;

        //just setting the hex codes, nothing else needed
        public ColorScheme(String form_bg,String form_text,String form_panel_bg, String form_menu_bg, String form_ribbon) 
        {
            this.form_bg = form_bg;
            this.form_text = form_text;
            this.form_panel_bg = form_panel_bg;
            this.form_menu_bg= form_menu_bg;
            this.form_ribbon= form_ribbon;
        }
    }
}
