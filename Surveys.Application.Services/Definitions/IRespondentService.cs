using Surveys.Application.Dto;
using Surveys.Common.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Surveys.Application.Services.Definitions
{
    public interface IRespondentService
    {
        Task AddRespondent(RespondentDto dto);
        Task<IEnumerable<RespondentDto>> GetRespondents();
        Task<RespondentDto> GetRespondentById(int id);
        Task<ServiceResponseType> UpdateRespondent(int id, RespondentDto dto);
        Task<bool> DeleteRespondent(int id);
    }
}
