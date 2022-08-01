using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AdminController : Controller
    {
        private readonly IAdminManager manager;

        public AdminController(IAdminManager manager)
        {
            this.manager = manager;
        }

        [HttpPost("adminlogin")]
        public IActionResult AdminLogin(AdminLoginModel admin)
        {
            try
            {
                var result = manager.AdminLogin(admin);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Login successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Login Faild" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
