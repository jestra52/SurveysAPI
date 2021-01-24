using Surveys.Common.Enum;
using Surveys.Data.Domain.Definitions;
using Surveys.Data.Domain.Entities;
using Surveys.Data.Domain.Implementations;
using Surveys.Data.Domain.Repositories.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Surveys.Data.Domain.Repositories
{
    public class ResponseRepository : Repository<Response, int>, IResponseRepository
    {
        public ResponseRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public async Task<IEnumerable<Response>> GetResponseByForeignId(int id, int caller)
        {
            return await Task.Run(() => GetSet()
                .Where(e => (e.SurveyResponseId.Equals(id) && caller.Equals((int)CallerType.SurveyResponse))
                    || (e.QuestionId.Equals(id) && caller.Equals((int)CallerType.Question))
                    || (e.RespondentId.Equals(id) && caller.Equals((int)CallerType.Respondent))));
        }
    }
}
