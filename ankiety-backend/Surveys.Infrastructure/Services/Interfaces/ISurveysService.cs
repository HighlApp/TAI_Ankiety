using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Requests.Surveys.PostSurvey;
using Surveys.Infrastructure.Requests.Surveys.UpdateSurvey;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Surveys.Infrastructure.Services.Interfaces
{
    public interface ISurveysService
    {
        Task<Response<SurveyDTO>> GetByIdAsync(Guid id);
        Task<Response<IEnumerable<SurveyDTO>>> GetAllAsync();
        Task<Response<SurveyDTO>> DeleteAsync(Guid id);
        Task<Response<SurveyDTO>> UpdateAsync(UpdateSurveyRequest request);
        Task<Response<SurveyDTO>> PostAsync(PostSurveyRequest request);
    }
}
