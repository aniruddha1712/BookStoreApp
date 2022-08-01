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
    public class BookController : Controller
    {
        private readonly IBookManager manager;

        public BookController(IBookManager manager)
        {
            this.manager = manager;
        }

        //[Authorize(Roles = Role.Admin)]
        [HttpPost("addbook")]
        public IActionResult AddBook(BookModel book)
        {
            try
            {
                if (Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "AdminId").Value) == 1)
                {
                    var result = manager.AddBook(book);
                    if (result != null)
                    {
                        return this.Ok(new { Status = true, Message = "Book added", Data = result });
                    }
                    else
                    {
                        return this.BadRequest(new { Status = false, Message = "Faild to Add" });
                    }
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Unauthorize Admin" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //[Authorize(Roles = Role.Admin)]
        [HttpPut]
        [Route("updatebook")]
        public IActionResult UpdateBook(BookModel book)
        {
            try
            {
                if (Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "AdminId").Value) == 1)
                {
                    var result = manager.UpdateBook(book);
                    if (result != null)
                    {
                        return this.Ok(new { Status = true, Message = "Book updated", Data = result });
                    }
                    else
                    {
                        return this.BadRequest(new { Status = false, Message = "Faild to update" });
                    }
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Unauthorize Admin" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //[Authorize(Roles = Role.Admin)]
        [HttpDelete]
        [Route("deletebook")]
        public IActionResult DeleteBook(int bookId)
        {
            try
            {
                if (Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "AdminId").Value) == 1)
                {
                    var result = manager.DeleteBook(bookId);
                    if (result == true)
                    {
                        return this.Ok(new { Status = true, Message = "Book deleted" });
                    }
                    else
                    {
                        return this.BadRequest(new { Status = false, Message = "Book does not exist" });
                    }
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Unauthorize Admin" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { ex.Message });
            }
        }

        [HttpGet]
        [Route("getallbooks")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var result = manager.GetAllBooks();
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Your Books", Data= result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "something went wrong" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        [Route("getbookbyId")]
        public IActionResult GetBookById(int bookId)
        {
            try
            {
                var result = manager.GetBookById(bookId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Your Book", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book does not exist" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
