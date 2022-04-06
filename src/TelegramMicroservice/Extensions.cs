using System.Globalization;
using TelegramMicroservice.Dtos;
using TL;

namespace TelegramMicroservice
{
    public static class Extensions
    {
        public static SignInClientDto AsDto(this User user)
        {
            return new SignInClientDto(user.phone,user.username);
        }
    }
}