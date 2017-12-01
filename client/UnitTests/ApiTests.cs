using System;
using System.Linq;
using FlyApi;
using FlyApi.Enums;
using FlyApi.Exceptions;
using FlyApi.Mappers;
using FlyApi.Mock;
using Xunit;

namespace UnitTests
{
    public class ApiTests
    {
        [Fact]
        public void ApiPathMapperValidAddDevice()
        {
            string path = ApiPathMapper.GetPath(ApiPaths.AddDevice);
            Assert.Equal(path, "addDevice.php");
        }

        [Fact]
        public void DataTypeMapperValidLogin()
        {
            string type = DataTypeMapper.GetPath(DataTypes.Login);
            Assert.Equal(type, "Login");
        }


        // API calls
        [Fact]
        public void GetShutdownValid()
        {
            Client client = new Client(true);
            try
            {
                client.GetShutdownPending(ApiResponses.ValidMetadata).Wait();
            }
            catch (Exception exception)
            {
                Assert.True(false, "Exception thrown in http client: " + exception);
            }
        }

        [Fact]
        public void GetLoggedState()
        {
            Client client = new Client(true);
            try
            {
                client.GetLoggedState(ApiResponses.ValidMetadata).Wait();
            }
            catch (Exception exception)
            {
                Assert.True(false, "Exception thrown in http client: " + exception);
            }
        }

        [Fact]
        public void GetShutdownInvalidException()
        {
            Client client = new Client(true);
            var exception = Record.Exception(() => client.GetShutdownPending(ApiResponses.InvalidMetadata).Wait());
            if (exception != null)
                Assert.IsType(typeof(DatabaseException), exception.InnerException);
            else
                Assert.True(false, "Exception was not throw");
        }

        [Fact]
        public void GetLoggedStateInvalidException()
        {
            Client client = new Client(true);
            var exception = Record.Exception(() => client.GetLoggedState(ApiResponses.InvalidMetadata).Wait());
            if (exception != null)
                Assert.IsType(typeof(DatabaseException), exception.InnerException);
            else
                Assert.True(false, "Exception was not throw");
        }

        [Fact]
        public void VerifyUserLoginValid()
        {
            Client client = new Client(true);
            try
            {
                client.VerifyUserLogin(ApiResponses.ValidMetadata, ApiResponses.ValidMetadata).Wait();
            }
            catch (Exception exception)
            {
                Assert.True(false, "Exception thrown in http client: " + exception);
            }
        }

        [Fact]
        public void VerifyUserLoginInvalidException()
        {
            Client client = new Client(true);
            var exception = Record.Exception(() => client.VerifyUserLogin(ApiResponses.InvalidMetadata, ApiResponses.InvalidMetadata).Wait());
            if (exception != null)
                Assert.IsType(typeof(DatabaseException), exception.InnerException);
            else
                Assert.True(false, "Exception was not throw");
        }

        [Fact]
        public void VerifyDeviceIdValid()
        {
            Client client = new Client(true);
            try
            {
                client.VerifyDeviceId(ApiResponses.ValidMetadata).Wait();
            }
            catch (Exception exception)
            {
                Assert.True(false, "Exception thrown in http client: " + exception);
            }
        }

        [Fact]
        public void VerifyDeviceIdInvalidException()
        {
            Client client = new Client(true);
            var exception = Record.Exception(() => client.VerifyDeviceId(ApiResponses.InvalidMetadata).Wait());
            if (exception != null)
                Assert.IsType(typeof(DatabaseException), exception.InnerException);
            else
                Assert.True(false, "Exception was not throw");
        }

        [Fact]
        public void SetShutdownValid()
        {
            Client client = new Client(true);
            try
            {
                client.SetShutdownPending(ApiResponses.ValidMetadata).Wait();
            }
            catch (Exception exception)
            {
                Assert.True(false, "Exception thrown in http client: " + exception);
            }
        }

        [Fact]
        public void SetShutdownInvalidException()
        {
            Client client = new Client(true);
            var exception = Record.Exception(() => client.SetShutdownPending(ApiResponses.InvalidMetadata).Wait());
            if (exception != null)
                Assert.IsType(typeof(DatabaseException), exception.InnerException);
            else
                Assert.True(false, "Exception was not throw");
        }

        [Fact]
        public void SetLoggedStateValid()
        {
            Client client = new Client(true);
            try
            {
                client.SetLoggedState(ApiResponses.ValidMetadata).Wait();
            }
            catch (Exception exception)
            {
                Assert.True(false, "Exception thrown in http client: " + exception);
            }
        }

        [Fact]
        public void SetLoggedStateInvalidException()
        {
            Client client = new Client(true);
            var exception = Record.Exception(() => client.SetLoggedState(ApiResponses.InvalidMetadata).Wait());
            if (exception != null)
                Assert.IsType(typeof(DatabaseException), exception.InnerException);
            else
                Assert.True(false, "Exception was not throw");
        }

        [Fact]
        public void ClearShutdownValid()
        {
            Client client = new Client(true);
            try
            {
                client.ClearShutdownPending(ApiResponses.ValidMetadata).Wait();
            }
            catch (Exception exception)
            {
                Assert.True(false, "Exception thrown in http client: " + exception);
            }
        }

        [Fact]
        public void ClearShutdownInvalidException()
        {
            Client client = new Client(true);
            var exception = Record.Exception(() => client.ClearShutdownPending(ApiResponses.InvalidMetadata).Wait());
            if (exception != null)
                Assert.IsType(typeof(DatabaseException), exception.InnerException);
            else
                Assert.True(false, "Exception was not throw");
        }

        [Fact]
        public void ClearLoggedStateValid()
        {
            Client client = new Client(true);
            try
            {
                client.ClearLoggedState(ApiResponses.ValidMetadata).Wait();
            }
            catch (Exception exception)
            {
                Assert.True(false, "Exception thrown in http client: " + exception);
            }
        }

        [Fact]
        public void ClearLoggedStateInvalidException()
        {
            Client client = new Client(true);
            var exception = Record.Exception(() => client.ClearLoggedState(ApiResponses.InvalidMetadata).Wait());
            if (exception != null)
                Assert.IsType(typeof(DatabaseException), exception.InnerException);
            else
                Assert.True(false, "Exception was not throw");
        }

        [Fact]
        public void GetDevicesValid()
        {
            Client client = new Client(true);
            try
            {
                var devices = client.GetDevices(ApiResponses.ValidMetadata).Result;
                if (devices.ElementAt(0).Name == ApiResponses.MockDevice.Name)
                    Assert.True(true);
                else
                    Assert.True(false, "Wrong device!");
            }
            catch (Exception exception)
            {
                Assert.True(false, "Exception thrown in http client: " + exception);
            }
        }

        [Fact]
        public void GetDevicesInvalidException()
        {
            Client client = new Client(true);
            var exception = Record.Exception(() => client.GetDevices(ApiResponses.InvalidMetadata).Wait());
            if (exception != null)
                Assert.IsType(typeof(DatabaseException), exception.InnerException);
            else
                Assert.True(false, "Exception was not throw");
        }

        [Fact]
        public void AddDeviceValid()
        {
            Client client = new Client(true);
            try
            {
                client.AddDevice(ApiResponses.ValidMetadata, ApiResponses.ValidMetadata, ApiResponses.ValidMetadata, true).Wait();
            }
            catch (Exception exception)
            {
                Assert.True(false, "Exception thrown in http client: " + exception);
            }
        }

        [Fact]
        public void AddDeviceInvalidException()
        {
            Client client = new Client(true);
            var exception = Record.Exception(() => client.AddDevice(ApiResponses.InvalidMetadata, ApiResponses.InvalidMetadata, ApiResponses.InvalidMetadata, true).Wait());
            if (exception != null)
                Assert.IsType(typeof(DatabaseException), exception.InnerException);
            else
                Assert.True(false, "Exception was not throw");
        }

        [Fact]
        public void DeleteDeviceValid()
        {
            Client client = new Client(true);
            try
            {
                client.DeleteDevice(ApiResponses.ValidMetadata, ApiResponses.ValidMetadata).Wait();
            }
            catch (Exception exception)
            {
                Assert.True(false, "Exception thrown in http client: " + exception);
            }
        }

        [Fact]
        public void DeleteDeviceInvalidException()
        {
            Client client = new Client(true);
            var exception = Record.Exception(() => client.DeleteDevice(ApiResponses.InvalidMetadata, ApiResponses.InvalidMetadata).Wait());
            if (exception != null)
                Assert.IsType(typeof(DatabaseException), exception.InnerException);
            else
                Assert.True(false, "Exception was not throw");
        }
    }
}
