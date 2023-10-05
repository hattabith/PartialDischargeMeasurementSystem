public class TXTFileReader : IFileReader
{
    private readonly string _fileName;
    private readonly List<ParsedData> _data;

    public TXTFileReader (string fileName)
    {
        _fileName = fileName;
    }
    public string GetFileName()
    {
        return _fileName;
    }

    public List<ParsedData> GetParseFileData()
    {
        var fileStrings = new List<ParsedData>();
        return fileStrings;
    }

}