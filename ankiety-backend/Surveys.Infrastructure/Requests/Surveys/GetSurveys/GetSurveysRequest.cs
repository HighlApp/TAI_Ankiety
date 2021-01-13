using MediatR;
using System.Collections.Generic;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Requests.Surveys.GetSurveys
{
    public class GetSurveysRequest : IRequest<Response<IEnumerable<SurveyDTO>>>
    {
    }
}
