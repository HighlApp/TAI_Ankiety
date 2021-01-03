using MediatR;
using Microsoft.AspNetCore.Mvc;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Requests.Questions.DeleteQuestion;
using Surveys.Infrastructure.Requests.Questions.GetQuestion;
using Surveys.Infrastructure.Requests.Questions.PostQuestion;
using Surveys.Infrastructure.Requests.Questions.UpdateQuestion;
using System;
using System.Threading.Tasks;

namespace Surveys.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuestionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<Response<QuestionDTO>> GetQuestion(Guid id)
             => await _mediator.Send(new GetQuestionRequest { Id = id });

        [HttpPost("{surveyId}")]
        public async Task<Response<QuestionDTO>> PostQuestion(PostQuestionRequest request, Guid surveyId)
        {
            request.SurveyId = surveyId;
            return await _mediator.Send(request);
        } 


        [HttpDelete("{id}")]
        public async Task<Response<QuestionDTO>> DeleteQuestion(Guid id)
             => await _mediator.Send(new DeleteQuestionRequest { Id = id });

        [HttpPut]
        public async Task<Response<QuestionDTO>> UpdateQuestion(UpdateQuestionRequest request)
             => await _mediator.Send(request);

        //[HttpDelete]
        //[Route("option/{id}")]
        //public async Task<Response> DeleteOption(Guid id)
    }
}
