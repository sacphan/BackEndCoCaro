

using CoCaro.Data.Models;
using System.Collections.Generic;

namespace CoCaro.Service.WorkContext
{
    public interface IWorkContext
    {
        User CurrentUser { get; set; }

        User GetUserCookie();
        void SetUserCookie();

        User UserCookie { get; set; }

    }
}