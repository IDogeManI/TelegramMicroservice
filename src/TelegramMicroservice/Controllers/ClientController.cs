using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TelegramMicroservice.Dtos;
using TL;

namespace TelegramMicroservice.Controller
{

    [ApiController]
    [Route("signin")]
    public class ClientController : ControllerBase
    {
        [HttpGet]
        public ActionResult<SignApiDto> Get(int apiId, string apiHash)
        {
            Session.api_id = apiId;
            Session.api_hash = apiHash;
            return Session.AsDto();
        }
        [HttpPost]
        public async Task<ActionResult<SignInClientDto>> Post()
        {
            if (Session.api_id == 0 || Session.api_hash == null)
                return NotFound();
            using var client = new WTelegram.Client(Session.Config);
            var my = await client.LoginUserIfNeeded();
            await client.Auth_SignIn(my.phone,
            (await client.Auth_SendCode(my.phone, Session.api_id, Session.api_hash, new CodeSettings())).phone_code_hash,
            my.lang_code);
            return my.AsDto();
        }

        [HttpPost("{chatId}/{message}")]
        public async Task<ActionResult<SendMessageToChatDto>> Post(int chatId, string message)
        {
            if (Session.api_id == 0 || Session.api_hash == null)
                return NotFound();
            using var client = new WTelegram.Client(Session.Config);
            var my = await client.LoginUserIfNeeded();
            var chats = await client.Messages_GetAllChats();
            var chat = chats.chats[chatId];
            await client.SendMessageAsync(chat, message);
            return new SendMessageToChatDto(chat.Title, message);
        }

        [HttpPost("{chatId}")]
        public async Task<ActionResult<GetMessageFromChatDto>> Post(int chatId)
        {
            if (Session.api_id == 0 || Session.api_hash == null)
                return NotFound();
            using var client = new WTelegram.Client(Session.Config);
            var my = await client.LoginUserIfNeeded();
            var chats = await client.Messages_GetAllChats();
            var chat = chats.chats[chatId];
            var messageBase = await client.Messages_GetHistory(chat, limit: 1);
            var message = ((Message)messageBase.Messages[0]).message;
            return new GetMessageFromChatDto(chat.Title, message);
        }

    }
}