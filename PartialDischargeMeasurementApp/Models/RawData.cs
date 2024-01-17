
using System.ComponentModel.DataAnnotations;

namespace PartialDischargeMeasurementApp.Models
{
    public class RawData
    {
        [Key]
        public int RawDataId { get; set; }
        public int ExploreId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string FilePath { get; set; }
        public string RawDataText { get; set; }

    }
}
