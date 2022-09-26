using System;
using lava_tubes;

List<string> lines = new List<string>();
using (var reader = new StreamReader("input.txt"))
{
    while (!reader.EndOfStream)
    {
        lines.Add(reader.ReadLine());
    }
}

for (int i = 0; i < lines.Count; i++)
{
    Tube.createRow(lines[i]);
}

Tube.mapping();
Console.WriteLine(Tube.SumOfRisks());

Tube.BasinScan();

int multiple = 1;
foreach (int i in Basin.ThreeBiggest())
{
    multiple *= i;
}

Console.WriteLine(multiple);