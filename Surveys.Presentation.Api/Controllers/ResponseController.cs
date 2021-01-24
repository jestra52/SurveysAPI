using Microsoft.AspNetCore.Mvc;
using Surveys.Application.Services.Definitions;
using System.Threading.Tasks;

namespace Surveys.Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : Controller
    {
        private readonly IResponseService _responseService;

        public ResponseController(IResponseService responseService)
        {
            _responseService = responseService;
        }

        /// <summary>
        /// Gets the responses of a Survey response by its Id.
        /// </summary>
        [HttpGet]
        [Route("GetBySurveyResponseId/{surveyResponseId:int}", Name = nameof(GetResponsesBySurveyResponseId))]
        public async Task<IActionResult> GetResponsesBySurveyResponseId(int surveyResponseId)
        {
            var responses = await _responseService.GetResponsesBySurveyResponseId(surveyResponseId);

            if (responses == null)
                return NotFound();

            return Ok(responses);
        }

        /// <summary>
        /// Gets the responses by Question id.
        /// </summary>
        [HttpGet]
        [Route("GetByQuestionId/{questionId:int}", Name = nameof(GetResponsesByQuestionId))]
        public async Task<IActionResult> GetResponsesByQuestionId(int questionId)
        {
            var responses = await _responseService.GetResponsesByQuestionId(questionId);

            if (responses == null)
                return NotFound();

            return Ok(responses);
        }

        /// <summary>
        /// Gets the responses of a Respondent by its id.
        /// </summary>
        [HttpGet]
        [Route("GetByRespondentId/{respondentId:int}", Name = nameof(GetResponsesByRespondentId))]
        public async Task<IActionResult> GetResponsesByRespondentId(int respondentId)
        {
            var responses = await _responseService.GetResponsesBySurveyResponseId(respondentId);

            if (responses == null)
                return NotFound();
             
            return Ok(responses);
        }
    }
}
