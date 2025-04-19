using Xunit;
using ASP.Models;

namespace ASP.Tests
{
    public class PlanTests
    {
        [Fact]
        public void Plan_CanBeInitializedWithProperties()
        {
            // Arrange
            var plan = new Plan
            {
                Id = 1,
                OwnerId = "user123",
                PlanName = "Zombie Survival",
                Scenario = "City Escape",
                Shelter = "Basement",
                FoodSources = "Canned Beans",
                WaterSources = "Rainwater",
                Weapons = "Baseball Bat",
                Vehicles = "Pickup Truck",
                Fuel = "Gasoline",
                Budget = 2000.00m,
                TotalPrice = 1500.00m
            };

            // Assert
            Assert.Equal(1, plan.Id);
            Assert.Equal("user123", plan.OwnerId);
            Assert.Equal("Zombie Survival", plan.PlanName);
            Assert.Equal("City Escape", plan.Scenario);
            Assert.Equal("Basement", plan.Shelter);
            Assert.Equal("Canned Beans", plan.FoodSources);
            Assert.Equal("Rainwater", plan.WaterSources);
            Assert.Equal("Baseball Bat", plan.Weapons);
            Assert.Equal("Pickup Truck", plan.Vehicles);
            Assert.Equal("Gasoline", plan.Fuel);
            Assert.Equal(2000.00m, plan.Budget);
            Assert.Equal(1500.00m, plan.TotalPrice);
        }

        [Fact]
        public void Plan_DefaultValues_ShouldBeCorrect()
        {
            // Arrange
            var plan = new Plan();

            // Assert
            Assert.Equal(0, plan.Id);
            Assert.Null(plan.OwnerId);
            Assert.Null(plan.PlanName);
            Assert.Null(plan.Scenario);
            Assert.Null(plan.Shelter);
            Assert.Null(plan.FoodSources);
            Assert.Null(plan.WaterSources);
            Assert.Null(plan.Weapons);
            Assert.Null(plan.Vehicles);
            Assert.Null(plan.Fuel);
            Assert.Equal(0m, plan.Budget);
            Assert.Equal(0m, plan.TotalPrice);
        }
    }
}
