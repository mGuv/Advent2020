    using System.Threading;
using System.Threading.Tasks;
using Advent2020.DI;

namespace Runner.Console
{
    /// <summary>
    /// Console Wrapper for easily writing new lines or overwriting the current line
    /// </summary>
    [Injectable]
    public class Writer
    {
        /// <summary>
        /// Writes a line that ends with a line break, defaults to empty for forcing printing on a new line
        /// </summary>
        /// <param name="line">The line to write out</param>
        public void WriteNewLine(string line = "")
        {
            System.Console.WriteLine(line);
        }

        /// <summary>
        /// Erases the current Line and writes a new one
        /// </summary>
        /// <param name="line">The line to write</param>
        public void WriteLine(string line)
        {
            System.Console.SetCursorPosition(0, System.Console.CursorTop);
            System.Console.Write(new string(' ', System.Console.BufferWidth));
            System.Console.SetCursorPosition(0, System.Console.CursorTop - 1);
            System.Console.Write(line);
        }
    }
}
