using System;
using System.Collections.Generic;
using System.Linq;
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
        protected string GetRawInput(Arguments arguments)
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

        protected string[] GetInput(Arguments arguments)
        {
            string[] rawInput = this.GetRawInput(arguments).Split("\n");

            // Not taking any chances this year, ensure any new line related character is removed
            for (int i = 0; i < rawInput.Length; i++)
            {
                rawInput[i] = rawInput[i].Replace("\r", "").Replace("\n", "");
            }

            // Rider is adding a blank line to the end so ensure only lines with content are allowed through
            return rawInput.Where(s => s.Length > 0).ToArray();
        }
    }
}
