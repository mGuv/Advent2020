using System.Collections.Generic;
using System.Linq;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day4
{
    /// <summary>
    /// Solution to Day3 - Part A, Nested loops but now with modulo
    /// </summary>
    [Injectable]
    public class ProblemA : FileProblem
    {
        public override string Run(Arguments arguments, Writer writer)
        {
            string[] lines = this.GetRawInput(arguments).Split("\n");

            List<Dictionary<string, string>> passports = new List<Dictionary<string, string>>();

            for (int i = 0; i < lines.Length; i++)
            {
                Dictionary<string, string> passport = new Dictionary<string, string>();
                while (lines[i] != "" && i <lines.Length)
                {
                    // parse current line
                    string line = lines[i];
                    string[] fields = line.Split(" ");
                    foreach (string[] parts in fields.Select(p => p.Split(":")))
                    {
                        passport.Add(parts[0], parts[1]);
                    }
                    i++;
                }

                if (passport.Count > 0)
                {
                    passports.Add(passport);
                }
            }

            int validPassports = 0;
            string[] requiredFields =
            {
                "byr",
                "iyr",
                "eyr",
                "hgt",
                "hcl",
                "ecl",
                "pid"
            };

            foreach (Dictionary<string,string> dictionary in passports)
            {
                bool valid = true;
                foreach (string requiredField in requiredFields)
                {
                    if (!dictionary.ContainsKey(requiredField))
                    {
                        valid = false;
                        break;
                    }
                }

                if (valid)
                {
                    validPassports++;
                }
            }

            return $"found {validPassports} valid passports";
        }
    }
}
