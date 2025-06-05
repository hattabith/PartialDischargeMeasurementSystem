using System.Globalization;
public class CSVFileReader : IFileReader
{
    private readonly string _fileName;
    private readonly List<ParsedData> _data;

    public CSVFileReader(string fileName)
    {
        _fileName = fileName;

        if (fileName == null)
        {
            throw new ArgumentNullException(nameof(fileName));
        }
        if (!File.Exists(fileName))
        {
            throw new FileNotFoundException(nameof(fileName));
        }

        CultureInfo.CurrentCulture = new CultureInfo("en-US");
        CultureInfo.CurrentUICulture = new CultureInfo("en-US");

        string[] lines = File.ReadAllLines(_fileName);

        if (lines == null)
        {
            throw new Exception("File is null!");
        }
        if (lines.Length == 0)
        {
            throw new Exception("File is empty!");
        }

        string[] dataLines = new string[lines.Length - 1];

        for (int i = 1; i < lines.Length; i++)
        {
            dataLines[i - 1] = lines[i];
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

    private List<ParsedData> addParsedElements(string[] Lines)
    {
        List<ParsedData> data = new List<ParsedData>();
        int i = 0;
        foreach (string Line in Lines)
        {
            string[] elements = Line.Split(',', StringSplitOptions.RemoveEmptyEntries);
            if (elements.Length != 3)
            {
                throw new Exception("File have incorrect format! " + _fileName);
            }
            ParsedData parsedData = new ParsedData();
            parsedData.Id = i;
            i = i + 1;
            parsedData.CH1 = float.Parse(elements[1]);
            parsedData.CH2 = float.Parse(elements[2]);
            data.Add(parsedData);
        }
        return data;  
    }
}

