using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class WishlistRepository : IWishlistRepository
    {
        public IConfiguration Configuration { get; }

        public WishlistRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string AddToWishlist(int bookId, int userId)
        {
            using SqlConnection connection = new SqlConnection(Configuration["ConnectionString:BookStore"]);
            try
            {
                SqlCommand command = new SqlCommand("spAddToWishlist", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@BookId", bookId);
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                var result = command.ExecuteNonQuery();
                connection.Close();

                if (result > 0)
                {
                    return "Added to WishList";
                }
                else
                {
                    return "Failed to Add";
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool RemoveFromWishlist(int wishlistId)
        {
            using SqlConnection connection = new SqlConnection(Configuration["ConnectionString:BookStore"]);
            try
            {
                SqlCommand command = new SqlCommand("spRemoveFromWishlist", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@WishlistId", wishlistId);

                connection.Open();
                var result = command.ExecuteNonQuery();
                connection.Close();

                if (result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<WishlistModel> GetWishlistItem(int userId)
        {
            using SqlConnection connection = new SqlConnection(Configuration["ConnectionString:BookStore"]);
            try
            {
                List<WishlistModel> cartList = new List<WishlistModel>();
                SqlCommand command = new SqlCommand("spGetAllWishlistItem", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        WishlistModel wish = new WishlistModel();
                        WishlistModel temp = GetCartDetails(wish, reader);
                        cartList.Add(temp);
                    }
                    return cartList;
                }
                else
                {
                    connection.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public WishlistModel GetCartDetails(WishlistModel wish, SqlDataReader rdr)
        {
            wish.BookId = Convert.ToInt32(rdr["BookId"] == DBNull.Value ? default : rdr["BookId"]);
            wish.UserId = Convert.ToInt32(rdr["UserId"] == DBNull.Value ? default : rdr["UserId"]);
            wish.WishlistId = Convert.ToInt32(rdr["WishlistId"] == DBNull.Value ? default : rdr["WishlistId"]);
            wish.BookName = Convert.ToString(rdr["BookName"] == DBNull.Value ? default : rdr["BookName"]);
            wish.Author = Convert.ToString(rdr["Author"] == DBNull.Value ? default : rdr["Author"]);
            wish.BookImage = Convert.ToString(rdr["BookImage"] == DBNull.Value ? default : rdr["BookImage"]);
            wish.DiscountPrice = Convert.ToDouble(rdr["DiscountPrice"] == DBNull.Value ? default : rdr["DiscountPrice"]);
            wish.ActualPrice = Convert.ToDouble(rdr["ActualPrice"] == DBNull.Value ? default : rdr["ActualPrice"]);
            return wish;
        }
    }
}
