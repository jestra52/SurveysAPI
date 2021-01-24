using Surveys.Application.Dto;
using Surveys.Common.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Surveys.Application.Services.Definitions
{
    public interface IQuestionService
    {
        Task AddQuestion(QuestionDto dto);
        Task<IEnumerable<QuestionDto>> GetQuestions();
        Task<QuestionDto> GetQuestionById(int id);
        Task<ServiceResponseType> UpdateQuestion(int id, QuestionDto dto);
        Task<ServiceResponseType> DeleteQuestion(int id);
    }
}
