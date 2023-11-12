
public struct PDHalfPeriodData
{
    public List<ParsedData> PDList { get; set; } 
    public bool SignVoltage {  get; set; }   
    public  float AvgPDCurrent {  get; set; }
    public float AvgPDPower { get; set; }
    public float AvgPDEnergy {  get; set; }
    public int FreqrencyPD {  get; set; }
    public float VoltageFirstPD { get; set; }
}

