using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathApp.secondary_objects
{
    public class UserEventArgs : EventArgs
    {
        public User User { get; }

        public UserEventArgs(User user)
        {
            User = user;
        }
    }

}
