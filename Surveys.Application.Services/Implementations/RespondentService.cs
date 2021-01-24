using AutoMapper;
using Surveys.Application.Dto;
using Surveys.Application.Services.Definitions;
using Surveys.Common.Enum;
using Surveys.Data.Domain.Entities;
using Surveys.Data.Domain.Repositories.Definitions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Surveys.Application.Services.Implementations
{
    public class RespondentService : IRespondentService
    {
        private readonly IRespondentRepository _respondentRepository;
        private readonly IMapper _mapper;

        public RespondentService(IRespondentRepository respondentRepository, IMapper mapper)
        {
            _respondentRepository = respondentRepository;
            _mapper = mapper;
        }

        public async Task AddRespondent(RespondentDto dto)
        {
            var respondent = _mapper.Map<Respondent>(dto);

            await _respondentRepository.AddAsync(respondent);

            dto.Id = respondent.Id;
        }

        public async Task<IEnumerable<RespondentDto>> GetRespondents()
        {
            var respondents = await _respondentRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<RespondentDto>>(respondents);
        }

        public async Task<RespondentDto> GetRespondentById(int id)
        {
            var respondent = await _respondentRepository.GetAsync(id);

            return _mapper.Map<RespondentDto>(respondent);
        }

        public async Task<ServiceResponseType> UpdateRespondent(int id, RespondentDto dto)
        {
            var respondent = await _respondentRepository.GetAsync(id);

            if (respondent == null)
                return ServiceResponseType.NotFound;

            respondent.Name = dto.Name ?? respondent.Name;
            respondent.HashedPassword = dto.Email ?? respondent.HashedPassword;
            respondent.Email = dto.Email ?? respondent.Email;

            await _respondentRepository.Edit(respondent);

            return ServiceResponseType.Ok;
        }

        public async Task<bool> DeleteRespondent(int id)
        {
            var existingRespondent = await _respondentRepository.GetAsync(id);

            if (existingRespondent == null)
                return false;

            await _respondentRepository.RemoveAsync(existingRespondent);

            return true;
        }
    }
}
