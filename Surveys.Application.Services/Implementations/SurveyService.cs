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

            surveysDto
                .ToList()
                .ForEach(s => s.QuestionOrders = questionOrdersDto
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

        public async Task<ServiceResponseType> UpdateSurvey(int id, SurveyDto dto)
        {
            var survey = await _surveyRepository.GetAsync(id);

            if (survey == null)
                return ServiceResponseType.NotFound;

            survey.Name = dto.Name ?? survey.Name;
            survey.Description = dto.Description ?? survey.Description;

            await _surveyRepository.Edit(survey);

            return ServiceResponseType.Ok;
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
    }
}
