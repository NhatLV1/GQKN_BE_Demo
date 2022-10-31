using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PVI.GQKN.API.Services.ModelDTOs;

namespace PVI.GQKN.API.Services.Tests
{
    [TestClass()]
    public class AuthPVIServiceTests
    {
        [TestMethod()]
        public void login_should_success()
        {
            // Arrange
            IOptions<AppSettings> options = Options.Create<AppSettings>(new AppSettings()
            {
                Pias = new PiasCredentials() {
                    Url = "",
                    CpId = "ad5b04d374093366dd7bd7b69ad84151",
                    Key = "fbc5105b0f16a9ec26ec1db8484bf233ef67a686"
                }
            });
            var username = "cbkd1";
            var password = "abc123";
            var loggerMock = new Mock<ILogger<AuthPVIService>>();
            
            HttpClient client = new HttpClient();
            IAuthPVI authPVI = new AuthPVIService(client, options, loggerMock.Object);

            // Act
            var result = authPVI.Login(username, password, IAuthPVI.LOGIN_TYPE_QLCD).Result;

            // Assert
            Assert.IsTrue(result.Status == LoginResultPVIDto.STATUS_SUCCESS, result.ToString());
            Assert.IsTrue(!string.IsNullOrEmpty(result.Token), result.Message, result.ToString());
        }

        [TestMethod()]
        public void Verify_Token_Test_Should_Return_Failed()
        {
            // Arrange
            //var service = new AuthPVIService();

            // Act

            // Assert
        }

    }
}