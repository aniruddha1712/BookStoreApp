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
    public class FeedbackRepository : IFeedbackRepository
    {
        public IConfiguration Configuration { get; }

        public FeedbackRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string AddFeedback(AddFeedbackModel feedback,int userId)
        {
            using SqlConnection connection = new SqlConnection(Configuration["ConnectionString:BookStore"]);
            try
            {
                SqlCommand command = new SqlCommand("spAddFeedback", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Rating", feedback.Rating);
                command.Parameters.AddWithValue("@Comment", feedback.Comment);
                command.Parameters.AddWithValue("@BookId", feedback.BookId);
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                var result = command.ExecuteNonQuery();
                connection.Close();

                if (result == 2)
                {
                    return "Feedback added";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<FeedbackModel> GetFeedback(int bookId)
        {
            using SqlConnection connection = new SqlConnection(Configuration["ConnectionString:BookStore"]);
            try
            {
                List<FeedbackModel> feedbackList = new List<FeedbackModel>();
                SqlCommand command = new SqlCommand("spGetFeedback", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@BookId", bookId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        FeedbackModel feedback = new FeedbackModel();
                        feedback.FeedbackId = Convert.ToInt32(reader["FeedbackId"] == DBNull.Value ? default : reader["FeedbackId"]);
                        feedback.BookId = Convert.ToInt32(reader["BookId"] == DBNull.Value ? default : reader["BookId"]);
                        feedback.UserId = Convert.ToInt32(reader["UserId"] == DBNull.Value ? default : reader["UserId"]);
                        feedback.Comment = Convert.ToString(reader["Comment"] == DBNull.Value ? default : reader["Comment"]);
                        feedback.Rating = Convert.ToDouble(reader["Rating"] == DBNull.Value ? default : reader["Rating"]);
                        feedback.FullName = Convert.ToString(reader["FullName"] == DBNull.Value ? default : reader["FullName"]);
                        feedbackList.Add(feedback);
                    }
                    return feedbackList;
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
    }
}
