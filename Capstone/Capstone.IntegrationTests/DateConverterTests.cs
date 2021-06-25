using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.IntegrationTests
{
    [TestClass]
    public class DateConverterTests : IntegrationTestBase
    {
        [TestMethod]
        public void InvalidInputShouldReturnEmptyString()
        {
            // Arrange
            DateConverter dateConverter = new DateConverter();
            // Act

            string expected = "";
            string result = dateConverter.FormatDate(14);

            // Assert

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void ElevenShouldReturnNov()
        {
            DateConverter dateConverter = new DateConverter();
            string expected = "Nov.";
            string result = dateConverter.FormatDate(11);
            Assert.AreEqual(expected, result);
        }
    }
}
