using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRepository
    {
        UserRegisterModel Register(UserRegisterModel user);
        LoginResponseModel Login(LoginModel user);
        string ForgotPassword(string emailId);
        string ResetPassword(ResetPassModel user);
    }
}
