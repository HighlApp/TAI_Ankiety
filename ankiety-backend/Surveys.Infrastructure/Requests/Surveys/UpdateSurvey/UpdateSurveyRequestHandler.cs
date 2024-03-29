﻿using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.Services.Interfaces;

namespace Surveys.Infrastructure.Requests.Surveys.UpdateSurvey
{
    class UpdateSurveyRequestHandler : IRequestHandler<UpdateSurveyRequest, Response<SurveyDTO>>
    {
        private readonly ISurveysService _surveysService;

        public UpdateSurveyRequestHandler(ISurveysService surveysService)
        {
            _surveysService = surveysService;
        }

        public async Task<Response<SurveyDTO>> Handle(
            UpdateSurveyRequest request, CancellationToken cancellationToken)
            => await _surveysService.UpdateAsync(request);
    }
}
