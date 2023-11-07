
using NPOI.POIFS.Crypt.Dsig;
using PartialDischargeMeasurementApp;
using System.Reflection.Emit;


// C:\Users\Dmitriy\source\repos\PartialDischargeMeasurementSystem\PartialDischargeMeasurementApp\Temp\cutData1.txt



// Low level signal is: -0.16
// Less than -0.16 is PD

/*static string GetFileExtension(string fileName)
{
    // Отримання розширення файлу з імені
    return System.IO.Path.GetExtension(fileName);
}
*/

string? fileName = null;

if (args.Length > 0)
{
    fileName = args[0];
}
if (args.Length == 0)
{
    Console.WriteLine("No arguments passed");
    do
    {
        Console.WriteLine("Input file name: ");
        fileName = Console.ReadLine();
    } while (fileName != null);

}


Console.WriteLine("File name is: {0}", fileName);
Console.WriteLine();

List<ParsedData> rawDataFromFile;
switch (Path.GetExtension(fileName).ToUpper())
{
    case ".DAT":
        throw new Exception("DAT file not processed yet...");
    case ".TXT":
        Console.WriteLine("Reading TXT file");
        rawDataFromFile = new TXTFileReader(fileName).GetParseFileData();
        break;
    case ".XLS":
        Console.WriteLine("Reading Excel file");
        rawDataFromFile = new ExcelFileReader(fileName).GetParseFileData();
        break;
    case ".CSV":
        throw new Exception("CSV file not processed yet...");
    default: throw new Exception("File extention incorrect!");
}

//ShowRawData(rawDataFromFile);

var zeros = new WaveZeroFinder(rawDataFromFile);

foreach (var data in zeros.GetZeroData())
{
    Console.WriteLine("Zero point is: " + data.ToString());
}

Console.WriteLine("Number of zero points is: " + zeros.GetZeroData().Count);

Console.WriteLine();
Console.WriteLine("Number of middle calc is: " + zeros.GetMiddleValues().Count);
//ShowMiddleSum(zeros);

var partialDischarges = new PDIdentifier(rawDataFromFile);

Console.WriteLine();
Console.WriteLine("Partial discharge count is: " + partialDischarges.GetPartialDischargeList().Count);
Console.WriteLine();

foreach (var pd in partialDischarges.GetPartialDischargeList())
{
    Console.WriteLine("Partial discharge Id: {0}, CH1: {1}, CH2: {2}", pd.Id, pd.CH1, pd.CH2);
}

static void ShowMiddleSum(WaveZeroFinder zeros)
{
    foreach (var data in zeros.GetMiddleValues())
    {
        Console.WriteLine("Middle sum is: " + data.ToString());
    }
    
}


static void ShowRawData(List<ParsedData> rawDataFromFile)
{
    foreach (ParsedData data in rawDataFromFile)
    {
        Console.WriteLine("Id: {0}, CH1: {1}, CH2: {2}", data.Id, data.CH1, data.CH2);
    }
}

/*static bool TempPD(ParsedData data)
{
    if (data.CH2 < -0.5)
    {
        return true;
    }
    return false;
}

var PDList = new List<ParsedData>();    

foreach (ParsedData data in fileParser.GetParseFileData())
{
    if (TempPD(data))
    {
        PDList.Add(data);
    }
}

Console.WriteLine();
Console.Write("PDList count: ");
Console.WriteLine(PDList.Count);
Console.WriteLine();
static float PowerPD(ParsedData data)
{
    return 0.5f * data.CH1 * data.CH2;
}

foreach(ParsedData data in PDList)
{
    Console.WriteLine("Id: {0}, CH1: {1}, CH2: {2}, Power: {3}", data.Id, data.CH1, data.CH2, PowerPD(data));
}   

static float AllPower(List<ParsedData> data)
{
    float sum = 0;
    foreach(ParsedData element in data)
    {
        sum += PowerPD(element);
    }
    return sum;
}

Console.WriteLine();
Console.Write("All power: ");
Console.WriteLine(AllPower(PDList));
Console.WriteLine();


var WaveTest = new WaveHalfPeriodAnalyzer(fileParser.GetParseFileData());
*/