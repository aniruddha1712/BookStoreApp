using BusinessLayer.Interface;
using CommonLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AddressManager: IAddressManager
    {
        private readonly IAddressRepository repository;
        public AddressManager(IAddressRepository repository)
        {
            this.repository = repository;
        }

        public string AddAddress(AddressModel address,int userId)
        {
            try
            {
                return repository.AddAddress(address, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public AddressModel UpdateAddress(AddressModel address, int userId)
        {
            try
            {
                return repository.UpdateAddress(address, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteAddress(int addressId, int userId)
        {
            try
            {
                return repository.DeleteAddress(addressId, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public AddressModel GetAddressById(int addressId, int userId)
        {
            try
            {
                return repository.GetAddressById(addressId, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<AddressModel> GetAllAddress(int userId)
        {
            try
            {
                return repository.GetAllAddress(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
