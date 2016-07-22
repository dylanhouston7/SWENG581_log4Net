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
    public class AppenderSkeletonUnitTest
    {
        public class StubAppender : AppenderSkeleton
        {
            protected override void Append(LoggingEvent loggingEvent)
            {
                throw new NotImplementedException();
            }
        }

        [TestMethod]
        public void TestAppenderSkeleton_LayoutPropertySetGet_Layout()
        {
            // Arrange
            PatternLayout standardLayout = new PatternLayout("%message%newline");
            StubAppender c = new StubAppender();

            // Act
            c.Layout = standardLayout;

            // Assert            
            PatternLayout result = c.Layout as PatternLayout;
            if (result == null)
            {
                Assert.Fail("Layout type returned not the same as the set Layout type");
            }
            else
            {
                Assert.AreEqual(standardLayout.ConversionPattern, result.ConversionPattern, false);
            }
        }
    }
}
