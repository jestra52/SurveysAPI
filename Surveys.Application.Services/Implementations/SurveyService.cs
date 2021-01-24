using AutoMapper;
using Microsoft.Data.SqlClient;
using Surveys.Application.Dto;
using Surveys.Application.Services.Definitions;
using Surveys.Common.Enum;
using Surveys.Data.Domain.Entities;
using Surveys.Data.Domain.Repositories.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Surveys.Application.Services.Implementations
{
    public class SurveyService : ISurveyService
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IQuestionOrderRepository _questionOrderRepository;
        private readonly IMapper _mapper;

        public SurveyService(
            ISurveyRepository surveyRepository,
            IQuestionOrderRepository questionOrderRepository,
            IMapper mapper)
        {
            _surveyRepository = surveyRepository;
            _questionOrderRepository = questionOrderRepository;
            _mapper = mapper;
        }

        public async Task AddSurvey(SurveyDto dto)
        {
            var survey = _mapper.Map<Survey>(dto);

            await _surveyRepository.AddAsync(survey);

            dto.Id = survey.Id;
        }

        public async Task<IEnumerable<SurveyDto>> GetSurveys()
        {
            var surveys = await _surveyRepository.GetAllAsync();
            var questionOrders = await _questionOrderRepository.GetAllAsync();
            var surveysDto = _mapper.Map<IEnumerable<SurveyDto>>(surveys);
            var questionOrdersDto = _mapper.Map<IEnumerable<QuestionOrderDto>>(questionOrders);

            surveysDto.ToList().ForEach(s => s.QuestionOrders = questionOrdersDto
                .Where(q => q.SurveyId.Equals(s.Id.Value)));

            return _mapper.Map<IEnumerable<SurveyDto>>(surveys);
        }

        public async Task<SurveyDto> GetSurveyById(int id)
        {
            var survey = await _surveyRepository.GetAsync(id);
            var questionOrders = await _questionOrderRepository.GetQuestionOrdersByForeignId(id, (int)CallerType.Survey);
            var dto = _mapper.Map<SurveyDto>(survey);

            dto.QuestionOrders = _mapper.Map<IEnumerable<QuestionOrderDto>>(questionOrders);

            return dto;
        }

        public async Task<int> UpdateSurvey(SurveyDto dto)
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
                    ParameterName = "@Description",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Size = 1000,
                    Value = dto.Description ?? (object)DBNull.Value
                }
            };

            var affectedRows = await _surveyRepository.ExecuteSqlRawAsync("[dbo].[SP_UpdateSurvey] @Id, @Name, @Description", parameters);

            return affectedRows;
        }

        public async Task<bool> DeleteSurvey(int id)
        {
            var existingSurvey = await _surveyRepository.GetAsync(id);

            if (existingSurvey == null)
                return false;

            var param = new SqlParameter[]
            {
                new SqlParameter() {
                    ParameterName = "@QuestionId",
                    SqlDbType =  System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = DBNull.Value
                },
                new SqlParameter() {
                    ParameterName = "@SurveyId",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Size = 50,
                    Value = id
                }
            };

            await _surveyRepository.ExecuteSqlRawAsync("[dbo].[SP_DeleteQuestionOrdersBySurveyIdQuestionId] @QuestionId, @SurveyId", param);
            await _surveyRepository.RemoveAsync(existingSurvey);

            return true;
        }

        public async Task<IEnumerable<QuestionOrderDto>> GetQuestionOrdersBySurveyId(int id)
        {
            var questionOrders = await _questionOrderRepository.GetQuestionOrdersByForeignId(id, (int)CallerType.Survey);

            return _mapper.Map<IEnumerable<QuestionOrderDto>>(questionOrders);
        }
    }
}
