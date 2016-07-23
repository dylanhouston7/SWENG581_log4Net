using System;
using System.IO;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using log4net;
using log4net.Appender;
using log4net.Core;

namespace log4netUnitTest
{
    [TestClass]
    public class ConsoleAppenderUnitTest
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            String input = "Test String";
            StringBuilder result = new StringBuilder();
            StringWriter resultWriter = new StringWriter(result);
            Console.SetOut(resultWriter);

            // Act
            log.Info(input);

            // Assert
            Assert.AreEqual(input, result.ToString());
        }
    }
}
