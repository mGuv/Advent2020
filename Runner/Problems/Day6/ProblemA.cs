using System.Collections.Generic;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day6
{
    /// <summary>
    /// Solution to Day6 - Part A, just kinda more string parsing
    /// </summary>
    [Injectable]
    public class ProblemA : FileProblem
    {
        public override string Run(Arguments arguments, Writer writer)
        {
            string input = this.GetRawInput(arguments);
            string[] lines = input.Split('\n');

            int total = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                string group = "";
                while (lines[i].Length > 0 && i < lines.Length)
                {
                    group += lines[i];
                    i++;
                }

                // group found, count unique
                HashSet<char> uniqueAnswer = new HashSet<char>();
                for (int j = 0; j < group.Length; j++)
                {
                    if (uniqueAnswer.Add(group[j]))
                    {
                        total++;
                    }
                }
            }

            return $"Unique Answers: {total}";
        }
    }
}
