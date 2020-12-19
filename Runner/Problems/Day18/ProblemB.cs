using System.Collections.Generic;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day18
{
    /// <summary>
    /// Solution to Day18 - Part B, lexical parsing with substitutions
    /// </summary>
    [Injectable]
    public class ProblemB : FileProblem
    {
        /// <inheritdoc/>
        public override string Run(Arguments arguments, Writer writer)
        {
            string[] parts = this.GetInput(arguments);
            ulong sum = 0;
            string[] subLetters = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q"};
            for (var index = 0; index < parts.Length; index++)
            {
                string part = "0 + " + parts[index];
                System.Console.WriteLine(part);
                List<int> depth = new List<int>();
                Dictionary<string, string> subs = new Dictionary<string, string>();

                for (int i = 0; i < part.Length; i++)
                {
                    if (part[i] == ' ')
                    {
                        continue;
                    }

                    if (part[i] == '(')
                    {
                        depth.Add(i);
                        continue;
                    }

                    if (part[i] == ')')
                    {
                        // sub time
                        int previousDepth = depth[depth.Count - 1];
                        depth.RemoveAt(depth.Count - 1);

                        // So from previousDepth to i is a sub string
                        string nextSub = subLetters[subs.Count];
                        string bit = part.Substring(previousDepth + 1, i - previousDepth - 1);
                        subs.Add(nextSub, bit);
                        part = part.Substring(0, previousDepth) + nextSub + part.Substring(i + 1);
                        i = previousDepth;
                    }
                }

                sum += (ulong) this.ParseString(subs, part);
            }

            //(7 * 5 * 6 + (9 * 8 + 3 * 3 + 5) + 7) * (6 + 3 * 9) + 6 + 7 + (7 * 5) * 4

            // Take string and replace brackets with subs, i.e.
            // A * C + 6 + 7 + D * 4
            // A = 7 * 5 * 6 + B + 7
            // B = 9 * 8 + 3 * 3 + 5
            // C = 6 + 3 * 9
            // D = 7 * 5


            // Could recurse here on every bracket?

            return $"{sum}";
        }

        private long GetValue(Dictionary<string, string> subs, string toParse)
        {
            if (long.TryParse(toParse, out long val))
            {
                return val;
            }

            return this.ParseString(subs, subs[toParse]);
        }

        private long ParseString(Dictionary<string, string> subs, string toParse)
        {
            List<string> bits = new List<string>();
            bits.AddRange(toParse.Split(' '));

            while (bits.Contains("+"))
            {
                // get index of +
                int nextPlus = bits.IndexOf("+");

                long a = this.GetValue(subs, bits[nextPlus - 1]);
                long b = this.GetValue(subs, bits[nextPlus + 1]);

                long combined = a + b;

                bits.RemoveAt(nextPlus - 1);
                bits.RemoveAt(nextPlus - 1);
                bits.RemoveAt(nextPlus - 1);
                bits.Insert(nextPlus - 1, combined.ToString());
            }

            while (bits.Contains("*"))
            {
                // get index of +
                int nextPlus = bits.IndexOf("*");

                long a = this.GetValue(subs, bits[nextPlus - 1]);
                long b = this.GetValue(subs, bits[nextPlus + 1]);

                long combined = a * b;

                bits.RemoveAt(nextPlus - 1);
                bits.RemoveAt(nextPlus - 1);
                bits.RemoveAt(nextPlus - 1);
                bits.Insert(nextPlus - 1, combined.ToString());
            }

            return long.Parse(bits[0]);
        }

        private long GetNextValue(ref string toProcess)
        {
            long runningNumber = -1;
            bool areMulti = false;
            while (toProcess.Length > 0)
            {
                if (toProcess[0] == ' ')
                {
                    toProcess = toProcess.Remove(0, 1);
                    continue;
                }

                if (toProcess[0] == '(')
                {
                    toProcess = toProcess.Remove(0, 1);

                    long nestedVal =  GetNextValue(ref toProcess);
                    if (runningNumber < 0)
                    {
                        runningNumber = nestedVal;
                    }
                    else
                    {
                        if (areMulti)
                        {
                            runningNumber *= nestedVal;
                        }
                        else
                        {
                            runningNumber += nestedVal;
                        }
                    }

                    continue;
                }

                if (toProcess[0] == ')')
                {
                    toProcess = toProcess.Remove(0, 1);
                    break;
                }

                // It's an operator so... Apply previous to next and return it
                if (toProcess[0] == '*')
                {
                    areMulti = true;
                    toProcess = toProcess.Remove(0, 1);
                    continue;
                }

                if (toProcess[0] == '+')
                {
                    areMulti = false;
                    toProcess = toProcess.Remove(0, 1);
                    continue;
                }

                int val = int.Parse(toProcess[0].ToString());
                if (runningNumber < 0)
                {
                    runningNumber = val;
                }
                else
                {
                    if (areMulti)
                    {
                        // we only wanna do this if
                        runningNumber *= val;
                    }
                    else
                    {
                        runningNumber += val;
                    }
                }

                toProcess = toProcess.Remove(0, 1);
            }

            return runningNumber;
        }
    }
}
