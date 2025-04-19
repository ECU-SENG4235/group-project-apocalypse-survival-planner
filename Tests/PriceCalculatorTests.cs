using Xunit;
using ASP.Services;
using System.Collections.Generic;

namespace ASP.Tests
{
    public class PlanPriceCalculatorTests
    {
        [Fact]
        public void CalculateTotal_ReturnsCorrectSum_ForValidItems()
        {
            var items = new Dictionary<string, (int quantity, decimal price)>
            {
                { "Canned Food", (3, 10m) },   // 30
                { "Rainwater Collection", (1, 80m) }, // 80
                { "Crossbow", (2, 350m) } // 700
            };

            var total = PlanPriceCalculator.CalculateTotal(items);

            Assert.Equal(810m, total);
        }

        [Fact]
        public void CalculateTotal_ReturnsZero_WhenNoItems()
        {
            var items = new Dictionary<string, (int quantity, decimal price)>();

            var total = PlanPriceCalculator.CalculateTotal(items);

            Assert.Equal(0m, total);
        }

        [Fact]
        public void CalculateTotal_IgnoresZeroQuantities()
        {
            var items = new Dictionary<string, (int quantity, decimal price)>
            {
                { "Canned Food", (0, 10m) },
                { "Solar Panel", (0, 1500m) }
            };

            var total = PlanPriceCalculator.CalculateTotal(items);

            Assert.Equal(0m, total);
        }

        [Fact]
        public void CalculateTotal_WorksWithDecimalPrices()
        {
            var items = new Dictionary<string, (int quantity, decimal price)>
            {
                { "Batteries", (4, 12.5m) } // 50.0
            };

            var total = PlanPriceCalculator.CalculateTotal(items);

            Assert.Equal(50.0m, total);
        }

        [Fact]
        public void CalculateTotal_WorksWithLargeQuantities()
        {
            var items = new Dictionary<string, (int quantity, decimal price)>
            {
                { "Biofuel", (1000, 125m) } // 125000
            };

            var total = PlanPriceCalculator.CalculateTotal(items);

            Assert.Equal(125000m, total);
        }
    }
}
