
using System;
using System.Collections.Generic;
using System.Linq;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day16
{
    /// <summary>
    /// Solution to Day 16 - Part A - Predicate factories and string parsing
    /// </summary>
    [Injectable]
    public class ProblemA : FileProblem
    {
        /// <inheritdoc/>
        public override string Run(Arguments arguments, Writer writer)
        {
            string[] input = this.GetInput(arguments);
            List<IEnumerable<int>> nearbyTickets = new List<IEnumerable<int>>();

            List<Predicate<int>> validators = new List<Predicate<int>>();

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].Contains("your ticket"))
                {
                    // i + 1 is my ticket
                    // i + 3 is first neaby ticket
                    for (int j = i + 3; j < input.Length; j++)
                    {
                        nearbyTickets.Add(input[j].Split(',').Select(int.Parse));
                    }
                    break;
                }

                // Otherwise it's still a rule to parse
                string[] parts = input[i].Split(':')[1].Split("or");

                int[] setOne = parts[0].Split('-').Select(s =>
                {
                    s = s.Trim();
                    return int.Parse(s);
                }).ToArray();
                int[] setTwo = parts[1].Split('-').Select(s =>
                {
                    s = s.Trim();
                    return int.Parse(s);
                }).ToArray();

                validators.Add(this.BuildRule(setOne[0], setOne[1], setTwo[0], setTwo[1]));
            }

            int errorCount = 0;

            foreach (IEnumerable<int> nearbyTicket in nearbyTickets)
            {
                foreach (int field in nearbyTicket)
                {
                    int invalidCount = 0;
                    foreach (Predicate<int> validator in validators)
                    {
                        if (!validator(field))
                        {
                            invalidCount++;
                        }
                    }

                    if (invalidCount == validators.Count)
                    {
                        errorCount += field;
                    }
                }
            }


            return $"Error Count: {errorCount}";
        }

        private Predicate<int> BuildRule(int min, int max, int min2, int max2)
        {
            System.Console.WriteLine($"Building rule >= {min} <= {max} OR >= {min2} < {max2}");
            return (x) => (x >= min && x <= max) || (x >= min2 && x <= max2);
        }
    }
}
