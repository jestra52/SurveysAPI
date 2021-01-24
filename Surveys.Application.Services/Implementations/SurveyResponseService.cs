using AutoMapper;
using Surveys.Application.Dto;
using Surveys.Application.Services.Definitions;
using Surveys.Common.Enum;
using Surveys.Data.Domain.Repositories.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveys.Application.Services.Implementations
{
    public class SurveyResponseService : ISurveyResponseService
    {
        private readonly ISurveyResponseRepository _surveyResponseRepository;
        private readonly IMapper _mapper;

        public SurveyResponseService(
            ISurveyResponseRepository surveyResponseRepository,
            IMapper mapper)
        {
            _surveyResponseRepository = surveyResponseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SurveyResponseDto>> GetSurveyResponsesBySurveyId(int id)
        {
            var surveyResponses = await _surveyResponseRepository.GetSurveyResponsesByForeignId(id, (int)CallerType.Survey);

            return _mapper.Map<IEnumerable<SurveyResponseDto>>(surveyResponses);
        }

        public async Task<IEnumerable<SurveyResponseDto>> GetSurveyResponsesByRespondentId(int id)
        {
            var surveyResponses = await _surveyResponseRepository.GetSurveyResponsesByForeignId(id, (int)CallerType.Respondent);

            return _mapper.Map<IEnumerable<SurveyResponseDto>>(surveyResponses);
        }
    }
}
