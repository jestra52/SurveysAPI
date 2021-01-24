using AutoMapper;
using Microsoft.Data.SqlClient;
using Surveys.Application.Dto;
using Surveys.Application.Services.Definitions;
using Surveys.Data.Domain.Entities;
using Surveys.Data.Domain.Repositories.Definitions;
using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task<int> UpdateRespondent(RespondentDto dto)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter() {
                    ParameterName = "@Id",
                    SqlDbType =  System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = dto.Id
                },
                new SqlParameter() {
                    ParameterName = "@Name",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Size = 50,
                    Value = dto.Name ?? (object)DBNull.Value
                },
                new SqlParameter() {
                    ParameterName = "@HashedPassword",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Size = 1000,
                    Value = dto.HashedPassword ?? (object)DBNull.Value
                },
                new SqlParameter() {
                    ParameterName = "@Email",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Size = 254,
                    Value = dto.Email ?? (object)DBNull.Value
                }
            };

            var affectedRows = await _respondentRepository.ExecuteSqlRawAsync("[dbo].[SP_UpdateRespondent] @Id, @Name, @HashedPassword, @Email", parameters);

            return affectedRows;
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
