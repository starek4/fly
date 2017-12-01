using System;
using System.Globalization;
using Shared.Enviroment;
using Shared.Logging;
using Xunit;

namespace UnitTests
{
    public class LoggerTests
    {
        [Fact]
        public void FormatLogTestWindows()
        {
            string now = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            string expectedResult = now + " " + "INFO: " + "test" + Environment.NewLine;
            string msg = "test";
            BaseLogger logger = new BaseLogger();
            string formated = logger.FormatLog(now, LogEntryType.Info, msg, Environment.NewLine);
            Assert.True(formated == expectedResult);
        }

        [Fact]
        public void GetLoggerValid()
        {
            var logger = EnviromentHelper.GetLogger();
            if (logger is WindowsLogger)
                Assert.True(true);
            else
                Assert.True(false, "Wrong type of logger...");
        }
    }
}
