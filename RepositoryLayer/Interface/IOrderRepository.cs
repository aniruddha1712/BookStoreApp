using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IOrderRepository
    {
        string PlaceOrder(PlaceOrderModel order, int userId);
        List<OrderModel> GetAllOrders(int userId);
    }
}
