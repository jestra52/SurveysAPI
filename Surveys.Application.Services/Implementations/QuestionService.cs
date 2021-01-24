using AutoMapper;
using Microsoft.Data.SqlClient;
using Surveys.Application.Dto;
using Surveys.Application.Services.Definitions;
using Surveys.Common.Enum;
using Surveys.Data.Domain.Entities;
using Surveys.Data.Domain.Repositories.Definitions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Surveys.Application.Services.Implementations
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public QuestionService(
            IQuestionRepository questionRepository,
            IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public async Task AddQuestion(QuestionDto dto)
        {
            var question = _mapper.Map<Question>(dto);

            await _questionRepository.AddAsync(question);

            dto.Id = question.Id;
        }

        public async Task<QuestionDto> GetQuestionById(int id)
        {
            var question = await _questionRepository.GetAsync(id);

            return _mapper.Map<QuestionDto>(question);
        }

        public async Task<IEnumerable<QuestionDto>> GetQuestions()
        {
            var questions = await _questionRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<QuestionDto>>(questions);
        }

        public async Task<ServiceResponseType> UpdateQuestion(int id, QuestionDto dto)
        {
            var question = await _questionRepository.GetAsync(id);

            if (question == null)
                return ServiceResponseType.NotFound;

            question.Text = dto.Text ?? question.Text;

            await _questionRepository.Edit(question);

            return ServiceResponseType.Ok;
        }

        public async Task<ServiceResponseType> DeleteQuestion(int id)
        {
            var existingQuestion = await _questionRepository.GetAsync(id);

            if (existingQuestion == null)
                return ServiceResponseType.NotFound;

            var param = new SqlParameter[]
            {
                new SqlParameter() {
                    ParameterName = "@QuestionId",
                    SqlDbType =  System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = id
                },
                new SqlParameter() {
                    ParameterName = "@SurveyId",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Size = 50,
                    Value = DBNull.Value
                }
            };

            await _questionRepository.ExecuteSqlRawAsync("[dbo].[SP_DeleteQuestionOrdersBySurveyIdQuestionId] @QuestionId, @SurveyId", param);
            await _questionRepository.RemoveAsync(existingQuestion);

            return ServiceResponseType.Ok;
        }
    }
}
