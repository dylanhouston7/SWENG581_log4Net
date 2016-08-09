using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace log4netIntegrationTest
{
    /// <summary>
    /// Integration test class for the FileAppender
    /// </summary>
    [TestClass]
    public class FileAppenderIntegrationTest
    {
        /// <summary>
        /// Ensures that the DoAppend method works without throwing errors or exceptions under normal operation.
        /// Reads the resulting file from the file system to ensure that the message was correctly appended to the file.
        /// </summary>
        [TestMethod]
        public void SystemTestDoAppendArray()
        {
            string input = "Test Log Message";
            StringBuilder result = new StringBuilder();
            StringWriter writer = new StringWriter(result);

            FileAppender appender = new FileAppender();
            appender.Writer = writer;
            appender.Layout = new PatternLayout("%message");
            appender.File = "tempfile.txt";

            // Store the path of the appender
            string outputPath = appender.File;

            // Delete the log file if it already exists.
            if (File.Exists(appender.File))
            {
                File.Delete(appender.File);
            }

            LoggingEventData logData = new LoggingEventData();
            logData.Message = input;

            List<LoggingEvent> list = new List<LoggingEvent>();
            list.Add(new LoggingEvent(logData));

            appender.ActivateOptions();
            // Call the DoAppend overload method
            appender.DoAppend(list.ToArray());

            appender.Close();

            // Ensure there are no errors and that the result matches what we fed the TextWriterAppender.
            OnlyOnceErrorHandler errorHandler = (OnlyOnceErrorHandler)appender.ErrorHandler;
            Assert.IsNull(errorHandler.ErrorMessage);

            string fileText = File.ReadAllText(outputPath);
            Assert.AreEqual(input, fileText);
        }
    }
}
