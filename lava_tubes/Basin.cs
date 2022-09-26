using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lava_tubes
{
    public class Basin
    {
        public static List<Basin> _basins { get; } = new List<Basin>();
        public List<Tube> Tubes { get; set; } = new List<Tube>();
        public int Size
        {
            get { return Tubes.Count; }
        }
        public Basin(Tube lowPoint)
        {
            _basins.Add(this);
            this.NewBasinTile(lowPoint);
        }
        /// <summary>
        /// Initializes all basins
        /// </summary>
        public static void InitializeBasins()
        {
            for (int i = 0; i < Tube._tubes.Count; i++)
            {
                for (int j = 0; j < Tube._tubes[i].Count; j++)
                {
                    if (Tube._tubes[i][j].Risk > 0)
                    {
                        new Basin(Tube._tubes[i][j]);
                    }
                }
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
            for (int i = 0; i < _basins.Count; i++)
            {
                if (sizes.Min() < _basins[i].Size)
                {
                    for (int j = 0; j < sizes.Length; j++)
                    {
                        if (sizes[j] == sizes.Min())
                        {
                            sizes[j] = _basins[i].Size;
                            break;
                        }
                    }
                }
            }
            return sizes;
        }
        /// <summary>
        /// Returns the multiple of the 3 largest basins' sizes
        /// </summary>
        public static int MultipleOfSizes()
        {
            int multiple = 1;
            int[] largests = ThreeLargest();
            for (int i = 0; i < largests.Length; i++)
            {
                multiple *= largests[i];
            }
            return multiple;
        }
    }
}
