using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Surveys.Application.Dto;
using Surveys.Application.Services.Definitions;
using Surveys.Common.Enum;
using Surveys.Presentation.Api.Common;
using System.Reflection;
using System.Threading.Tasks;

namespace Surveys.Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : Controller
    {
        private readonly ISurveyService _surveyService;
        private readonly ILogger _logger;

        public SurveyController(
            ISurveyService surveyService,
            ILogger<SurveyController> logger)
        {
            _surveyService = surveyService;
            _logger = logger;
        }

        /// <summary>
        /// Adds a Survey record.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Survey
        ///     {
        ///        "name": "New Survey",
        ///        "description": "Description of new Survey"
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SurveyDto dto)
        {
            _logger.LogInformation("Performing insert request...");

            if (Util.IsAnyNullOrEmpty(dto))
            {
                _logger.LogWarning("Body from request is empty.");
                return BadRequest();
            }

            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);

            _logger.LogInformation("Saving record...");

            await _surveyService.AddSurvey(dto);

            _logger.LogInformation("Record saved successfully. Redirecting to record info...");

            return RedirectToAction(nameof(GetSurveyById), new { id = dto.Id });
        }

        /// <summary>
        /// Retrieves all Surveys.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Performing fetching request...");

            var surveys = await _surveyService.GetSurveys();

            _logger.LogInformation("Sending response...");

            return Ok(new { surveys });
        }

        /// <summary>
        /// Finds a Survey by given Id.
        /// </summary>
        [HttpGet]
        [Route("{id:int}", Name = nameof(GetSurveyById))]
        public async Task<IActionResult> GetSurveyById(int id)
        {
            _logger.LogInformation("Performing fetching request...");

            var survey = await _surveyService.GetSurveyById(id);

            if (survey == null)
            {
                _logger.LogWarning("Record does not exist. Unable to fetch record.");
                return NotFound();
            }

            _logger.LogInformation("Record found. Sending response...");

            return Ok(survey);
        }

        /// <summary>
        /// Retrieves the number of total responses by Survey.
        /// </summary>
        [HttpGet]
        [Route(nameof(GetTotalResponses))]
        public async Task<IActionResult> GetTotalResponses()
        {
            _logger.LogInformation("Performing fetching request...");

            var results = await _surveyService.GetVSurveyResponsesAsync();

            _logger.LogInformation("Sending response...");

            return Ok(new { results });
        }

        /// <summary>
        /// Updates a Survey by given Id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/Survey/{id}
        ///     {
        ///        "name": "Survey modified",
        ///        "description": "Description of modified Survey"
        ///     }
        ///
        /// </remarks>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int? id, [FromBody] SurveyDto dto)
        {
            _logger.LogInformation("Performing update request...");

            if (Util.IsAnyNullOrEmpty(dto) || id == null)
            {
                _logger.LogWarning("Body from request is empty.");
                return BadRequest();
            }

            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);

            _logger.LogInformation("Updating record...");

            var response = await _surveyService.UpdateSurvey(id.Value, dto);

            if (response.Equals(ServiceResponseType.NotFound))
            {
                _logger.LogWarning("Record does not exist. Unable to update record.");
                return NotFound();
            }

            _logger.LogInformation("Record updated successfully.");

            return Ok();
        }

        /// <summary>
        /// Deletes a Survey by given Id.
        /// </summary>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Remove(int id)
        {
            _logger.LogInformation("Performing delete request...");

            var response = await _surveyService.DeleteSurvey(id);

            if (response.Equals(ServiceResponseType.NotFound))
            {
                _logger.LogWarning("Record does not exist. Unable to delete record.");
                return NotFound();
            }

            _logger.LogInformation("Record updated successfully.");

            return Ok();
        }
    }
}
