using System.ComponentModel.DataAnnotations;

namespace PartialDischargeMeasurementApp.Models
{
    public class InsulationExplore
    {
        [Key]
        public int ExploreId { get; set; }
        [Required]
        public string InsulationName { get; set; }
        [Required]
        public double InsulationThickness { get; set; }
        [Required]
        public double AppliedVoltage { get; set; }
        [Required]
        public double Coefficient { get; set; }
        [Required]
        public string? Description { get; set; }

        // Relationships

        public List<RawData> RowDataOscillo { get; set; }
        public List<PDData> PDDatas { get; set; }
    }
}
