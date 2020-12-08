using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day8
{
    /// <summary>
    /// Solution to Day8 - Part A - Time to build an emulator (smells like 2019!)
    /// </summary>
    [Injectable]
    public class ProblemA : FileProblem
    {
        /// <inheritdoc/>
        public override string Run(Arguments arguments, Writer writer)
        {
            string[] lines = this.GetInput(arguments);

            Console console = new Console();
            try
            {
                return $"Accumulator value:{console.Run(lines)}";
            }
            catch (RepeatedOffsetException e)
            {
                // Silly output because why not
                writer.WriteNewLine("--- ERROR ---");
                writer.WriteNewLine("Console failed to execute given instructions...");
                writer.WriteNewLine("Dumping Memory...");
                writer.WriteNewLine($"Offset: {e.Offset}");
                writer.WriteNewLine($"Accumulator: {e.Accumulator}");
                return $"-------------";
            }
        }
    }
}
