using System;
using System.Reflection;
using System.Threading.Tasks;
using Advent2020.DI;
using Microsoft.Extensions.DependencyInjection;
using Runner.Console;
using Runner.Problems;

namespace Runner
{
    class Program
    {
        /// <summary>
        /// Main entry point
        /// </summary>
        /// <param name="args">Command line arguments passed in</param>
        /// <returns>Empty Task that completes when the application has finished</returns>
        /// <exception cref="ArgumentException">Exception thrown when the program was called with invalid argumente</exception>
        static void Main(string[] args)
        {
            ProviderBuilder builder = new ProviderBuilder();
            IServiceProvider container = builder.AutowireAssembly(Assembly.GetExecutingAssembly()).Build();
            Arguments arguments = new Arguments(args);

            if (!arguments.HasArgument("p"))
            {
                throw new ArgumentException("No problem specified with argument p");
            }

            Program.Autorun(container, arguments);
        }

        /// <summary>
        /// Runs the given Problem
        /// </summary>
        /// <param name="container">The DI container to use for any extra dependencies</param>
        /// <param name="arguments">The arguments provided to the Program for problem specific arguments</param>
        /// <param name="problem">The problem itself to run</param>
        /// <returns>Awaitable Task that ends when Problem is finished</returns>
        /// <exception cref="Exception">Thrown if there is something wrong with the Container</exception>
        static void RunProblem(IServiceProvider container, Arguments arguments, IProblem problem)
        {
            Writer? writer = container.GetService<Writer>();

            if (writer is null)
            {
                throw new Exception("Container has failed as it couldn't load the basic Writer class");
            }

            // Run Problem on background task
            string answer = problem.Run(arguments, writer);

            // Problem has finished, write out a sanity message to ensure it completed
            writer.WriteNewLine();
            writer.WriteNewLine("----");
            writer.WriteNewLine("Problem Finished!");
            writer.WriteNewLine(answer);
        }


        /// <summary>
        /// Auto runs the application based of given arguments
        /// </summary>
        /// <param name="container">The DI Container to grab Problems through</param>
        /// <param name="arguments">The incoming Program arguments</param>
        /// <returns>Awaitable Task that ends on Problem execution</returns>
        /// <exception cref="ArgumentException">Thrown when an invalid Problem is specified</exception>
        static void Autorun(IServiceProvider container, Arguments arguments)
        {
            // Check Type exists
            Type? type = Type.GetType($"Runner.Problems.{arguments.GetArgument("p")}");
            if (type is null)
            {
                throw new ArgumentException(
                    $"Couldn't load type specified for argument p: {arguments.GetArgument("p")}");
            }

            // Check Type is actually runnable
            IProblem? problem = container.GetService(type) as IProblem;
            if (problem is null)
            {
                throw new ArgumentException(
                    $"Type specified for argument p is not a runnable IProblem: {arguments.GetArgument("p")}");
            }

            // Run
            Program.RunProblem(container, arguments, problem);
        }
    }
}
