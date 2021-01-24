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
        private readonly IQuestionOrderRepository _questionOrderRepository;
        private readonly IMapper _mapper;

        public QuestionService(
            IQuestionRepository questionRepository,
            IQuestionOrderRepository questionOrderRepository,
            IMapper mapper)
        {
            _questionRepository = questionRepository;
            _questionOrderRepository = questionOrderRepository;
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

        public async Task<int> UpdateQuestion(QuestionDto dto)
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
                    ParameterName = "@Text",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Size = 200,
                    Value = dto.Text ?? (object)DBNull.Value
                }
            };

            var affectedRows = await _questionRepository.ExecuteSqlRawAsync("[dbo].[SP_UpdateQuestion] @Id, @Text", parameters);

            return affectedRows;
        }

        public async Task<bool> DeleteQuestion(int id)
        {
            var existingQuestion = await _questionRepository.GetAsync(id);

            if (existingQuestion == null)
                return false;

            await _questionRepository.RemoveAsync(existingQuestion);

            return true;
        }
    }
}
