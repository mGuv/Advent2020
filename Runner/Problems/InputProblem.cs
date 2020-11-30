using System;
using System.Collections.Generic;
using Runner.Console;

namespace Runner.Problems
{
    /// <summary>
    /// Problem Class to extend when reading an input from the Program arguments
    /// </summary>
    public abstract class InputProblem : IProblem
    {
        /// <inheritdoc/>
        public abstract IAsyncEnumerable<string> RunAsync(Arguments arguments);

        /// <summary>
        /// Uses the arguments to find the input data
        /// </summary>
        /// <param name="arguments">The given Problem arguments</param>
        /// <returns>The input argument</returns>
        /// <exception cref="ArgumentException">Exception thrown when the input was missing</exception>
        protected string GetInput(Arguments arguments)
        {
            if (!arguments.HasArgument("i"))
            {
                throw new ArgumentException("No input parameter i was provided");
            }

            return arguments.GetArgument("i");
        }
    }
}
