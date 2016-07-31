using log4net.Core;
using System;

using log4net.Repository;

namespace log4netUnitTest.Stubs
{
    /// <summary>
    /// Class to stub out the LoggingEventClass
    /// </summary>
    public class StubLoggingEvent : LoggingEvent
    {
        /// <summary>
        /// Stub constructor
        /// </summary>
        /// <param name="data">Some data</param>
        public StubLoggingEvent(LoggingEventData data) : base(null, null, data)
        {
        }

        /// <summary>
        /// Stub constructor
        /// </summary>
        /// <param name="callerStackBoundaryDeclaringType">A type</param>
        /// <param name="repository">ILoggerRepository object</param>
        /// <param name="data">Logging event data</param>
        public StubLoggingEvent(Type callerStackBoundaryDeclaringType, ILoggerRepository repository, LoggingEventData data) : base(callerStackBoundaryDeclaringType, repository, data)
        {
        }
    }
}
