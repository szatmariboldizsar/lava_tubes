using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lava_tubes
{
    /// <summary>
    /// Models and contains Basins, can only be created after all Tubes have been created
    /// </summary>
    public class Basin
    {
        public static List<Basin> Basins { get; set; } = new List<Basin>();
        public List<Tube> Tubes { get; set; } = new List<Tube>();
        public int Size
        {
            get { return Tubes.Count; }
        }
        public Basin(Tube lowPoint)
        {
            Basins.Add(this);
            this.NewBasinTile(lowPoint);
            this.ValidateBasin();
        }
        /// <summary>
        /// Initializes all basins
        /// </summary>
        public static void InitializeBasins()
        {
            if (Tube.IsComplete)
            {
                try
                {
                    for (int i = 0; i < Tube.Tubes.Count; i++)
                    {
                        for (int j = 0; j < Tube.Tubes[i].Count; j++)
                        {
                            if (Tube.Tubes[i][j].Risk > 0)
                            {
                                 new Basin(Tube.Tubes[i][j]);
                            }
                        }
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    Basins.Clear();
                }
            }
            else
            {
                Console.WriteLine("The Tube Map hasn't been created yet.");
            }
        }
        private void NewBasinTile(Tube tube)
        {
            tube.IsEncountered = true;
            this.Tubes.Add(tube);
            for (int i = 0; i < tube.Adjacents.Count; i++)
            {
                if (tube.Adjacents[i].Height != 9 && !tube.Adjacents[i].IsEncountered)
                {
                    this.NewBasinTile(tube.Adjacents[i]);
                }
            }
        }
        private static int[] ThreeLargest()
        {
            int[] sizes = { 0, 0, 0 };
            for (int i = 0; i < Basins.Count; i++)
            {
                if (sizes.Min() < Basins[i].Size)
                {
                    for (int j = 0; j < sizes.Length; j++)
                    {
                        if (sizes[j] == sizes.Min())
                        {
                            sizes[j] = Basins[i].Size;
                            break;
                        }
                    }
                }
            }
            return sizes;
        }

        private void ValidateBasin()
        {
            int count = 0;
            for (int i = 0; i < this.Size; i++)
            {
                if (this.Tubes[i].Risk > 0)
                {
                    count++;
                }
            }

            if (count != 1)
            {
                throw new ArgumentException("Incorrect input format! All Basins should only 1 low point!");
            }
        }
        /// <summary>
        /// Returns the product of the 3 largest basins' sizes
        /// </summary>
        public static int ProductOfSizes()
        {
            int product = 1;
            int[] largests = ThreeLargest();
            for (int i = 0; i < largests.Length; i++)
            {
                product *= largests[i];
            }
            return product;
        }
    }
}
