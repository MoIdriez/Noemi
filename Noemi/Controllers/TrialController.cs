using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using WebGrease.Css.ImageAssemblyAnalysis;

namespace Noemi.Controllers
{
    public class TrialController : BaseController
    {

        public ActionResult Index()
        {
            var model = GetModel(1);
            return View("Next", model);
        }


        [HttpPost]
        public ActionResult Next(TrialViewModel model)
        {
            var trial = new Trial
            {
                TimeColour = model.TimeColour,
                TimeNext = model.TimeNext,
                Image = model.Image,
                Colour = model.Colour,
                Slider = model.Slider
            };

            var trials = Participant.Trials;
            trials.Add(trial);
            Participant.Trials = trials;
            
            if (HasFinished())
                return Redirect(Config.ExternalLink);

            return View(GetModel(model.Index++));
        }

        private bool HasFinished()
        {
            return Participant.Trials.Count == Participant.Images.Count;
        }

        public TrialViewModel GetModel(int index)
        {
            return new TrialViewModel
            {
                Index = index,
                ColourMode = Convert.ToInt32(Config.ColourMode),
                Colours = GetColours(),
                TimeNext = 0,
                TimeColour = 0,
                Image = GetImage(index),
                Slider = 50
            };
        }

        public string GetImage(int index)
        {
            return Participant.Images[index];
        }

        public string[,] GetColours()
        {
            var colourMode = Convert.ToInt32(Config.ColourMode);
            var colourStr = colourMode == 36 ? Config.Colours36 : colourMode == 9 ? Config.Colours9 : Config.Colours4;
            var colourArr = colourStr.Split(',');

            if (Config.ColourOrderIsRandom)
                return Make2DArray(Randomize(colourArr).ToArray(), colourMode);

            var colours = Make2DArray(colourArr, colourMode);

            switch (Config.ColourOrientation)
            {
                case Config.Orientation.FlipHorizontal:
                    return FlipArrayHorizontal(colours);
                case Config.Orientation.FlipVertical:
                    return FlipArrayVertical(colours);
                case Config.Orientation.FlipDouble:
                    return FlipArrayDouble(colours);
                default:
                    return colours;
            }
        }

        public static string ArrayToString<T>(T[,] arr)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var x in arr)
            {
                builder.Append(x);
                if (!x.Equals(arr[arr.GetLength(0) - 1, arr.GetLength(1) - 1]))
                    builder.Append(",");
            }
            return builder.ToString();
        }

        public static IEnumerable<T> Randomize<T>(IEnumerable<T> source)
        {
            Random rnd = new Random();
            return source.OrderBy(x => rnd.Next()).ToArray();
        }

        private static T[,] FlipArrayDouble<T>(T[,] arr)
        {
            return FlipArrayVertical(FlipArrayHorizontal(arr));
        }

        private static T[,] FlipArrayVertical<T>(T[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < (double) arr.GetLength(1) / 2; j++)
                {
                    var temp = arr[i, j];
                    arr[i, j] = arr[i, arr.GetLength(1) - 1 - j];
                    arr[i, arr.GetLength(1) - 1 - j] = temp;
                }
            }
            return arr;
        }

        private static T[,] FlipArrayHorizontal<T>(T[,] arr)
        {
            for (int i = 0; i < (double) arr.GetLength(0) / 2; i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    var temp = arr[i, j];
                    arr[i, j] = arr[arr.GetLength(1) - 1 - i, j];
                    arr[arr.GetLength(1) - 1 - i, j] = temp;
                }
            }
            return arr;
        }


        private static T[,] Make2DArray<T>(T[] input, int colourMode)
        {
            var side = colourMode == 36 ? 6 : colourMode == 9 ? 3 : 2;
            
            T[,] output = new T[side, side];
            for (int i = 0; i < side; i++)
            {
                for (int j = 0; j < side; j++)
                {
                    output[i, j] = input[i * side + j];
                }
            }
            return output;
        }

        public class TrialViewModel
        {
            public int Index { get; set; }
            public double TimeColour { get; set; }
            public double TimeNext { get; set; }
            public int ColourMode { get; set; }
            public string[,] Colours { get; set; }
            public string Image { get; set; }
            public string Colour { get; set; }
            public int Slider { get; set; }

        }
    }
}