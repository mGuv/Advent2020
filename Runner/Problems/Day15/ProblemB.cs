using System.Collections.Generic;
using System.Linq;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day15
{
    /// <summary>
    /// Solution to Day 15 - Part B - Tracking an indexed list (same solution as Part A)
    /// </summary>
    [Injectable]
    public class ProblemB : FileProblem
    {
        public override string Run(Arguments arguments, Writer writer)
        {
            writer.WriteNewLine(this.GetInput(arguments)[0]);
            int[] input = this.GetInput(arguments)[0].Split(",").Select(int.Parse).ToArray();

            Dictionary<int, int> lastSeenIndex = new Dictionary<int, int>();
            List<int> turns = new List<int>();

            for (int i = 0; i < input.Length; i++)
            {
                turns.Add(input[i]);
                lastSeenIndex[input[i]] = i;
            }

            for (int i = input.Length; i < 30000000; i++)
            {

                // look at the last digit
                if (lastSeenIndex.ContainsKey(turns[i - 1]))
                {
                    // seen it before, track the difference and add it
                    turns.Add((i - 1) - lastSeenIndex[turns[i - 1]]);
                    lastSeenIndex[turns[i - 1]] = i - 1;
                    continue;
                }

                // Not seen it before so add it at this index
                lastSeenIndex[turns[i - 1]] = i - 1;
                turns.Add(0);
            }

            return $"Last digit: {turns[^1]}";
        }
    }
}
