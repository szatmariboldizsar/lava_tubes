using System;
using lava_tubes;

string input = "input.txt";
Tube.Initialize(input);

Console.WriteLine("Sum of all risk levels:");
Console.WriteLine(Tube.SumOfRisks());

Console.WriteLine("Multiple of the 3 largest basins' sizes:");
Console.WriteLine(Basin.MultipleOfSizes());