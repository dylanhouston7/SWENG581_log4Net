using Microsoft.VisualStudio.TestTools.UnitTesting;
using log4net.Appender;
using Moq;
using System.IO;
using log4net;
using log4net.Core;

namespace log4netUnitTest
{
    /// <summary>
    /// Unit tests the FileAppender class.
    /// </summary>
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
            _logger.Level = _logger.Hierarchy.LevelMap["Info"];

            _fileAppender = new FileAppender();

            _fileAppender.Name = "fileAppender";
            _fileAppender.File = _fileName;
            _fileAppender.AppendToFile = true;

            _fileAppender.LockingModel = new FileAppender.MinimalLock();
            _fileAppender.ActivateOptions();
  
            _logger.AddAppender(_fileAppender);

        }

        [TestMethod]
        public void Test_Should_Write_Info()
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
        public void Test_Should_Write_Debug()
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
        public void Test_Should_Write_Fatal()
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
        public void Test_Should_Write_Warn()
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


        [TestMethod]
        public void Test_Should_Append_Message()
        {
            var testString = "Append message";
            var outputString = string.Empty;

           
            var mockWriter = new Mock<TextWriter>(MockBehavior.Strict);
            mockWriter.Setup(x => x.Write(It.IsAny<string>())).Callback<string>(s => outputString = s);

            _fileAppender.Layout = _basicLayout;
            var t = mockWriter.Object;
            
            //Assign mock writer
            _fileAppender.Writer = mockWriter.Object;
            _fileAppender.ActivateOptions();

            //var mockLock = new Mock<FileAppender.ExclusiveLock>();
            //mockLock.Setup(x => x.AcquireLock()).Returns(new FileStream(_fileName,FileMode.Open));

            //_fileAppender.LockingModel = mockLock.Object;

            var eventData = new LoggingEventData();
            eventData.Message = testString;

            var loggingEvent = new LoggingEvent(eventData);
            _fileAppender.DoAppend(loggingEvent);

            //Assert
            Assert.AreEqual(testString, outputString);

        }

        [TestMethod]
        public void Test_Should_Write_With_Filter_Level()
        {
            var testString = "Append message";
            var outputString = string.Empty;

            var mockWriter = new Mock<TextWriter>(MockBehavior.Strict);
            mockWriter.Setup(x => x.Write(It.IsAny<string>())).Callback<string>(s => outputString = s);

            _fileAppender.Layout = _basicLayout;

            //Assign mock writer
            _fileAppender.Writer = mockWriter.Object;
            _fileAppender.ActivateOptions();

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
        public void Test_Should_NotWrite_With_Different_Filter_Level()
        {
            
            var testString = "Append message";
            var outputString = string.Empty;

            var mockWriter = new Mock<TextWriter>(MockBehavior.Strict);
            mockWriter.Setup(x => x.Write(It.IsAny<string>())).Callback<string>(s => outputString = s);

            _fileAppender.Layout = _basicLayout;

            //Assign mock writer
            _fileAppender.Writer = mockWriter.Object;
            _fileAppender.ActivateOptions();

            //Assign Different Level
            var eventData = new LoggingEventData();
            eventData.Message = testString;
            eventData.Level = Level.Info;


            var loggingEvent = new LoggingEvent(eventData);


            var level = new log4net.Filter.LevelMatchFilter();
            level.LevelToMatch = Level.Error;


            _fileAppender.AddFilter(level);
            _fileAppender.DoAppend(loggingEvent);



            //Assert
            Assert.AreEqual(string.Empty, outputString);

        }

        /// <summary>
        /// Validates that the encoding can be properly set for a file appender.
        /// </summary>
        [TestMethod]
        public void TestSetEncodingFlag()
        {
            FileAppender fileAppender = new FileAppender();
            fileAppender.Encoding = System.Text.Encoding.ASCII;
            Assert.AreEqual(System.Text.Encoding.ASCII, fileAppender.Encoding);
        }

        /// <summary>
        /// Validates that the append to file flag can be properly set for a file appender.
        /// </summary>
        [TestMethod]
        public void TestSetAppendToFileFlag()
        {
            FileAppender fileAppender = new FileAppender();
            fileAppender.AppendToFile = true;
            Assert.AreEqual(true, fileAppender.AppendToFile);

            fileAppender.AppendToFile = false;
            Assert.AreEqual(false, fileAppender.AppendToFile);
        }

        /// <summary>
        /// Ensures that the locking model is set to a default value, whenever the FileAppender
        /// does not have a locking model already set.
        /// </summary>
        [TestMethod]
        public void TestEnsureLockingModelDefaultSetForActivateOptions()
        {
            FileAppender fileAppender = new FileAppender();
            fileAppender.ActivateOptions();
            FileAppender.ExclusiveLock exclusiveLock = new FileAppender.ExclusiveLock();
            Assert.AreEqual(exclusiveLock.GetType(), fileAppender.LockingModel.GetType());
        }

        /// <summary>
        /// Ensures that the locking model is NOT set to a default value, whenever the FileAppender
        /// does have a locking model already set.
        /// </summary>
        [TestMethod]
        public void TestEnsureLockingModelDefaultNotSetForActivateOptions()
    {
            FileAppender fileAppender = new FileAppender();
            fileAppender.LockingModel = new FileAppender.ExclusiveLock();
            fileAppender.ActivateOptions();
            
            FileAppender.InterProcessLock interProcessLock = new FileAppender.InterProcessLock();
            Assert.AreNotEqual(interProcessLock.GetType(), fileAppender.LockingModel.GetType());
        }

    }
}
