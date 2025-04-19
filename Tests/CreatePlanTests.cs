using ASP.Models;
using Xunit;

namespace ASP.Tests
{
    public class PlanModelTests
    {
        [Fact]
        public void CanCreatePlan_WithValidValues()
        {
            // Arrange
            var plan = new Plan
            {
                Id = 1,
                PlanName = "Test Plan",
                Budget = 5000
            };

            // Act & Assert
            Assert.Equal(1, plan.Id);
            Assert.Equal("Test Plan", plan.PlanName);
            Assert.Equal(5000, plan.Budget);
        }
    }
}
