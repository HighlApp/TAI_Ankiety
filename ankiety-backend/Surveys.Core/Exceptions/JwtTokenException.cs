using System;

namespace Surveys.Core.Exceptions
{
    public class JwtTokenException : Exception
    {
        public JwtTokenException(string message) : base(message)
        {
        }
    }
}
