namespace Surveys.Data.Domain.Entities
{
    public partial class VSurveyResponses
    {
        public int SurveyId { get; set; }
        public string SurveyName { get; set; }
        public string SurveyDescription { get; set; }
        public int TotalResponses { get; set; }
    }
}
