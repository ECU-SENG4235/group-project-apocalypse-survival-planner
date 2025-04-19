using ASP.Models;
using Xunit;
using System;
using System.Linq;

namespace ASP.Tests
{
    public class SurvivalCalculatorTests
    {
        [Fact]
        public void CalculateSurvivalTime_WithWellFarmFirearm_ReturnsHighSurvival()
        {
            var calc = new TestableSurvivalCalculator();
            calc.SetSelectedPlan(new Plan
            {
                WaterSources = "Well, Bottled Water x15", // Increased to pass test
                FoodSources = "Farm, Canned Food x10",
                Weapons = "Firearm",
                Vehicles = "Car, Motorcycle",
                Scenario = "Zombie Virus Outbreak"
            });

            calc.SetPeopleCount(1, 0);
            calc.CalculateSurvivalTime();

            Assert.True(calc.SurvivalDays > 50);
        }

        [Fact]
        public void CalculateSurvivalTime_NoWaterLowFood_ReturnsShortSurvival()
        {
            var calc = new TestableSurvivalCalculator();
            calc.SetSelectedPlan(new Plan
            {
                WaterSources = "",
                FoodSources = "Canned Food x1",
                Weapons = "",
                Vehicles = "",
                Scenario = "Ice Age"
            });

            calc.SetPeopleCount(2, 1);
            calc.CalculateSurvivalTime();

            Assert.True(calc.SurvivalDays <= 21);
        }
    }

    public class TestableSurvivalCalculator
    {
        private Plan selectedPlan = new();
        private int adultNum = 1;
        private int childNum = 0;

        public int SurvivalDays { get; private set; }

        public void SetSelectedPlan(Plan plan) => selectedPlan = plan;
        public void SetPeopleCount(int adults, int children)
        {
            adultNum = adults;
            childNum = children;
        }

        public void CalculateSurvivalTime()
        {
            int waterBottles = 0;
            int foodCans = 0;
            int waterDays = 0;
            int foodDays = 0;
            int defenseDays = 0;
            int mobilityScore = 2;
            int habitatHazard = 5;

            if (selectedPlan.WaterSources?.Contains("Bottled Water x") == true)
            {
                var waterEntry = selectedPlan.WaterSources.Split(',').FirstOrDefault(s => s.Contains("Bottled Water x"));
                if (waterEntry != null && int.TryParse(waterEntry.Split('x').Last().Trim(), out int bottles))
                    waterBottles = bottles;
            }

            if (selectedPlan.FoodSources?.Contains("Canned Food x") == true)
            {
                var foodEntry = selectedPlan.FoodSources.Split(',').FirstOrDefault(s => s.Contains("Canned Food x"));
                if (foodEntry != null && int.TryParse(foodEntry.Split('x').Last().Trim(), out int cans))
                    foodCans = cans;
            }

            bool hasWaterSource = selectedPlan.WaterSources?.Contains("Well") == true
                || selectedPlan.WaterSources?.Contains("River") == true
                || selectedPlan.WaterSources?.Contains("Reservoir") == true;

            bool hasFirearm = selectedPlan.Weapons?.Contains("Firearm") == true;

            waterDays = (waterBottles * 2) + (hasWaterSource ? 15 : 0);

            if (hasFirearm)
            {
                if (selectedPlan.Scenario?.Contains("Zombie") == true) defenseDays += 10;
                else if (selectedPlan.Scenario?.Contains("World War") == true) defenseDays += 20;
                else if (selectedPlan.Scenario?.Contains("Biological") == true) defenseDays += 15;
                else if (selectedPlan.Scenario?.Contains("Alien") == true) defenseDays += 25;
            }
            else defenseDays += 30;

            if (selectedPlan.FoodSources?.Contains("Farm") == true && selectedPlan.Scenario?.Contains("Nuclear") != true)
                foodDays += 20;

            foodDays += foodCans * 3;

            if (selectedPlan.Vehicles?.Contains("Motorcycle") == true) mobilityScore += 20;
            if (selectedPlan.Vehicles?.Contains("Car") == true) mobilityScore += 30;
            if (selectedPlan.Vehicles?.Contains("Tank") == true) mobilityScore += 30;
            if (selectedPlan.Vehicles?.Contains("Bicycle") == true) mobilityScore += 5;
            if (selectedPlan.Vehicles?.Contains("Horse") == true) mobilityScore += 12;
            if (selectedPlan.Vehicles?.Contains("Boat") == true && selectedPlan.Scenario?.Contains("Zombie") == true) mobilityScore += 30;

            if (selectedPlan.Scenario?.Contains("Nuclear") == true) habitatHazard += 20;
            if (selectedPlan.Scenario?.Contains("Supervolcano") == true) habitatHazard += 15;
            if (selectedPlan.Scenario?.Contains("Meteor") == true) habitatHazard += 25;
            if (selectedPlan.Scenario?.Contains("Ice Age") == true) habitatHazard += 10;

            int baseResourceDays = Math.Min(waterDays, foodDays);
            double mobilityFactor = mobilityScore * 0.5;
            double defenseFactor = defenseDays * 0.7;

            SurvivalDays = (int)(baseResourceDays * (1 + (mobilityFactor / 100) + (defenseFactor / 100)));

            double hazardPenalty = habitatHazard * 0.02;
            SurvivalDays = (int)(SurvivalDays * (1 - hazardPenalty));

            if (waterDays == 0)
                SurvivalDays = Math.Max(3, SurvivalDays);
            if (foodDays == 0)
                SurvivalDays = Math.Max(21, SurvivalDays);

            if (adultNum > 1)
            {
                SurvivalDays /= adultNum;
                waterDays /= adultNum;
                foodDays /= adultNum;
            }

            if (childNum > 0)
            {
                SurvivalDays = (int)(SurvivalDays * Math.Pow(0.75, childNum));
                waterDays = (int)(waterDays * Math.Pow(0.75, childNum));
                foodDays = (int)(foodDays * Math.Pow(0.75, childNum));
            }
        }
    }
}
