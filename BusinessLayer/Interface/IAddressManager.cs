using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IAddressManager
    {
        string AddAddress(AddressModel address, int userId);
        AddressModel UpdateAddress(AddressModel address, int userId);
        bool DeleteAddress(int addressId, int userId);
        AddressModel GetAddressById(int addressId, int userId);
        List<AddressModel> GetAllAddress(int userId);
    }
}
