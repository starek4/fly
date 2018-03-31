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
    }
}
