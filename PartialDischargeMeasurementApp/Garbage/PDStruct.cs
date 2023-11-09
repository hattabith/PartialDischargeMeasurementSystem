using PartialDischargeMeasurementApp.DataProcessing;

public struct PDStruct 
{
    public List<ParsedData> HalfWave {  get; set; }    
    public List<PDIdentifier> PDPoints { get; set; }
    public float PDEnergy { get; set; }
    public float PDPower { get; set; }
}

