using ASP.Models;
using ASP.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class PlanServiceTests
{
    private readonly PlanService _planService;
    private readonly Mock<PlansDbContext> _dbContextMock;

    public PlanServiceTests()
    {
        var options = new DbContextOptionsBuilder<PlansDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _dbContextMock = new Mock<PlansDbContext>(options);
        _planService = new PlanService(_dbContextMock.Object);
    }

    [Fact]
    public async Task GetPlansAsync_ReturnsAllPlans()
    {
        // Arrange
        var plans = new List<Plan>
        {
            new Plan { Id = 1, PlanName = "Plan 1" },
            new Plan { Id = 2, PlanName = "Plan 2" }
        };
        _dbContextMock.Setup(db => db.Plans.ToListAsync()).ReturnsAsync(plans);

        // Act
        var result = await _planService.GetPlansAsync();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("Plan 1", result[0].PlanName);
        Assert.Equal("Plan 2", result[1].PlanName);
    }

    [Fact]
    public async Task GetPlanByIdAsync_ReturnsCorrectPlan()
    {
        // Arrange
        var plan = new Plan { Id = 1, PlanName = "Plan 1" };
        _dbContextMock.Setup(db => db.Plans.FindAsync(1)).ReturnsAsync(plan);

        // Act
        var result = await _planService.GetPlanByIdAsync(1);

        // Assert
        Assert.Equal("Plan 1", result.PlanName);
    }

    [Fact]
    public async Task AddPlanAsync_AddsPlan()
    {
        // Arrange
        var plan = new Plan { Id = 1, PlanName = "Plan 1" };

        // Act
        await _planService.AddPlanAsync(plan);

        // Assert
        _dbContextMock.Verify(db => db.Plans.Add(plan), Times.Once);
        _dbContextMock.Verify(db => db.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task UpdatePlanAsync_UpdatesPlan()
    {
        // Arrange
        var plan = new Plan { Id = 1, PlanName = "Plan 1" };

        // Act
        await _planService.UpdatePlanAsync(plan);

        // Assert
        _dbContextMock.Verify(db => db.Entry(plan).State = EntityState.Modified, Times.Once);
        _dbContextMock.Verify(db => db.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task DeletePlanAsync_DeletesPlan()
    {
        // Arrange
        var plan = new Plan { Id = 1, PlanName = "Plan 1" };
        _dbContextMock.Setup(db => db.Plans.FindAsync(1)).ReturnsAsync(plan);

        // Act
        await _planService.DeletePlanAsync(1);

        // Assert
        _dbContextMock.Verify(db => db.Plans.Remove(plan), Times.Once);
        _dbContextMock.Verify(db => db.SaveChangesAsync(default), Times.Once);
    }
}
