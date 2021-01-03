using MediatR;
using Surveys.Core.Enums;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.DTO;
using System.Collections.Generic;

namespace Surveys.Infrastructure.Requests.Surveys.PostSurvey
{
    public class PostSurveyRequest : IRequest<Response<SurveyDTO>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SurveyType SurveyType { get; set; }
        public int? QuestionsOnPage { get; set; }

        //public bool Sent { get; set; }
        //public int SentInvitations { get; set; }
        //public int FilledInvitations { get; set; }
        public IEnumerable<QuestionDTO> Questions { get; set; }
    }
}
