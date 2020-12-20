using System.Collections.Generic;

namespace Surveys.Infrastructure.Common
{
    public class Response<T>
    {
        public bool IsError { get; set; }

        public IEnumerable<string> ErrorMessages { get; set; }

        public T ResponseData { get; set; }

        public Response(T responseData)
        {
            IsError = false;
            ResponseData = responseData;
        }

        public Response(IEnumerable<string> errorMessages)
        {
            IsError = true;
            ResponseData = default;
            ErrorMessages = errorMessages;
        }
    }
}
