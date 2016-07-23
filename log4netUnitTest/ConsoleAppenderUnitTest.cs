using System;
using System.IO;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;

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

        [TestMethod]
        public void TestConsoleAppender_TargetPropertySetGet_SetNull()
        {
            // Arrange
            ConsoleAppender c = new ConsoleAppender();

            // Act
            try
            {
                c.Target = null;
            }           
            catch (Exception e)
            {
                // Assert
                Assert.Fail("The Target property Set method does not handle Null Argument Exceptions");
            }
        }

        [TestMethod]
        public void TestConsoleAppender_DoAppendLogEvent_ConsoleOut()
        {
            // Arrange            
            string logInput = "Test Log Message Input";
            StringBuilder consoleResult = new StringBuilder();
            StringWriter mockedConsoleOut = new StringWriter(consoleResult);
            Console.SetOut(mockedConsoleOut);
            StringBuilder errorResult = new StringBuilder();
            StringWriter mockedErrorOut = new StringWriter(errorResult);
            Console.SetError(mockedErrorOut);

            ConsoleAppender c = new ConsoleAppender();
            c.Layout = new PatternLayout("%message");            

            LoggingEventData logData = new LoggingEventData();
            logData.Message = logInput;
            LoggingEvent logEvent = new LoggingEvent(logData);

            // Act
            c.DoAppend(logEvent);

            // Assert
            Assert.AreEqual(logInput, consoleResult.ToString(), false);
            Assert.AreEqual("", errorResult.ToString(), false);
        }

        [TestMethod]
        public void TestConsoleAppender_DoAppendLogEvent_ErrorOut()
        {
            // Arrange
            string logInput = "Test Log Message Input";
            StringBuilder consoleResult = new StringBuilder();
            StringWriter mockedConsoleOut = new StringWriter(consoleResult);
            Console.SetOut(mockedConsoleOut);
            StringBuilder errorResult = new StringBuilder();
            StringWriter mockedErrorOut = new StringWriter(errorResult);
            Console.SetError(mockedErrorOut);

            ConsoleAppender c = new ConsoleAppender();
            c.Layout = new PatternLayout("%message");
            c.Target = ConsoleAppender.ConsoleError;

            LoggingEventData logData = new LoggingEventData();
            logData.Message = logInput;
            LoggingEvent logEvent = new LoggingEvent(logData);

            // Act
            c.DoAppend(logEvent);

            // Assert
            Assert.AreEqual(logInput, errorResult.ToString(), false);
            Assert.AreEqual("", consoleResult.ToString(), false);
        }

        [TestMethod]
        public void TestConsoleAppender_DoAppendLogEventConsoleOut_NullLayout()
        {
            // Arrange            
            string logInput = "Test Log Message Input";
            StringBuilder consoleResult = new StringBuilder();
            StringWriter mockedConsoleOut = new StringWriter(consoleResult);
            Console.SetOut(mockedConsoleOut);
            StringBuilder errorResult = new StringBuilder();
            StringWriter mockedErrorOut = new StringWriter(errorResult);
            Console.SetError(mockedErrorOut);

            ConsoleAppender c = new ConsoleAppender();
            c.Layout = null;

            LoggingEventData logData = new LoggingEventData();
            logData.Message = logInput;
            LoggingEvent logEvent = new LoggingEvent(logData);

            // Act
            c.DoAppend(logEvent);

            // Assert
            Assert.AreEqual("", consoleResult.ToString(), false);
            Assert.AreEqual("", errorResult.ToString(), false);
        }
    }
}
