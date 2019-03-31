using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Application
{
    class Analysis
    {
        public static Tuple<List<Tuple<int, int>>, List<Tuple<int, int>>> FinalLocationHistogram(List<List<Tuple<int, int, int>>> paths, Tuple<int, int> grid_dims, int iteration)
        {

            List<Tuple<int, int, int>> ends = new List<Tuple<int, int, int>>();
            for (int i = 0; i < paths.Count; i++)
            {
                if(paths[i].Count >= iteration)
                {
                    ends.Add(paths[i][iteration]);
                }
            }
            List<int> x_output = Enumerable.Repeat(0, grid_dims.Item1).ToList();
            List<int> y_output = Enumerable.Repeat(0, grid_dims.Item2).ToList();
            for (int i = 0; i < ends.Count; i++)
            {
                x_output[ends[i].Item1]++;
                y_output[ends[i].Item2]++;
            }
            int x_minus = grid_dims.Item1 / 2;
            List<Tuple<int, int>> x_bins = new List<Tuple<int, int>>();
            int y_minus = grid_dims.Item2 / 2;
            List<Tuple<int, int>> y_bins = new List<Tuple<int, int>>();
            for (int i = 0; i < x_output.Count; i++)
            {
                x_bins.Add(new Tuple<int, int>(i - x_minus, x_output[i]));
            }
            for (int i = 0; i < y_output.Count; i++)
            {
                y_bins.Add(new Tuple<int, int>(i - y_minus, y_output[i]));
            }
            return new Tuple<List<Tuple<int, int>>, List<Tuple<int, int>>>(x_bins, y_bins);
        }
    }
}
