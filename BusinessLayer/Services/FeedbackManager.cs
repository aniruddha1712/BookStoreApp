using BusinessLayer.Interface;
using CommonLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class FeedbackManager : IFeedbackManager
    {
        private readonly IFeedbackRepository repository;
        public FeedbackManager(IFeedbackRepository repository)
        {
            this.repository = repository;
        }

        public string AddFeedback(AddFeedbackModel feedback, int userId)
        {
            try
            {
                return repository.AddFeedback(feedback,userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<FeedbackModel> GetFeedback(int bookId)
        {
            try
            {
                return repository.GetFeedback(bookId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
