using System.ComponentModel.DataAnnotations;

namespace StudyCaseAGIT.Models
{
    public class ProductionAdjustment
    {
        [Key]
        public int Id { get; set; }
        public int ProductionPlanId { get; set; }
        public int Monday { get; set; }
        public int Tuesday { get; set; }
        public int Wednesday { get; set; }
        public int Thursday { get; set; }
        public int Friday { get; set; }
        public int Saturday { get; set; }
        public int Sunday { get; set; }
        public DateTime AdjustmentDate { get; set; }
    }
}
