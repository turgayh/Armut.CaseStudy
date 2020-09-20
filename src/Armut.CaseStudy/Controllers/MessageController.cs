using System;
using Armut.CaseStudy.Model;
using Armut.CaseStudy.Operation.MessageService;
using Armut.CaseStudy.Operation.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Armut.CaseStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize]
    public class MessageController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        private readonly ILogger<MessageController> _logger;

        public MessageController(IUserService userService, IMessageService messageService, ILogger<MessageController> logger)
        {
            _userService = userService;
            _messageService = messageService;
            _logger = logger;
        }


        [HttpPost("send-message")]
        public IActionResult SendMessage([FromBody] SendMessageRequest req)
        {
            _logger.LogInformation("MessageController-SendMessage Index executed at {date}- {sender} send to message {receieve}", DateTime.UtcNow ,req.SenderUsername,req.ReceiveUsername);
            var senderCheck = _userService.GetUserIdByUsername(req.SenderUsername);
            var recieverCheck = _userService.GetUserIdByUsername(req.ReceiveUsername);

            
            if (!senderCheck.Success)
                return Ok(senderCheck);

            if (!recieverCheck.Success)
                return Ok(recieverCheck);

            SendMessage sendMessage = new SendMessage();
            sendMessage.SenderId = senderCheck.Content;
            sendMessage.RecieveId = recieverCheck.Content;

            var checkBlock = _messageService.IsBlockedUser(sendMessage.SenderId, sendMessage.RecieveId);
            if (!checkBlock.Success) return Ok(checkBlock);

            sendMessage.Message = req.Message;
            return Ok(_messageService.SendMessage(sendMessage));
        }

        [Authorize]
        [HttpPost("list-messages")]
        public IActionResult ListMessages([FromBody] ListMessagesModel req) 
        {
            _logger.LogInformation("MessageController-ListMessages Index executed at {date}- {sender} list history", DateTime.UtcNow, req.username);
            var senderId = _userService.GetUserIdByUsername(req.username);
            var recieverId = _userService.GetUserIdByUsername(req.checkUser);


            if (!senderId.Success)
                return Ok(senderId);

            if (!recieverId.Success)
                return Ok(recieverId);

            var userId = senderId.Content;
            var receiverId = recieverId.Content;
            return Ok(_messageService.ListMessages(userId, receiverId, req.startTime, req.endTime));
        }

    }
}
