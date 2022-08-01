using BusinessLayer.Interface;
using CommonLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AdminManager : IAdminManager
    {
        private readonly IAdminRepository repository;
        public AdminManager(IAdminRepository repository)
        {
            this.repository = repository;
        }
        public AdminModel AdminLogin(AdminLoginModel user)
        {
            try
            {
                return repository.AdminLogin(user);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
