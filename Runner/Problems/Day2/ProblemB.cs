using System.Collections.Generic;
using System.Linq;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day2
{
    /// <summary>
    /// Solution to Day2 - Part B, utilising some basic string parsing
    /// </summary>
    [Injectable]
    public class ProblemB : FileProblem
    {
        /// <inheritdoc/>
        public override async IAsyncEnumerable<string> RunAsync(Arguments arguments)
        {
            string[] parts = this.GetInput(arguments);

            int valid = 0;

            foreach (string part in parts)
            {
                string[] bits = part.Split(' ');
                string[] bounds = bits[0].Split('-');
                char character = bits[1].Substring(0, 1).ToCharArray()[0];
                string password = bits[2];

                int pos1 = int.Parse(bounds[0]) - 1;
                int pos2 = int.Parse(bounds[1]) - 1;

                if (password[pos1] == character ^ password[pos2] == character)
                {
                    valid++;
                }
            }

            yield return $"Found {valid} valid passwords";
        }
    }
}
