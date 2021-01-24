using Surveys.Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Surveys.Application.Services.Definitions
{
    public interface ISurveyResponseService
    {
        Task<IEnumerable<SurveyResponseDto>> GetSurveyResponsesBySurveyId(int id);
        Task<IEnumerable<SurveyResponseDto>> GetSurveyResponsesByRespondentId(int id);
    }
}
