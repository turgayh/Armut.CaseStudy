using Armut.CaseStudy.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Armut.CaseStudy.Operation.MessageService
{
    public interface IMessageService
    {
        public ServiceResponse<string> SendMessage(SendMessage message);
        public ServiceResponse<string> IsBlockedUser(string userId, string checkUserId);
        public ServiceResponse<List<SendMessage>> ListMessages(string userId, string checkUserId, DateTime startTime, DateTime endTime);
    }
}