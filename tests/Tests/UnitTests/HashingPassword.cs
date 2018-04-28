using DatabaseController;
using Tests.IntegrationTests;
using Xunit;

namespace Tests.UnitTests
{
    public class HashingPassword
    {
        [Fact]
        public void HasPassword()
        {
            string hashedPassword = PasswordHasher.HashPassword(TestUserDevice.User.Password);
            bool success = PasswordHasher.VerifyHashedPassword(hashedPassword, TestUserDevice.User.Password);
            Assert.True(success);
        }

        [Fact]
        public void PasswordNullCheck()
        {
            bool success = PasswordHasher.VerifyHashedPassword(null, TestUserDevice.User.Password);
            Assert.False(success);
        }

        [Fact]
        public void HashPasswordNullCheck()
        {
            bool success = PasswordHasher.VerifyHashedPassword(TestUserDevice.User.Password, null);
            Assert.False(success);
        }

        [Fact]
        public void HashPasswordNull()
        {
            string pass = PasswordHasher.HashPassword(null);
            Assert.Null(pass);
        }
    }
}
