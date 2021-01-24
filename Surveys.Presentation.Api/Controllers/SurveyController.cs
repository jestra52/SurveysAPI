﻿using Microsoft.AspNetCore.Mvc;
using Surveys.Application.Dto;
using Surveys.Application.Services.Definitions;
using Surveys.Common.Enum;
using System.Threading.Tasks;

namespace Surveys.Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : Controller
    {
        private readonly ISurveyService _surveyService;

        public SurveyController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
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
            if (dto == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);

            await _surveyService.AddSurvey(dto);

            return RedirectToAction(nameof(GetSurveyById), new { id = dto.Id });
        }

        /// <summary>
        /// Retrieves all Surveys.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var surveys = await _surveyService.GetSurveys();

            return Ok(new { surveys });
        }

        /// <summary>
        /// Finds a Survey by given Id.
        /// </summary>
        [HttpGet]
        [Route("{id:int}", Name = nameof(GetSurveyById))]
        public async Task<IActionResult> GetSurveyById(int id)
        {
            var survey = await _surveyService.GetSurveyById(id);

            if (survey == null)
                return NotFound();

            return Ok(survey);
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
            if (dto == null || id == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);

            var response = await _surveyService.UpdateSurvey(id.Value, dto);

            if (response.Equals(ServiceResponseType.NotFound))
                return NotFound();

            return Ok();
        }

        /// <summary>
        /// Deletes a Survey by given Id.
        /// </summary>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Remove(int id)
        {
            var response = await _surveyService.DeleteSurvey(id);

            if (response.Equals(ServiceResponseType.NotFound))
                return NotFound();

            return Ok();
        }
    }
}
