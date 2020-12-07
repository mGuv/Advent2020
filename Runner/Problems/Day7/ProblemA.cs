using System.Collections.Generic;
using System.Text.RegularExpressions;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day7
{
    /// <summary>
    /// Solution to Day7 - Part A - Regex and set searching
    /// </summary>
    [Injectable]
    public class ProblemA : FileProblem
    {
        public override string Run(Arguments arguments, Writer writer)
        {
            string[] lines = this.GetInput(arguments);

            // look up of child -> parents
            Dictionary<string, HashSet<string>> bags = new Dictionary<string, HashSet<string>>();
            foreach (string line in lines)
            {
                // luckily we can easily split both sides to make parsing slightly earlier
                string[] parts = line.Split("contain");

                // the digit doesn't actually matter for this part so just find any match of two words
                Regex r = new Regex(@"([a-z]+ [a-z]+)");

                // Left hand side of "contain" only has one bag, the parent container
                string parent = r.Match(parts[0]).Value;

                // Right hand side contains n number of bags as children
                foreach (Match match in r.Matches(parts[1]))
                {
                    // check if child has been seen before
                    if (!bags.ContainsKey(match.Value))
                    {
                        // child hasn't been seen in a container before, create it's parent set
                        bags.Add(match.Value, new HashSet<string>());
                    }

                    // Add the current parent as a container for this child
                    bags[match.Value].Add(parent);
                }
            }

            // Should now know all the bags parents, so check parent chain from gold up the stack until there is no parent
            HashSet<string> seenBags = new HashSet<string>();
            Queue<string> openSet = new Queue<string>();

            // Seed with gold bag
            openSet.Enqueue("shiny gold");

            // Keep searching till we run out of bags
            while (openSet.Count > 0)
            {
                // Get next parent
                string next = openSet.Dequeue();

                if (!seenBags.Add(next))
                {
                    // Bag seen before as part of other chain, don't need to expand it
                    continue;
                }

                // Check if it has any parents
                if (bags.ContainsKey(next))
                {
                    // add parents to open set
                    foreach (string bag in bags[next])
                    {
                        openSet.Enqueue(bag);
                    }
                }
            }

            // As I was lazy and put gold bag in open set, need to reduce this by 1 to avoid counting itself
            return $"{seenBags.Count - 1} can eventually contain a shiny gold bag";
        }
    }
}
