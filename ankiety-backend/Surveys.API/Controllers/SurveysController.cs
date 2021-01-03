using MediatR;
using Microsoft.AspNetCore.Mvc;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Requests.Surveys.DeleteSurvey;
using Surveys.Infrastructure.Requests.Surveys.GetSurvey;
using Surveys.Infrastructure.Requests.Surveys.GetSurveys;
using Surveys.Infrastructure.Requests.Surveys.PostSurvey;
using Surveys.Infrastructure.Requests.Surveys.UpdateSurvey;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            => await _mediator.Send(new GetSurveyRequest { Id = id });

        [HttpPost]
        public async Task<Response<SurveyDTO>> PostSurvey(PostSurveyRequest request)
            => await _mediator.Send(request);

        [HttpDelete("{id}")]
        public async Task<Response<SurveyDTO>> DeleteSurvey(Guid id)
             => await _mediator.Send(new DeleteSurveyRequest { Id = id });

        [HttpPut("{id}")]
        public async Task<Response<SurveyDTO>> UpdateSurvey(UpdateSurveyRequest request, Guid id)
        {
            request.Id = id;
            return await _mediator.Send(request);
        }
    }
}
