using Surveys.Application.Dto;
using Surveys.Application.Services.Definitions;
using Surveys.Common.Enum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Surveys.Tests.Integration
{
    public class MockSurveyService : ISurveyService
    {
        private readonly IEnumerable<SurveyDto> _surveys;

        public MockSurveyService()
        {
            _surveys = new List<SurveyDto>
            {
                new SurveyDto()
                {
                    Id = 1,
                    Name = "Survey 1",
                    Description = "Description of survey 1 modified"
                },
                new SurveyDto()
                {
                    Id = 2,
                    Name = "Survey 2",
                    Description = "Description of survey 2"
                },
                new SurveyDto()
                {
                    Id = 3,
                    Name = "Survey 3",
                    Description = "Description of survey 3"
                },
                new SurveyDto()
                {
                    Id = 5,
                    Name = "Survey 3",
                    Description = "Description of survey 3"
                }
            };
        }

        public Task AddSurvey(SurveyDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteSurvey(int id)
        {
            throw new NotImplementedException();
        }

        public Task<SurveyDto> GetSurveyById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SurveyDto>> GetSurveys()
        {
            return await Task.Run(() => _surveys);
        }

        public Task<IEnumerable<VSurveyResponsesDto>> GetVSurveyResponsesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponseType> UpdateSurvey(int id, SurveyDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
