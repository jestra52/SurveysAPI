using Surveys.Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Surveys.Application.Services.Definitions
{
    public interface IQuestionService
    {
        Task AddQuestion(QuestionDto dto);
        Task<IEnumerable<QuestionDto>> GetQuestions();
        Task<QuestionDto> GetQuestionById(int id);
        Task<int> UpdateQuestion(QuestionDto dto);
        Task<bool> DeleteQuestion(int id);
    }
}
