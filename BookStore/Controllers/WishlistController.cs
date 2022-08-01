using BusinessLayer.Interface;
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
    public class WishlistController : Controller
    {
        private readonly IWishlistManager manager;

        public WishlistController(IWishlistManager manager)
        {
            this.manager = manager;
        }
        [HttpPost("addtowishlist")]
        public IActionResult AddToWishlist(int bookId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = manager.AddToWishlist(bookId, userId);
                if (result != null)
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
        [HttpDelete("removefromwishlist")]
        public IActionResult RemoveFromWishlist(int wishlistId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = manager.RemoveFromWishlist(wishlistId);
                if (result == true)
                {
                    return this.Ok(new { Status = true, Message = "Removed from wishlist" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("getwishlistitem")]
        public IActionResult GetWishlistItem()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = manager.GetWishlistItem(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
