using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Surveys.Application.Services.Definitions;
using System.Linq;
using System.Threading.Tasks;

namespace Surveys.Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyResponseController : Controller
    {
        private readonly ISurveyResponseService _surveyResponseService;
        private readonly ILogger _logger;

        public SurveyResponseController(
            ISurveyResponseService surveyResponseService,
            ILogger<SurveyResponseController> logger)
        {
            _surveyResponseService = surveyResponseService;
            _logger = logger;
        }

        /// <summary>
        /// Gets the survey responses by Survey Id.
        /// </summary>
        [HttpGet]
        [Route("GetBySurveyId/{surveyId:int}", Name = nameof(GetSurveyResponsesBySurveyId))]
        public async Task<IActionResult> GetSurveyResponsesBySurveyId(int surveyId)
        {
            _logger.LogInformation("Performing fetching request...");

            var surveyResponses = await _surveyResponseService.GetSurveyResponsesBySurveyId(surveyId);

            if (surveyResponses.Count() == 0)
            {
                _logger.LogWarning("Unable to find records. There are no records.");
                return NotFound();
            }

            _logger.LogInformation("Records found. Sending response...");

            return Ok(surveyResponses);
        }

        /// <summary>
        /// Gets the survey responses by Respondent Id.
        /// </summary>
        [HttpGet]
        [Route("GetByRespondentId/{respondentId:int}", Name = nameof(GetSurveyResponsesByRespondentId))]
        public async Task<IActionResult> GetSurveyResponsesByRespondentId(int respondentId)
        {
            _logger.LogInformation("Performing fetching request...");

            var surveyResponses = await _surveyResponseService.GetSurveyResponsesByRespondentId(respondentId);

            if (surveyResponses.Count() == 0)
            {
                _logger.LogWarning("Unable to find records. There are no records.");
                return NotFound();
            }

            _logger.LogInformation("Records found. Sending response...");

            return Ok(surveyResponses);
        }
    }
}
