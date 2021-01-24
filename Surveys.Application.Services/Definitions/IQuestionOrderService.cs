using Surveys.Application.Dto;
using Surveys.Common.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Surveys.Application.Services.Definitions
{
    public interface IQuestionOrderService
    {
        Task<IEnumerable<QuestionOrderDto>> GetQuestionOrdersBySurveyId(int id);
        Task<ServiceResponseType> ChangeQuestionOrderBySurveyId(int surveyId, int from, int to);
    }
}
