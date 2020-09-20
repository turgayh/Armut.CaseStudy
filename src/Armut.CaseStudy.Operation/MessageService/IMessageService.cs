using Armut.CaseStudy.Model;

namespace Armut.CaseStudy.Operation.MessageService
{
    public interface IMessageService
    {
        public ServiceResponse<string> SendMessage(SendMessage message);
    }
}