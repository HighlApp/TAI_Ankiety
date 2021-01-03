using MediatR;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.DTO;
using System;

namespace Surveys.Infrastructure.Requests.Surveys.DeleteSurvey
{
    public class DeleteSurveyRequest : IRequest<Response<SurveyDTO>>
    {
        public Guid Id { get; set; }
    }
}
