using Xunit;
using ASP.Models;
using System.Collections.Generic;

namespace ASP.Tests
{
    public class ItemCategoryTests
    {
        private ItemCategory GetSampleCategory()
        {
            return new ItemCategory
            {
                Labels = new[] { "Apples", "Bananas", "Cherries" },
                Prices = new[] { 1.0m, 0.5m, 2.0m },
                Quantities = new[] { 2, 0, 3 }
            };
        }

        [Fact]
        public void GetSelected_ReturnsCorrectItems()
        {
            var category = GetSampleCategory();
            var result = category.GetSelected();

            Assert.Equal(2, result.Count);
            Assert.Contains("Apples x2", result);
            Assert.Contains("Cherries x3", result);
            Assert.DoesNotContain("Bananas x0", result);
        }

        [Fact]
        public void GetTotal_ReturnsCorrectSum()
        {
            var category = GetSampleCategory();
            var total = category.GetTotal();

            // Apples: 2 x 1.0 = 2.0
            // Cherries: 3 x 2.0 = 6.0
            // Total = 8.0
            Assert.Equal(8.0m, total);
        }

        [Fact]
        public void GetItemCounts_ReturnsCorrectDictionary()
        {
            var category = GetSampleCategory();
            var dict = category.GetItemCounts();

            Assert.Equal(2, dict.Count);
            Assert.True(dict.ContainsKey("Apples"));
            Assert.True(dict.ContainsKey("Cherries"));
            Assert.False(dict.ContainsKey("Bananas"));

            Assert.Equal((2, 1.0m), dict["Apples"]);
            Assert.Equal((3, 2.0m), dict["Cherries"]);
        }

        [Fact]
        public void AllZeroQuantities_ReturnsEmptyResults()
        {
            var category = new ItemCategory
            {
                Labels = new[] { "A", "B", "C" },
                Prices = new[] { 1.0m, 1.0m, 1.0m },
                Quantities = new[] { 0, 0, 0 }
            };

            Assert.Empty(category.GetSelected());
            Assert.Equal(0.0m, category.GetTotal());
            Assert.Empty(category.GetItemCounts());
        }

        [Fact]
        public void EmptyArrays_ShouldReturnEmptyResults()
        {
            var category = new ItemCategory
            {
                Labels = new string[] { },
                Prices = new decimal[] { },
                Quantities = new int[] { }
            };

            Assert.Empty(category.GetSelected());
            Assert.Equal(0.0m, category.GetTotal());
            Assert.Empty(category.GetItemCounts());
        }
    }
}
