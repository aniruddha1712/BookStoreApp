using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IFeedbackManager
    {
        string AddFeedback(AddFeedbackModel feedback, int userId);
        List<FeedbackModel> GetFeedback(int bookId);
    }
}
