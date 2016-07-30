using log4net;
using log4net.Appender;
using log4net.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;

namespace log4netIntegrationTest
{
    public class FileAppenderIntegrationTest
    {
        ILog _log;
        log4net.Repository.Hierarchy.Logger _logger;
        FileAppender _fileAppender;
        log4net.Layout.PatternLayout _basicLayout = new log4net.Layout.PatternLayout("%m");
        string _fileName = "tempfile.txt";

        [TestMethod]
        public void Should_Write_Info()
        {
            //Arrange
            var testString = "Write INFO Message";
            var outputString = string.Empty;

            var mockWriter = new Mock<TextWriter>(MockBehavior.Strict);
            mockWriter.Setup(x => x.Write(It.IsAny<string>())).Callback<string>(s => outputString = s);

            _fileAppender.Layout = _basicLayout;

            var level = new log4net.Filter.LevelMatchFilter();
            level.LevelToMatch = Level.Info;


            _fileAppender.AddFilter(level);

            _fileAppender.LockingModel = new FileAppender.MinimalLock();

            //Assign mock writer
            _fileAppender.Writer = mockWriter.Object;
            _fileAppender.ActivateOptions();

            //Act
            _log.Info(testString);

            //Assert
            Assert.AreEqual(testString, outputString);

        }

        [TestMethod]
        public void Should_Write_Debug()
        {
            //Arrange
            var testString = "Write DEBUG Message";
            var outputString = string.Empty;

            var mockWriter = new Mock<TextWriter>(MockBehavior.Strict);
            mockWriter.Setup(x => x.Write(It.IsAny<string>())).Callback<string>(s => outputString = s);

            _fileAppender.Layout = _basicLayout;

            //Assign mock writer
            _fileAppender.Writer = mockWriter.Object;
            _fileAppender.ActivateOptions();

            //Act
            _log.Debug(testString);

            //Assert
            Assert.AreEqual(testString, outputString);
        }

        [TestMethod]
        public void Should_Write_Fatal()
        {
            //Arrange
            var testString = "Write FATAL Message";
            var outputString = string.Empty;

            var mockWriter = new Mock<TextWriter>(MockBehavior.Strict);
            mockWriter.Setup(x => x.Write(It.IsAny<string>())).Callback<string>(s => outputString = s);

            _fileAppender.Layout = _basicLayout;

            //Assign mock writer
            _fileAppender.Writer = mockWriter.Object;
            _fileAppender.ActivateOptions();

            //Act
            _log.Fatal(testString);

            //Assert
            Assert.AreEqual(testString, outputString);
        }

        [TestMethod]
        public void Should_Write_Warn()
        {
            //Arrange
            var testString = "Write WARN Message";
            var outputString = string.Empty;

            var mockWriter = new Mock<TextWriter>(MockBehavior.Strict);
            mockWriter.Setup(x => x.Write(It.IsAny<string>())).Callback<string>(s => outputString = s);

            _fileAppender.Layout = _basicLayout;

            //Assign mock writer
            _fileAppender.Writer = mockWriter.Object;
            _fileAppender.ActivateOptions();

            //Act
            _log.Warn(testString);

            //Assert
            Assert.AreEqual(testString, outputString);
        }
    }
}
