using BusinessLayer.Interface;
using CommonLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartManager : ICartManager
    {
        private readonly ICartRepository repository;
        public CartManager(ICartRepository repository)
        {
            this.repository = repository;
        }
        public AddToCartModel AddToCart(AddToCartModel cart, int userId)
        {
            try
            {
                return repository.AddToCart(cart,userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string UpdateCart(int cartId, int bookQty)
        {
            try
            {
                return repository.UpdateCart(cartId, bookQty);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool RemoveFromCart(int cartId)
        {
            try
            {
                return repository.RemoveFromCart(cartId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<CartModel> GetCartItem(int userId)
        {
            try
            {
                return repository.GetCartItem(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
