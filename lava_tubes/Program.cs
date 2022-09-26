using lava_tubes;

string input = "input.txt";
Tube.InitializeTubes(input);

Console.WriteLine("Sum of all risk levels:");
Console.WriteLine(Tube.SumOfRisks());

Basin.InitializeBasins();
Console.WriteLine("Multiple of the 3 largest basins' sizes:");
Console.WriteLine(Basin.MultipleOfSizes());