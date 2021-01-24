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
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly ILogger _logger;

        public QuestionController(
            IQuestionService questionService,
            ILogger<QuestionController> logger)
        {
            _questionService = questionService;
            _logger = logger;
        }

        /// <summary>
        /// Adds a Question record.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Question
        ///     {
        ///        "text": "Text of Question"
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] QuestionDto dto)
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

            await _questionService.AddQuestion(dto);

            _logger.LogInformation("Record saved successfully. Redirecting to record info...");

            return RedirectToAction(nameof(GetQuestionById), new { id = dto.Id });
        }

        /// <summary>
        /// Retrieves all Questions.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Performing fetching request...");

            var questions = await _questionService.GetQuestions();

            _logger.LogInformation("Sending response...");

            return Ok(new { questions });
        }

        /// <summary>
        /// Finds a Question by given Id.
        /// </summary>
        [HttpGet]
        [Route("{id:int}", Name = nameof(GetQuestionById))]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            _logger.LogInformation("Performing fetching request...");

            var question = await _questionService.GetQuestionById(id);

            if (question == null)
            {
                _logger.LogWarning("Record does not exist. Unable to fetch record.");
                return NotFound();
            }

            _logger.LogInformation("Record found. Sending response...");

            return Ok(question);
        }

        /// <summary>
        /// Updates a Question by given Id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/Question/{id}
        ///     {
        ///        "text": "Text of Question modified"
        ///     }
        ///
        /// </remarks>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int? id, [FromBody] QuestionDto dto)
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

            var response = await _questionService.UpdateQuestion(id.Value, dto);

            if (response.Equals(ServiceResponseType.NotFound))
            {
                _logger.LogWarning("Record does not exist. Unable to update record.");
                return NotFound();
            }

            _logger.LogInformation("Record updated successfully.");

            return Ok();
        }

        /// <summary>
        /// Deletes a Question by given Id.
        /// </summary>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Remove(int id)
        {
            _logger.LogInformation("Performing delete request...");

            var response = await _questionService.DeleteQuestion(id);

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
