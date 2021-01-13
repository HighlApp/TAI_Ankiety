using System;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Surveys.Infrastructure.DTO;
using System.Collections.Generic;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.Requests.Surveys.GetSurvey;
using Surveys.Infrastructure.Requests.Surveys.GetSurveys;
using Surveys.Infrastructure.Requests.Surveys.PostSurvey;
using Surveys.Infrastructure.Requests.Surveys.UpdateSurvey;
using Surveys.Infrastructure.Requests.Surveys.DeleteSurvey;
using Surveys.Infrastructure.Requests.Surveys.GetSurveyToFill;
using Surveys.Infrastructure.Requests.Surveys.PostFilledSurvey;

namespace Surveys.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveysController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SurveysController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<Response<IEnumerable<SurveyDTO>>> GetSurveys()
            => await _mediator.Send(new GetSurveysRequest());

        [HttpGet("{id}")]
        public async Task<Response<SurveyDTO>> GetSurvey(Guid id)
            => await _mediator.Send(new GetSurveyRequest(id));

        [HttpPost]
        public async Task<Response<SurveyDTO>> PostSurvey(PostSurveyRequest request)
            => await _mediator.Send(request);

        [HttpDelete("{id}")]
        public async Task<Response<SurveyDTO>> DeleteSurvey(Guid id)
             => await _mediator.Send(new DeleteSurveyRequest(id));

        [HttpPut]
        public async Task<Response<SurveyDTO>> UpdateSurvey(UpdateSurveyRequest request)
            => await _mediator.Send(request);

        [HttpGet]
        [Route("fill/{id}")]
        public async Task<Response<SurveyToFillDTO>> GetSurveyToFill(Guid surveyId)
            => await _mediator.Send(new GetSurveyToFillRequest(surveyId));

        [HttpPost]
        [Route("fill")]
        public async Task<Response<StatusResponseDTO>> PostFilledSurvey(PostFilledSurveyRequest request)
            => await _mediator.Send(request);
    }
}
