using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IBookManager
    {
        BookModel AddBook(BookModel book);
        BookModel UpdateBook(BookModel book);
        bool DeleteBook(int bookId);
        List<BookModel> GetAllBooks();
        BookModel GetBookById(int bookId);
    }
}
