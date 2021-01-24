using Microsoft.AspNetCore.Mvc;
using Surveys.Application.Services.Definitions;
using Surveys.Common.Enum;
using System.Threading.Tasks;

namespace Surveys.Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionOrderController : Controller
    {
        private readonly IQuestionOrderService _questionOrderService;

        public QuestionOrderController(IQuestionOrderService questionOrderService)
        {
            _questionOrderService = questionOrderService;
        }

        /// <summary>
        /// Gets the question order by Survey Id.
        /// </summary>
        [HttpGet]
        [Route("GetBySurveyId/{surveyId:int}", Name = nameof(GetQuestionOrdersBySurveyId))]
        public async Task<IActionResult> GetQuestionOrdersBySurveyId(int surveyId)
        {
            var questionOrder = await _questionOrderService.GetQuestionOrdersBySurveyId(surveyId);

            if (questionOrder == null)
                return NotFound();

            return Ok(questionOrder);
        }

        /// <summary>
        /// Gets the question order by Question Id.
        /// </summary>
        [HttpGet]
        [Route("GetByQuestionId{questionId:int}", Name = nameof(GetQuestionOrdersByQuestionId))]
        public async Task<IActionResult> GetQuestionOrdersByQuestionId(int questionId)
        {
            var questionOrder = await _questionOrderService.GetQuestionOrdersByQuestionId(questionId);

            if (questionOrder == null)
                return NotFound();

            return Ok(questionOrder);
        }

        /// <summary>
        /// Change the order of a question to a certain position in the survey.
        /// </summary>
        [HttpGet]
        [Route("ChangeQuestionOrder", Name = nameof(ChangeQuestionOrder))]
        public async Task<IActionResult> ChangeQuestionOrder(int surveyId, int from, int to)
        {
            var responseType = await _questionOrderService.ChangeQuestionOrderBySurveyId(surveyId, from, to);

            return responseType switch
            {
                ServiceResponseType.NotFound => NotFound(),
                ServiceResponseType.Ok => Ok(),
                _ => BadRequest(),
            };
        }
    }
}
