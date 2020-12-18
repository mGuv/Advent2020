using System;
using System.Collections.Generic;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day14
{
    /// <summary>
    /// Solution to Day 14 - Part B - Some very messy binary casting with branching paths
    /// </summary>
    [Injectable]
    public class ProblemB : FileProblem
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
                long address = long.Parse(input[i].Substring(leftPos, rightPos - leftPos));
                ulong value = ulong.Parse(input[i].Substring(input[i].IndexOf("=") + 1));

                string asBinary = Convert.ToString(address, 2);
                asBinary = asBinary.Trim();
                // pad left till 36 digits
                asBinary = asBinary.PadLeft(36, '0');
                char[] aschar = asBinary.ToCharArray();


                writer.WriteNewLine(currentMask);

                writer.WriteNewLine(asBinary);

                // apply mask
                for (int s = 0; s < currentMask.Length; s++)
                {
                    if (currentMask[s] == 'X')
                    {
                        aschar[s] = currentMask[s];
                    }
                    else if (currentMask[s] == '1')
                    {
                        aschar[s] = '1';
                    }
                }

                // Now need to store every possible instance where X can be both 0 and 1

                Queue<char[]> toProcess = new Queue<char[]>();
                toProcess.Enqueue(aschar);
                while (toProcess.Count > 0)
                {
                    char[] next = toProcess.Dequeue();
                    bool processed = false;
                    for (int s = 0; s < next.Length; s++)
                    {
                        if (next[s] == 'X')
                        {
                            char[] with0 = new char[next.Length];
                            next.CopyTo(with0, 0);
                            char[] with1 = new char[next.Length];
                            next.CopyTo(with1, 0);

                            with0[s] = '0';
                            with1[s] = '1';

                            toProcess.Enqueue(with0);
                            toProcess.Enqueue(with1);

                            processed = true;
                            break;
                        }
                    }

                    if (processed)
                    {
                        continue;
                    }
                    // finished, so work it out
                    string asString = new string(next);
                    memory[Convert.ToUInt64(asString, 2)] = value;
                }
            }

            ulong total = 0;
            foreach (KeyValuePair<ulong,ulong> keyValuePair in memory)
            {
                total += keyValuePair.Value;
            }
            return $"total:{total}";
        }
    }
}
