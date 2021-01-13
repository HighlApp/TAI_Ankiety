﻿using System;
using System.Linq;
using Surveys.Core.Entities;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.Extensions;
using Surveys.Infrastructure.Services.Interfaces;
using Surveys.Infrastructure.Repositories.Interfaces;

namespace Surveys.Infrastructure.Services
{
    public class InvitationsService : IInvitationsService
    {
        private readonly ISurveysRepository _surveysRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IInvitationsRepository _invitationsRepository;

        public InvitationsService(IInvitationsRepository invitationsRepository, 
            ISurveysRepository surveysRepository, IHttpContextAccessor httpContextAccessor)
        {
            _surveysRepository = surveysRepository;
            _httpContextAccessor = httpContextAccessor;
            _invitationsRepository = invitationsRepository;
        }

        public async Task<Response<IEnumerable<AdministratorInvitationDTO>>> GetAllInvitationsAsync()
        {
            IEnumerable<Invitation> invitations = await _invitationsRepository.GetInvitationWithUsers();

            IEnumerable<AdministratorInvitationDTO> invitationsDTO = invitations.Select(x => new AdministratorInvitationDTO()
            {
                Id = x.Id,
                ExpirationDate = x.ExpirationDate,
                SendDate = x.SendDate,
                StartDate = x.StartDate,
                SurveyId = x.SurveyId,
                UserId = x.UserId,
                FilledSurvey = x.FilledDate != null,
                SurveyName = x.Survey.Name,
                Username = x.User.UserName,
                Expired = x.ExpirationDate != null && x.ExpirationDate < DateTime.Now
            });

            return new Response<IEnumerable<AdministratorInvitationDTO>>(invitationsDTO);
        }

        public async Task<Response<StatusResponseDTO>> InviteUsersAsync(
            IEnumerable<Guid> usersId, InvitationDetailsDTO details)
        {
            foreach (Guid userId in usersId)
            {
                Invitation invitation = new Invitation
                {
                    SurveyId = details.SurveyId,
                    StartDate = details.StartDate != null && details.StartDate < DateTime.Now ? 
                        details.StartDate : DateTime.Now,
                    ExpirationDate = details.ExpirationDate != null && details.ExpirationDate < DateTime.Now ? 
                        details.StartDate : null,
                    SendDate = DateTime.Now,
                    UserId = userId.ToString()
                };

                await _invitationsRepository.AddAsync(invitation);
            }

            await _invitationsRepository.SaveAsync();

            return new Response<StatusResponseDTO>(new StatusResponseDTO(true));
        }

        public async Task<Response<IEnumerable<UserDTO>>> GetUsersForInvitationAsync(Guid surveyId)
        {
            Survey survey = await _surveysRepository.GetByIdAsync(surveyId);

            if (survey == null)
                return new Response<IEnumerable<UserDTO>>("Survey does not exist");

            //TODO: FILL

            return null;
        }

        public async Task<Response<IEnumerable<UserInvitationDTO>>> GetUserInvitationsAsync()
        {
            Guid userId = new Guid(_httpContextAccessor.GetUserId()); 

            IEnumerable<Invitation> invitations = await _invitationsRepository.GetUserInvitations(userId);

            IEnumerable<UserInvitationDTO> response = invitations.Select(x => new UserInvitationDTO()
            {
                Id = x.Id,
                SurveyId = x.SurveyId,
                Name = x.Survey.Name,
                ExpirationDate = x.ExpirationDate,
                SendDate = x.SendDate
            });

            return new Response<IEnumerable<UserInvitationDTO>>(response);
        }

        public async Task<Response<StatusResponseDTO>> DeleteInvitationAsync(Guid invitationId)
        {
            Invitation invitation = await _invitationsRepository.GetByIdAsync(invitationId);

            if (invitation == null)
                return new Response<StatusResponseDTO>("Invitation does not exist");

            if (invitation.FilledDate != null)
                return new Response<StatusResponseDTO>("Invitation can not be removed, because survey was filled");

            if (invitation.StartDate != null)
                return new Response<StatusResponseDTO>("Invitation can not be removed, because user started filling survey");

            _invitationsRepository.Delete(invitation);
            await _invitationsRepository.SaveAsync();

            return new Response<StatusResponseDTO>(new StatusResponseDTO(true));
        }
    }
}