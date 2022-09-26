using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lava_tubes
{
    internal class Basin
    {
        private static List<Basin> _basins = new List<Basin>();
        public List<Tube> Tubes = new List<Tube>();
        public int Size = 0;

        public Basin()
        {
            _basins.Add(this);
        }

        public void NewBasinTile(Tube tube)
        {
            tube.IsEncountered = true;
            this.Size++;
            this.Tubes.Add(tube);
            foreach (Tube adjacent in tube.Adjacents)
            {
                if (adjacent.Height != 9 && !adjacent.IsEncountered)
                {
                    this.NewBasinTile(adjacent);
                }
            }
        }

        public static int[] ThreeBiggest()
        {
            int[] sizes = { 0, 0, 0 };
            foreach (Basin basin in _basins)
            {
                if (sizes.Min() < basin.Size)
                {
                    for (int i = 0; i < sizes.Length; i++)
                    {
                        if (sizes[i] == sizes.Min())
                        {
                            sizes[i] = basin.Size;
                            break;
                        }
                    }
                }
            }
            return sizes;
        }
    }
}
