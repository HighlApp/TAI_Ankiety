using System.Collections.Generic;

namespace Surveys.Infrastructure.Common
{
    public class Response<T>
    {
        public bool IsError { get; }

        public IEnumerable<string> ErrorMessages { get; }

        public T Data { get; }

        public Response(T responseData)
        {
            IsError = false;
            ErrorMessages = null;
            Data = responseData;
        }

        public Response(string errorMessage)
        {
            IsError = true;
            Data = default;
            ErrorMessages = new List<string>
            {
                errorMessage
            };
        }

        public Response(IEnumerable<string> errorMessages)
        {
            IsError = true;
            Data = default;
            ErrorMessages = errorMessages;
        }
    }
}
