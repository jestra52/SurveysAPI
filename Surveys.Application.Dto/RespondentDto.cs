using System.ComponentModel.DataAnnotations;

namespace Surveys.Application.Dto
{
    public class RespondentDto
    {
        public int? Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string HashedPassword { get; set; }

        [MaxLength(254)]
        public string Email { get; set; }
    }
}
