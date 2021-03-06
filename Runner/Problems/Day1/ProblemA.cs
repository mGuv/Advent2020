using System.Threading.Tasks;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day1
{
    /// <summary>
    /// Solution to Day1 - Part A, utilising nested loops
    /// </summary>
    [Injectable]
    public class ProblemA : FileProblem
    {
        /// <inheritdoc/>
        public override string Run(Arguments arguments, Writer writer)
        {
            string[] parts = this.GetInput(arguments);
            for (int x = 0; x < parts.Length - 1; x++)
            {
                for (int y = x + 1; y < parts.Length; y++)
                {
                    writer.WriteLine($"Checking line {x} and {y}");

                    int a = int.Parse(parts[x].Trim());
                    int b = int.Parse(parts[y].Trim());

                    if (a + b == 2020)
                    {
                        return $"Answer: {(a * b).ToString()}";
                    }
                }
            }

            return "Failed to find an answer";
        }
    }
}
