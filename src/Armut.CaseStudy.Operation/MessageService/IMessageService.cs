using Armut.CaseStudy.Model;
using MongoDB.Driver;

namespace Armut.CaseStudy.Operation.MessageService
{
    public interface IMessageService
    {
        public ServiceResponse<string> SendMessage(SendMessage message);
        public ServiceResponse<string> IsBlockedUser(string userId, string checkUserId);
    }
}