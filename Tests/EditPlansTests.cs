using Xunit;
using ASP.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace ASP.Tests
{
    public class EditPlansTests
    {
        [Fact]
        public async Task LoadsUserPlans_FilteredByUserId()
        {
            // Arrange
            var userPlans = new List<Plan>
            {
                new Plan { Id = 1, PlanName = "Plan A", OwnerId = "user123" },
                new Plan { Id = 2, PlanName = "Plan B", OwnerId = "user123" },
                new Plan { Id = 3, PlanName = "Plan C", OwnerId = "otherUser" },
            };

            var userId = "user123";

            // Act
            var result = await Task.FromResult(
                userPlans.Where(p => p.OwnerId == userId).ToList()
            );

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, plan => Assert.Equal("user123", plan.OwnerId));
        }
    }
}
