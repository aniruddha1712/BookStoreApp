using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IOrderManager
    {
        string PlaceOrder(PlaceOrderModel order, int userId);
        List<OrderModel> GetAllOrders(int userId);
    }
}
