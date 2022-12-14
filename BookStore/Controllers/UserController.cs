using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : Controller
    {
        private readonly IUserManager manager;

        public UserController(IUserManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(UserRegisterModel user)
        {
            try
            {
                var result = manager.Register(user);
                if(result != null)
                {
                    return this.Ok(new ResponseModel<UserRegisterModel> { Status = true, Message = "User Registration successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Faild to Register" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginModel user)
        {
            try
            {
                var result = manager.Login(user);
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
        [HttpPost]
        [Route("forgotpassword/{email}")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                var result = manager.ForgotPassword(email);
                if (result.Equals("We will send you an email for resetting password"))
                {
                    return this.Ok(new { Status = true, Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Incorrect email or password" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize]
        [HttpPut]
        [Route("resetpassword")]
        public IActionResult ResetPassword(ResetPassModel user)
        {
            try
            {
                var emailId = User.FindFirst(ClaimTypes.Email).Value;
                var result = manager.ResetPassword(user,emailId);
                if (result.Equals("Password Updated"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        [Route("getuserbyid")]
        [Authorize(Roles = Role.User)]
        public IActionResult GetUserById()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = manager.GetUserById(userId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<UserRegisterModel> { Status = true, Message = "User details", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "User does not exist" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
