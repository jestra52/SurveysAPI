using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Surveys.Data.Domain.Entities
{
    public partial class SurveyResponse
    {
        public SurveyResponse()
        {
            Responses = new HashSet<Response>();
        }

        public int? Id { get; set; }
        public int SurveyId { get; set; }
        public int RespondentId { get; set; }
        [NotMapped]
        public byte[] Updated { get; set; }

        public virtual Respondent Respondent { get; set; }
        public virtual Survey Survey { get; set; }
        public virtual ICollection<Response> Responses { get; set; }
    }
}
