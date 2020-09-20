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
            var senderCheck = _userService.GetUserIdByUsername(req.SenderUsername);
            var recieverCheck = _userService.GetUserIdByUsername(req.ReceiveUsername);

            if (!senderCheck.Success)
                return Ok(senderCheck);

            if (!recieverCheck.Success)
                return Ok(recieverCheck);

            SendMessage sendMessage = new SendMessage();
            sendMessage.SenderId = senderCheck.Data;
            sendMessage.RecieveId = senderCheck.Data;
            sendMessage.Message = req.Message;
            return Ok(_messageService.SendMessage(sendMessage));
        }



    }
}
