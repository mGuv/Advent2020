using System.Collections.Generic;
using Runner.Console;

namespace Runner.Problems
{
    /// <summary>
    /// Base Interface for running a Problem
    /// </summary>
    public interface IProblem
    {
        /// <summary>
        /// Runs the Problem with the given arguments
        /// </summary>
        /// <param name="arguments">The arguments to control the problem</param>
        /// <returns>An awaitable, iterable stream of messages about the current problem's state</returns>
        IAsyncEnumerable<string> RunAsync(Arguments arguments);
    }
}
