using System;
using System.Collections.Generic;

namespace Surveys.Infrastructure.DTO
{
    public class FilledSurveyDTO
    {
        public Guid InvitationId { get; set; }

        public IEnumerable<QuestionAnswerDTO> Answers { get; set; }
    }
}
