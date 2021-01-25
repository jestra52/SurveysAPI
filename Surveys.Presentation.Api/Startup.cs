using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Surveys.Application.Dto;
using Surveys.Application.Services.Definitions;
using Surveys.Application.Services.Implementations;
using Surveys.Data.Domain.Definitions;
using Surveys.Data.Domain.Implementations;
using Surveys.Data.Domain.Repositories;
using Surveys.Data.Domain.Repositories.Definitions;
using System;
using System.IO;
using System.Reflection;

namespace Surveys
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Surveys API");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            InitDbContext(services);
            RegisterRepositories(services);
            RegisterServices(services);

            var mapperConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new AutoMapperConfig());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);
            services.AddControllers();
            services.AddSwaggerGen(c => 
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        private void InitDbContext(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("SurveysDB");

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<UnitOfWork>(opts => opts.UseSqlServer(connectionString));
        }

        private void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<ISurveyRepository, SurveyRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IRespondentRepository, RespondentRepository>();
            services.AddScoped<IQuestionOrderRepository, QuestionOrderRepository>();
            services.AddScoped<ISurveyResponseRepository, SurveyResponseRepository>();
            services.AddScoped<IResponseRepository, ResponseRepository>();
            services.AddScoped<IVSurveyResponsesRepository, VSurveyResponsesRepository>();
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ISurveyService, SurveyService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IRespondentService, RespondentService>();
            services.AddScoped<IQuestionOrderService, QuestionOrderService>();
            services.AddScoped<ISurveyResponseService, SurveyResponseService>();
            services.AddScoped<IResponseService, ResponseService>();
        }
    }
}
