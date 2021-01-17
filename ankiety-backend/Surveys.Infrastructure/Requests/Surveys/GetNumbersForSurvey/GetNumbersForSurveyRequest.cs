using MediatR;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.DTO;
using System;

namespace Surveys.Infrastructure.Requests.Surveys.GetNumbersForSurvey
{
    public class GetNumbersForSurveyRequest : IRequest<Response<SurveyNumbersDTO>>
    {
        public GetNumbersForSurveyRequest(Guid surveyId)
        {
            Id = surveyId;
        }

        public Guid Id { get; set; }
    }
}
