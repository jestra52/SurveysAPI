using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Surveys.Application.Services.Definitions;
using System.Linq;
using System.Threading.Tasks;

namespace Surveys.Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : Controller
    {
        private readonly IResponseService _responseService;
        private readonly ILogger _logger;

        public ResponseController(
            IResponseService responseService,
            ILogger<ResponseController> logger)
        {
            _responseService = responseService;
            _logger = logger;
        }

        /// <summary>
        /// Gets the responses of a Survey response by its Id.
        /// </summary>
        [HttpGet]
        [Route("GetBySurveyResponseId/{surveyResponseId:int}", Name = nameof(GetResponsesBySurveyResponseId))]
        public async Task<IActionResult> GetResponsesBySurveyResponseId(int surveyResponseId)
        {
            _logger.LogInformation("Performing fetching request...");

            var responses = await _responseService.GetResponsesBySurveyResponseId(surveyResponseId);

            if (responses.Count() == 0)
            {
                _logger.LogWarning("Unable to find records. There are no records.");
                return NotFound();
            }

            _logger.LogInformation("Records found. Sending response...");

            return Ok(responses);
        }

        /// <summary>
        /// Gets the responses by Question id.
        /// </summary>
        [HttpGet]
        [Route("GetByQuestionId/{questionId:int}", Name = nameof(GetResponsesByQuestionId))]
        public async Task<IActionResult> GetResponsesByQuestionId(int questionId)
        {
            _logger.LogInformation("Performing fetching request...");

            var responses = await _responseService.GetResponsesByQuestionId(questionId);

            if (responses.Count() == 0)
            {
                _logger.LogWarning("Unable to find records. There are no records.");
                return NotFound();
            }

            _logger.LogInformation("Records found. Sending response...");

            return Ok(responses);
        }

        /// <summary>
        /// Gets the responses of a Respondent by its id.
        /// </summary>
        [HttpGet]
        [Route("GetByRespondentId/{respondentId:int}", Name = nameof(GetResponsesByRespondentId))]
        public async Task<IActionResult> GetResponsesByRespondentId(int respondentId)
        {
            _logger.LogInformation("Performing fetching request...");

            var responses = await _responseService.GetResponsesBySurveyResponseId(respondentId);

            if (responses.Count() == 0)
            {
                _logger.LogWarning("Unable to find records. There are no records.");
                return NotFound();
            }

            _logger.LogInformation("Records found. Sending response...");

            return Ok(responses);
        }
    }
}
