using Microsoft.AspNetCore.Mvc;
using Surveys.Application.Services.Definitions;
using System.Threading.Tasks;

namespace Surveys.Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyResponseController : Controller
    {
        private readonly ISurveyResponseService _surveyResponseService;

        public SurveyResponseController(ISurveyResponseService surveyResponseService)
        {
            _surveyResponseService = surveyResponseService;
        }

        /// <summary>
        /// Gets the survey responses by Survey Id.
        /// </summary>
        [HttpGet]
        [Route("GetBySurveyId/{surveyId:int}", Name = nameof(GetSurveyResponsesBySurveyId))]
        public async Task<IActionResult> GetSurveyResponsesBySurveyId(int surveyId)
        {
            var questionOrder = await _surveyResponseService.GetSurveyResponsesBySurveyId(surveyId);

            if (questionOrder == null)
                return NotFound();

            return Ok(questionOrder);
        }

        /// <summary>
        /// Gets the survey responses by Respondent Id.
        /// </summary>
        [HttpGet]
        [Route("GetByRespondentId/{respondentId:int}", Name = nameof(GetSurveyResponsesByRespondentId))]
        public async Task<IActionResult> GetSurveyResponsesByRespondentId(int respondentId)
        {
            var questionOrder = await _surveyResponseService.GetSurveyResponsesByRespondentId(respondentId);

            if (questionOrder == null)
                return NotFound();

            return Ok(questionOrder);
        }
    }
}
