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
    //[Authorize(Roles =Role.User)]
    [ApiController]
    [Route("[Controller]")]
    public class CartController : Controller
    {
        private readonly ICartManager manager;

        public CartController(ICartManager manager)
        {
            this.manager = manager;
        }

        [HttpPost("addtocart")]
        public IActionResult AddToCart(AddToCartModel cart)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = manager.AddToCart(cart,userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Added to Cart", Data = result });
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

        [HttpPost("updatecart")]
        public IActionResult UpdateCart(int cartId, int bookQty)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = manager.UpdateCart(cartId, bookQty);
                if (result == "Quantity updated")
                {
                    return this.Ok(new { Status = true, Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Data = result });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("removefromcart")]
        public IActionResult RemoveFromCart(int cartId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = manager.RemoveFromCart(cartId);
                if (result == true)
                {
                    return this.Ok(new { Status = true, Message="Removed from Cart"});
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

        [HttpPost("getcartitem")]
        public IActionResult GetCartItem()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = manager.GetCartItem(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Data=result });
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
