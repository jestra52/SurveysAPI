using System.Text.Json.Serialization;

namespace Surveys.Application.Dto
{
    public class SurveyResponseDto
    {
        [JsonIgnore]
        public int? Id { get; set; }

        public int? SurveyId { get; set; }

        public int? RespondentId { get; set; }
    }
}
