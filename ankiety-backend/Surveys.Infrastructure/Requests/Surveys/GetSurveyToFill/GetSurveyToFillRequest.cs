using System;
using MediatR;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Requests.Surveys.GetSurveyToFill
{
    public class GetSurveyToFillRequest : IRequest<Response<SurveyToFillDTO>>
    {
        public GetSurveyToFillRequest(Guid surveyId)
        {
            SurveyId = surveyId;
        }

        public Guid SurveyId { get; set; }
    }
}
