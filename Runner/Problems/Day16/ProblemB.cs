using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day16
{
    /// <summary>
    /// Solution to Day 11 - Part A - Conway's game of life (kind of)
    /// </summary>
    [Injectable]
    public class ProblemB : FileProblem
    {
        /// <inheritdoc/>
        public override string Run(Arguments arguments, Writer writer)
        {
            string[] input = this.GetInput(arguments);
            List<int[]> nearbyTickets = new List<int[]>();
            int[] myTicket = new int[0];

            List<Validator> validators = new List<Validator>();

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].Contains("your ticket"))
                {
                    // i + 1 is my ticket
                    myTicket = input[i + 1].Split(',').Select(int.Parse).ToArray();
                    // i + 3 is first neaby ticket
                    for (int j = i + 3; j < input.Length; j++)
                    {
                        nearbyTickets.Add(input[j].Split(',').Select(int.Parse).ToArray());
                    }

                    break;
                }

                // Otherwise it's still a rule to parse
                string[] split = input[i].Split(':');
                string[] parts = split[1].Split("or");

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

                Validator newValidator = new Validator(split[0],
                    this.BuildRule(split[0], setOne[0], setOne[1], setTwo[0], setTwo[1]));
                validators.Add(newValidator);
            }

            List<int[]> safeTickets = new List<int[]>();
            foreach (int[] nearbyTicket in nearbyTickets)
            {
                bool validTicket = true;
                foreach (int field in nearbyTicket)
                {
                    int invalidCount = 0;
                    foreach (Validator validator in validators)
                    {
                        if (!validator.GetFunction()(field))
                        {
                            invalidCount++;
                        }
                    }

                    if (invalidCount == validators.Count)
                    {
                        validTicket = false;
                        break;
                    }
                }

                if (validTicket)
                {
                    safeTickets.Add(nearbyTicket);
                }
            }

            Stack<Node> openSet = new Stack<Node>();
            foreach (Validator validator in validators)
            {
                openSet.Push(new Node(validator));
            }

            while (openSet.Count > 0)
            {
                Node next = openSet.Pop();

                int index = next.GetIndex();
                Validator validator = next.GetNextValidator();

                // Run next against all tickets
                bool valid = true;
                foreach (int[] nearbyTicket in safeTickets)
                {
                    if (!validator.GetFunction()(nearbyTicket[index]))
                    {
                        // writer.WriteNewLine($"{nearbyTicket[index]} broke rule {validator.GetName()} as index {index}");
                        valid = false;
                        break;
                    }
                }

                if (valid)
                {
                    // writer.WriteNewLine("Found match");
                    // check if we're done
                    if (next.GetIndex() == validators.Count - 1)
                    {
                        // that was the last one, dump this one?
                        return $"found it! {next.CalculateTicket(myTicket)}";
                    }

                    // add the next validator this one doesn't have
                    foreach (Validator validator1 in validators)
                    {
                        if (!next.HasValidator(validator1))
                        {
                            openSet.Push(next.WithNextValidator(validator1));
                        }
                    }
                }
            }


            return $"well found nothing";
        }

        private Predicate<int> BuildRule(string name, int min, int max, int min2, int max2)
        {
            return (x) => (x >= min && x <= max) || (x >= min2 && x <= max2);
        }
    }
}
