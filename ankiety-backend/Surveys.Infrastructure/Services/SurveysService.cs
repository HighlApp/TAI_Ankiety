using Surveys.Core.Entities;
using Surveys.Core.Exceptions;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Repositories.Interfaces;
using Surveys.Infrastructure.Requests.Surveys.PostSurvey;
using Surveys.Infrastructure.Requests.Surveys.UpdateSurvey;
using Surveys.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            var survey = await _surveysRepository.GetByIdAsync(id);
            if (survey == null)
                throw new NotFoundException("Survey not found.");
            _surveysRepository.Delete(survey);
            await _surveysRepository.SaveAsync();
            var surveyDto = MapToSurveyDTO(survey);
            return new Response<SurveyDTO>(surveyDto);
        }

        public async Task<Response<IEnumerable<SurveyDTO>>> GetAllAsync()
        {
            var surveys = await _surveysRepository.GetAllAsync();
            var surveyDtos = new List<SurveyDTO>();
            foreach (var survey in surveys)
            {
                var surveyDto = MapToSurveyDTO(survey);
                surveyDtos.Add(surveyDto);
            }
            return new Response<IEnumerable<SurveyDTO>>(surveyDtos);
        }

        public async Task<Response<SurveyDTO>> GetByIdAsync(Guid id)
        {
            var survey = await _surveysRepository.GetByIdAsync(id);
            if (survey == null)
                throw new NotFoundException("Survey not found");
            SurveyDTO surveyDto = MapToSurveyDTO(survey);
            return new Response<SurveyDTO>(surveyDto);
        }

        public async Task<Response<SurveyDTO>> PostAsync(PostSurveyRequest request)
        {
            var survey = new Survey
            {
                Name = request.Name,
                Description = request.Description,
                SurveyType = request.SurveyType,
                QuestionsOnPage = request.QuestionsOnPage,
                //Questions = request.Questions
            };
            await _surveysRepository.AddAsync(survey);
            await _surveysRepository.SaveAsync();

            survey = await _surveysRepository.GetByIdAsync(survey.Id);
            var surveyDto = MapToSurveyDTO(survey);
            return new Response<SurveyDTO>(surveyDto);
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
            var surveyDto = MapToSurveyDTO(survey);
            return new Response<SurveyDTO>(surveyDto);
        }

        private SurveyDTO MapToSurveyDTO(Survey survey)
        {
            return new SurveyDTO
            {
                Id = survey.Id,
                Description = survey.Description,
                Name = survey.Name,
                SurveyType = survey.SurveyType,
                QuestionsOnPage = survey.QuestionsOnPage,
                //Questions = survey.Questions
            };
        }
    }
}
