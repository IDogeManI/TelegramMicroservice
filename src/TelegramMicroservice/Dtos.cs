using System;
using TL;

namespace TelegramMicroservice.Dtos{
    public record SignApiDto(string ApiHash, int ApiId);
    public record SignInClientDto(string PhoneNumber,string Username);
    public record SendMessageToChatDto(string Title,string Message);
    public record GetMessageFromChatDto(string Title, string Message);
}