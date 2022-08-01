using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IAdminManager
    {
        AdminModel AdminLogin(AdminLoginModel admin);
    }
}
