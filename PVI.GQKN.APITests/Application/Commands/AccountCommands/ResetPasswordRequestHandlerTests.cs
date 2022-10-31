using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PVI.GQKN.API.Services;
using PVI.GQKN.Domain.Models.Identity;
using PVI.GQKN.Infrastructure.Contracts;
using PVI.GQKN.Infrastructure.Idempotency;
using System.Configuration;
using System.Net.Http;

namespace PVI.GQKN.API.Application.Commands.AccountCommands.Tests
{
    [TestClass()]
    public class ResetPasswordRequestHandlerTests
    {
        private (Mock<UserManager<ApplicationUser>> userManager,
            Mock<ILogger<ResetPasswordRequestHandler>> logger,
            Mock<IConfiguration> configurations,
            Mock<IUrlHelper> urlHelper,
            Mock<IEmailSender> emailSender,
            Mock<IBieuMauRepository> bieuMauRepository,
            Mock<IViewRenderService> viewRenderService) CreateMocks()
        {
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
               Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null
             );
            var mockLogger = new Mock<ILogger<ResetPasswordRequestHandler>>();
            var mockConfigurations = new Mock<IConfiguration>();
            var mockUrlHelper = new Mock<IUrlHelper>();
            var mockEmailSender = new Mock<IEmailSender>();
            var mockBieuMauRepository = new Mock<IBieuMauRepository>();
            var mockViewRenderService = new Mock<IViewRenderService>();

            return (mockUserManager, 
                mockLogger, mockConfigurations, mockUrlHelper, mockEmailSender, mockBieuMauRepository, mockViewRenderService);
        }

        [TestMethod()]
        public async Task User_not_exists_should_return_false()
        {
            // Arrange
            var command = new ResetPasswordRequest(email: "trieu.lth@gmail.com");
            var mocks = CreateMocks();

            var commandHandler = new ResetPasswordRequestHandler(
                userManager: mocks.userManager.Object,
                logger: mocks.logger.Object,
                configurations: mocks.configurations.Object,
                urlHelper: mocks.urlHelper.Object,
                emailSender: mocks.emailSender.Object,
                bieuMauRepository: mocks.bieuMauRepository.Object,
                viewRenderService: mocks.viewRenderService.Object
            );

            ApplicationUser user = null;
                mocks.userManager.Setup(p =>  p.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            CancellationToken cancelToken = default(CancellationToken);

            // Act
            var result = await commandHandler.Handle(command, cancelToken);

            // Assert
            Assert.IsTrue(result == false);
        }

        [TestMethod()]
        public async Task send_email_should_return_true()
        {
            // Arrange
            var mockUserEmail = "trieu.lth@gmail.com";
            var command = new ResetPasswordRequest(email: mockUserEmail);
            var mocks = CreateMocks();

            var commandHandler = new ResetPasswordRequestHandler(
                userManager: mocks.userManager.Object,
                logger: mocks.logger.Object,
                configurations: mocks.configurations.Object,
                urlHelper: mocks.urlHelper.Object,
                emailSender: mocks.emailSender.Object,
                bieuMauRepository: mocks.bieuMauRepository.Object,
                viewRenderService: mocks.viewRenderService.Object
            );

            ApplicationUser fakeUser = new ApplicationUser() { 
                Email = mockUserEmail
            };
            mocks.userManager.Setup(p => p.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(fakeUser);
            mocks.userManager.Setup(p => p.GeneratePasswordResetTokenAsync(fakeUser)).ReturnsAsync("faketoken");

            HttpContext fakeHttpContext = new DefaultHttpContext();
            var fakeContext = new ActionContext(fakeHttpContext, new RouteData(), new ActionDescriptor());
            mocks.urlHelper.Setup(p => p.ActionContext).Returns(fakeContext);

            var fakeBieuMau = new BieuMauEmail() { 
                TieuDe = "",
                NoiDung = ""
            };
            mocks.bieuMauRepository.Setup(e => e.GetByGuidAsync(It.IsAny<string>()))
                .ReturnsAsync(fakeBieuMau);

            CancellationToken cancelToken = default(CancellationToken);
            mocks.emailSender.Setup(s => s.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await commandHandler.Handle(command, cancelToken);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void  bieu_mau_not_exists_should_throws_ConfigurationErrorsException()
        {
            // Arrange
            var mockUserEmail = "trieu.lth@gmail.com";
            var command = new ResetPasswordRequest(email: mockUserEmail);
            var mocks = CreateMocks();

            var commandHandler = new ResetPasswordRequestHandler(
                userManager: mocks.userManager.Object,
                logger: mocks.logger.Object,
                configurations: mocks.configurations.Object,
                urlHelper: mocks.urlHelper.Object,
                emailSender: mocks.emailSender.Object,
                bieuMauRepository: mocks.bieuMauRepository.Object,
                viewRenderService: mocks.viewRenderService.Object
            );

            ApplicationUser fakeUser = new ApplicationUser()
            {
                Email = mockUserEmail
            };
            mocks.userManager.Setup(p => p.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(fakeUser);
            mocks.userManager.Setup(p => p.GeneratePasswordResetTokenAsync(fakeUser)).ReturnsAsync("faketoken");

            HttpContext fakeHttpContext = new DefaultHttpContext();
            var fakeContext = new ActionContext(fakeHttpContext, new RouteData(), new ActionDescriptor());
            mocks.urlHelper.Setup(p => p.ActionContext).Returns(fakeContext);
            BieuMauEmail fakeBieuMau = null;
            mocks.bieuMauRepository.Setup(p => p.GetByGuidAsync(It.IsAny<string>())).ReturnsAsync(fakeBieuMau);

            CancellationToken cancelToken = default(CancellationToken);
            mocks.emailSender.Setup(s => s.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            // Act
            var exceptionThrown = Assert.ThrowsExceptionAsync<ConfigurationErrorsException>(async () => await commandHandler.Handle(command, cancelToken));

            // Assert
            Assert.IsTrue(exceptionThrown != null);

        }
    }
}