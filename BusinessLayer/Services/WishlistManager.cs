using BusinessLayer.Interface;
using CommonLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishlistManager: IWishlistManager
    {
        private readonly IWishlistRepository repository;
        public WishlistManager(IWishlistRepository repository)
        {
            this.repository = repository;
        }

        public string AddToWishlist(int bookId, int userId)
        {
            try
            {
                return repository.AddToWishlist(bookId, userId);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public bool RemoveFromWishlist(int wishlistId)
        {
            try
            {
                return repository.RemoveFromWishlist(wishlistId);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<WishlistModel> GetWishlistItem(int userId)
        {
            try
            {
                return repository.GetWishlistItem(userId);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
