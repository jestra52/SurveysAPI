using Surveys.Application.Dto;
using Surveys.Common.Enum;
using Surveys.Data.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Surveys.Application.Services.Definitions
{
    public interface ISurveyService
    {
        Task AddSurvey(SurveyDto dto);
        Task<IEnumerable<SurveyDto>> GetSurveys();
        Task<SurveyDto> GetSurveyById(int id);
        Task<ServiceResponseType> UpdateSurvey(int id, SurveyDto dto);
        Task<bool> DeleteSurvey(int id);
        Task<IEnumerable<VSurveyResponsesDto>> GetVSurveyResponsesAsync();
    }
}
