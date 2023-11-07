
public class PDNoizeChecker
{
    private List<ParsedData> _rawData;
    private float _positiveNoize;
    private float _negativeNoize;
    public PDNoizeChecker(List<ParsedData> data)
    {
        _rawData = data;

        var pozitiveNoize = new List<float>();
        var negativeNoize = new List<float>();

        foreach (ParsedData element in _rawData)
        {
            if (element.CH2 > 0) pozitiveNoize.Add(element.CH2);
            if (element.CH2 < 0) negativeNoize.Add(element.CH2);
        }

        float noize = 0;

        for (int i = 0; i < pozitiveNoize.Count; i++)
        {
            noize += pozitiveNoize[i];
        }
        _positiveNoize = (noize / pozitiveNoize.Count) * 2;

        noize = 0;
        for (int i = 0; i < negativeNoize.Count; i++)
        {
            noize += negativeNoize[i];
        }
        _negativeNoize = (noize / negativeNoize.Count) * 2;

    }
    public float GetPozitiveNoizeLevel()
    {
        return _positiveNoize;
    }
    public float GetNegativeNoizeLevel()
    {
        return _negativeNoize;
    }
}