using System;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day5
{
    /// <summary>
    /// Solution to Day5 - Part A, binary parsing
    /// </summary>
    [Injectable]
    public class ProblemA : FileProblem
    {
        public override string Run(Arguments arguments, Writer writer)
        {
            string[] lines = this.GetInput(arguments);

            int highest = int.MinValue;
            foreach (string line in lines)
            {
                string asBinary = line.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1');
                int id = Convert.ToInt32(asBinary, 2);

                if (id > highest)
                {
                    highest = id;
                }
            }

            return $"Highest passport ID is {highest}";
        }
    }
}
