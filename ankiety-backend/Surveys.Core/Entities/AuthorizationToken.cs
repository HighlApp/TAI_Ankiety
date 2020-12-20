namespace Surveys.Core.Entities
{
    public class AuthorizationToken
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}
