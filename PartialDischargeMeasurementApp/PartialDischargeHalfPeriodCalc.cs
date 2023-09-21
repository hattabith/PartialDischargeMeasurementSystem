


// C:\Users\Dmitriy\source\repos\PartialDischargeMeasurementSystem\PartialDischargeMeasurementApp\Temp\cutData1.txt



public class PartialDischargeHalfPeriodCalc
{
    private readonly List<PartialDischarhgeStruct> _partialDischargeElements;
    private readonly List<ParsedData> _parsedData;    
    public PartialDischargeHalfPeriodCalc(List<ParsedData> data)
    {
        if (data == null)
        {
            throw new Exception("Data is null");
        }
        _parsedData = data;
    }
    public void GetPD()
    {

    }
}
