using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Surveys.Application.Dto
{
    public class QuestionDto
    {
        [JsonIgnore]
        public int? Id { get; set; }

        [MaxLength(200)]
        public string Text { get; set; }
    }
}
