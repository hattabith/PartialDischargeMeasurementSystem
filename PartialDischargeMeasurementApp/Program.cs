using PartialDischargeMeasurementApp.DataProcessing;
using PartialDischargeMeasurementApp.DataSavers;
using System.Globalization;


// RC filter for 10kV 50Hz is 320 pF and 10 MOhm rezistor


CultureInfo.CurrentCulture = new CultureInfo("en-US");
CultureInfo.CurrentUICulture = new CultureInfo("en-US");

string? fileName = null;  // C:\Users\Dmitriy\source\repos\PartialDischargeMeasurementSystem\PartialDischargeMeasurementApp\Temp\cutData1.txt
string? inputArgs = null;
float coeficient = 1;

do  // need refactoring
{

    if (args.Length > 0)
    {
        inputArgs = args[0];
    }
    if (args.Length == 0)
    {
        Console.WriteLine("No arguments passed");
        do
        {
            Console.WriteLine("Input file name or file name and coeficient: ");
            inputArgs = Console.ReadLine();
        } while (inputArgs == null);

    }

    string[] elements = inputArgs.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    fileName = elements[0];
    if (elements.Length >= 2) coeficient = (float)Convert.ToDouble(elements[1]);

    Console.WriteLine("File name is: {0}", fileName);
    Console.WriteLine("Coeficient is: {0}", coeficient);
    Console.WriteLine();

    List<ParsedData> rawDataFromFile;
    switch (Path.GetExtension(fileName).ToUpper())  // need refactoring
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
            Console.WriteLine("Reading Excel file");
            rawDataFromFile = new CSVFileReader(fileName).GetParseFileData();
            break;
        default: throw new Exception("File extension incorrect!");
    }

    //test for many files

    // TODO : make test for many files

    //ShowRawData(rawDataFromFile);

    var zeros = new WaveZeroFinder(rawDataFromFile);

    //foreach (var data in zeros.GetZeroData())
    //{
    //    Console.WriteLine("Zero point is: " + data.ToString());
    //}

    //Console.WriteLine("Number of zero points is: " + zeros.GetZeroData().Count);

    //Console.WriteLine();
    //Console.WriteLine("Number of middle calk is: " + zeros.GetMiddleValues().Count);
    //ShowMiddleSum(zeros);

    var partialDischarges = new PDIdentifier(rawDataFromFile);

    Console.WriteLine();
    Console.WriteLine("Partial discharge count is: " + partialDischarges.GetPartialDischargeList().Count);
    Console.WriteLine();

    foreach (var pd in partialDischarges.GetPartialDischargeList())
    {
        Console.WriteLine("Partial discharge Id: {0}, CH1: {1}, CH2: {2}", pd.Id, pd.CH1, pd.CH2);
    }



    //Console.WriteLine(Path.GetFileName(fileName));
    //Console.WriteLine(Path.GetFileNameWithoutExtension(fileName));
    //Console.WriteLine(Path.GetFullPath(fileName));
    //Console.WriteLine(Path.GetPathRoot(fileName));
    //Console.WriteLine(Path.ChangeExtension(fileName, ".csv"));
    //Console.WriteLine(fileName);

    //Console.WriteLine();
    //string? fileNameCSV;
    //do
    //{
    //    Console.WriteLine("Input file name for save data: ");
    //    fileNameCSV = Console.ReadLine();
    //} while (fileNameCSV == null);

    var fileSaver = new SaveRezultToCSV(Path.ChangeExtension(fileName, ".csv"), rawDataFromFile, coeficient);

    Console.WriteLine();
    Console.WriteLine("Repeat program? 'n' - no");

} while (Console.ReadKey().Key != ConsoleKey.N);



// TODO : need refactoring ShowMiddleSum
static void ShowMiddleSum(WaveZeroFinder zeros)
{
    foreach (var data in zeros.GetMiddleValues())
    {
        Console.WriteLine("Middle sum is: " + data.ToString());
    }

}


// TODO : need refactoring ShowRawData
static void ShowRawData(List<ParsedData> rawDataFromFile)
{
    foreach (ParsedData data in rawDataFromFile)
    {
        Console.WriteLine("Id: {0}, CH1: {1}, CH2: {2}", data.Id, data.CH1, data.CH2);
    }
}
