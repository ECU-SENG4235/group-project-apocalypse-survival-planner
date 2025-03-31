using System.Collections.Generic;

namespace ASP.Services
{
    public static class PlanPriceCalculator
    {
        private static readonly Dictionary<string, decimal> ItemPrices = new()
        {
            // Food
            { "Canned Food", 10 },
            { "Garden", 100 },
            { "Farm", 500 },
            { "Hunting/Fishing", 150 },
            { "MREs", 75 },
            { "Foraging", 30 },
            { "Insect Farming", 200 },

            // Water
            { "Bottled Water", 20 },
            { "River", 50 },
            { "Reservoir", 200 },
            { "Well", 100 },
            { "Rainwater Collection", 80 },
            { "Desalination", 400 },
            { "Water Purification Tablets", 15 },

            // Weapons
            { "Bat", 30 },
            { "Firearm", 600 },
            { "Chainsaw", 250 },
            { "Slingshot", 40 },
            { "Crossbow", 350 },
            { "Katana", 500 },
            { "Molotov Cocktails", 60 },
            { "Spear", 90 },
            { "Taser", 120 },

            // Vehicles
            { "Car", 10000 },
            { "Bicycle", 300 },
            { "Helicopter", 50000 },
            { "Boat", 8000 },
            { "Motorcycle", 3000 },
            { "Tank", 100000 },
            { "Horse", 5000 },
            { "ATV", 7000 },
            { "Bus", 25000 },

            // Fuel/Power
            { "Gasoline", 100 },
            { "Solar Panel", 1500 },
            { "Batteries", 50 },
            { "Hamster-wheel Generator", 200 },
            { "Wind Turbine", 2000 },
            { "Hydroelectric Generator", 3000 },
            { "Coal", 75 },
            { "Wood-Burning Stove", 150 },
            { "Biofuel", 125 }
        };

        public static decimal CalculateTotal(Dictionary<string, (int quantity, decimal price)> itemCounts)
        {
            decimal total = 0;
            foreach (var item in itemCounts.Values)
            {
                total += item.quantity * item.price;
            }
            return total;
        }

    }
}
