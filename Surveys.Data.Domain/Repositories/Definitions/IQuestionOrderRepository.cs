using Surveys.Data.Domain.Definitions;
using Surveys.Data.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Surveys.Data.Domain.Repositories.Definitions
{
    public interface IQuestionOrderRepository : IRepository<QuestionOrder, int>
    {
        Task<IEnumerable<QuestionOrder>> GetQuestionOrdersByForeignId(int id, int caller);
    }
}
