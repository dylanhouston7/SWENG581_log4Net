using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Util;
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
    /// Class to perform integration testing on the TextWriterAppender
    /// </summary>
    [TestClass]
    public class TextWriterAppenderIntegrationTest
    {
        /// <summary>
        /// Ensures that a TextWriterAppender cannot be appended if a layout or writer is not set.
        /// </summary>
        [TestMethod]
        public void IntegrationTestDoAppendWithoutLayoutThrowsErrorMessage()
        {
            // Set up the TextWriterAppender -- but with no layout or writer
            TextWriterAppender textWriter = new TextWriterAppender();
            string input = "Test Log Message";

            // Append log data -- without a layout or writer being set for the TextWriterAppender
            LoggingEventData logData = new LoggingEventData();
            logData.Message = input;
            LoggingEvent logEvent = new LoggingEvent(logData);
            textWriter.DoAppend(logEvent);

            OnlyOnceErrorHandler onlyOnceErrorHandler = (OnlyOnceErrorHandler)textWriter.ErrorHandler;

            // Check for any error message
            Assert.IsNotNull(onlyOnceErrorHandler.ErrorMessage);
        }


        /// <summary>
        /// Ensures that a TextWriterAppender cannot be appended to when a writer is not set for it.
        /// </summary>
        [TestMethod]
        public void IntegrationTestDoAppendWithoutWriter()
        {
            // Set up the TextWriterAppender-- with a layout, but not a writer.
            TextWriterAppender textWriter = new TextWriterAppender();
            textWriter.Layout = new log4net.Layout.PatternLayout("%m");

            string input = "Test Log Message";

            // Append log data -- without a writer being set for the TextWriterAppender
            LoggingEventData logData = new LoggingEventData();
            logData.Message = input;
            LoggingEvent logEvent = new LoggingEvent(logData);
            textWriter.DoAppend(logEvent);

            OnlyOnceErrorHandler onlyOnceErrorHandler = (OnlyOnceErrorHandler)textWriter.ErrorHandler;

            // Ensure there is an error message
            Assert.IsNotNull(onlyOnceErrorHandler.ErrorMessage);
        }

        /// <summary>
        /// Ensures that a TextWriter can be appended to when a layout is set.
        /// </summary>
        [TestMethod]
        public void IntegrationTestDoAppendWithLayoutAndWriterDoesNotThrowErrorMessage()
        {
            // Set up the TextWriterAppender-- with a layout AND a writer.
            TextWriterAppender textWriter = new TextWriterAppender();
            textWriter.Layout = new log4net.Layout.PatternLayout("%m");
            StringBuilder result = new StringBuilder();
            StringWriter writer = new StringWriter(result);
            textWriter.Writer = writer;

            string input = "Test Log Message";

            // Append log data -- without a writer being set for the TextWriterAppender
            LoggingEventData logData = new LoggingEventData();
            logData.Message = input;
            LoggingEvent logEvent = new LoggingEvent(logData);
            textWriter.DoAppend(logEvent);

            OnlyOnceErrorHandler onlyOnceErrorHandler = (OnlyOnceErrorHandler)textWriter.ErrorHandler;

            // Ensure there are no error messages
            Assert.IsNull(onlyOnceErrorHandler.ErrorMessage);
        }

        /// <summary>
        /// Ensures that a TextWriter cannot be appended to when its writer is closed.
        [TestMethod]
        public void IntegrationTestDoAppendWithClosedWriterDoesThrowErrorMessage()
        {
            // Set up the TextWriterAppender-- with a layout AND a CLOSED writer.
            TextWriterAppender textWriter = new TextWriterAppender();
            textWriter.Layout = new log4net.Layout.PatternLayout("%m");

            StringBuilder result = new StringBuilder();
            StringWriter writer = new StringWriter(result);
            textWriter.Writer = writer;
            textWriter.Writer.Close();

            string input = "Test Log Message";

            // Append log data -- with a CLOSED writer being set for the TextWriterAppender
            LoggingEventData logData = new LoggingEventData();
            logData.Message = input;
            LoggingEvent logEvent = new LoggingEvent(logData);
            textWriter.DoAppend(logEvent);

            OnlyOnceErrorHandler onlyOnceErrorHandler = (OnlyOnceErrorHandler)textWriter.ErrorHandler;

            // Ensure there is an error message
            Assert.IsNotNull(onlyOnceErrorHandler.ErrorMessage);
        }

        /// <summary>
        /// Ensures that the DoAppend method works without throwing errors or exceptions under normal operation.
        /// </summary>
        [TestMethod]
        public void TestDoAppendArray()
        {
            string input = "Test Log Message";
            StringBuilder result = new StringBuilder();
            StringWriter writer = new StringWriter(result);

            TextWriterAppender appender = new TextWriterAppender();
            appender.Writer = writer;
            appender.Layout = new PatternLayout("%message");

            LoggingEventData logData = new LoggingEventData();
            logData.Message = input;

            // Build an array of two stublogging events.
            List<LoggingEvent> list = new List<LoggingEvent>();
            list.Add(new LoggingEvent(logData));

            // Call the DoAppend overload method
            appender.DoAppend(list.ToArray());

            OnlyOnceErrorHandler errorHandler = (OnlyOnceErrorHandler)appender.ErrorHandler;

            // Ensure there are no errors and that the result matches what we fed the TextWriterAppender.
            Assert.IsNull(errorHandler.ErrorMessage);
            Assert.AreEqual(input, result.ToString());
        }
    }
}
