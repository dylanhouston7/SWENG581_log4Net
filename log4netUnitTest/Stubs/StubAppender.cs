using log4net.Appender;
using log4net.Core;
using System;


namespace log4netUnitTest.Stubs
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
            return;
        }
    }
}
