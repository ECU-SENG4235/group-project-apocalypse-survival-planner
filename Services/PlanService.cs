using ASP.Data;
using ASP.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PlanService
{
    private readonly PlansDbContext _dbContext;

    public PlanService(PlansDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Plan>> GetPlansAsync()
    {
        return await _dbContext.Plans.ToListAsync();
    }

    public async Task<Plan> GetPlanByIdAsync(int id)
    {
        return await _dbContext.Plans.FindAsync(id);
    }

    public async Task AddPlanAsync(Plan plan)
    {
        _dbContext.Plans.Add(plan);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdatePlanAsync(Plan plan)
    {
        _dbContext.Entry(plan).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeletePlanAsync(int id)
    {
        var plan = await _dbContext.Plans.FindAsync(id);
        if (plan != null)
        {
            _dbContext.Plans.Remove(plan);
            await _dbContext.SaveChangesAsync();
        }
    }
    public async Task SavePlanAsync(Plan plan)
    {
        if (plan.Id == 0)
        {
            await AddPlanAsync(plan);
        }
        else
        {
            await UpdatePlanAsync(plan);
        }
    }
}

