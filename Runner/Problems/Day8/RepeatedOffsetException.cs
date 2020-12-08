using System;

namespace Runner.Problems.Day8
{
    /// <summary>
    /// Exception thrown when the Console attempts to run the same instruction twice
    /// </summary>
    public class RepeatedOffsetException : Exception
    {
        /// <summary>
        /// The Offset of the offending instruction
        /// </summary>
        public readonly int Offset;

        /// <summary>
        /// The current value of the Accumulator at time of Exception
        /// </summary>
        public readonly int Accumulator;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="offset">The program offset that failed</param>
        /// <param name="accumulator">The current value of the accumulator</param>
        public RepeatedOffsetException(int offset, int accumulator): base($"Console attempted to execute instruction twice at offset {offset}")
        {
            this.Offset = offset;
            this.Accumulator = accumulator;
        }
    }
}
