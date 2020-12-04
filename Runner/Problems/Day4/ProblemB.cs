using System;
using System.Collections.Generic;
using System.Linq;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day4
{
    /// <summary>
    /// Solution to Day4 - Part B,  more difficult input parsing and dictionaries, plus method variables now
    /// </summary>
    [Injectable]
    public class ProblemB : FileProblem
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


            Dictionary<string, Predicate<string>> requiredFields = new Dictionary<string, Predicate<string>> {
                {"byr", this.BuildYearValidator(1920, 2002)},
                {"iyr", this.BuildYearValidator(2010, 2020)},
                {"eyr", this.BuildYearValidator(2020, 2030)},
                {"hgt", this.ValidateHeight},
                {"hcl", this.ValidateHairColour},
                {"ecl", this.ValidateEyeColour},
                {"pid", this.ValidatePid}

            };

            foreach (Dictionary<string,string> dictionary in passports)
            {
                bool valid = true;
                foreach (var requiredField in requiredFields)
                {
                    if (!dictionary.ContainsKey(requiredField.Key))
                    {
                        valid = false;
                        break;
                    }

                    if (!requiredField.Value(dictionary[requiredField.Key]))
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

        private bool ValidatePid(string pid)
        {
            string without0 = pid.TrimStart('0');
            return int.TryParse(without0, out _);
        }

        private bool ValidateEyeColour(string eyeColour)
        {
            HashSet<string> validColours = new HashSet<string>
            {
                "amb",
                "blu",
                "brn",
                "gry",
                "grn",
                "hzl",
                "oth"
            };

            return validColours.Contains(eyeColour);
        }
        private Predicate<string> BuildYearValidator(int min, int max)
        {
            return (s) =>
            {
                if (!int.TryParse(s, out int asInt))
                {
                    return false;
                }

                return asInt >= min && asInt <= max;
            };
        }

        private bool ValidateHeight(string height)
        {
            // rules state at least 2 digits of height and 2 digits for unit
            if (height.Length < 4)
            {
                return false;
            }


            string unit = height.Substring(height.Length - 2);
            int value = int.Parse(height.Substring(0, height.Length - 2));
            if (unit == "cm")
            {
                return value >= 150 && value <= 193;
            }

            if (unit == "in")
            {
                return value >= 59 && value <= 76;
            }

            return false;
        }

        private bool ValidateHairColour(string hairColour)
        {
            if (hairColour.Length != 7)
            {
                return false;
            }

            if (hairColour[0] != '#')
            {
                return false;
            }

            hairColour = hairColour.Substring(1);

            if (!int.TryParse(hairColour, System.Globalization.NumberStyles.HexNumber, null, out _))
            {
                return false;
            }

            return true;
        }
    }
}
