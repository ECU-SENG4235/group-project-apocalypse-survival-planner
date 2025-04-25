using ASP.Models;
using ASP.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ASP.Components.Pages
{
    public partial class CreatePlan : ComponentBase
    {
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        private string buttonText = "Save Plan";
        private string planName = "";

        private string _selectedScenario = "Zombie Virus Outbreak";
        private string selectedScenario
        {
            get => _selectedScenario;
            set
            {
                _selectedScenario = value;
                UpdateScenarioImage();
            }
        }

        private string budgetText = "";
        private decimal budget = 0;
        private bool budgetExceeded = false;
        private decimal overBudgetAmount = 0;

        private string scenarioImageUrl = "";
        private Dictionary<string, string> scenarioImages = new()
        {
            { "Zombie Virus Outbreak", "images/scenarios/ZombieApoc.webp" },
            { "Nuclear Fallout", "images/scenarios/NuclearFall.webp" },
            { "Alien Invasion", "images/scenarios/AlienInv.webp" },
            { "Ice Age", "images/scenarios/IceAge.webp" },
            { "Supervolcano Eruption", "images/scenarios/SuperVol.webp" },
            { "AI Uprising", "images/scenarios/AIUp.webp" },
            { "World War III", "images/scenarios/WW3.webp" },
            { "Biological Plague", "images/scenarios/BioPlag.webp" },
            { "Meteor Impact", "images/scenarios/MetImp.webp" }
        };

        private void UpdateScenarioImage()
        {
            scenarioImageUrl = scenarioImages.GetValueOrDefault(selectedScenario, null);
        }

        private string _selectedShelter = "Bunker";
        private string selectedShelter
        {
            get => _selectedShelter;
            set
            {
                _selectedShelter = value;
                UpdateShelterImage();
                RecalculateTotalPrice();
            }
        }

        private string shelterImageUrl = "";
        private Dictionary<string, string> shelterImages = new()
        {
            { "Bunker", "images/shelters/Bunker.webp" },
            { "Mountain Cabin", "images/shelters/MountainCabin.webp" },
            { "Metropolitan Apartment", "images/shelters/MetropolitanApartment.webp" },
            { "Suburban House", "images/shelters/SuburbanHouse.webp" },
            { "Medieval Castle", "images/shelters/MedievalCastle.webp" },
            { "Jungle Hut", "images/shelters/JungleHut.webp" },
            { "Military Base", "images/shelters/MilitaryBase.webp" },
            { "Cruise Ship", "images/shelters/CruiseShip.webp" },
            { "RV", "images/shelters/RV.webp" },
            { "Tent", "images/shelters/Tent.webp" }
        };

        private void UpdateShelterImage()
        {
            if (!shelterImages.TryGetValue(selectedShelter, out shelterImageUrl))
            {
                shelterImageUrl = null;
            }
        }

        private Dictionary<string, decimal> shelterPrices = new()
        {
            { "Bunker", 40000 },
            { "Mountain Cabin", 85000 },
            { "Metropolitan Apartment", 120000 },
            { "Suburban House", 250000 },
            { "Medieval Castle", 2000000 },
            { "Jungle Hut", 500 },
            { "Military Base", 5000000 },
            { "Cruise Ship", 1000000 },
            { "RV", 80000 },
            { "Tent", 100 }
        };

        private string GetShelterLabel(string shelterName)
        {
            return shelterPrices.TryGetValue(shelterName, out var price)
                ? $"{shelterName} (${price:N0})"
                : shelterName;
        }

        private ItemCategory Food = new()
        {
            Labels = new[] { "Canned Food", "Garden", "Farm", "Hunting/Fishing", "MREs", "Foraging", "Insect Farming" },
            Prices = new[] { 2m, 250m, 5000m, 400m, 10m, 20m, 150m },
            Quantities = new int[7]
        };

        private ItemCategory Water = new()
        {
            Labels = new[] { "Bottled Water", "River", "Reservoir", "Well", "Rainwater Collection", "Desalination", "Water Purification Tablets" },
            Prices = new[] { 1m, 125m, 10000m, 3000m, 200m, 10000m, 10m },
            Quantities = new int[7]
        };

        private ItemCategory Weapons = new()
        {
            Labels = new[] { "Bat", "Firearm", "Chainsaw", "Slingshot", "Crossbow", "Katana", "Molotov Cocktails", "Spear", "Taser" },
            Prices = new[] { 30m, 700m, 250m, 40m, 300m, 200m, 10m, 20m, 150m },
            Quantities = new int[9]
        };

        private ItemCategory Vehicles = new()
        {
            Labels = new[] { "Car", "Bicycle", "Helicopter", "Boat", "Motorcycle", "Tank", "Horse", "ATV", "Bus" },
            Prices = new[] { 15000m, 500m, 250000m, 8000m, 4000m, 300000m, 2500m, 7000m, 20000m },
            Quantities = new int[9]
        };

        private ItemCategory Fuel = new()
        {
            Labels = new[] { "Gasoline", "Solar Panel", "Batteries", "Hamster-wheel Generator", "Wind Turbine", "Hydroelectric Generator", "Coal", "Wood-Burning Stove", "Biofuel" },
            Prices = new[] { 4m, 300m, 20m, 50m, 2000m, 10000m, 100m, 200m, 300m },
            Quantities = new int[9]
        };

        private decimal totalPrice = 0;

        private void OnQuantityChanged(int value, int index, ItemCategory category)
        {
            category.Quantities[index] = value;
            RecalculateTotalPrice();
        }

        private void RecalculateTotalPrice()
        {
            var itemCounts = new Dictionary<string, (int, decimal)>();

            foreach (var pair in Food.GetItemCounts()) itemCounts[pair.Key] = pair.Value;
            foreach (var pair in Water.GetItemCounts()) itemCounts[pair.Key] = pair.Value;
            foreach (var pair in Weapons.GetItemCounts()) itemCounts[pair.Key] = pair.Value;
            foreach (var pair in Vehicles.GetItemCounts()) itemCounts[pair.Key] = pair.Value;
            foreach (var pair in Fuel.GetItemCounts()) itemCounts[pair.Key] = pair.Value;

            totalPrice = PlanPriceCalculator.CalculateTotal(itemCounts);

            if (shelterPrices.TryGetValue(selectedShelter, out var priceOfShelter))
            {
                totalPrice += priceOfShelter;
            }

            if (decimal.TryParse(budgetText, out var parsedBudget))
            {
                budget = parsedBudget;
                budgetExceeded = totalPrice > budget;
                overBudgetAmount = budgetExceeded ? totalPrice - budget : 0;
            }
            else
            {
                budgetExceeded = false;
                overBudgetAmount = 0;
            }
        }

        private async Task SavePlan()
        {
            if (string.IsNullOrWhiteSpace(planName))
            {
                buttonText = "Plan name cannot be empty.";
                return;
            }

            if (Food.GetSelected().Count == 0)
            {
                buttonText = "Please select at least one food source.";
                return;
            }
            if (Water.GetSelected().Count == 0)
            {
                buttonText = "Please select at least one water source.";
                return;
            }
            if (Weapons.GetSelected().Count == 0)
            {
                buttonText = "Please select at least one weapon.";
                return;
            }
            if (Vehicles.GetSelected().Count == 0)
            {
                buttonText = "Please select at least one vehicle.";
                return;
            }
            if (Fuel.GetSelected().Count == 0)
            {
                buttonText = "Please select at least one fuel source.";
                return;
            }

            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var userId = user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                buttonText = "User not authenticated.";
                return;
            }

            var newPlan = new Plan
            {
                PlanName = planName,
                Scenario = selectedScenario,
                Shelter = selectedShelter,
                FoodSources = string.Join(", ", Food.GetSelected()),
                WaterSources = string.Join(", ", Water.GetSelected()),
                Weapons = string.Join(", ", Weapons.GetSelected()),
                Vehicles = string.Join(", ", Vehicles.GetSelected()),
                Fuel = string.Join(", ", Fuel.GetSelected()),
                Budget = decimal.TryParse(budgetText, out var parsedBudget) ? parsedBudget : 0,
                TotalPrice = totalPrice,
                OwnerId = userId
            };

            await PlanService.AddPlanAsync(newPlan);
            buttonText = $"Plan '{planName}' saved successfully!";
        }
    }
}
