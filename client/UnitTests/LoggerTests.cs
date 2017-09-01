using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared.Logging;

namespace SharedUnitTests
{
    [TestClass]
    public class LoggerTests
    {
        [TestMethod]
        public void FormatLogTestWindows()
        {
            string now = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            string expectedResult = now + "INFO: " + "test" + Environment.NewLine;
            string msg = "test";
            BaseLogger logger = new BaseLogger();
            string formated = logger.FormatLog(now, LogEntryType.Info, msg, Environment.NewLine);
            Assert.IsTrue(formated == expectedResult);
        }
    }
}
