using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using log4net.Appender;
namespace log4netUnitTest
{
    [TestClass]
    public class ConsoleAppenderUnitTest
    {
        [TestMethod]
        public void TestConsoleAppender_TargetPropertySetGet_SetConsoleOut()
        {
            // Arrange
            ConsoleAppender c = new ConsoleAppender();

            // Act
            c.Target = ConsoleAppender.ConsoleOut;

            // Assert
            string result = c.Target;
            Assert.AreEqual(ConsoleAppender.ConsoleOut, result, false);
        }

        [TestMethod]
        public void TestConsoleAppender_TargetPropertySetGet_SetErrorOut()
        {
            // Arrange
            ConsoleAppender c = new ConsoleAppender();

            // Act
            c.Target = ConsoleAppender.ConsoleError;

            // Assert
            string result = c.Target;
            Assert.AreEqual(ConsoleAppender.ConsoleError, result, false);
        }

        /// <summary>
        /// Ensures that the console appender does now allow a null "target" value.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestConsoleAppender_TargetPropertySetGet_SetNull()
        {
            // 7/22/2016 Dylan: I modified this test method by adding an attribute, which
            // expects that an exception is being thrown for the below code upon execution.
            // This is to ensure that the intent of the unit test is met, while making
            // sure that the test passes in the testing framework if the desired behavior is achieved.
            
            // Arrange
            ConsoleAppender c = new ConsoleAppender();

            // Act
            c.Target = null;
        }

        [TestMethod]
        public void TestConsoleAppender_TargetPropertySet_ConsoleError()
        {
            // Arrange
            ConsoleAppender c = new ConsoleAppender();
            string expectedString = "Console.Error";
            bool expectedBool = true;

            // Act 
            c.Target = expectedString; 

            // Assert
            Assert.AreEqual(expectedString,c.Target);
            Assert.AreEqual(expectedBool, c.getWriteToErrorStream);
        }

        [TestMethod]
        public void TestConsoleAppender_TargetPropertySet_ConsoleOut()
        {
            //Arrange
            ConsoleAppender c = new ConsoleAppender();
            string expectedString = "Console.Out";
            bool expectedBool = false;
            //Act
            c.Target = expectedString;

            //Assert
            Assert.AreEqual(expectedString, c.Target);
            Assert.AreEqual(expectedBool, c.getWriteToErrorStream);
        }
    }
}
