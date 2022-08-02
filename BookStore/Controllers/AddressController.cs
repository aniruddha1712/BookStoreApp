using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Authorize(Roles = Role.User)]
    [ApiController]
    [Route("[Controller]")]
    public class AddressController : Controller
    {
        private readonly IAddressManager manager;

        public AddressController(IAddressManager manager)
        {
            this.manager = manager;
        }
        [HttpPost("addAddress")]
        public IActionResult AddAddress(AddressModel address)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = manager.AddAddress(address, userId);
                if (result == "Address added")
                {
                    return this.Ok(new { Status = true, Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Faild to add" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPut("updateAddress")]
        public IActionResult UpdateAddress(AddressModel address)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = manager.UpdateAddress(address, userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Faild to update" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpDelete("deleteAddress")]
        public IActionResult DeleteAddress(int addressId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = manager.DeleteAddress(addressId, userId);
                if (result == true)
                {
                    return this.Ok(new { Status = true, Message = "Address deleted" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Faild to delete" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("getaddressbyId")]
        public IActionResult GetAddressById(int addressId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = manager.GetAddressById(addressId, userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Getting Address",Data=result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Faild to get Address" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("getalladdress")]
        public IActionResult GetAllAddress()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = manager.GetAllAddress(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Getting Address", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Faild to get Address" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
