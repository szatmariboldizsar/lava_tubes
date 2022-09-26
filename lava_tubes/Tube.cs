using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lava_tubes
{
    /// <summary>
    /// Models and contains Lava Tubes created from an input, which holds an equal amount of numbers in each line
    /// </summary>
    public class Tube
    {
        public static List<List<Tube>> _tubes { get; } = new List<List<Tube>>();
        public List<Tube> Adjacents { get; set; } = new List<Tube>();
        public int Height { get; }
        public bool IsEncountered { get; set; } = false;
        public int Risk { get; set; }
        public Tube(int height)
        {
            Height = height;
            Risk = Height + 1;
        }
        /// <summary>
        /// Initializes all Tube objects, their adjacents and sets low points based on an input.
        /// </summary>
        /// <param name="input">input file location, must contain an equal amount of numbers in each line</param>
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
                        throw new ArgumentException("Incorrect input format!");
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
                        throw new ArgumentException("Incorrect input format!");
                    }
                }
            }
            Console.WriteLine("Succesful reading of input!");
        }

        private static void CreateRow(string nums)
        {
            List<Tube> tubes = new List<Tube>();
            for (int i = 0; i < nums.Length; i++)
            {
                int num = nums[i] - '0';
                if (num >= 0 && num <= 9)
                {
                    Tube currentTube = new Tube(num);
                    tubes.Add(currentTube);
                }
                else
                {
                    throw new ArgumentException("Incorrect input format!");
                }
            }
            _tubes.Add(tubes);
        }
        private static void ScanAdjacents()
        {
            for (int i = 0; i < _tubes.Count; i++)
            {
                for (int j = 0; j < _tubes[i].Count; j++)
                {
                    if (i == 0)
                    {
                        _tubes[i][j].Adjacents.Add(_tubes[i + 1][j]);
                    }
                    else if (i == _tubes.Count - 1)
                    {
                        _tubes[i][j].Adjacents.Add(_tubes[i - 1][j]);
                    }
                    else
                    {
                        _tubes[i][j].Adjacents.Add(_tubes[i - 1][j]);
                        _tubes[i][j].Adjacents.Add(_tubes[i + 1][j]);
                    }

                    if (j == 0)
                    {
                        _tubes[i][j].Adjacents.Add(_tubes[i][j + 1]);
                    }
                    else if (j == _tubes[i].Count - 1)
                    {
                        _tubes[i][j].Adjacents.Add(_tubes[i][j - 1]);
                    }
                    else
                    {
                        _tubes[i][j].Adjacents.Add(_tubes[i][j - 1]);
                        _tubes[i][j].Adjacents.Add(_tubes[i][j + 1]);
                    }
                }
            }
        }
        private static void SetLowPoints()
        {
            for (int i = 0; i < _tubes.Count; i++)
            {
                for (int j = 0; j < _tubes[i].Count; j++)
                {
                    for (int k = 0; k < _tubes[i][j].Adjacents.Count; k++)
                    {
                        if (_tubes[i][j].Height >= _tubes[i][j].Adjacents[k].Height)
                        {
                            _tubes[i][j].Risk = 0;
                            break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Returns the sum of all low points' risk levels.
        /// </summary>
        public static int SumOfRisks()
        {
            int sum = 0;
            for (int i = 0; i < _tubes.Count; i++)
            {
                for (int j = 0; j < _tubes[i].Count; j++)
                {
                    sum += _tubes[i][j].Risk;
                }
            }
            return sum;
        }
    }
}
