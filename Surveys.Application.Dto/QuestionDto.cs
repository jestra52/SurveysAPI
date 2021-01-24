using System.ComponentModel.DataAnnotations;

namespace Surveys.Application.Dto
{
    public class QuestionDto
    {
        public int? Id { get; set; }

        [MaxLength(200)]
        public string Text { get; set; }
    }
}
