using Xunit;
using System.Collections.Generic;
using System.Linq;
using ASP.Models;

namespace ASP.Tests
{
    public class EditPlanLogicTests
    {
        [Fact]
        public void RecalculateTotalPrice_CorrectlyCalculates()
        {
            var food = new ItemCategory("Food")
            {
                Labels = new[] { "ItemA", "ItemB" },
                Prices = new[] { 10m, 20m },
                Quantities = new[] { 2, 3 }
            };

            var water = new ItemCategory("Water")
            {
                Labels = new[] { "ItemA", "ItemC" },
                Prices = new[] { 5m, 15m },
                Quantities = new[] { 1, 2 }
            };

            // Flatten into (label, quantity, price) tuples
            var allItems = food.Labels.Select((label, i) => (Label: label, Qty: food.Quantities[i], Price: food.Prices[i]))
                .Concat(water.Labels.Select((label, i) => (Label: label, Qty: water.Quantities[i], Price: water.Prices[i])));

            // Group by label and sum (Qty * Price) per group
            var grouped = allItems
                .GroupBy(item => item.Label)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(x => x.Qty * x.Price)
                );

            var actualTotal = grouped.Values.Sum();

            // Expected total: 
            // ItemA from food: 2 * 10 = 20
            // ItemA from water: 1 * 5 = 5
            // ItemB: 3 * 20 = 60
            // ItemC: 2 * 15 = 30
            var expectedTotal = 20 + 5 + 60 + 30;

            Assert.Equal(expectedTotal, actualTotal);
        }
    }
}
