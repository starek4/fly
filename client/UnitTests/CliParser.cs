using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared.CLI;

namespace UnitTests
{
    [TestClass]
    public class CliParser
    {
        // Right parameters: client -l login -p password
        [TestMethod]
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
            Assert.IsFalse(result);
        }

        [TestMethod]
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
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void EmptyArguments()
        {
            string[] arguments = { };
            Arguments parsedArguments = new Arguments();
            bool result = Parser.Parse(arguments, ref parsedArguments);
            Assert.IsFalse(result);
        }

        [TestMethod]
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
            Assert.IsFalse(result);
        }

        [TestMethod]
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
            Assert.IsTrue(result);
        }

        [TestMethod]
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
            Assert.IsTrue(result);
        }

        [TestMethod]
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
                    Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail("Wrong parsing arguments...");
            }
        }
    }
}
