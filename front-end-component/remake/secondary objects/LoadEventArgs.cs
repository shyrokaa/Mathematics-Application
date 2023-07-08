using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathApp.secondary_objects
{
    public class LoadEventArgs
    {
        public SimplifiedRequest SimplifiedRequest { get; }

        public LoadEventArgs(SimplifiedRequest req)
        {
            SimplifiedRequest = req;
        }
    }
}
