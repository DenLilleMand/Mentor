using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.Views.Manage.Abstract
{
    interface IAuthProvider
    {
        bool Authenticate(string username, string password);
    }
}
