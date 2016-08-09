using log4net.Appender;
using log4net.Core;
using System;


namespace log4netIntegrationTest.Stubs
{
    /// <summary>
    /// Stub class for the AppenderSkeleton.
    /// </summary>
    /// <remarks>
    /// This was originally in the AppenderSkeletonUnitTest.cs file
    /// </remarks>
    public class StubAppender : AppenderSkeleton
    {
        /// <summary>
        /// Implementation of the append method. Throws a not implemented exception.
        /// </summary>
        /// <param name="loggingEvent">The logging event</param>
        protected override void Append(LoggingEvent loggingEvent)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a string value for the logging event
        /// </summary>
        /// <param name="loggingEvent">The logging event to turn into a string</param>
        /// <returns>The logging event as a string</returns>
        public string RenderLoggingEventTest(LoggingEvent loggingEvent)
        {
            return this.RenderLoggingEvent(loggingEvent);
        }
    }
}
