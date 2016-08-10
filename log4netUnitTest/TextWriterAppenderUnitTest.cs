using Microsoft.VisualStudio.TestTools.UnitTesting;

using log4net.Layout;
using log4net.Appender;
using System.Text;
using System.IO;
using log4net.Core;
using System;
using log4netUnitTest.Stubs;
using log4net.Util;
using System.Collections.Generic;

namespace log4netUnitTest
{
    /// <summary>
    /// Test Case TextWriterAppender-1: 
    /// </summary>
    /// <remarks>
    /// Path: 1-2-3(F)-5
    /// Input Vector:
    /// m_immediateFlush == False
    /// </remarks>   
    [TestClass]
    public class TextWriterAppenderUnitTest
    {   
        /// <summary>
        /// Stub for calling into the non-public methods of the TextAppender
        /// </summary>
        public class TextWriterAppenderStub
        {
            // TODO
        }

        /// <summary>
        /// Test Case TextWriterAppender-1: Tests setting the immediate flush to false
        /// </summary>
        /// <remarks>
        /// Input Vector:
        /// m_immediateFlush == False
        /// </remarks>   
        [TestMethod]
        public void TestTextWriterAppender_Append_ImmediateFlushFalse()
        {
            // Arrange
            bool flush_result;
            String input = "Test Log Message";
            StringBuilder result = new StringBuilder();
            StringWriter writer = new StringWriter(result);

            TextWriterAppender cut = new TextWriterAppender();
            cut.Layout = new PatternLayout("%message");
            cut.ImmediateFlush = false;
            cut.Writer = writer;

            LoggingEventData logData = new LoggingEventData();
            logData.Message = input;
            LoggingEvent logEvent = new LoggingEvent(logData);

            // Act
            cut.DoAppend(logEvent);
            flush_result = cut.ImmediateFlush;

            // Assert            
            Assert.AreEqual("", result, "Output is non-empty: " + result.ToString());
            Assert.AreEqual(false, flush_result, "Flush value mismatch");
        }

        /// <summary>
        /// Test Case TextWriterAppender-2: Tests setting the immediate flush to true
        /// </summary>
        /// <remarks>
        /// Input Vector:
        /// m_immediateFlush == True
        /// </remarks>   
        [TestMethod]
        public void TestTextWriterAppender_Append_ImmediateFlushTrue()
        {
            // Arrange
            bool flush_result;
            String input = "Test Log Message";
            StringBuilder result = new StringBuilder();
            StringWriter writer = new StringWriter(result);

            TextWriterAppender cut = new TextWriterAppender();
            cut.Writer = writer;
            cut.Layout = new PatternLayout("%message");
            cut.ImmediateFlush = true;

            LoggingEventData logData = new LoggingEventData();
            logData.Message = input;
            LoggingEvent logEvent = new LoggingEvent(logData);

            // Act
            cut.DoAppend(logEvent);
            flush_result = cut.ImmediateFlush;

            // Assert            
            Assert.AreEqual(true, flush_result);
            Assert.AreEqual(input, result.ToString(), "Actual Result: " + result.ToString());
        }

        /// <summary>
        /// Test Case TextWriterAppender-3: 
        /// </summary>
        /// <remarks>
        /// Path: 
        /// Input Vector:
        /// 
        /// </remarks>   
        [TestMethod]
        public void TestTextWriterAppender_CloseWriter()
        {
            // Arrange
            
            // Act
            
            // Assert            
            
        }

        /// <summary>
        /// Test Case TextWriterAppender-4: 
        /// </summary>
        /// <remarks>
        /// Path: 
        /// Input Vector:
        /// 
        /// </remarks>   
        [TestMethod]
        public void TestTextWriterAppender_OnClose()
        {
            // Arrange

            // Act

            // Assert            

        }

        /// <summary>
        /// Test Case TextWriterAppender-5: 
        /// </summary>
        /// <remarks>
        /// Path: 
        /// Input Vector:
        /// 
        /// </remarks>   
        [TestMethod]
        public void TestTextWriterAppender_PrepareWriter()
        {
            // Arrange

            // Act

            // Assert            

        }

        /// <summary>
        /// Test Case TextWriterAppender-6: 
        /// </summary>
        /// <remarks>
        /// Path: 
        /// Input Vector:
        /// 
        /// </remarks>   
        [TestMethod]
        public void TestTextWriterAppender_Reset()
        {
            // Arrange

            // Act

            // Assert            

        }

        /// <summary>
        /// Test Case TextWriterAppender-7: 
        /// </summary>
        /// <remarks>
        /// Path: 
        /// Input Vector:
        /// 
        /// </remarks>   
        [TestMethod]
        public void TestTextWriterAppender_WriteFooter()
        {
            // Arrange
            String input_footer = "Log Footer";
            String input_message = "Log Message";
            StringBuilder result = new StringBuilder();
            StringWriter writer = new StringWriter(result);

            TextWriterAppender cut = new TextWriterAppender();
            PatternLayout patt_layout = new PatternLayout("%message");
            patt_layout.Footer = input_footer;
            cut.Layout = patt_layout;
            cut.ImmediateFlush = true;
            cut.Writer = writer;

            LoggingEventData logData = new LoggingEventData();
            logData.Message = input_message;
            LoggingEvent logEvent = new LoggingEvent(logData);

            // Act            
            cut.DoAppend(logEvent);
            cut.Close();

            // Assert            
            Assert.AreEqual(input_message + input_footer, result.ToString(), "Actual Result: " + result.ToString());
        }

        /// <summary>
        /// Test Case TextWriterAppender-8: 
        /// </summary>
        /// <remarks>
        /// Path: 
        /// Input Vector:
        /// 
        /// </remarks>   
        [TestMethod]
        public void TestTextWriterAppender_WriteHeader()
        {
            // Arrange
            String input_header = "Log Header";
            String input_message = "Log Message";
            StringBuilder result = new StringBuilder();
            StringWriter writer = new StringWriter(result);

            TextWriterAppender cut = new TextWriterAppender();
            PatternLayout patt_layout = new PatternLayout("%message");
            patt_layout.Header = input_header;
            cut.Layout = patt_layout;
            cut.ImmediateFlush = true;
            cut.Writer = writer;

            LoggingEventData logData = new LoggingEventData();
            logData.Message = input_message;
            LoggingEvent logEvent = new LoggingEvent(logData);

            // Act
            cut.DoAppend(logEvent);

            // Assert            
            Assert.AreEqual(input_header + input_message, result.ToString(), "Actual Result: " + result.ToString());
        }

        /// <summary>
        /// Tests the getter for the error handler property
        /// </summary>
        [TestMethod]
        public void TestGetErrorHandler()
        {
            TextWriterAppender appender = new TextWriterAppender();
            IErrorHandler handler = appender.ErrorHandler;
            Assert.IsNotNull(handler);
        }

        /// <summary>
        /// Tests the getter for the error handler property
        /// </summary>
        [TestMethod]
        public void TestSetErrorHandler()
        {
            TextWriterAppender appender = new TextWriterAppender();
            OnlyOnceErrorHandler errorHandler = new OnlyOnceErrorHandler();
            appender.ErrorHandler = errorHandler;

            // Reference comparison
            Assert.AreEqual(errorHandler, appender.ErrorHandler);        
        }

        [TestMethod]
        public void TestSetImmediateFlushFalse()
        {
            //Arrange
            TextWriterAppender appender = new TextWriterAppender();
            bool expectedBool = false;
            
            //Act
            appender.ImmediateFlush = expectedBool;
            
            //Assert
            Assert.AreEqual(appender.ImmediateFlush,expectedBool);
        }
    }
}
