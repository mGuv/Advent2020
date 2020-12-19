using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day18
{
    /// <summary>
    /// Solution to Day18 - Part A, lexical parsing and a mess
    /// </summary>
    [Injectable]
    public class ProblemA : FileProblem
    {
        /// <inheritdoc/>
        public override string Run(Arguments arguments, Writer writer)
        {
            string[] parts = this.GetInput(arguments);
            ulong sum = 0;
            foreach (string part in parts)
            {
                string adjusted = part;
                sum += (ulong) GetNextValue(ref adjusted);
            }

            //(7 * 5 * 6 + (9 * 8 + 3 * 3 + 5) + 7) * (6 + 3 * 9) + 6 + 7 + (7 * 5) * 4

            // Could recurse here on every bracket?

            return $"{sum}";
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
