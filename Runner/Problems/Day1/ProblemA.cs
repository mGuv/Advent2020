using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day1
{
    /// <summary>
    /// Currently just an example problem that should be replaced once the problem has gone live
    /// </summary>
    [Injectable]
    public class ProblemA : InputProblem
    {
        /// <inheritdoc/>
        public override async IAsyncEnumerable<string> RunAsync(Arguments arguments)
        {
            string input = this.GetInput(arguments);
            yield return "got input " + input;
            await Task.Delay(5000);
            yield return "1 of 10";
            await Task.Delay(2000);
            yield return "5 of 10";
            Thread.Sleep(2000);
            yield return "6 of 10";
            await Task.Delay(2000);
            yield return "9 or 10";
            await Task.Delay(2000);
        }
    }
}
