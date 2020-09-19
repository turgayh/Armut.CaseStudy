using Armut.CaseStudy.Model;
using Microsoft.Extensions.Logging;

namespace Armut.CaseStudy.Operation.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly ILogger<MessageService> _logger;

        public MessageService(ILogger<MessageService> logger)
        {
            _logger = logger;
        }

        public ServiceResponse<string> SendMessage()
        {
            throw new System.NotImplementedException();
        }
    }
}