using Microsoft.VisualStudio.TestTools.UnitTesting;
using log4net.Appender;
using Moq;
using System.IO;
using log4net;
using log4net.Core;

namespace log4netUnitTest
{
    [TestClass]
    public class FileAppenderTest
    {
        ILog _log;
        log4net.Repository.Hierarchy.Logger _logger;
        FileAppender _fileAppender;
        log4net.Layout.PatternLayout _basicLayout = new log4net.Layout.PatternLayout("%m");
        string _fileName = "tempfile.txt";

        [TestInitialize]
        public void Setup()
        {
            _log = LogManager.GetLogger("fileappenderlog");
            _logger = (log4net.Repository.Hierarchy.Logger)_log.Logger;
            _logger.Hierarchy.Configured = true;
            _logger.Level = _logger.Hierarchy.LevelMap["Error"];

            _fileAppender = new FileAppender();

            _fileAppender.Name = "fileAppender";
            _fileAppender.File = _fileName;
            _fileAppender.AppendToFile = true;

            _fileAppender.ActivateOptions();

            _logger.AddAppender(_fileAppender);

        }

        [TestMethod]
        public void Should_Append_Message()
        {
            var testString = "Append message";
            var outputString = string.Empty;

            var mockWriter = new Mock<TextWriter>(MockBehavior.Strict);
            mockWriter.Setup(x => x.Write(It.IsAny<string>())).Callback<string>(s => outputString = s);

            _fileAppender.Layout = _basicLayout;

            //Assign mock writer
            _fileAppender.Writer = mockWriter.Object;

            var eventData = new LoggingEventData();
            eventData.Message = testString;

            var loggingEvent = new LoggingEvent(eventData);
            _fileAppender.DoAppend(loggingEvent);


        }

        [TestMethod]
        public void Should_Write_With_Filter_Level()
        {
            var testString = "Append message";
            var outputString = string.Empty;

            var mockWriter = new Mock<TextWriter>(MockBehavior.Strict);
            mockWriter.Setup(x => x.Write(It.IsAny<string>())).Callback<string>(s => outputString = s);

            _fileAppender.Layout = _basicLayout;

            //Assign mock writer
            _fileAppender.Writer = mockWriter.Object;

            var eventData = new LoggingEventData();
            eventData.Message = testString;
            eventData.Level = Level.Error;


            var loggingEvent = new LoggingEvent(eventData);


            var level = new log4net.Filter.LevelMatchFilter();
            level.LevelToMatch = Level.Error;


            _fileAppender.AddFilter(level);
            _fileAppender.DoAppend(loggingEvent);

            //Assert
            Assert.AreEqual(testString, outputString);
        }

        [TestMethod]
        public void Should_NotWrite_With_Different_Filter_Level()
        {
            var testString = "Append message";
            var outputString = string.Empty;

            var mockWriter = new Mock<TextWriter>(MockBehavior.Strict);
            mockWriter.Setup(x => x.Write(It.IsAny<string>())).Callback<string>(s => outputString = s);

            _fileAppender.Layout = _basicLayout;

            //Assign mock writer
            _fileAppender.Writer = mockWriter.Object;

            //Assign Different Level
            var eventData = new LoggingEventData();
            eventData.Message = testString;
            eventData.Level = Level.Info;


            var loggingEvent = new LoggingEvent(eventData);


            var level = new log4net.Filter.LevelMatchFilter();
            level.LevelToMatch = Level.Info;


            _fileAppender.AddFilter(level);
            _fileAppender.DoAppend(loggingEvent);



            //Assert
            Assert.AreEqual(string.Empty, outputString);

        }

        // MOVE THESE TEST TO INTEGRATION
        [TestMethod]
        public void Should_Write_Info()
        {

            //Arrange
            var testString = "Write INFO Message";
            var outputString = string.Empty;

            var mockWriter = new Mock<TextWriter>(MockBehavior.Strict);
            mockWriter.Setup(x => x.Write(It.IsAny<string>())).Callback<string>(s => outputString = s);

            _fileAppender.Layout = _basicLayout;

            //Assign mock writer
            _fileAppender.Writer = mockWriter.Object;

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

            //Act
            _log.Warn(testString);

            //Assert
            Assert.AreEqual(testString, outputString);
        }
    }
}
