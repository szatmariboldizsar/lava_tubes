using System;
using lava_tubes;

string input = "input.txt";
Tube.initialize(input);
Console.WriteLine(Tube.SumOfRisks());

Tube.BasinScan();

int multiple = 1;
foreach (int i in Basin.ThreeBiggest())
{
    multiple *= i;
}

Console.WriteLine(multiple);