using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Surveys.Application.Services.Definitions;
using Surveys.Common.Enum;
using System.Linq;
using System.Threading.Tasks;

namespace Surveys.Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionOrderController : Controller
    {
        private readonly IQuestionOrderService _questionOrderService;
        private readonly ILogger _logger;

        public QuestionOrderController(
            IQuestionOrderService questionOrderService,
            ILogger<QuestionOrderController> logger)
        {
            _questionOrderService = questionOrderService;
            _logger = logger;
        }

        /// <summary>
        /// Gets the question order by Survey Id.
        /// </summary>
        [HttpGet]
        [Route("GetBySurveyId/{surveyId:int}", Name = nameof(GetQuestionOrdersBySurveyId))]
        public async Task<IActionResult> GetQuestionOrdersBySurveyId(int surveyId)
        {
            _logger.LogInformation("Performing fetching request...");

            var questionOrder = await _questionOrderService.GetQuestionOrdersBySurveyId(surveyId);

            if (questionOrder.Count() == 0)
            {
                _logger.LogWarning("Unable to find records. There are no records.");
                return NotFound();
            }

            _logger.LogInformation("Records found. Sending response...");

            return Ok(questionOrder);
        }

        /// <summary>
        /// Gets the question order by Question Id.
        /// </summary>
        [HttpGet]
        [Route("GetByQuestionId{questionId:int}", Name = nameof(GetQuestionOrdersByQuestionId))]
        public async Task<IActionResult> GetQuestionOrdersByQuestionId(int questionId)
        {
            _logger.LogInformation("Performing fetching request...");

            var questionOrder = await _questionOrderService.GetQuestionOrdersByQuestionId(questionId);

            if (questionOrder.Count() == 0)
            {
                _logger.LogWarning("Unable to find records. There are no records.");
                return NotFound();
            }

            _logger.LogInformation("Records found. Sending response...");

            return Ok(questionOrder);
        }

        /// <summary>
        /// Change the order of a question to a certain position in the survey.
        /// </summary>
        [HttpGet]
        [Route("ChangeQuestionOrder", Name = nameof(ChangeQuestionOrder))]
        public async Task<IActionResult> ChangeQuestionOrder(int surveyId, int from, int to)
        {
            _logger.LogInformation("Performing update request...");
            _logger.LogInformation("Updating record...");

            var responseType = await _questionOrderService.ChangeQuestionOrderBySurveyId(surveyId, from, to);

            switch (responseType)
            {
                case ServiceResponseType.NotFound:
                    _logger.LogWarning("Some record was not found.");
                    return NotFound();
                case ServiceResponseType.Ok:
                    _logger.LogInformation("Record updated successfully.");
                    return Ok();
                default:
                    _logger.LogWarning("Something went wrong.");
                    return BadRequest();
            }
        }
    }
}
