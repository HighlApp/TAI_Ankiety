using System;
using System.Linq;
using Surveys.Core.Enums;
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
                QuestionsOnPage = request.QuestionsOnPage ?? 1
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

        public async Task<Response<FilledSurveySummaryDTO>> GetFilledSurvey(Guid invitationId)
        {
            double? userResult = 0;
            double? maxResult = 0;

            Invitation invitation = 
                await _invitationsRepository.GetInvitationWithSurveyDetails(invitationId);

            if (invitation == null)
                return new Response<FilledSurveySummaryDTO>("Invitation does not exist");

            IEnumerable<Answer> userAnswers = 
                await _answersRepository.GetInvitationAnswers(invitationId);

            User user = await _userRepository.FindByIdAsync(invitation.UserId);

            FilledSurveySummaryDTO result = new FilledSurveySummaryDTO()
            {
                Name = invitation.Survey.Name,
                Description = invitation.Survey.Description,
                Type = invitation.Survey.SurveyType.ToString(), //TODO: Check
                Questions = new List<FilledSurveyQuestionDTO>(),
                User = new UserDTO { 
                    Name = user.Name, 
                    Surname = user.Surname 
                }                
            };

            foreach (Question question in invitation.Survey.Questions)
            {
                FilledSurveyQuestionDTO filledSurveyQuestion = new FilledSurveyQuestionDTO()
                {
                    Text = question.Text,
                    Type = question.QuestionType.ToString(), //TODO: Check
                    Options = new List<FilledSurveyOptionDTO>(),
                    AnswerText = userAnswers
                        .Where(x => x.QuestionId == question.Id)
                        .FirstOrDefault().AnswerText
                };

                foreach (var option in question.Options)
                {
                    bool isSelected = userAnswers
                        .Where(x => x.OptionId == option.Id)
                        .FirstOrDefault() != null;

                    var filledSurveyOption = new FilledSurveyOptionDTO() { 
                        Text = option.OptionText, 
                        Value = option?.Value, 
                        IsSelected = isSelected 
                    };

                    filledSurveyQuestion.Options.Add(filledSurveyOption);

                    if (isSelected)
                        userResult += option.Value;
                }

                result.Questions.Add(filledSurveyQuestion);

                if(question.QuestionType == QuestionType.OneChoice)
                {
                    maxResult += question.Options.Select(x => x.Value).Max();
                }
                else if(question.QuestionType == QuestionType.MultipleChoice)
                {
                    question.Options.Where(x => x.Value > 0)
                        .Select(x => x.Value).Sum();
                }
            }

            if (result.Type == SurveyType.HiddenValues.ToString()) //TODO: Check
            {
                result.MaxValue = maxResult;
                result.Value = userResult;
            }

            return new Response<FilledSurveySummaryDTO>(result);
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

        private SurveyDTO MapToSurveyDTO(Survey survey)
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

        private IEnumerable<QuestionDTO> MapToQuestionDTO(ICollection<Question> questions)
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
    }
}
