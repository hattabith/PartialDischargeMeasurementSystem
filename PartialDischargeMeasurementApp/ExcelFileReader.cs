using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.Util;

public class ExcelFileReader : IFileReader
{
    private readonly string _fileName;
    private readonly List<ParsedData> _data;


    public ExcelFileReader(string fileName)
    {
        _fileName = fileName;

        IWorkbook workBook;

        using (FileStream stream = new FileStream(_fileName, FileMode.Open, FileAccess.Read))
        {
            workBook = new XSSFWorkbook(stream);
        }

        ISheet sheet = workBook.GetSheetAt(0);

        // Чтение данных из ячейки
        /* IRow row = sheet.GetRow(0);
        string cellValue = row.GetCell(1).ToString();
 
        Console.WriteLine(cellValue); */

            for (int row = 0; row <= sheet.LastRowNum; row++)
            {
                if (sheet.GetRow(row) != null)
                {
                    for (int col = 0; col <= sheet.GetRow(row).LastCellNum; col++)
                    {
                        if (sheet.GetRow(row).GetCell(col) != null) Console.Write($"{sheet.GetRow(row).GetCell(col).ToString()}\t");
                    Console.Write(' ');
                    }
                    Console.WriteLine();
                }
            }
            

    }
    public string GetFileName()
    {
        return _fileName;
    }
    public List<ParsedData> GetParseFileData()
    {
        var fileLines = new List<ParsedData>();
    
        return fileLines;
    }
}