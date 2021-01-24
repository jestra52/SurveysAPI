using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Surveys.Application.Dto;
using Surveys.Application.Services.Definitions;
using Surveys.Common.Enum;
using Surveys.Presentation.Api.Common;
using System.Threading.Tasks;

namespace Surveys.Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RespondentController : Controller
    {
        private readonly IRespondentService _respondentService;
        private readonly ILogger _logger;

        public RespondentController(
            IRespondentService respondentService,
            ILogger<RespondentController> logger)
        {
            _respondentService = respondentService;
            _logger = logger;
        }

        /// <summary>
        /// Adds a Respondent record.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Survey
        ///     {
        ///        "name": "New Respondent",
        ///        "hashedPassword": "Hashed password of new Respondent",
        ///        "email": "New Respondent's email",
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] RespondentDto dto)
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

            await _respondentService.AddRespondent(dto);

            _logger.LogInformation("Record saved successfully. Redirecting to record info...");

            return RedirectToAction(nameof(GetRespondentById), new { id = dto.Id });
        }

        /// <summary>
        /// Retrieves all Respondents.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Performing fetching request...");

            var respondents = await _respondentService.GetRespondents();

            _logger.LogInformation("Sending response...");

            return Ok(new { respondents });
        }

        /// <summary>
        /// Finds a Respondent by given Id.
        /// </summary>
        [HttpGet]
        [Route("{id:int}", Name = nameof(GetRespondentById))]
        public async Task<IActionResult> GetRespondentById(int id)
        {
            _logger.LogInformation("Performing fetching request...");

            var respondent = await _respondentService.GetRespondentById(id);

            if (respondent == null)
            {
                _logger.LogWarning("Record does not exist. Unable to fetch record.");
                return NotFound();
            }

            _logger.LogInformation("Record found. Sending response...");

            return Ok(respondent);
        }

        /// <summary>
        /// Updates a Respondent by given Id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/Survey
        ///     {
        ///        "name": "Modified Respondent",
        ///        "hashedPassword": "Hashed password of modified Respondent",
        ///        "email": "Modified Respondent's email",
        ///     }
        ///
        /// </remarks>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int? id, [FromBody] RespondentDto dto)
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

            var response = await _respondentService.UpdateRespondent(id.Value, dto);

            if (response.Equals(ServiceResponseType.NotFound))
            {
                _logger.LogWarning("Record does not exist. Unable to update record.");
                return NotFound();
            }

            return Ok();
        }

        /// <summary>
        /// Deletes a Respondent by given Id.
        /// </summary>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Remove(int id)
        {
            _logger.LogInformation("Performing delete request...");

            var isRespondentRemoved = await _respondentService.DeleteRespondent(id);

            if (!isRespondentRemoved)
            {
                _logger.LogWarning("Record does not exist. Unable to delete record.");
                return NotFound();
            }

            return Ok();
        }
    }
}
