using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Surveys.Data.Domain.Entities
{
    public partial class Question
    {
        public Question()
        {
            QuestionOrders = new HashSet<QuestionOrder>();
            Responses = new HashSet<Response>();
        }

        public int? Id { get; set; }
        public string Text { get; set; }
        [NotMapped]
        public byte[] Updated { get; set; }

        public virtual ICollection<QuestionOrder> QuestionOrders { get; set; }
        public virtual ICollection<Response> Responses { get; set; }
    }
}
