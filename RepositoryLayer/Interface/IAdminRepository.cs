using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IAdminRepository
    {
        AdminModel AdminLogin(AdminLoginModel admin);
    }
}
