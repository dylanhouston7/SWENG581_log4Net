using Microsoft.VisualStudio.TestTools.UnitTesting;

using log4net.Layout;
using log4netUnitTest.Stubs;
using log4net.Core;
using log4net.Util;

namespace log4netUnitTest
{
    [TestClass]
    public class AppenderSkeletonUnitTest
    {
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

        /// <summary>
        /// Tests the getter for the threshold property.
        /// </summary>
        [TestMethod]
        public void TestGetThreshold()
        {
            StubAppender stub = new StubAppender();
            Assert.IsNull(stub.Threshold);
        }

        /// <summary>
        /// Tests the setter for the threshold property.
        /// </summary>
        [TestMethod]
        public void TestSetThreshold()
        {
            StubAppender stub = new StubAppender();
            Assert.IsNull(stub.Threshold);
            stub.Threshold = Level.Off;
            Assert.AreEqual(Level.Off.ToString(), stub.Threshold.ToString());
        }

        /// <summary>
        /// Tests the getter for the name property.
        /// </summary>
        [TestMethod]
        public void TestGetName()
        {
            StubAppender stub = new StubAppender();
            Assert.IsNull(stub.Name);
        }

        /// <summary>
        /// Tests the setter for the name property.
        /// </summary>
        [TestMethod]
        public void TestSetName()
        {
            StubAppender stub = new StubAppender();
            Assert.IsNull(stub.Name);
            stub.Name = "squid";
            Assert.AreEqual("squid", stub.Name);
        }

        /// <summary>
        /// Tests the constructor for the AppenderSkeleton class.
        /// </summary>
        [TestMethod]
        public void TestConstructor()
        {
            StubAppender stub = null;
            Assert.IsNull(stub);
            stub = new StubAppender();
            Assert.IsNotNull(stub);
        }


        /// <summary>
        /// Tests the constructor for the AppenderSkeleton class.
        /// This test specifically checks to make sure the error handler (m_errorHandler) for the object is not null
        /// upon object initialization. This is a fundamental assumption. Dylan found out about this assumption
        /// by reading code documentation.
        /// </summary>
        [TestMethod]
        public void TestConstructorForErrorHandler()
        {
            StubAppender stub = new StubAppender();
            Assert.IsNotNull(stub.ErrorHandler);
            Assert.IsInstanceOfType(stub.ErrorHandler, typeof(OnlyOnceErrorHandler));
        }

    }
}
