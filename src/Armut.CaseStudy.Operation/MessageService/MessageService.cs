using System;
using System.Linq.Expressions;
using Armut.CaseStudy.Model;
using Armut.CaseStudy.Model.Dtos;
using Armut.CaseStudy.Operation.Helper;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Armut.CaseStudy.Operation.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly ILogger<MessageService> _logger;
        private readonly IContext _context;

        public MessageService(ILogger<MessageService> logger, IContext context)
        {
            _logger = logger;
            _context = context;
        }

        public ServiceResponse<ReplaceOneResult> SendMessage(SendMessage message)
        {
            ServiceResponse<ReplaceOneResult> response = new ServiceResponse<ReplaceOneResult>();
            SendMessageDto sendMessageDto = new SendMessageDto();

            try
            {
                var messageContext = _context.MessageContext();
                var data = messageContext.Find(user => user.SenderId == message.SenderId).FirstOrDefault();
                if (data == null)
                {
                    SendMessageDto initial = new SendMessageDto();
                    initial.SenderId = message.SenderId;
                    messageContext.InsertOne(initial);
                    data = initial;
                }
                data.Messages.Add(message);
                data.SenderId = message.SenderId;
                var result = messageContext.ReplaceOne(user => user.SenderId == message.SenderId, data);
                response.Data = result;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "Message-SendMessage throw error");
                throw;
            }
            return response;
        }
    }
}