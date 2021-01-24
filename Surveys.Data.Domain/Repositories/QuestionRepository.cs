using Surveys.Data.Domain.Definitions;
using Surveys.Data.Domain.Entities;
using Surveys.Data.Domain.Implementations;
using Surveys.Data.Domain.Repositories.Definitions;

namespace Surveys.Data.Domain.Repositories
{
    public class QuestionRepository : Repository<Question, int>, IQuestionRepository
    {
        public QuestionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {}
    }
}
