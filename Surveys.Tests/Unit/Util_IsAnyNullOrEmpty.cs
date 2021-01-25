using NUnit.Framework;
using Surveys.Application.Dto;
using Surveys.Presentation.Api.Common;

namespace Surveys.Tests.Unit
{
    [TestFixture]
    public class Util_IsAnyNullOrEmpty
    {
        [Test]
        public void IsAnyNullOrEmpty_InputIsNull_ReturnTrue()
        {
            // Arrange
            var isAnyEmpty = Util.IsAnyNullOrEmpty(null);

            // Assert
            Assert.IsTrue(isAnyEmpty);
        }

        [Test]
        public void IsAnyNullOrEmpty_InputisAnyEmpty_ReturnTrue()
        {
            // Arrange
            var emptyObject = new {};
            var isAnyEmpty = Util.IsAnyNullOrEmpty(emptyObject);

            // Assert
            Assert.IsTrue(isAnyEmpty);
        }

        [Test]
        public void IsAnyNullOrEmpty_InputIsNotEmptyButValuesNull_ReturnTrue()
        {
            // Arrange
            var emptyObject = new QuestionOrderDto();
            var isAnyEmpty = Util.IsAnyNullOrEmpty(emptyObject);

            // Assert
            Assert.IsTrue(isAnyEmpty);
        }

        [Test]
        public void IsAnyNullOrEmpty_InputIsNotEmpty_ReturnFalse()
        {
            // Arrange
            var emptyObject = new QuestionOrderDto { OrderNbr = 1, QuestionId = 1, SurveyId = 1 };
            var isAnyEmpty = Util.IsAnyNullOrEmpty(emptyObject);

            // Assert
            Assert.IsFalse(isAnyEmpty);
        }

        [Test]
        public void IsAnyNullOrEmpty_InputIsNotEmptyButSingleValueIsNull_ReturnFalse()
        {
            // Arrange
            var emptyObject = new QuestionOrderDto { OrderNbr = 1, QuestionId = 1 };
            var isAnyEmpty = Util.IsAnyNullOrEmpty(emptyObject);

            // Assert
            Assert.IsTrue(isAnyEmpty);
        }
    }
}