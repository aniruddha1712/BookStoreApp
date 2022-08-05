using BusinessLayer.Interface;
using CommonLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderRepository repository;
        public OrderManager(IOrderRepository repository)
        {
            this.repository = repository;
        }

        public string PlaceOrder(PlaceOrderModel order,int userId)
        {
            try
            {
                return repository.PlaceOrder(order, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<OrderModel> GetAllOrders(int userId)
        {
            try
            {
                return repository.GetAllOrders(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
