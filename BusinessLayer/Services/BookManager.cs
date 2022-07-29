using BusinessLayer.Interface;
using CommonLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class BookManager : IBookManager
    {
        private readonly IBookRepository repository;
        public BookManager(IBookRepository repository)
        {
            this.repository = repository;
        }
        public BookModel AddBook(BookModel book)
        {
            try
            {
                return repository.AddBook(book);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public BookModel UpdateBook(BookModel book)
        {
            try
            {
                return repository.UpdateBook(book);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public bool DeleteBook(int bookId)
        {
            try
            {
                return repository.DeleteBook(bookId);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public List<BookModel> GetAllBooks()
        {
            try
            {
                return repository.GetAllBooks();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public BookModel GetBookById(int bookId)
        {
            try
            {
                return repository.GetBookById(bookId);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
