using AutoMapper;
using Surveys.Data.Domain.Entities;

namespace Surveys.Application.Dto
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Survey, SurveyDto>();
            CreateMap<SurveyDto, Survey>()
                .ForMember(s => s.Updated, opt => opt.Ignore());

            CreateMap<Question, QuestionDto>();
            CreateMap<QuestionDto, Question>()
                .ForMember(s => s.Updated, opt => opt.Ignore());

            CreateMap<Respondent, RespondentDto>();
            CreateMap<RespondentDto, Respondent>()
                .ForMember(s => s.Created, opt => opt.Ignore());

            CreateMap<QuestionOrder, QuestionOrderDto>();
            CreateMap<QuestionOrderDto, QuestionOrder>();
        }
    }
}
