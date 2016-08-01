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
        // Statically defined log levels for test cases
        static Level lvl_null = null;
        static Level lvl_0 = new Level(0, "");
        static Level lvl_1 = new Level(1, "");
        static Level lvl_2 = new Level(2, "");
        static Level lvl_3 = new Level(3, "");
        
        /// <summary>
        /// Concrete Appender that is derived from the base SkeletonAppender class for the
        /// purpose of testing the abstract SkeletonAppender class that cannot be instantiated
        /// on its own.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        public class TestAppender : AppenderSkeleton
        {
            public StringBuilder m_appendOutput = new StringBuilder();

            public bool Base_RequiresLayout()
            {
                return base.RequiresLayout;
            }

            public bool Base_IsAsSeverAsThreshold(Level level)
            {
                return base.IsAsSevereAsThreshold(level);
            }

            public bool Base_FilterEvent(LoggingEvent loggingEvent)
            {
                return base.FilterEvent(loggingEvent);
            }

            protected override void Append(LoggingEvent loggingEvent)
            {
                m_appendOutput.Append(loggingEvent.GetLoggingEventData().Message);
            }
        }

        /// <summary>
        /// Test Case AppenderSkeleton-X: IsAsSeverAsThreshold
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        [TestMethod]
        public void TestAppenderSkeleton_IsAsSeverAsThreshold_NullThreshold()
        {
            // Arrange
            TestAppender c = new TestAppender();
            c.Threshold = lvl_null;
            Level input_level = lvl_1;

            // Act
            bool result = c.Base_IsAsSeverAsThreshold(input_level);

            // Assert            
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// Test Case AppenderSkeleton-X: IsAsSeverAsThreshold
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        [TestMethod]
        public void TestAppenderSkeleton_IsAsSeverAsThreshold_LevelGreaterThanThreshold()
        {
            // Arrange
            TestAppender c = new TestAppender();
            c.Threshold = lvl_0;
            Level input_level = lvl_1;

            // Act
            bool result = c.Base_IsAsSeverAsThreshold(input_level);

            // Assert            
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// Test Case AppenderSkeleton-X: IsAsSeverAsThreshold
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        [TestMethod]
        public void TestAppenderSkeleton_IsAsSeverAsThreshold_LevelEqualToThreshold()
        {
            // Arrange
            TestAppender c = new TestAppender();
            c.Threshold = lvl_1;
            Level input_level = lvl_1;

            // Act
            bool result = c.Base_IsAsSeverAsThreshold(input_level);

            // Assert            
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// Test Case AppenderSkeleton-X: IsAsSeverAsThreshold
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        [TestMethod]
        public void TestAppenderSkeleton_IsAsSeverAsThreshold_LevelLessThanThreshold()
        {
            // Arrange
            TestAppender c = new TestAppender();
            c.Threshold = lvl_1;
            Level input_level = lvl_0;

            // Act
            bool result = c.Base_IsAsSeverAsThreshold(input_level);

            // Assert            
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Test Case AppenderSkeleton-X: AddFilter
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        [TestMethod]
        public void TestAppenderSkeleton_AddFilter_NullFilter()
        {
            // Arrange
            TestAppender c = new TestAppender();
            log4net.Filter.IFilter f = null;

            // Act
            try
            {
                c.AddFilter(f);
            }
            catch (ArgumentNullException e)
            {
                // Assert Pass
                return;
            }
            catch(Exception e)
            {
                // No Op
            }

            // Assert Fail
            Assert.Fail("NullArgument Exception Not thrown");            
        }

        /// <summary>
        /// Test Case AppenderSkeleton-X: AddFilter
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        [TestMethod]
        public void TestAppenderSkeleton_AddFilter_NonNullFilters()
        {
            // Arrange
            TestAppender c = new TestAppender();
            log4net.Filter.IFilter f1 = new log4net.Filter.LevelMatchFilter();
            log4net.Filter.IFilter f2 = new log4net.Filter.LevelRangeFilter();
            log4net.Filter.IFilter f3 = new log4net.Filter.DenyAllFilter();

            // Act
            c.AddFilter(f1);
            c.AddFilter(f2);
            c.AddFilter(f3);

            // Assert
            log4net.Filter.LevelMatchFilter result_f1 = c.FilterHead as log4net.Filter.LevelMatchFilter;
            log4net.Filter.LevelRangeFilter result_f2 = c.FilterHead.Next as log4net.Filter.LevelRangeFilter;
            log4net.Filter.DenyAllFilter result_f3 = c.FilterHead.Next.Next as log4net.Filter.DenyAllFilter;
            log4net.Filter.IFilter result_f4 = c.FilterHead.Next.Next.Next;
            Assert.AreNotEqual(null, result_f1);
            Assert.AreNotEqual(null, result_f2);
            Assert.AreNotEqual(null, result_f3);
            Assert.AreEqual(null, result_f4);
        }

        /// <summary>
        /// Test Case AppenderSkeleton-X: ClearFilters
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        [TestMethod]
        public void TestAppenderSkeleton_ClearFilters()
        {
            // Arrange
            TestAppender c = new TestAppender();
            log4net.Filter.IFilter f = new log4net.Filter.LevelMatchFilter();
            c.AddFilter(f);

            // Assert - Act - Assert
            Assert.AreNotEqual(null, c.FilterHead);
            c.ClearFilters();
            Assert.AreEqual(null, c.FilterHead);
        }

        /// <summary>
        /// Test Case AppenderSkeleton-X: FilterEvent
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        [TestMethod]
        public void TestAppenderSkeleton_FilterEvent_IsNotAsSeverAsThreshold()
        {
            // Arrange
            TestAppender c = new TestAppender();
            c.Threshold = lvl_1;
            LoggingEventData logData = new LoggingEventData();
            logData.Level = lvl_0;
            LoggingEvent logEvent = new LoggingEvent(logData);

            // Act
            bool result = c.Base_FilterEvent(logEvent);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Test Case AppenderSkeleton-X: FilterEvent
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        [TestMethod]
        public void TestAppenderSkeleton_FilterEvent_Deny()
        {
            // Arrange
            TestAppender c = new TestAppender();
            c.Threshold = null;
            log4net.Filter.DenyAllFilter f = new log4net.Filter.DenyAllFilter();
            c.AddFilter(f);
            LoggingEventData logData = new LoggingEventData();
            LoggingEvent logEvent = new LoggingEvent(logData);

            // Act
            bool result = c.Base_FilterEvent(logEvent);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Test Case AppenderSkeleton-X: FilterEvent
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        [TestMethod]
        public void TestAppenderSkeleton_FilterEvent_NeutralAccept()
        {
            // Arrange
            TestAppender c = new TestAppender();
            c.Threshold = null;
            log4net.Filter.LevelMatchFilter f1 = new log4net.Filter.LevelMatchFilter();
            log4net.Filter.LevelMatchFilter f2 = new log4net.Filter.LevelMatchFilter();
            f1.LevelToMatch = lvl_0;
            f2.LevelToMatch = lvl_1;
            c.AddFilter(f1);
            c.AddFilter(f2);
            LoggingEventData logData = new LoggingEventData();
            logData.Level = lvl_1;
            LoggingEvent logEvent = new LoggingEvent(logData);

            // Act
            bool result = c.Base_FilterEvent(logEvent);

            // Assert
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// Test Case AppenderSkeleton-X: RequiresLayout Get
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        [TestMethod]
        public void TestAppenderSkeleton_RequiresLayoutPropertyGet()
        {
            // Arrange
            TestAppender c = new TestAppender();

            // Act
            bool result = c.Base_RequiresLayout();

            // Assert            
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Test Case AppenderSkeleton-X: Threshold Get-Set
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        [TestMethod]
        public void TestAppenderSkeleton_ThresholdProperty()
        {
            // Arrange
            TestAppender c = new TestAppender();
            c.Threshold = lvl_0;

            // Act
            Level result = c.Threshold;

            // Assert            
            Assert.AreEqual(result.Value, lvl_0.Value);
        }

        /// <summary>
        /// Test Case AppenderSkeleton-X: Layout Get-Set
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        [TestMethod]
        public void TestAppenderSkeleton_LayoutPropertySetGet_Layout()
        {
            // Arrange
            PatternLayout standardLayout = new PatternLayout("%message%newline");
            TestAppender c = new TestAppender();

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
