using BusinessLayer.Interface;
using CommonLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository repository;
        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }
        public UserRegisterModel Register(UserRegisterModel user)
        {
            try
            {
                return repository.Register(user);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public LoginResponseModel Login(LoginModel user)
        {
            try
            {
                return repository.Login(user);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public string ForgotPassword(string email)
        {
            try
            {
                return repository.ForgotPassword(email);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public string ResetPassword(ResetPassModel user)
        {
            try
            {
                return repository.ResetPassword(user);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
