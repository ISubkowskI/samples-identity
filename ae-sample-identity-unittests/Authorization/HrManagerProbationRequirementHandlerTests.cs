using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Ae.Sample.Identity.Authorization;
using Ae.Sample.Identity.Data;

namespace Ae.Sample.Identity.Unittests.Authorization
{
    public class HrManagerProbationRequirementHandlerTests
    {
        [Fact]
        public async Task HandleRequirementAsync_UserIsNotOnProbation_Succeeds()
        {
            // Arrange
            var employmentDate = DateTimeOffset.Now.AddDays(-100);
            int probationMonths = 3;

            var requirement = new HrManagerProbationRequirement(probationMonths);
         
            var identity = new ClaimsIdentity(
                        [
                           new (AppClaimTypes.EmploymentDate, employmentDate.ToString("yyyy-MM-dd")),
                        ],
                        ConstsWebApp.CookieName);
            var principal = new ClaimsPrincipal(identity);
            var context = new AuthorizationHandlerContext([requirement], principal, null);

            var handler = new HrManagerProbationRequirementHandler();

            // Act
            await handler.HandleAsync(context);

            // Assert
            Assert.True(context.HasSucceeded);
        }

        [Fact]
        public async Task HandleRequirementAsync_UserIsOnProbation_Fails()
        {
            // Arrange
            var employmentDate = DateTimeOffset.Now.AddDays(-15);
            int probationMonths = 3;

            var requirement = new HrManagerProbationRequirement(probationMonths);

            var identity = new ClaimsIdentity(
                        [
                           new (AppClaimTypes.EmploymentDate, employmentDate.ToString("yyyy-MM-dd")),
                        ],
                        ConstsWebApp.CookieName);
            var principal = new ClaimsPrincipal(identity);
            var context = new AuthorizationHandlerContext([requirement], principal, null);

            var handler = new HrManagerProbationRequirementHandler();

            // Act
            await handler.HandleAsync(context);

            // Assert
            Assert.False(context.HasSucceeded);
        }

        [Fact]
        public async Task HandleRequirementAsync_UserHasNotClaimEmploymentDate_Fails()
        {
            // Arrange
            int probationMonths = 3;

            var requirement = new HrManagerProbationRequirement(probationMonths);

            var identity = new ClaimsIdentity(
                        [
                           new ("Department", "HR"),
                        ],
                        ConstsWebApp.CookieName);
            var principal = new ClaimsPrincipal(identity);
            var context = new AuthorizationHandlerContext([requirement], principal, null);

            var handler = new HrManagerProbationRequirementHandler();

            // Act
            await handler.HandleAsync(context);

            // Assert
            Assert.False(context.HasSucceeded);
        }

        [Fact]
        public async Task HandleRequirementAsync_UserHasIncorrectEmploymentDateFormat_Fails()
        {
            // Arrange
            int probationMonths = 3;
            var requirement = new HrManagerProbationRequirement(probationMonths);

            var identity = new ClaimsIdentity(
                        [
                           new (AppClaimTypes.EmploymentDate, "2025-35"),
                        ],
                        ConstsWebApp.CookieName);
            var principal = new ClaimsPrincipal(identity);
            var context = new AuthorizationHandlerContext([requirement], principal, null);

            var handler = new HrManagerProbationRequirementHandler();

            // Act
            await handler.HandleAsync(context);

            // Assert
            Assert.False(context.HasSucceeded);
        }
    }
}
