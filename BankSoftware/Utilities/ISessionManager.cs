using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftware.Utilities
{
    public interface ISessionManager
    {
        User CurrentUser { get; set; }

        User LoggedInUser { get; set; }
        string UserSecret { get; set; }
    }
}
