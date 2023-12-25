using PartialDischargeMeasurementApp.DataProcessing;
using PartialDischargeMeasurementApp.DataSavers;



string? fileName = null;  // C:\Users\Dmitriy\source\repos\PartialDischargeMeasurementSystem\PartialDischargeMeasurementApp\Temp\cutData1.txt

do
{

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
        } while (fileName == null);

    }


    Console.WriteLine("File name is: {0}", fileName);
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
            throw new Exception("CSV file not processed yet...");
        default: throw new Exception("File extension incorrect!");
    }

    //test for many files

    //ShowRawData(rawDataFromFile);

    var zeros = new WaveZeroFinder(rawDataFromFile);

    foreach (var data in zeros.GetZeroData())
    {
        Console.WriteLine("Zero point is: " + data.ToString());
    }

    Console.WriteLine("Number of zero points is: " + zeros.GetZeroData().Count);

    Console.WriteLine();
    Console.WriteLine("Number of middle calk is: " + zeros.GetMiddleValues().Count);
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

    var fileSaver = new SaveRezultToCSV(Path.ChangeExtension(fileName, ".csv"), rawDataFromFile);

    Console.WriteLine();
    Console.WriteLine("Repeat program? 'n' - no");
    
} while (Console.ReadKey().Key != ConsoleKey.N);

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
