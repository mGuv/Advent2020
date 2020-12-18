using System;
using System.Collections.Generic;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day14
{
    /// <summary>
    /// Solution to Day 14 - Part A - Some very messy binary manipulation
    /// </summary>
    [Injectable]
    public class ProblemA : FileProblem
    {
        public override string Run(Arguments arguments, Writer writer)
        {
            string[] input = this.GetInput(arguments);

            ulong answer = 0;
            Dictionary<ulong, ulong> memory = new Dictionary<ulong, ulong>();

            string currentMask = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].Contains("mask"))
                {
                    currentMask = input[i].Substring(input[i].IndexOf("=") + 2);
                    continue;
                }

                //
                int leftPos = input[i].IndexOf("[") + 1;
                int rightPos = input[i].IndexOf("]");
                ulong address = ulong.Parse(input[i].Substring(leftPos, rightPos - leftPos));

                string asBinary = Convert.ToString((long)ulong.Parse(input[i].Substring(input[i].IndexOf("=") + 1)), 2);
                asBinary = asBinary.Trim();
                // pad left till 36 digits
                asBinary = asBinary.PadLeft(36, '0');
                char[] aschar = asBinary.ToCharArray();


                writer.WriteNewLine(currentMask);

                writer.WriteNewLine(asBinary);

                // apply mask
                for (int s = 0; s < currentMask.Length; s++)
                {
                    if (currentMask[s] != 'X')
                    {
                        aschar[s] = currentMask[s];
                    }
                }

                string asString = new string(aschar);


                memory[address] = Convert.ToUInt64(asString, 2);
            }

            ulong total = 0;
            foreach (KeyValuePair<ulong,ulong> keyValuePair in memory)
            {
                total += keyValuePair.Value;
            }
            return $"total: {total}";
        }
    }
}
