using System.Globalization;

public class TXTFileReader : IFileReader
{
    private readonly string _fileName;
    private readonly List<ParsedData> _data;

    public TXTFileReader(string fileName)
    {
        _fileName = fileName;

        if (_fileName == null)
        {
            throw new Exception("File name is null");
        }
        if (!File.Exists(_fileName))
        {
            throw new Exception("File does not exist");
        }

        CultureInfo.CurrentCulture = new CultureInfo("en-US");
        CultureInfo.CurrentUICulture = new CultureInfo("en-US");

        string[] lines = File.ReadAllLines(_fileName);

        if (lines == null)
        {
            throw new Exception("File is null");
        }
        if (lines.Length == 0)
        {
            throw new Exception("File is empty");
        }
        string[] dataLines = new string[lines.Length - 11];   // Need 5 for USB save or 11 for Oscillo Save
        for (int i = 11; i < lines.Length; i++)    // Need 5 for USB save or 11 for Oscillo Save
        {
            dataLines[i - 11] = lines[i]; // Need 5 for USB save or 11 for Oscillo Save
        }

        _data = addParsedElements(dataLines);
    }
    public List<ParsedData> GetParseFileData()
    {
        return _data;
    }
    public string GetFileName()
    {
        return _fileName;
    }
    private List<ParsedData> addParsedElements(string[] lines)
    {
        List<ParsedData> data = new List<ParsedData>();
        foreach (string line in lines)
        {
            string[] elements = line.Split("\t\t", StringSplitOptions.RemoveEmptyEntries);
            if (elements.Length != 3)
            {
                throw new Exception("File is not in correct format");
            }
            ParsedData parsedData = new ParsedData();
            parsedData.Id = int.Parse(elements[0]);
            parsedData.CH1 = float.Parse(elements[1]);
            parsedData.CH2 = float.Parse(elements[2]);
            data.Add(parsedData);
        }
        return data;
    }
}