using System;
using MediatR;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Requests.Surveys.UpdateSurvey
{
    public class UpdateSurveyRequest : IRequest<Response<SurveyDTO>>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
