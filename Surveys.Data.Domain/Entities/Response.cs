using Microsoft.EntityFrameworkCore;

namespace Surveys.Data.Domain.Entities
{
    public partial class Response
    {
        public int SurveyResponseId { get; set; }
        public int QuestionId { get; set; }
        public int RespondentId { get; set; }
        public string Answer { get; set; }

        public virtual Question Question { get; set; }
        public virtual Respondent Respondent { get; set; }
        public virtual SurveyResponse SurveyResponse { get; set; }
    }
}
