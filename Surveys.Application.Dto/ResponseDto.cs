using System.ComponentModel.DataAnnotations;

namespace Surveys.Application.Dto
{
    public class ResponseDto
    {
        public int? SurveyResponseId { get; set; }

        public int? QuestionId { get; set; }

        public int? RespondentId { get; set; }

        [MaxLength(100)]
        public string Answer { get; set; }
    }
}
