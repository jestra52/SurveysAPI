using Surveys.Data.Domain.Definitions;
using Surveys.Data.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Surveys.Data.Domain.Repositories.Definitions
{
    public interface IResponseRepository : IRepository<Response, int>
    {
        Task<IEnumerable<Response>> GetResponseByForeignId(int id, int caller);
    }
}
