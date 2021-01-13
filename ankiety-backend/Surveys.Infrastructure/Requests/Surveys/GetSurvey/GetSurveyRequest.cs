using System;
using MediatR;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Requests.Surveys.GetSurvey
{
    public class GetSurveyRequest : IRequest<Response<SurveyDTO>>
    {
        public GetSurveyRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
