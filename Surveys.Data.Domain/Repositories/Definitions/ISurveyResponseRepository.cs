using Surveys.Data.Domain.Definitions;
using Surveys.Data.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Surveys.Data.Domain.Repositories.Definitions
{
    public interface ISurveyResponseRepository : IRepository<SurveyResponse, int>
    {
        Task<IEnumerable<SurveyResponse>> GetSurveyResponsesByForeignId(int id, int caller);
    }
}
