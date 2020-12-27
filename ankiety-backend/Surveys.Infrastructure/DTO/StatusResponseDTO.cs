namespace Surveys.Infrastructure.DTO
{
    public class StatusResponseDTO
    {
        public StatusResponseDTO(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; }
    }
}
