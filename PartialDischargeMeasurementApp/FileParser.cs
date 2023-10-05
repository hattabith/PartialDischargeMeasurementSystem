using System.Globalization;
public class FileParser : IFileReader
{
    private readonly List<ParsedData> _data;
    private string _fileName;
    public FileParser(string file)
    {
        _fileName = file;
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
        _data = addParsedElements(lines);
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
            string[] elements = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
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
