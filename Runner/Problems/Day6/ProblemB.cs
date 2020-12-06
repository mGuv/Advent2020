using System.Collections.Generic;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day6
{
    /// <summary>
    /// Solution to Day6 - Part B, the same pas part A but the success criteria is different
    /// </summary>
    [Injectable]
    public class ProblemB : FileProblem
    {
        public override string Run(Arguments arguments, Writer writer)
        {
            string input = this.GetRawInput(arguments);
            string[] lines = input.Split('\n');

            int total = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                string group = "";
                int people = 0;
                while (lines[i].Length > 0 && i < lines.Length)
                {
                    people++;
                    group += lines[i];
                    i++;
                }

                // group found, count all same
                Dictionary<char, int> groupAnswers = new Dictionary<char, int>();
                for (int j = 0; j < group.Length; j++)
                {
                    if (groupAnswers.ContainsKey(group[j]))
                    {
                        groupAnswers[group[j]] = groupAnswers[group[j]] + 1;
                    }
                    else
                    {
                        groupAnswers[group[j]] = 1;
                    }
                }

                foreach (KeyValuePair<char,int> groupAnswer in groupAnswers)
                {
                    if (groupAnswer.Value == people)
                    {
                        total++;
                    }
                }
            }

            return $"Unique Answers: {total}";
        }
    }
}
