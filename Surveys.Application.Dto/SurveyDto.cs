using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Surveys.Application.Dto
{
    public class SurveyDto
    {
        public int? Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public IEnumerable<QuestionOrderDto> QuestionOrders { get; set; }
    }
}
