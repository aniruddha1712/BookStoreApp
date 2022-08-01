using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICartManager
    {
        AddToCartModel AddToCart(AddToCartModel cart, int userId);
        string UpdateCart(int cartId, int bookQty);
        bool RemoveFromCart(int cartId);
        List<CartModel> GetCartItem(int userId);
    }
}
