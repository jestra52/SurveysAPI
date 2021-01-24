using Surveys.Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Surveys.Application.Services.Definitions
{
    public interface ISurveyService
    {
        Task AddSurvey(SurveyDto dto);
        Task<IEnumerable<SurveyDto>> GetSurveys();
        Task<SurveyDto> GetSurveyById(int id);
        Task<int> UpdateSurvey(SurveyDto dto);
        Task<bool> DeleteSurvey(int id);
        Task<IEnumerable<QuestionOrderDto>> GetQuestionOrdersBySurveyId(int id);
    }
}
