using log4net.Core;
using log4net.Util;
using log4netIntegrationTest.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace log4netIntegrationTest
{
    /// <summary>
    /// Class to perform integration testing of the AppenderSkeleton class.
    /// </summary>
    [TestClass]
    public class AppenderSkeletonIntegrationTest
    {
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
            StubLoggingEvent loggingEvent = new StubLoggingEvent(eventData);
            stub.DoAppend(loggingEvent);

            // Check for any error message
            Assert.IsNotNull(onlyOnceErrorHandler.ErrorMessage);
        }
    }
}

