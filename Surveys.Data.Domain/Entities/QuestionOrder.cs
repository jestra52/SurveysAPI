using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Surveys.Data.Domain.Entities
{
    public partial class QuestionOrder
    {
        public int OrderNbr { get; set; }
        public int QuestionId { get; set; }
        public int SurveyId { get; set; }

        public virtual Question Question { get; set; }
        public virtual Survey Survey { get; set; }
    }
}
