using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.Util;
using System.Globalization;
using NPOI.HSSF.UserModel;

//add new file record
public class ExcelFileReader : IFileReader
{
    private readonly string _fileName;
    private readonly List<ParsedData> _data;


    public ExcelFileReader(string fileName)
    {
        _fileName = fileName;

        IWorkbook workBook;

        CultureInfo.CurrentCulture = new CultureInfo("en-US");
        CultureInfo.CurrentUICulture = new CultureInfo("en-US");

        using (FileStream stream = new FileStream(_fileName, FileMode.Open, FileAccess.Read))
        {
            workBook = new HSSFWorkbook(stream);
        }

        ISheet sheet = workBook.GetSheetAt(0);



        // Чтение данных из ячейки
        /* IRow row = sheet.GetRow(0);
        string cellValue = row.GetCell(1).ToString();
 
        Console.WriteLine(cellValue); */

            var data = new List<ParsedData>();

            for (int row = 5; row <= sheet.LastRowNum; row++)
            {
                var parsedData = new ParsedData();

                if (sheet.GetRow(row) != null)
                {
                parsedData.Id = int.Parse(sheet.GetRow(row).GetCell(0).ToString());
                parsedData.CH1 = float.Parse(sheet.GetRow(row).GetCell(1).ToString());
                parsedData.CH2 = float.Parse(sheet.GetRow(row).GetCell(2).ToString());
                data.Add(parsedData);

                //for (int col = 0; col <= sheet.GetRow(row).LastCellNum; col++)
                //{
                //    if (sheet.GetRow(row).GetCell(col) != null) Console.Write($"{sheet.GetRow(row).GetCell(col).ToString()}\t");
                //Console.Write(' ');
                //}
                //Console.WriteLine();
            }
            }
            
            _data =data;
    }
    public string GetFileName()
    {
        return _fileName;
    }
    public List<ParsedData> GetParseFileData()
    {
        return _data;
    }
}