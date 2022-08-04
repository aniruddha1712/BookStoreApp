using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IFeedbackRepository
    {
        string AddFeedback(AddFeedbackModel feedback, int userId);
        List<FeedbackModel> GetFeedback(int bookId);
    }
}
