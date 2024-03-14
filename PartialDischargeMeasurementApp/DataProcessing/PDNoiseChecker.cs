
public class PDNoiseChecker
{
    private List<ParsedData> _rawData;
    private float _positiveNoise;
    private float _negativeNoise;
    public PDNoiseChecker(List<ParsedData> data)
    {
        _rawData = data;

        var positiveNoise = new List<float>();
        var negativeNoise = new List<float>();

        foreach (ParsedData element in _rawData)
        {
            if (element.CH2 > 0) positiveNoise.Add(element.CH2);
            if (element.CH2 < 0) negativeNoise.Add(element.CH2);
        }

        float noise = 0;

        for (int i = 0; i < positiveNoise.Count; i++)
        {
            noise += positiveNoise[i];
        }
        _positiveNoise = (noise / positiveNoise.Count) * 3;

        noise = 0;
        for (int i = 0; i < negativeNoise.Count; i++)
        {
            noise += negativeNoise[i];
        }
        _negativeNoise = (noise / negativeNoise.Count) * 3;

    }

    // TODO PDNoiseChecker refactoring and return diferent type of noise level
    public float GetPositiveNoiseLevel()
    {
        return _positiveNoise;
    }
    public float GetNegativeNoiseLevel()
    {
        return _negativeNoise;
    }
}