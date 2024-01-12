using System.ComponentModel.DataAnnotations;

namespace PartialDischargeMeasurementApp.Models
{
    public class PDData
    {
        [Key] 
        public int PDId { get; set; }
        public int PDIdOscilloscope { get; set; }
        public int ExploreId { get; set; }
        public double Voltage { get; set; }
        public double Amplitude { get; set; }
        public double SinglePDCharge { get; set; }
        public double SinglePDPower { get; set; }

    }
}
