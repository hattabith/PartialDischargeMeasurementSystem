string? fileName = null;
if (args.Length == 0)
{
    Console.WriteLine("No arguments passed");
    fileName = "default.txt";
}
if (args.Length > 0)
{
    fileName = args[0];
}

Console.WriteLine("File name is: {0}", fileName);
