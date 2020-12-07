using System.Collections.Generic;
using System.Text.RegularExpressions;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day7
{
    /// <summary>
    /// Solution to Day7 - Part B - Slightly different regex and logic
    /// </summary>
    [Injectable]
    public class ProblemB : FileProblem
    {
        public override string Run(Arguments arguments, Writer writer)
        {
            string[] lines = this.GetInput(arguments);

            // look up of Bag -> Contents
            Dictionary<string, Dictionary<string, int>> bags = new Dictionary<string, Dictionary<string, int>>();
            foreach (string line in lines)
            {
                // luckily we can easily split both sides to make parsing slightly earlier
                string[] parts = line.Split("contain");

                // Too lazy to write one regex so here's one for the left without value and one for the right
                Regex r = new Regex(@"([a-z]+ [a-z]+)");
                Regex rd = new Regex(@"(\d [a-z]+ [a-z]+)");

                // Left hand side of "contain" only has one bag, the parent container
                string parent = r.Match(parts[0]).Value;
                bags.Add(parent, new Dictionary<string, int>());

                // Right hand side contains a quantity and a bag
                foreach (Match match in rd.Matches(parts[1]))
                {
                    // Keep track of the bag and how many
                    int amount = int.Parse(match.Value[0].ToString());
                    bags[parent].Add(match.Value.Substring(2), amount);
                }
            }

            // Count of bags opened so far
            int totalBags = 0;
            // Set of bags that still need opening to see what's inside
            Queue<string> openSet = new Queue<string>();
            // Seed with gold bag
            openSet.Enqueue("shiny gold");

            // Keep searching till we run out of bags
            while (openSet.Count > 0)
            {
                // Count the next bag
                totalBags++;
                string bag = openSet.Dequeue();

                // look up its contents and add each bag separately
                // This is slow and lazy but pretty elegant still
                foreach (KeyValuePair<string, int> bagAmount in bags[bag])
                {
                    for (int i = 0; i < bagAmount.Value; i++)
                    {
                        openSet.Enqueue(bagAmount.Key);
                    }
                }
            }

            // As I was lazy and put gold bag in open set, need to reduce this by 1 to avoid counting itself
            return $"{totalBags - 1} bags are inside the shiny gold bag";
        }
    }
}
