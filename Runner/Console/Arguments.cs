using System;
using System.Collections.Generic;

namespace Runner.Console
{
    /// <summary>
    /// Wrapper for command line arguments to make it easier to read
    /// </summary>
    public class Arguments
    {
        /// <summary>
        /// Internal storage of any passed in arguments
        /// </summary>
        private Dictionary<string, string> rawArguments = new Dictionary<string, string>();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="arguments">The raw command arguments to parse</param>
        /// <exception cref="ArgumentException">Thrown when a badly formed argument is provided</exception>
        public Arguments(string[] arguments)
        {
            foreach (string argument in arguments)
            {
                int splitIndex = argument.IndexOf('=');

                if (splitIndex < 0)
                {
                    throw new ArgumentException($"Badly formatted argument: {argument}");
                }

                this.rawArguments.Add(argument.Substring(0, splitIndex), argument.Substring(splitIndex + 1));
            }
        }

        /// <summary>
        /// Checks if the given argument is present
        /// </summary>
        /// <param name="key">The argument to check the presence of</param>
        /// <returns>True if the argument is set, otherwise false</returns>
        public bool HasArgument(string key)
        {
            return this.rawArguments.ContainsKey(key);
        }

        /// <summary>
        /// Gets the value of the given argument
        /// </summary>
        /// <param name="key">The argument to get</param>
        /// <returns>The raw value of that argument</returns>
        public string GetArgument(string key)
        {
            return this.rawArguments[key];
        }
    }
}
