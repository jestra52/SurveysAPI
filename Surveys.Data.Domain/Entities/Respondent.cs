using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Surveys.Data.Domain.Entities
{
    public partial class Respondent
    {
        public Respondent()
        {
            Responses = new HashSet<Response>();
            SurveyResponses = new HashSet<SurveyResponse>();
        }

        public int? Id { get; set; }
        public string Name { get; set; }
        public string HashedPassword { get; set; }
        public string Email { get; set; }
        [NotMapped]
        public byte[] Created { get; set; }

        public virtual ICollection<Response> Responses { get; set; }
        public virtual ICollection<SurveyResponse> SurveyResponses { get; set; }
    }
}
