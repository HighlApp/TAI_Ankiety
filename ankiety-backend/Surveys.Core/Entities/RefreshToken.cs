using System;

namespace Surveys.Core.Entities
{
    public class RefreshToken
    {
        public string UserId { get; set; }

        public string Token { get; set; }

        public string JwtId { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public bool Invalidated { get; set; }

        public bool Used { get; set; }
    }
}
