using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log4netIntegrationTest
{
    /// <summary>
    /// Integration test class for the ConsoleAppender
    /// </summary>
    [TestClass]
    public class ConsoleAppenderIntegrationTest
    {
        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
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
