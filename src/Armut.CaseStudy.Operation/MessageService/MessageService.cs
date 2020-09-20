using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Armut.CaseStudy.Model;
using Armut.CaseStudy.Model.Dtos;
using Armut.CaseStudy.Operation.Helper;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
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

        public ServiceResponse<string> IsBlockedUser(string userId,string checkUserId)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var userInfoContext = _context.UserInfoContext();
                User user = userInfoContext.Find(user => user.UserId == userId).First();
                if(user.BlockedList.Contains(checkUserId))
                {
                    response.Success = false;
                    response.ErrorMessage = "You are blocked by " + user.Username;
                    return response;
                }
            }
            catch (Exception err)
            {
                _logger.LogError(err, "Message-IsBlockedUser throw error {date}", DateTime.UtcNow);
                throw;
            }
            return response;
        }

        public ServiceResponse<List<SendMessage>> ListMessages(string userId, string checkUserId , DateTime startTime, DateTime endTime)
        {
            ServiceResponse<List<SendMessage>> response = new ServiceResponse<List<SendMessage>>();

            try
            {
                var messagesContext = _context.MessageContext();
                List<SendMessage> totalMessage = new List<SendMessage>();
                var sendMessages = 
                    messagesContext.Find(msg => msg.SenderId == userId).FirstOrDefault().Messages.Where(val => val.RecieveId == checkUserId && val.TimeStamp <= endTime && startTime <= val.TimeStamp).ToList();
                var receiveMessages =
                    messagesContext.Find(msg => msg.SenderId == checkUserId).FirstOrDefault().Messages.Where(val => val.RecieveId == userId && val.TimeStamp <= endTime && startTime <= val.TimeStamp).ToList();
                receiveMessages.ForEach(val => totalMessage.Add(val));
                sendMessages.ForEach(val => totalMessage.Add(val));

                totalMessage = totalMessage.OrderByDescending(val => val.TimeStamp).ToList();
                response.Content = totalMessage;
                return response;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "Message-ListMessages throw error {date}", DateTime.UtcNow);
                throw;
            }
        }

        public ServiceResponse<string> SendMessage(SendMessage message)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
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
                response.Content = "Message send successfully";
            }
            catch (Exception err)
            {
                _logger.LogError(err, "Message-SendMessage throw error {date}", DateTime.UtcNow);
                throw;
            }
            return response;
        }
    }
}