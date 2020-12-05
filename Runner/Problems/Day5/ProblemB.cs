using System;
using System.Collections.Generic;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day5
{
    /// <summary>
    /// Solution to Day5 - Part B, binary and bounds
    /// </summary>
    [Injectable]
    public class ProblemB : FileProblem
    {
        public override string Run(Arguments arguments, Writer writer)
        {
            string[] lines = this.GetInput(arguments);

            HashSet<int> ids = new HashSet<int>();
            int min = Int32.MaxValue;
            int max = Int32.MinValue;
            foreach (string line in lines)
            {
                string asBinary = line.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1');
                int id = Convert.ToInt32(asBinary, 2);
                ids.Add(id);

                if (id < min)
                {
                    min = id;
                }

                if (id > max)
                {
                    max = id;
                }
            }

            for (int i = min; i <= max; i++)
            {
                if (!ids.Contains(i))
                {
                    return $"Seat found in loop {i}";
                }
            }

            return "Couldn't find seat?";
        }
    }
}
