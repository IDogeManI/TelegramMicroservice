using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TelegramMicroservice.Dtos;
using TL;

namespace TelegramMicroservice.Services
{

    public class ClientService : IClientService
    {
        public void AuthSession(int apiId, string apiHash)
        {
            Session.api_id = apiId;
            Session.api_hash = apiHash;
        }
        public async Task<ActionResult<SignInClientDto>> AuthClientAsync()
        {
            using var client = new WTelegram.Client(Session.Config);
            var my = await client.LoginUserIfNeeded();
            await client.Auth_SignIn(my.phone,
            (await client.Auth_SendCode(my.phone, Session.api_id, Session.api_hash, new CodeSettings())).phone_code_hash,
            my.lang_code);
            return my.AsDto();
        }
        public async Task<ActionResult<SendMessageToChatDto>> SendMessageAsync(int chatId, string message)
        {
            using var client = new WTelegram.Client(Session.Config);
            var my = await client.LoginUserIfNeeded();
            var chats = await client.Messages_GetAllChats();
            var chat = chats.chats[chatId];
            await client.SendMessageAsync(chat, message);
            return new SendMessageToChatDto(chat.Title, message);
        }
        public async Task<ActionResult<GetMessageFromChatDto>> GetMessageAsync(int chatId)
        {
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