using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Surveys.Data.Domain.Entities
{
    public partial class Survey
    {
        public Survey()
        {
            QuestionOrders = new HashSet<QuestionOrder>();
            SurveyResponses = new HashSet<SurveyResponse>();
        }

        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public byte[] Updated { get; set; }

        public virtual ICollection<QuestionOrder> QuestionOrders { get; set; }
        public virtual ICollection<SurveyResponse> SurveyResponses { get; set; }
    }
}
