using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserManager
    {
        UserRegisterModel Register(UserRegisterModel user);
        LoginResponseModel Login(LoginModel user);
        string ForgotPassword(string emailId);
        string ResetPassword(ResetPassModel user);

    }
}
