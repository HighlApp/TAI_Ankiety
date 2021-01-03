using MediatR;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.DTO;
using System.Collections.Generic;

namespace Surveys.Infrastructure.Requests.Surveys.GetSurveys
{
    public class GetSurveysRequest : IRequest<Response<IEnumerable<SurveyDTO>>>
    {
    }
}
