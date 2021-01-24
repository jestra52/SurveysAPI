using Surveys.Common.Enum;
using Surveys.Data.Domain.Definitions;
using Surveys.Data.Domain.Entities;
using Surveys.Data.Domain.Implementations;
using Surveys.Data.Domain.Repositories.Definitions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Surveys.Data.Domain.Repositories
{
    public class SurveyResponseRepository : Repository<SurveyResponse, int>, ISurveyResponseRepository
    {
        public SurveyResponseRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {}

        public async Task<IEnumerable<SurveyResponse>> GetSurveyResponsesByForeignId(int id, int caller)
        {
            return await Task.Run(() => GetSet()
                .Where(e => caller.Equals((int)CallerType.Survey)
                    ? e.SurveyId.Equals(id)
                    : e.RespondentId.Equals(id)));
        }
    }
}
