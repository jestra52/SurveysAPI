using Surveys.Data.Domain.Definitions;
using Surveys.Data.Domain.Entities;
using Surveys.Data.Domain.Implementations;
using Surveys.Data.Domain.Repositories.Definitions;

namespace Surveys.Data.Domain.Repositories
{
    public class VSurveyResponsesRepository : Repository<VSurveyResponses, int>, IVSurveyResponsesRepository
    {
        public VSurveyResponsesRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }
    }
}
