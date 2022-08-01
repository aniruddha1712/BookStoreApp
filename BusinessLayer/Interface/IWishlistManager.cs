using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IWishlistManager
    {
        string AddToWishlist(int bookId, int userId);
        bool RemoveFromWishlist(int wishlistId);
        List<WishlistModel> GetWishlistItem(int userId);
    }
}
