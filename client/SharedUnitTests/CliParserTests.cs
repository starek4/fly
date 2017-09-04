using System;
using Shared.CLI;
using Xunit;

namespace SharedUnitTests
{
    public class CliParserTests
    {
        // Right parameters: client -l login -p password
        [Fact]
        public void InvalidOrderArguments()
        {
            string[] arguments = 
            {
                "-p",
                "-l",
                "testing",
                "testing"
            };
            Arguments parsedArguments = new Arguments();
            bool result = Parser.Parse(arguments, ref parsedArguments);
            Assert.False(result);
        }

        [Fact]
        public void InvalidCountArguments()
        {
            string[] arguments =
            {
                "-l",
                "testing",
                "-p",
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
                "testing",
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
                "-p",
                "testing",
                "-l",
                "testing"
            };
            Arguments parsedArguments = new Arguments();
            bool result = Parser.Parse(arguments, ref parsedArguments);
            Assert.True(result);
        }

        [Fact]
        public void ValidArgumentsDifferentOrder()
        {
            string[] arguments =
            {
                "-l",
                "testing",
                "-p",
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
                "testing",
                "-p",
                "testing"
            };
            Arguments parsedArguments = new Arguments();
            bool result = Parser.Parse(arguments, ref parsedArguments);
            if (result)
            {
                if (parsedArguments.Login != String.Empty && parsedArguments.Password != String.Empty)
                    Assert.True(true);
            }
            else
            {
                Assert.True(false, "Wrong parsing arguments...");
            }
        }
    }
}
