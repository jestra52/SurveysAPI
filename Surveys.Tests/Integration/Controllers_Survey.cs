using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Surveys.Application.Dto;
using Surveys.Application.Services.Definitions;
using Surveys.Presentation.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Surveys.Tests.Integration
{
    [TestFixture]
    public class Controllers_Survey
    {
        private SurveyController _surveyController;
        private ISurveyService _surveyService;
        private ILogger<SurveyController> _logger;

        [SetUp]
        public void SetUp()
        {
            var mock = new Mock<ILogger<SurveyController>>();

            _logger = mock.Object;
            _surveyService = new MockSurveyService();
            _surveyController = new SurveyController(_surveyService, _logger);
        }

        [Test]
        public async Task Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var result = await _surveyController.Get();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task Get_WhenCalled_ReturnsAllSurveys()
        {
            // Act
            var result = (await _surveyController.Get()) as OkObjectResult;
            var surveys = await _surveyService.GetSurveys();
            var response = result.Value;
            Type t = response.GetType();
            PropertyInfo p = t.GetProperty("surveys");
            var items = (IEnumerable<SurveyDto>)p.GetValue(response);

            // Assert
            Assert.IsInstanceOf<IEnumerable<SurveyDto>>(items);
            Assert.AreEqual(items.Count(), surveys.Count());
        }
    }
}
