using System;

namespace Surveys.Core.Settings
{
    public class JwtTokenSettings
    {
        public string Secret { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan TokenLifeTime { get; set; }

        public TimeSpan RefreshTokenLifeTime { get; set; }
    }
}
