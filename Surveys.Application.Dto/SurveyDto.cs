using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Surveys.Application.Dto
{
    public class SurveyDto
    {
        [JsonIgnore]
        public int? Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [JsonIgnore]
        public IEnumerable<QuestionOrderDto> QuestionOrders { get; set; }
    }
}
