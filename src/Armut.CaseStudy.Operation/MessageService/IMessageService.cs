using Armut.CaseStudy.Model;
using MongoDB.Driver;

namespace Armut.CaseStudy.Operation.MessageService
{
    public interface IMessageService
    {
        public ServiceResponse<ReplaceOneResult> SendMessage(SendMessage message);
    }
}