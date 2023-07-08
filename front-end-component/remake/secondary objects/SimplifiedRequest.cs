using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathApp.secondary_objects
{
    public class SimplifiedRequest
    {

        public string RequestType { get; set; }
        public string RequestBody { get; set; }

        public SimplifiedRequest(string type, string body)
        {
            RequestType = type;
            RequestBody = body;

        }
    }

}
