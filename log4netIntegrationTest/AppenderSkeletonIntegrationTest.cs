using log4net.Core;
using log4net.Util;
using log4netIntegrationTest.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace log4netIntegrationTest
{
    /// <summary>
    /// Class to perform integration testing of the AppenderSkeleton class.
    /// </summary>
    [TestClass]
    public class AppenderSkeletonIntegrationTest
    {

        /// <summary>
        /// Ensures that the DoAppend method works without throwing errors or exceptions under normal operation.
        /// </summary>
        [TestMethod]
        public void TestDoAppend()
        {
            StubAppender stub = new StubAppender();
            LoggingEventData eventData = new LoggingEventData();
            LoggingEvent loggingEvent = new LoggingEvent(eventData);
            stub.DoAppend(loggingEvent);
            OnlyOnceErrorHandler errorHandler = (OnlyOnceErrorHandler)stub.ErrorHandler;
            Assert.IsNull(errorHandler.ErrorMessage);
        }

        /// <summary>
        /// Ensures that the DoAppend method throws an error when the appender is closed before the method is invoked.
        /// </summary>
        [TestMethod]
        public void TestDoAppendWhenClosed()
        {
            StubAppender stub = new StubAppender();
            LoggingEventData eventData = new LoggingEventData();
            LoggingEvent loggingEvent = new LoggingEvent(eventData);
            stub.Close();
            stub.DoAppend(loggingEvent);
            OnlyOnceErrorHandler errorHandler = (OnlyOnceErrorHandler)stub.ErrorHandler;
            Assert.IsNotNull(errorHandler.ErrorMessage);
        }


        /// <summary>
        /// Ensures that the DoAppend method works without throwing errors or exceptions under normal operation.
        /// </summary>
        [TestMethod]
        public void TestDoAppendArray()
        {
            StubAppender stub = new StubAppender();
            LoggingEventData eventData = new LoggingEventData();

            // Build an array of two logging events.
            List<LoggingEvent> list = new List<LoggingEvent>();
            list.Add(new LoggingEvent(eventData));
            list.Add(new LoggingEvent(eventData));

            // Call the DoAppend overload method
            stub.DoAppend(list.ToArray());

            OnlyOnceErrorHandler errorHandler = (OnlyOnceErrorHandler)stub.ErrorHandler;
            Assert.IsNull(errorHandler.ErrorMessage);
        }

        /// <summary>
        /// Ensures that the DoAppend method throws an error when the appender is closed before the method is invoked.
        /// </summary>
        [TestMethod]
        public void TestDoAppendArrayWhenClosed()
        {
            StubAppender stub = new StubAppender();
            LoggingEventData eventData = new LoggingEventData();
            stub.Close();

            // Build an array of two logging events.
            List<LoggingEvent> list = new List<LoggingEvent>();
            list.Add(new LoggingEvent(eventData));
            list.Add(new LoggingEvent(eventData));

            // Call the DoAppend overload method
            stub.DoAppend(list.ToArray());

            OnlyOnceErrorHandler errorHandler = (OnlyOnceErrorHandler)stub.ErrorHandler;
            Assert.IsNotNull(errorHandler.ErrorMessage);
        }


        /// <summary>
        /// Ensures that an error message is thrown when a developer attempts to append a logging event
        /// to a closed Appender.
        /// </summary>
        [TestMethod]
        public void IntegrationTestCloseAppender()
        {
            // Set up the stub Appender, and close the Appender.
            StubAppender stub = new StubAppender();
            stub.Close();

            // Cast the error handler so that we can retrieve error information from it.
            // Ensure that we start on a clean slate.
            OnlyOnceErrorHandler onlyOnceErrorHandler = (OnlyOnceErrorHandler)stub.ErrorHandler;
            Assert.IsNull(onlyOnceErrorHandler.ErrorMessage);

            // Attempt to set a logging event to the closed appender
            LoggingEventData eventData = new LoggingEventData();
            LoggingEvent loggingEvent = new LoggingEvent(eventData);
            stub.DoAppend(loggingEvent);

            // Check for any error message
            Assert.IsNotNull(onlyOnceErrorHandler.ErrorMessage);
        }

        /// <summary>
        /// Ensures that the program can render a string from a logging event object.
        /// </summary>
        [TestMethod]
        public void TestRenderLoggingEvent()
        {
            StubAppender stub = new StubAppender();
            LoggingEventData eventData = new LoggingEventData();
            stub.Layout = new log4net.Layout.PatternLayout("%m");
            LoggingEvent loggingEvent = new LoggingEvent(eventData);
            string loggingEventString = stub.RenderLoggingEventTest(loggingEvent);

            // The string should be an empty string, since we didn't set any data in the loggingEvent object.
            Assert.AreEqual("", loggingEventString);
        }

        /// <summary>
        /// Ensures that an exception is thrown when a layout is not set and the logging event is about to be rendered as a string.
        /// </summary>
        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void TestRenderLoggingEventException()
        {
            StubAppender stub = new StubAppender();
            LoggingEventData eventData = new LoggingEventData();
            LoggingEvent loggingEvent = new LoggingEvent(eventData);
            string loggingEventString = stub.RenderLoggingEventTest(loggingEvent);
        }
    }
}

