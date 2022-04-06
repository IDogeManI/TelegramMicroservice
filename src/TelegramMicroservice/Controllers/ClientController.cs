using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TelegramMicroservice.Dtos;
using TelegramMicroservice.Services;
using TL;

namespace TelegramMicroservice.Controller
{

    [ApiController]
    [Route("signin")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService clientService;
        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        [HttpGet]
        public ActionResult<SignApiDto> Get(int apiId, string apiHash)
        {
            clientService.AuthSession(apiId,apiHash);
            return Session.AsDto();
        }
        [HttpPost]
        public async Task<ActionResult<SignInClientDto>> Post()
        {
            if (Session.api_id == 0 || Session.api_hash == null)
                return NotFound();
            return await clientService.AuthClientAsync();
        }

        [HttpPost("{chatId}/{message}")]
        public async Task<ActionResult<SendMessageToChatDto>> Post(int chatId, string message)
        {
            if (Session.api_id == 0 || Session.api_hash == null)
                return NotFound();
            return await clientService.SendMessageAsync(chatId, message);
            
        }

        [HttpPost("{chatId}")]
        public async Task<ActionResult<GetMessageFromChatDto>> Post(int chatId)
        {
            if (Session.api_id == 0 || Session.api_hash == null)
                return NotFound();
            
            return await clientService.GetMessageAsync(chatId);
        }

    }
}