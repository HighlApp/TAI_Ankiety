using System;
using MediatR;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Requests.Surveys.DeleteSurvey
{
    public class DeleteSurveyRequest : IRequest<Response<SurveyDTO>>
    {
        public DeleteSurveyRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
