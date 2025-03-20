using Microsoft.EntityFrameworkCore;
using ASP.Data;
using ASP.Models;

public class PlanService
{
    private readonly PlansDbContext _context;

    public PlanService(PlansDbContext context)
    {
        _context = context;
    }

    public async Task<List<Plan>> GetPlansAsync()
    {
        return await _context.Plans.ToListAsync();
    }
}
