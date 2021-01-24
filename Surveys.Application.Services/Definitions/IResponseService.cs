using Surveys.Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Surveys.Application.Services.Definitions
{
    public interface IResponseService
    {
        Task<IEnumerable<ResponseDto>> GetResponsesBySurveyResponseId(int id);
        Task<IEnumerable<ResponseDto>> GetResponsesByQuestionId(int id);
        Task<IEnumerable<ResponseDto>> GetResponsesByRespondentId(int id);
    }
}
