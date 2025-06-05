public class WaveHalfPeriodAnalyzer
{
    private List<ParsedData> _rawDataFromFile;
    private List<float> _zeroLevelSignal;


    /// <summary>
    /// Get half period waves raw data from file
    /// Type of data is List<ParsedData>
    /// </summary>
    /// <param name="rawDataFromFile"></param>
    public WaveHalfPeriodAnalyzer(List<ParsedData> rawDataFromFile)
    {
        _rawDataFromFile = rawDataFromFile;

        float minCH1 = 0f;
        float maxCH1 = 0f;

        foreach (ParsedData element in _rawDataFromFile)
        {
            if (element.CH1 < minCH1) minCH1 = element.CH1;
            if (element.CH1 > maxCH1) maxCH1 = element.CH1;
        }

        var middleNearZero = (minCH1 + maxCH1) / 2;

        Console.WriteLine();
        Console.WriteLine("Midle zero is: {0}", middleNearZero);
        Console.WriteLine();

        for (int i = 0; i < _rawDataFromFile.Count - 9; i++)
        {
            var middle = new List<float>();
            for (int im = i; im <= i + 9; im++)
            {
                middle.Add(_rawDataFromFile[im].CH1);
            }
            float middleZero = 0;
            foreach (var elements in middle)
            {
                middleZero += elements;
            }

            middleZero /= middle.Count();

            if (middleZero <= middleNearZero)
            {
                i = i + 9;
                _zeroLevelSignal.Add(_rawDataFromFile[i].Id);
            }
        }
    }

    public List<List<ParsedData>> GetHalfPeriodWaves()  // need make it
    {
        var halfPeriodWaves = new List<List<ParsedData>>();
        for (int i = 0; i <= _zeroLevelSignal.Count - 1; i++)
        {
            // need thinking about how get part of data
        }

        //return _rawDataFromFile;
        return null;
    }
}