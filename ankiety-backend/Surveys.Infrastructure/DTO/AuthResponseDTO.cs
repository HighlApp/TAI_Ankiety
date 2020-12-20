namespace Surveys.Infrastructure.DTO
{
    public class AuthResponseDTO
    {
        public string Id { get; set; }

        public string Role { get; set; }

        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}
