using System;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.Requests.Questions.PostQuestion;
using Surveys.Infrastructure.Requests.Questions.UpdateQuestion;

namespace Surveys.Infrastructure.Services.Interfaces
{
    public interface IQuestionsService
    {
        Task<Response<QuestionDTO>> GetByIdAsync(Guid id);

        Task<Response<QuestionDTO>> DeleteAsync(Guid id);

        Task<Response<QuestionDTO>> UpdateAsync(UpdateQuestionRequest request);

        Task<Response<QuestionDTO>> PostAsync(PostQuestionRequest request);
    }
}
