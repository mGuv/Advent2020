using System;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day8
{
    /// <summary>
    /// Solution to Day8 - Part B - Wrapping the Emulator
    /// </summary>
    [Injectable]
    public class ProblemB : FileProblem
    {
        /// <inheritdoc/>
        public override string Run(Arguments arguments, Writer writer)
        {
            string[] lines = this.GetInput(arguments);
            Console console = new Console();


            // Massively lazy way of swapping commands but it got me a solution fast
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("jmp"))
                {
                    lines[i] = lines[i].Replace("jmp", "nop");
                }
                else
                {
                    lines[i] = lines[i].Replace("nop", "jmp");
                }

                try
                {
                    return $"Accumulator value: {console.Run(lines)}";
                }
                catch
                {
                    // ignored as trying to find genuine run
                }

                if (lines[i].Contains("jmp"))
                {
                    lines[i] = lines[i].Replace("jmp", "nop");
                }
                else
                {
                    lines[i] = lines[i].Replace("nop", "jmp");
                }
            }

            return "No valid program found";
        }
    }
}
