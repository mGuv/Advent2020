using System;
using System.Collections.Generic;
using System.Linq;

namespace Runner.Problems.Day8
{
    /// <summary>
    /// Represents the hand held 'game console'
    /// </summary>
    public class Console
    {
        /// <summary>
        /// Internal current instruction pointer
        /// </summary>
        private int offset;

        /// <summary>
        /// Current value int he accumulator
        /// </summary>
        private int accumulator;

        /// <summary>
        /// All the available operations the console can run
        /// </summary>
        private Dictionary<string, Action<string>> operations;

        /// <summary>
        /// Console constructor.
        /// </summary>
        public Console()
        {
            // Set up instructions
            this.operations = new Dictionary<string, Action<string>>()
            {
                {
                    "nop",
                    this.Noop
                },
                {
                    "acc",
                    this.Acc
                },
                {
                    "jmp",
                    this.Jmp
                }
            };
        }


        /// <summary>
        /// Runs the given instructions until a command is attempted to be repeated
        /// </summary>
        /// <param name="rawInstructions">The unparsed input instructions</param>
        /// <returns>The value in the accumulator when the program finished</returns>
        public int Run(string[] rawInstructions)
        {
            // Reset 'memory'
            this.offset = 0;
            this.accumulator = 0;

            (string command, string input)[] instructions =
                rawInstructions.Select(s => (s.Substring(0, 3), s.Substring(4))).ToArray();

            HashSet<int> usedIndexes = new HashSet<int>();

            while (offset < instructions.Length)
            {
                if (!usedIndexes.Add(this.offset))
                {
                    throw new RepeatedOffsetException(this.offset, this.accumulator);
                }

                this.operations[instructions[this.offset].command](instructions[this.offset].input);
            }

            return this.accumulator;
        }

        /// <summary>
        /// Null op command, does nothing except move offset
        /// </summary>
        /// <param name="_">Argument unused</param>
        private void Noop(string _)
        {
            this.offset++;
        }

        /// <summary>
        /// Adds the given value to the current accumulator
        /// </summary>
        /// <param name="input">The value to add</param>
        private void Acc(string input)
        {
            this.offset++;
            this.accumulator += int.Parse(input);
        }

        /// <summary>
        /// Jumps the offset pointer by the given amount
        /// </summary>
        /// <param name="input">The value to jump by</param>
        private void Jmp(string input)
        {
            this.offset += int.Parse(input);
        }
    }
}
