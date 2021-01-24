using AutoMapper;
using Surveys.Application.Dto;
using Surveys.Application.Services.Definitions;
using Surveys.Common.Enum;
using Surveys.Data.Domain.Repositories.Definitions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Surveys.Application.Services.Implementations
{
    public class ResponseService : IResponseService
    {
        private readonly IResponseRepository _responseRepository;
        private readonly IMapper _mapper;

        public ResponseService(
            IResponseRepository responseRepository,
            IMapper mapper)
        {
            _responseRepository = responseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ResponseDto>> GetResponsesBySurveyResponseId(int id)
        {
            var responses = await _responseRepository.GetResponseByForeignId(id, (int)CallerType.SurveyResponse);

            return _mapper.Map<IEnumerable<ResponseDto>>(responses);
        }

        public async Task<IEnumerable<ResponseDto>> GetResponsesByQuestionId(int id)
        {
            var responses = await _responseRepository.GetResponseByForeignId(id, (int)CallerType.Question);

            return _mapper.Map<IEnumerable<ResponseDto>>(responses);
        }

        public async Task<IEnumerable<ResponseDto>> GetResponsesByRespondentId(int id)
        {
            var responses = await _responseRepository.GetResponseByForeignId(id, (int)CallerType.Respondent);

            return _mapper.Map<IEnumerable<ResponseDto>>(responses);
        }
    }
}
