using ASP.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using ASP.Data;
using Microsoft.EntityFrameworkCore.InMemory;

namespace ASP.Tests
{
    public class PlanServiceTests
    {
        private PlansDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<PlansDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + System.Guid.NewGuid().ToString())
                .Options;

            return new PlansDbContext(options);
        }

        [Fact]
        public async Task GetPlansAsync_ReturnsAllPlans()
        {
            var context = GetDbContext();
            context.Plans.AddRange(new Plan { PlanName = "A" }, new Plan { PlanName = "B" });
            await context.SaveChangesAsync();

            var service = new PlanService(context);
            var plans = await service.GetPlansAsync();

            Assert.Equal(2, plans.Count);
        }

        [Fact]
        public async Task GetPlanByIdAsync_ReturnsCorrectPlan()
        {
            var context = GetDbContext();
            var plan = new Plan { Id = 1, PlanName = "Test" };
            context.Plans.Add(plan);
            await context.SaveChangesAsync();

            var service = new PlanService(context);
            var result = await service.GetPlanByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Test", result!.PlanName);
        }

        [Fact]
        public async Task AddPlanAsync_AddsPlanToDb()
        {
            var context = GetDbContext();
            var service = new PlanService(context);
            var plan = new Plan { PlanName = "New Plan" };

            await service.AddPlanAsync(plan);

            Assert.Single(context.Plans);
            Assert.Equal("New Plan", context.Plans.First().PlanName);
        }

        [Fact]
        public async Task UpdatePlanAsync_UpdatesPlanInDb()
        {
            var context = GetDbContext();
            var plan = new Plan { Id = 1, PlanName = "Old Name" };
            context.Plans.Add(plan);
            await context.SaveChangesAsync();

            plan.PlanName = "Updated Name";
            var service = new PlanService(context);
            await service.UpdatePlanAsync(plan);

            var updated = await context.Plans.FindAsync(1);
            Assert.Equal("Updated Name", updated!.PlanName);
        }

        [Fact]
        public async Task DeletePlanAsync_RemovesPlanFromDb()
        {
            var context = GetDbContext();
            var plan = new Plan { Id = 1 };
            context.Plans.Add(plan);
            await context.SaveChangesAsync();

            var service = new PlanService(context);
            await service.DeletePlanAsync(1);

            Assert.Empty(context.Plans);
        }

        [Fact]
        public async Task SavePlanAsync_AddsOrUpdatesBasedOnId()
        {
            var context = GetDbContext();
            var service = new PlanService(context);

            var newPlan = new Plan { PlanName = "New" };
            await service.SavePlanAsync(newPlan);
            Assert.Single(context.Plans);

            var existing = context.Plans.First();
            existing.PlanName = "Updated";
            await service.SavePlanAsync(existing);

            var updated = await context.Plans.FindAsync(existing.Id);
            Assert.Equal("Updated", updated!.PlanName);
        }
    }
}
