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
        /// <summary>
        /// Adds the Basin object to the static list on creation, sets it's Tubes, then validates the Basin.
        /// </summary>
        /// <param name="lowPoint">Each Basin only has 1 Tube at a low point, so it is advised that we begin from that.</param>
        public Basin(Tube lowPoint)
        {
            Basins.Add(this);
            this.NewBasinTile(lowPoint);
            this.ValidateBasin();
        }
        /// <summary>
        /// Initializes all basins from their low points. Can only work if the Tube Map have been successfully created.
        /// </summary>
        /// <exception cref="ArgumentException">If a Basin cannot be created because input has an incorrect format, the validation
        /// throws an ArgumentException. When this exception occurs, it clears the static list, so no incorrect data can be retrieved.</exception>
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
        /// <summary>
        /// Flags the given Tube as already encountered, then adds is to the Basin's Tubes. Loops through all of the adjacents of the Tube.
        /// As a height 9 indicates a wall of the basin, checks if the adjacent's height is anything else, or if it have been encountered before.
        /// If not, calls itself on the adjacent Tube. Completes the Basin's creation.
        /// </summary>
        /// <param name="tube">Should only begin from a Tube at a low point, everything else is completed through recursion.</param>
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
        /// <summary>
        /// Creates an array which holds the 3 largest basins' sizes, then returns it.
        /// </summary>
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
        /// <summary>
        /// Validates the Basin after all Tubes have been added to it.
        /// </summary>
        /// <exception cref="ArgumentException">If the basin has more or less than 1 Tube at a low point, throws ArgumentException.</exception>
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
        /// Returns the product of the 3 largest basins' sizes.
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
