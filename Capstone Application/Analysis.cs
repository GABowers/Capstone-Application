using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Application
{
    class Analysis
    {
        struct HSV { public float h; public float s; public float v; };

        public static Tuple<List<Tuple<int, int>>, List<Tuple<int, int>>, List<Tuple<int, int>>> FinalLocationHistogram(List<List<Tuple<int, int, int>>> paths, Tuple<int, int> grid_dims, int iteration)
        {

            List<Tuple<int, int, int>> ends = new List<Tuple<int, int, int>>();
            for (int i = 0; i < paths.Count; i++)
            {
                if(paths[i].Count >= iteration)
                {
                    ends.Add(paths[i][iteration]);
                }
            }
            List<Tuple<int, int>> ends_stateless = ends.Select(x => new Tuple<int, int>(x.Item1, x.Item2)).ToList();
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
            Tuple<List<Tuple<int, int>>, List<Tuple<int, int>>, List<Tuple<int, int>>> counts_n_hist = new Tuple<List<Tuple<int, int>>, List<Tuple<int, int>>, List<Tuple<int, int>>>(ends_stateless, x_bins, y_bins);
            return counts_n_hist;
        }

        public static Color Blend(Color cur_color, Color next)
        {
            // convert to HSL, combine H
            HSV cur_hsv = new HSV() { h = cur_color.GetHue(), s = cur_color.GetSaturation(), v = Brightness(cur_color) };
            HSV new_hsv = new HSV() { h = next.GetHue(), s = next.GetSaturation(), v = Brightness(next) };
            HSV combined = new HSV() { h = (cur_hsv.h + new_hsv.h) / 2.0f, s = (cur_hsv.s + new_hsv.s) / 2.0f, v = (cur_hsv.v + new_hsv.v) / 2.0f };
            Color new_color = ColorFromHSL(combined);
            return new_color;
        }

        static Color ColorFromHSL(HSV hsl)
        {
            if (hsl.s == 0)
            {
                int L = (int)hsl.v;
                return Color.FromArgb(255, L, L, L);
            }

            double min, max, h;
            h = hsl.h / 360.0;
            max = hsl.v < 0.5 ? hsl.v * (1 + hsl.s) : (hsl.v + hsl.s) - (hsl.v * hsl.s);
            min = (hsl.v * 2.0) - max;

            Color c = Color.FromArgb(255, (int)(255 * RGBChannelFromHue(min, max, h + 1 / 3.1)), (int)(255 * RGBChannelFromHue(min, max, h)), (int)(255 * RGBChannelFromHue(min, max, h - 1 / 3.1)));
            return c;
        }

        static float Brightness(Color c)
        {
            return (c.R * 0.299f + c.G * 0.587f + c.B * 0.114f) / 256f;
        }

        static double RGBChannelFromHue(double m1, double m2, double h)
        {
            h = (h + 1.0) % 1.0;
            if (h < 0)
            {
                h += 1;
            }
            if (h * 6 < 1)
            {
                return m1 + (m2 - m1) * 6 * h;
            }
            else if (h * 2 < 1)
            {
                return m2;
            }
            else if (h * 3 < 2)
            {
                return m1 + (m2 - m1) * 6 * (2.0 / 3.0 - h);
            }
            else
            {
                return m1;
            }
        }

    }
}
