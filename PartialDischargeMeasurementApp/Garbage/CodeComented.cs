﻿

// Low level signal is: -0.16
// Less than -0.16 is PD

/*static string GetFileExtension(string fileName)
{
    // Отримання розширення файлу з імені
    return System.IO.Path.GetExtension(fileName);
}
*/


// Half period is: 0.01 second
// Need make coefficient for PD
// Calc Current
// Calc Energy
// Calc Power
// Calc frequency positive PD and negative PD
// Find voltage first PD











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