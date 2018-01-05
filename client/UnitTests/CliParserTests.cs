using System;
using FlyUnix.Cli;
using Xunit;

namespace UnitTests
{
    // Right parameters: fly -l login
    public class CliParserTests
    {
        [Fact]
        public void InvalidCountArguments()
        {
            string[] arguments =
            {
                "-l",
                "testing",
                "INVALID"
            };
            Arguments parsedArguments = new Arguments();
            bool result = Parser.Parse(arguments, ref parsedArguments);
            Assert.False(result);
        }

        [Fact]
        public void EmptyArguments()
        {
            string[] arguments = { };
            Arguments parsedArguments = new Arguments();
            bool result = Parser.Parse(arguments, ref parsedArguments);
            Assert.False(result);
        }

        [Fact]
        public void InvaliArgumentsName()
        {
            string[] arguments =
            {
                "-p",
                "testing"
            };
            Arguments parsedArguments = new Arguments();
            bool result = Parser.Parse(arguments, ref parsedArguments);
            Assert.False(result);
        }

        [Fact]
        public void ValidArguments()
        {
            string[] arguments =
            {
                "-l",
                "testing"
            };
            Arguments parsedArguments = new Arguments();
            bool result = Parser.Parse(arguments, ref parsedArguments);
            Assert.True(result);
        }

        [Fact]
        public void ValidStoringCredentials()
        {
            string[] arguments =
            {
                "-l",
                "testing"
            };
            Arguments parsedArguments = new Arguments();
            bool result = Parser.Parse(arguments, ref parsedArguments);
            if (result)
            {
                if (parsedArguments.Login != String.Empty)
                    Assert.True(true);
            }
            else
                Assert.True(false, "Wrong parsing arguments...");
        }
    }
}
