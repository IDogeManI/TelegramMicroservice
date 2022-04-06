using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TelegramMicroservice.Dtos;

namespace TelegramMicroservice.Services
{
    public interface IClientService
    {
        Task<ActionResult<SignInClientDto>> AuthClientAsync();
        void AuthSession(int apiId, string apiHash);
        Task<ActionResult<GetMessageFromChatDto>> GetMessageAsync(int chatId);
        Task<ActionResult<SendMessageToChatDto>> SendMessageAsync(int chatId, string message);
    }
}