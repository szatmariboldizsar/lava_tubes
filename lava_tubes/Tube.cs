using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lava_tubes
{
    /// <summary>
    /// Models and contains Lava Tubes created from an input
    /// </summary>
    public class Tube
    {
        public static List<List<Tube>> Tubes { get; set; } = new List<List<Tube>>();
        public static bool IsComplete { get; set; } = false;
        public List<Tube> Adjacents { get; set; } = new List<Tube>();
        public int Height { get; }
        public bool IsEncountered { get; set; } = false;
        public int Risk { get; set; }
        public Tube(int height, List<Tube> row)
        {
            Height = height;
            Risk = Height + 1;
            row.Add(this);
        }
        /// <summary>
        /// Initializes all Tube objects, their adjacents and sets low points based on an input.
        /// </summary>
        /// <param name="input">input file location or Stream, must contain an equal amount of numbers in each line</param>
        public static void InitializeTubes(string input)
        {
            try
            {
                CreateMap(input);
                ScanAdjacents();
                SetLowPoints();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FileNotFoundException)
            {

                Console.WriteLine("The given input does not exist!");
            }

            if (!IsComplete)
            {
                Tubes.Clear();
            }
        }
        public static void InitializeTubes(Stream input)
        {
            try
            {
                CreateMap(input);
                ScanAdjacents();
                SetLowPoints();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FileNotFoundException)
            {

                Console.WriteLine("The given input does not exist!");
            }

            if (!IsComplete)
            {
                Tubes.Clear();
            }
        }
        private static void CreateMap(Stream input)
        {
            using (StreamReader reader = new StreamReader(input))
            {
                string firstline = reader.ReadLine();
                CreateRow(firstline);

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line.Length == firstline.Length)
                    {
                        CreateRow(line);
                    }
                    else
                    {

                        throw new ArgumentException("Incorrect input format! All lines should be the same length!");
                    }
                }
            }
            Console.WriteLine("Succesful reading of input!");
        }

        private static void CreateMap(string input)
        {
            using (StreamReader reader = new StreamReader(input))
                {
                    string firstline = reader.ReadLine();
                    CreateRow(firstline);

                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                    if (line.Length == firstline.Length)
                    {
                        CreateRow(line);
                    }
                    else
                    {
                        throw new ArgumentException("Incorrect input format! All lines should be the same length!");
                    }
                }
            }
            Console.WriteLine("Succesful reading of input!");
        }

        private static void CreateRow(string nums)
        {
            List<Tube> row = new List<Tube>();
            for (int i = 0; i < nums.Length; i++)
            {
                int num = nums[i] - '0';
                if (num >= 0 && num <= 9)
                {
                    Tube currentTube = new Tube(num, row);
                }
                else
                {
                    throw new ArgumentException("Incorrect input format! All characters must be numbers!");
                }
            }
            Tubes.Add(row);
        }
        private static void ScanAdjacents()
        {
            for (int i = 0; i < Tubes.Count; i++)
            {
                for (int j = 0; j < Tubes[i].Count; j++)
                {
                    if (i == 0)
                    {
                        Tubes[i][j].Adjacents.Add(Tubes[i + 1][j]);
                    }
                    else if (i == Tubes.Count - 1)
                    {
                        Tubes[i][j].Adjacents.Add(Tubes[i - 1][j]);
                    }
                    else
                    {
                        Tubes[i][j].Adjacents.Add(Tubes[i - 1][j]);
                        Tubes[i][j].Adjacents.Add(Tubes[i + 1][j]);
                    }

                    if (j == 0)
                    {
                        Tubes[i][j].Adjacents.Add(Tubes[i][j + 1]);
                    }
                    else if (j == Tubes[i].Count - 1)
                    {
                        Tubes[i][j].Adjacents.Add(Tubes[i][j - 1]);
                    }
                    else
                    {
                        Tubes[i][j].Adjacents.Add(Tubes[i][j - 1]);
                        Tubes[i][j].Adjacents.Add(Tubes[i][j + 1]);
                    }
                }
            }
        }
        private static void SetLowPoints()
        {
            for (int i = 0; i < Tubes.Count; i++)
            {
                for (int j = 0; j < Tubes[i].Count; j++)
                {
                    for (int k = 0; k < Tubes[i][j].Adjacents.Count; k++)
                    {
                        if (Tubes[i][j].Height >= Tubes[i][j].Adjacents[k].Height)
                        {
                            Tubes[i][j].Risk = 0;
                            break;
                        }
                    }
                }
            }
            IsComplete = true;
        }
        /// <summary>
        /// Returns the sum of all low points' risk levels.
        /// </summary>
        public static int SumOfRisks()
        {
            int sum = 0;
            for (int i = 0; i < Tubes.Count; i++)
            {
                for (int j = 0; j < Tubes[i].Count; j++)
                {
                    sum += Tubes[i][j].Risk;
                }
            }
            return sum;
        }
    }
}
