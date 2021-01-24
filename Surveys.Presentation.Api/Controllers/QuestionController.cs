using Microsoft.AspNetCore.Mvc;
using Surveys.Application.Dto;
using Surveys.Application.Services.Definitions;
using Surveys.Common.Enum;
using System.Threading.Tasks;

namespace Surveys.Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
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
            if (dto == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);

            await _questionService.AddQuestion(dto);

            return RedirectToAction(nameof(GetQuestionById), new { id = dto.Id });
        }

        /// <summary>
        /// Retrieves all Questions.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var questions = await _questionService.GetQuestions();

            return Ok(new { questions });
        }

        /// <summary>
        /// Finds a Question by given Id.
        /// </summary>
        [HttpGet]
        [Route("{id:int}", Name = nameof(GetQuestionById))]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            var question = await _questionService.GetQuestionById(id);

            if (question == null)
                return NotFound();

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
            if (dto == null || id == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);

            var response = await _questionService.UpdateQuestion(id.Value, dto);

            if (response.Equals(ServiceResponseType.NotFound))
                return NotFound();

            return Ok();
        }

        /// <summary>
        /// Deletes a Question by given Id.
        /// </summary>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Remove(int id)
        {
            var response = await _questionService.DeleteQuestion(id);

            if (response.Equals(ServiceResponseType.NotFound))
                return NotFound();

            return Ok();
        }
    }
}
