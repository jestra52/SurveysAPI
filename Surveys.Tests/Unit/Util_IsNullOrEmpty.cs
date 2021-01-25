using NUnit.Framework;
using Surveys.Presentation.Api.Common;

namespace Surveys.Tests.Unit
{
    [TestFixture]
    public class Util_IsNullOrEmpty
    {
        [Test]
        public void IsNullOrEmpty_InputIsNull_ReturnTrue()
        {
            // Arrange
            var isNullOrEmpty = Util.IsNullOrEmpty(null);

            // Assert
            Assert.IsTrue(isNullOrEmpty);
        }

        [Test]
        public void IsNullOrEmpty_InputNotEmpty_ReturnFalse()
        {
            // Arrange
            var isNullOrEmpty = Util.IsNullOrEmpty(1);

            // Assert
            Assert.IsFalse(Util.IsNullOrEmpty(1));
        }
    }
}
