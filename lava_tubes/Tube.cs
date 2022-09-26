using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lava_tubes
{
    internal class Tube
    {
        private static List<List<Tube>> _tubes = new List<List<Tube>>();
        public List<Tube> Adjacents { get; set; } = new List<Tube>();
        public int Height { get; set; }
        public bool IsLowPoint { get; set; } = true;
        public bool IsEncountered { get; set; } = false;
        public Tube(int height)
        {
            Height = height;
        }

        public static void createRow(string nums)
        {
            List<Tube> tubes = new List<Tube>();
            for (int i = 0; i < nums.Length; i++)
            {
                Tube currentTube = new Tube(nums[i] - '0');
                tubes.Add(currentTube);
            }
            _tubes.Add(tubes);
        }

        public static void mapping()
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
            foreach (List<Tube> row in _tubes)
            {
                foreach (Tube tube in row)
                {

                    foreach (Tube adjacent in tube.Adjacents)
                    {
                        if (tube.Height >= adjacent.Height)
                        {
                            tube.IsLowPoint = false;
                            break;
                        }
                    }
                }
            }
        }
        public int Risk()
        {
            return this.IsLowPoint ? this.Height + 1 : 0;
        }

        public static int SumOfRisks()
        {
            int sum = 0;
            foreach (List<Tube> row in _tubes)
            {
                foreach (Tube tube in row)
                {
                    sum += tube.Risk();
                }
            }
            return sum;
        }
    }
}
