using Microsoft.AspNetCore.Mvc;
using Surveys.Application.Dto;
using Surveys.Application.Services.Definitions;
using System.Threading.Tasks;

namespace Surveys.Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RespondentController : Controller
    {
        private readonly IRespondentService _respondentService;

        public RespondentController(IRespondentService respondentService)
        {
            _respondentService = respondentService;
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
            if (dto == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);

            await _respondentService.AddRespondent(dto);

            return RedirectToAction(nameof(GetRespondentById), new { id = dto.Id });
        }

        /// <summary>
        /// Retrieves all Respondents.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var respondents = await _respondentService.GetRespondents();

            return Ok(new { respondents });
        }

        /// <summary>
        /// Finds a Respondent by given Id.
        /// </summary>
        [HttpGet]
        [Route("{id:int}", Name = nameof(GetRespondentById))]
        public async Task<IActionResult> GetRespondentById(int id)
        {
            var respondent = await _respondentService.GetRespondentById(id);

            if (respondent == null)
                return NotFound();

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
            if (dto == null || id == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);

            var respondent = await _respondentService.GetRespondentById(id.Value);

            if (respondent == null)
                return NotFound();

            dto.Id = id;

            var rowsAffected = await _respondentService.UpdateRespondent(dto);

            if (rowsAffected == 0 || rowsAffected > 1)
                return Problem();

            return Ok();
        }

        /// <summary>
        /// Deletes a Respondent by given Id.
        /// </summary>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Remove(int id)
        {
            var isRespondentRemoved = await _respondentService.DeleteRespondent(id);

            if (!isRespondentRemoved)
                return NotFound();

            return Ok();
        }
    }
}
