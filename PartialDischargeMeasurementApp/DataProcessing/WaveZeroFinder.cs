
using System.Linq;

public class WaveZeroFinder
{
    private List<ParsedData> _rawData;
    private List<int> _zeroPoints;
    private List<float> _middleValues;

    float minimumZero = 0;

    public WaveZeroFinder(List<ParsedData> data)
    {
        _rawData = data;

        var filterWidth = (int)(_rawData.Count / 50);
        float middleSum = 0f;
        var zeroPoints = new List<int>();
        var middleValues = new List<float>();
        minimumZero = _rawData.Max(item => item.CH1) * 0.04f; 

        for (int i = 0;  i < _rawData.Count - filterWidth - 1; i++)
        {
             
            for (int j = i; j < i + filterWidth;  j++)
            {
                middleSum = middleSum + _rawData[j].CH1;
            }
            middleSum = middleSum / filterWidth;
            middleValues.Add(middleSum);
            if (middleSum <= minimumZero && middleSum >= (minimumZero * -1))
            {
                //_zeroPoints.Add((int)_rawData[(i + (filterWidth / 2))].Id);
                zeroPoints.Add(i + (filterWidth / 2 ) + 1);
                i += (int)(filterWidth / 2);
            }
            middleSum = 0f;
        }

        _zeroPoints = zeroPoints;
        _middleValues = middleValues;
    }
    public List<int> GetZeroData()
    {
        return _zeroPoints;
    }
    public List<float> GetMiddleValues()
    {
        return _middleValues;
    }
}