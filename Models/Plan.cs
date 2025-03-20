using System.ComponentModel.DataAnnotations;

namespace ASP.Models
{
    public class Plan
    {
        public int Id { get; set; }

        //[Required]
        public string? PlanName { get; set; }

        //[Required]
        public string? Scenario { get; set; }

        //[Required]
        public string? Shelter { get; set; }

        
        public string? FoodSources { get; set; }
        public string? WaterSources { get; set; }
        public string? Weapons { get; set; }
        public string? Vehicles { get; set; }
        public string? Fuel { get; set; }
    }
}
