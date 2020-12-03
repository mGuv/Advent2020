using System.Threading.Tasks;
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
        /// <param name="writer">The writer to log debug messages to</param>
        /// <returns>An awaitable task that upon completion contains the result</returns>
        Task<string> RunAsync(Arguments arguments, Writer writer);
    }
}
