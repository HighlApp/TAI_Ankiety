using System;
using System.Linq;
using Surveys.Core.Entities;
using System.Threading.Tasks;
using Surveys.Core.Exceptions;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.Services.Interfaces;
using Surveys.Infrastructure.Repositories.Interfaces;
using Surveys.Infrastructure.Requests.Questions.PostQuestion;
using Surveys.Infrastructure.Requests.Questions.UpdateQuestion;

namespace Surveys.Infrastructure.Services
{
    public class QuestionsService : IQuestionsService
    {
        private readonly IQuestionsRepository _questionsRepository;

        public QuestionsService(IQuestionsRepository questionsRepository)
        {
            _questionsRepository = questionsRepository;
        }

        public async Task<Response<QuestionDTO>> DeleteAsync(Guid id)
        {
            Question question = await _questionsRepository.GetByIdAsync(id);

            if (question == null)
                throw new NotFoundException("Question not found.");

            _questionsRepository.Delete(question);
            await _questionsRepository.SaveAsync();

            return new Response<QuestionDTO>(MapToQuestionDTO(question));
        }

        public async Task<Response<QuestionDTO>> GetByIdAsync(Guid id)
        {
            Question question = await _questionsRepository.GetByIdWithOptions(id);

            if (question == null)
                throw new NotFoundException("Question not found");

            return new Response<QuestionDTO>(MapToQuestionDTO(question));
        }

        public async Task<Response<QuestionDTO>> PostAsync(PostQuestionRequest request)
        {
            Question question = new Question
            {
                Text = request.Text,
                QuestionType = request.QuestionType,
                SurveyId = request.SurveyId,
                Options = request.Options
            };

            await _questionsRepository.AddAsync(question);
            await _questionsRepository.SaveAsync();

            question = await _questionsRepository.GetByIdAsync(question.Id);
            return new Response<QuestionDTO>(MapToQuestionDTO(question));
        }

        public async Task<Response<QuestionDTO>> UpdateAsync(UpdateQuestionRequest request)
        {
            if (request.Id == Guid.Empty)
                throw new BadRequestException();

            Question question = await _questionsRepository.GetByIdWithOptions(request.Id);

            if (question == null)
                throw new NotFoundException("Question not found");

            question.Text = request.Text ?? question.Text;

            foreach (var option in request.Options)
            {
                var optionToChange = question.Options.FirstOrDefault(m => m.Id == option.Id);

                if (optionToChange != null)
                {
                    optionToChange.OptionText = option.OptionText ?? optionToChange.OptionText;
                    optionToChange.Value = option.Value ?? optionToChange.Value;
                }
            }

            _questionsRepository.Update(question);
            await _questionsRepository.SaveAsync();

            return new Response<QuestionDTO>(MapToQuestionDTO(question));
        }

        private static QuestionDTO MapToQuestionDTO(Question question)
        {
            return new QuestionDTO
            {
                Id = question.Id,
                QuestionType = question.QuestionType,
                Text = question.Text
            };
        }
    }
}
