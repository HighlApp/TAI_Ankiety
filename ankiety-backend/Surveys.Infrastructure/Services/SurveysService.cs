using System;
using System.Linq;
using Surveys.Core.Entities;
using System.Threading.Tasks;
using Surveys.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.Extensions;
using Surveys.Infrastructure.Services.Interfaces;
using Surveys.Infrastructure.Repositories.Interfaces;
using Surveys.Infrastructure.Requests.Surveys.PostSurvey;
using Surveys.Infrastructure.Requests.Surveys.UpdateSurvey;

namespace Surveys.Infrastructure.Services
{
    public class SurveysService : ISurveysService
    {
        public readonly IUserRepository _userRepository;
        public readonly IAnswersRepository _answersRepository;
        public readonly ISurveysRepository _surveysRepository;
        public readonly IHttpContextAccessor _httpContextAccessor;
        public readonly IInvitationsRepository _invitationsRepository;

        public SurveysService(ISurveysRepository surveysRepository,
            IUserRepository userRepository,
            IAnswersRepository answersRepository,
            IHttpContextAccessor httpContextAccessor, 
            IInvitationsRepository invitationsRepository)
        {
            _userRepository = userRepository;
            _answersRepository = answersRepository;
            _surveysRepository = surveysRepository;
            _httpContextAccessor = httpContextAccessor;
            _invitationsRepository = invitationsRepository;
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
                QuestionsOnPage = request.QuestionsOnPage ?? 1,
                //Questions = MapToQuestion(request.Questions)
            };

            await _surveysRepository.AddAsync(survey);
            await _surveysRepository.SaveAsync();

            survey = await _surveysRepository.GetByIdAsync(survey.Id); 
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

        public Task<Response<FilledSurveyDTO>> GetFilledSurvey(Guid invitationId)
        {
            throw new NotImplementedException();

            //TODO TODO TODO 
        }

        public async Task<Response<StatusResponseDTO>> SaveFilledSurvey(FilledSurveyDTO filledSurvey)
        {
            Invitation invitation = await _invitationsRepository.GetByIdAsync(
                filledSurvey.InvitationId);

            if (invitation == null)
                return new Response<StatusResponseDTO>("Invitation does not exist");

            if (invitation.FilledDate != null)
                return new Response<StatusResponseDTO>("Invitation has already been filled");

            foreach (QuestionAnswerDTO item in filledSurvey.Answers)
            {
                Answer answer = new Answer
                {
                    OptionId = item.OptionId,
                    QuestionId = item.QuestionId,
                    InvitationId = filledSurvey.InvitationId,
                    AnswerText = item.AnswerText
                };

                await _answersRepository.AddAsync(answer);
            }

            invitation.FilledDate = DateTime.Now;
            await _invitationsRepository.SaveAsync();

            return new Response<StatusResponseDTO>(new StatusResponseDTO(true));
        }

        public async Task<Response<SurveyToFillDTO>> GetSurveyToFillAsync(Guid invitationId)
        {
            Invitation invitation = 
                await _invitationsRepository.GetInvitationWithUser(invitationId);

            if (invitation == null)
                return new Response<SurveyToFillDTO>("Invitation does not exist");

            string userId = _httpContextAccessor.GetUserId();

            if (invitation.User.Id != userId)
                return new Response<SurveyToFillDTO>("Invitation does not exist");

            Survey survey = 
                await _surveysRepository.GetByIdWithQuestionsAndAnswerOptionsAsync(invitation.SurveyId);

            SurveyToFillDTO response = new SurveyToFillDTO()
            {
                Id = survey.Id,
                QuestionsOnPage = survey.QuestionsOnPage ?? survey.Questions.Count,
                ExpirationDate = invitation.ExpirationDate,
                Questions = MapToQuestionDTO(survey.Questions)
            };

            return new Response<SurveyToFillDTO>(response);
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
                Options = x.Options.Select(x => new OptionDTO()
                {
                    Id = x.Id,
                    OptionText = x.OptionText,
                    Value = x.Value
                }).ToList()
            }); 
        }

        //private static ICollection<Question> MapToQuestion(IEnumerable<QuestionDTO> question)
        //{
        //    return (ICollection<Question>)question.Select(x => new Question()
        //    {
        //        Text = x.Text,
        //        Id = x.Id,
        //        QuestionType = x.QuestionType,
        //        Options = x.Options.Select(x => new Option()
        //        {
        //            Id = x.Id,
        //            OptionText = x.OptionText,
        //            Value = x.Value
        //        }).ToList()
        //    });
        //}
    }
}
