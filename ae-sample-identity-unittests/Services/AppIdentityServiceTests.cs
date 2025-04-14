using Microsoft.Extensions.Logging;
using Moq;
using FluentAssertions;
using Ae.Sample.Identity.Services;

namespace Ae.Sample.Identity.Unittests.Services
{
    public class AppIdentityServiceTests
    {
        private readonly AppIdentityService _service;

        public AppIdentityServiceTests()
        {
           
            _service = new AppIdentityService(
                Mock.Of<ILogger<AppIdentityService>>(),
                new AccountsService(Mock.Of<ILogger<AccountsService>>()));
        }

        [Fact]
        public async Task TryVerifyCredentialAsync_WithValidCredentials_ReturnsTrue()
        {
            // Arrange
            var username = "info@softaren.com";
            var password = "Demo";

            // Act
            var vcResult = await _service.TryVerifyCredentialAsync(username, password);

            // Assert
            vcResult.IsVerified.Should().BeTrue();
            vcResult.Principal.Should().NotBeNull();
        }

        [Fact]
        public async Task TryVerifyCredentialAsync_WithInvalidPassword_ReturnsFalse()
        {
            // Arrange
            var username = "info@softaren.com";
            var password = "WrongPassword";

            // Act
            var vcResult = await _service.TryVerifyCredentialAsync(username, password);

            // Assert
            vcResult.IsVerified.Should().BeFalse();
            vcResult.Principal.Should().BeNull();
        }

        [Fact]
        public async Task TryVerifyCredentialAsync_WithNonexistentUser_ReturnsFalse()
        {
            // Arrange
            var username = "nonexistent@example.com";
            var password = "Password123!";

            // Act
            var vcResult = await _service.TryVerifyCredentialAsync(username, password);

            // Assert
            vcResult.IsVerified.Should().BeFalse();
            vcResult.Principal.Should().BeNull();
        }

        [Theory]
        [InlineData(null, "password")]
        [InlineData("", "password")]
        [InlineData("username", null)]
        [InlineData("username", "")]
        public async Task TryVerifyCredentialAsync_WithInvalidInput_ReturnsFalse(string? username, string? password)
        {
            // Act
            var vcResult = await _service.TryVerifyCredentialAsync(username, password);

            // Assert
            vcResult.IsVerified.Should().BeFalse();
            vcResult.Principal.Should().BeNull();
        }
    }
}
