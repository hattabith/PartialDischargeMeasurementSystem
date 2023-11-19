
using PartialDischargeMeasurementApp.DataProcessing;

public struct PDHalfPeriodData
{
    public List<ParsedData> PDList { get; set; } 
    public bool IsPositiveHalfPeriod {  get; set; }   
    //public  float AvgPDCurrent {  get; set; }
    //public float AvgPDPower { get; set; }
    //public float AvgPDEnergy {  get; set; }
    //public int FrequencyPD {  get; set; }
    //public float VoltageFirstPD { get; set; }
}

