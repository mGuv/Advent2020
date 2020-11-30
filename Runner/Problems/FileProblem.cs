using System;
using System.Collections.Generic;
using Runner.Console;

namespace Runner.Problems
{
    /// <summary>
    /// Problem Class to extend when reading an input from a File
    /// </summary>
    public abstract class FileProblem : IProblem
    {
        /// <inheritdoc/>
        public abstract IAsyncEnumerable<string> RunAsync(Arguments arguments);

        /// <summary>
        /// Uses the arguments to find the input data
        /// </summary>
        /// <param name="arguments">The given Problem arguments</param>
        /// <returns>The entire files content as a string</returns>
        /// <exception cref="ArgumentException">Exception thrown when the input file was invalid</exception>
        protected string GetInput(Arguments arguments)
        {
            if (!arguments.HasArgument("f"))
            {
                throw new ArgumentException("No input file parameter f was provided");
            }

            string filePath = arguments.GetArgument("f");
            if (!System.IO.File.Exists(filePath))
            {
                throw new ArgumentException($"No input file was present at: {filePath}");
            }

            return System.IO.File.ReadAllText(filePath);
        }
    }
}
