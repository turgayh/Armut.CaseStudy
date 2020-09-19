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
    [ValidateAntiForgeryToken]
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
        public IActionResult SendMessage(string username)
        {
            var usernameCheck = _userService.GetUserIdByUsername(username);
            if (!usernameCheck.Success)
                return NotFound(usernameCheck);
            string receiverId = usernameCheck.Data;

        }



    }
}
