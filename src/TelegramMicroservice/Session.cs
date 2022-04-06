using TelegramMicroservice.Dtos;

namespace TelegramMicroservice
{
    public static class Session
    {
        public static int api_id = 0;
        public static string api_hash = null;

        public static string Config(string what)
        {
            switch (what)
            {
                case "api_id": return api_id.ToString();
                case "api_hash": return api_hash;
                default: return null;
            }
        }
        public static SignApiDto AsDto()
        {
            return new SignApiDto(api_hash,api_id);
        }
    }
}