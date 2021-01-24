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
    public class QuestionOrderService : IQuestionOrderService
    {
        private readonly IQuestionOrderRepository _questionOrderRepository;
        private readonly IMapper _mapper;

        public QuestionOrderService(
            IQuestionOrderRepository questionOrderRepository,
            IMapper mapper)
        {
            _questionOrderRepository = questionOrderRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<QuestionOrderDto>> GetQuestionOrdersBySurveyId(int id)
        {
            var questionOrders = await _questionOrderRepository.GetQuestionOrdersByForeignId(id, (int)CallerType.Survey);

            return _mapper.Map<IEnumerable<QuestionOrderDto>>(questionOrders).OrderBy(q => q.OrderNbr);
        }

        public async Task<IEnumerable<QuestionOrderDto>> GetQuestionOrdersByQuestionId(int id)
        {
            var questionOrders = await _questionOrderRepository.GetQuestionOrdersByForeignId(id, (int)CallerType.Question);

            return _mapper.Map<IEnumerable<QuestionOrderDto>>(questionOrders).OrderBy(q => q.OrderNbr);
        }

        public async Task<ServiceResponseType> ChangeQuestionOrderBySurveyId(int surveyId, int from, int to)
        {
            var questionOrders = await _questionOrderRepository.GetQuestionOrdersByForeignId(surveyId, (int)CallerType.Survey);

            if (questionOrders.Count() == 0)
                return ServiceResponseType.NotFound;

            var sortedByOrder = questionOrders.OrderBy(q => q.OrderNbr).ToList();
            var fromQuestionOrder = sortedByOrder.Where(q => q.OrderNbr.Equals(from)).FirstOrDefault();
            var toQuestionOrder = sortedByOrder.Where(q => q.OrderNbr.Equals(to)).FirstOrDefault();

            if (fromQuestionOrder == null || toQuestionOrder == null)
                return ServiceResponseType.NotFound;

            fromQuestionOrder.OrderNbr = to;
            sortedByOrder[from - 1] = fromQuestionOrder;

            if (from > to)
            {
                sortedByOrder.ForEach(q =>
                {
                    if (q.OrderNbr >= to && q.OrderNbr <= from && !q.QuestionId.Equals(fromQuestionOrder.QuestionId))
                        q.OrderNbr += 1;
                });
            }
            else
            {
                sortedByOrder.ForEach(q =>
                {
                    if (q.OrderNbr >= from && q.OrderNbr <= to && !q.QuestionId.Equals(fromQuestionOrder.QuestionId))
                        q.OrderNbr -= 1;
                });
            }

            await _questionOrderRepository.EditMany(sortedByOrder);

            return ServiceResponseType.Ok;
        }
    }
}
