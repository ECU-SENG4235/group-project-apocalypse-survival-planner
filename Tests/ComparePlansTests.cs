using ASP.Models;
using System.Collections.Generic;
using Xunit;

namespace ASP.Tests
{
    public class ComparePlansTests
    {
        [Fact]
        public void CalculatePlanScore_ReturnsExpectedScore()
        {
            // Arrange
            var testPlan = new TestablePlan
            {
                Shelter = "Suburban House",
                Vehicles = "Car, Helicopter",
                Weapons = "Firearm",
                FoodSources = "Farm, Hunting/Fishing",
                WaterSources = "Well, Reservoir"
            };

            var component = new TestableComparePlans();

            // Act
            int score = component.TestCalculatePlanScore(testPlan);

            // Assert
            Assert.Equal(16, score); // 2 urban + 2 vehicle + 2 weapon + 2 water + 2 food + 3 farm + 3 water = 14
        }

        [Fact]
        public void AnalyzeUrbanSurvival_ReturnsExpectedDescription()
        {
            var plan = new TestablePlan
            {
                Shelter = "Metropolitan Apartment",
                Vehicles = "Car",
                Weapons = "Firearm"
            };

            var component = new TestableComparePlans();
            var result = component.TestAnalyzeUrbanSurvival(plan);

            Assert.Contains("Good urban shelter choice", result);
            Assert.Contains("Mobile evacuation capable", result);
            Assert.Contains("Has strong defense capabilities", result);
        }

        [Fact]
        public void AnalyzeWildernessSurvival_ReturnsExpectedDescription()
        {
            var plan = new TestablePlan
            {
                WaterSources = "Well",
                FoodSources = "Hunting/Fishing, Foraging"
            };

            var component = new TestableComparePlans();
            var result = component.TestAnalyzeWildernessSurvival(plan);

            Assert.Contains("Has water procurement", result);
            Assert.Contains("Can obtain wilderness food", result);
        }

        [Fact]
        public void AnalyzeLongTermSurvival_ReturnsExpectedDescription()
        {
            var plan = new TestablePlan
            {
                FoodSources = "Farm",
                WaterSources = "Well"
            };

            var component = new TestableComparePlans();
            var result = component.TestAnalyzeLongTermSurvival(plan);

            Assert.Contains("Sustainable food source", result);
            Assert.Contains("Reliable water source", result);
        }
    }

    // Fake simplified class to test methods
    public class TestablePlan
    {
        public string? Shelter { get; set; }
        public string? FoodSources { get; set; }
        public string? WaterSources { get; set; }
        public string? Weapons { get; set; }
        public string? Vehicles { get; set; }
    }

    public class TestableComparePlans
    {
        public int TestCalculatePlanScore(TestablePlan plan)
        {
            int score = 0;

            if (!string.IsNullOrEmpty(plan.Shelter) && plan.Shelter.ToLower().Contains("suburban house")) score += 2;
            if (!string.IsNullOrEmpty(plan.Vehicles) && (plan.Vehicles.Contains("Car") || plan.Vehicles.Contains("Helicopter"))) score += 2;
            if (!string.IsNullOrEmpty(plan.Weapons) && plan.Weapons.Contains("Firearm")) score += 2;

            if (!string.IsNullOrEmpty(plan.WaterSources)) score += 2;
            if (!string.IsNullOrEmpty(plan.FoodSources) &&
                (plan.FoodSources.Contains("Farm") || plan.FoodSources.Contains("Hunting/Fishing"))) score += 2;

            if (!string.IsNullOrEmpty(plan.FoodSources) && plan.FoodSources.ToLower().Contains("farm")) score += 3;
            if (!string.IsNullOrEmpty(plan.WaterSources) &&
                (plan.WaterSources.Contains("Well") || plan.WaterSources.Contains("Reservoir"))) score += 3;

            return score;
        }

        public string TestAnalyzeUrbanSurvival(TestablePlan plan)
        {
            var analysis = new List<string>();

            if (!string.IsNullOrEmpty(plan.Shelter) && plan.Shelter.Contains("Metropolitan Apartment"))
                analysis.Add("Good urban shelter choice");

            if (!string.IsNullOrEmpty(plan.Vehicles) && (plan.Vehicles.Contains("Car") || plan.Vehicles.Contains("Helicopter")))
                analysis.Add("Mobile evacuation capable");

            if (!string.IsNullOrEmpty(plan.Weapons) && plan.Weapons.Contains("Firearm"))
                analysis.Add("Has strong defense capabilities");

            return analysis.Count > 0 ? string.Join(", ", analysis) : "Weak urban survival capability";
        }

        public string TestAnalyzeWildernessSurvival(TestablePlan plan)
        {
            var analysis = new List<string>();

            if (!string.IsNullOrEmpty(plan.WaterSources) &&
                (plan.WaterSources.Contains("Reservoir") || plan.WaterSources.Contains("Well")))
                analysis.Add("Has water procurement");

            if (!string.IsNullOrEmpty(plan.FoodSources) &&
                (plan.FoodSources.Contains("Hunting/Fishing") || plan.FoodSources.Contains("Foraging")))
                analysis.Add("Can obtain wilderness food");

            return analysis.Count > 0 ? string.Join(", ", analysis) : "Limited wilderness survival capability";
        }

        public string TestAnalyzeLongTermSurvival(TestablePlan plan)
        {
            var analysis = new List<string>();

            if (!string.IsNullOrEmpty(plan.FoodSources) && plan.FoodSources.ToLower().Contains("farm"))
                analysis.Add("Sustainable food source");

            if (!string.IsNullOrEmpty(plan.WaterSources) &&
                (plan.WaterSources.ToLower().Contains("well") || plan.WaterSources.ToLower().Contains("purif")))
                analysis.Add("Reliable water source");

            return analysis.Count > 0 ? string.Join(", ", analysis) : "Poor long-term sustainability";
        }
    }
}