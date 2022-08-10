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
    [ApiController]
    [Route("[Controller]")]
    public class FeedbackController : Controller
    {
        private readonly IFeedbackManager manager;

        public FeedbackController(IFeedbackManager manager)
        {
            this.manager = manager;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("addfeedback")]
        public IActionResult AddFeedback(AddFeedbackModel feedback)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = manager.AddFeedback(feedback, userId);
                if (result == "Feedback added")
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
        [HttpGet("getfeedback/{bookId}")]
        public IActionResult GetFeedback(int bookId)
        {
            try
            {
                var result = manager.GetFeedback(bookId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Faild to get" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
