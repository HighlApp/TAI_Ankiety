using System;
using System.Linq;
using Surveys.Core.Entities;
using System.Threading.Tasks;
using Surveys.Core.Exceptions;
using System.Collections.Generic;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.Services.Interfaces;
using Surveys.Infrastructure.Repositories.Interfaces;
using Surveys.Infrastructure.Requests.Surveys.PostSurvey;
using Surveys.Infrastructure.Requests.Surveys.UpdateSurvey;

namespace Surveys.Infrastructure.Services
{
    public class SurveysService : ISurveysService
    {
        public readonly ISurveysRepository _surveysRepository;

        public SurveysService(ISurveysRepository surveysRepository)
        {
            _surveysRepository = surveysRepository;
        }

        public async Task<Response<SurveyDTO>> DeleteAsync(Guid id)
        {
            Survey survey = await _surveysRepository.GetByIdAsync(id);

            if (survey == null)
                throw new NotFoundException("Survey not found.");

            _surveysRepository.Delete(survey);
            await _surveysRepository.SaveAsync();

            return new Response<SurveyDTO>(MapToSurveyDTO(survey));
        }

        public async Task<Response<IEnumerable<SurveyDTO>>> GetAllAsync()
        {
            IEnumerable<Survey> surveys = await _surveysRepository.GetAllAsync();
            IEnumerable<SurveyDTO> surveysDTO = surveys.Select(s => MapToSurveyDTO(s));

            return new Response<IEnumerable<SurveyDTO>>(surveysDTO);
        }

        public async Task<Response<SurveyDTO>> GetByIdAsync(Guid id)
        {
            Survey survey = await _surveysRepository.GetByIdWithQuestionsAndAnswerOptionsAsync(id);

            if (survey == null)
                throw new NotFoundException("Survey not found");

            return new Response<SurveyDTO>(MapToSurveyDTO(survey));
        }

        public async Task<Response<SurveyDTO>> PostAsync(PostSurveyRequest request)
        {
            Survey survey = new Survey
            {
                Name = request.Name,
                Description = request.Description,
                SurveyType = request.SurveyType,
                QuestionsOnPage = request.QuestionsOnPage,
                Questions = MapToQuestion(request.Questions)
            };

            await _surveysRepository.AddAsync(survey);
            await _surveysRepository.SaveAsync();

            survey = await _surveysRepository.GetByIdAsync(survey.Id); //TODO: Check that
            return new Response<SurveyDTO>(MapToSurveyDTO(survey));
        }

        public async Task<Response<SurveyDTO>> UpdateAsync(UpdateSurveyRequest request)
        {
            if (request.Id == Guid.Empty)
                throw new BadRequestException();

            Survey survey = await _surveysRepository.GetByIdAsync(request.Id);

            if (survey == null)
                throw new NotFoundException("Survey not found");

            survey.Name = request.Name ?? survey.Name;
            survey.Description = request.Description ?? survey.Description;
            _surveysRepository.Update(survey);

            await _surveysRepository.SaveAsync();
            return new Response<SurveyDTO>(MapToSurveyDTO(survey));
        }

        private static SurveyDTO MapToSurveyDTO(Survey survey)
        {
            return new SurveyDTO
            {
                Id = survey.Id,
                Description = survey.Description,
                Name = survey.Name,
                SurveyType = survey.SurveyType,
                QuestionsOnPage = survey.QuestionsOnPage,
                Questions = MapToQuestionDTO(survey.Questions)
            };
        }

        private static IEnumerable<QuestionDTO> MapToQuestionDTO(ICollection<Question> questions)
        {
            if (questions == null)
                return null;

            return questions.Select(x => new QuestionDTO()
            {
                Text = x.Text,
                Id = x.Id,
                QuestionType = x.QuestionType,
                Options = x.Options
            });
        }

        private static ICollection<Question> MapToQuestion(IEnumerable<QuestionDTO> questionDtos)
        {
            Question question;
            var questions = new List<Question>();
            foreach (var questionDto in questionDtos)
            {
                question = new Question
                {
                    Text = questionDto.Text,
                    Id = questionDto.Id,
                    QuestionType = questionDto.QuestionType,
                    Options = questionDto.Options
                };
                questions.Add(question);
            }
            return questions;
        }
    }
}
