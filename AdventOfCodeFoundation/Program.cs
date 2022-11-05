using AdventOfCodeFoundation.IO;
using System.Reflection;
using UnionsAoCFoundation.Plumbing;

namespace AdventOfCodeFoundation
{
    internal class Program
    {
        static async Task Main(string[] args)
        {           
            if (args.Intersect(new[] {"?", "h", "-h", "--h", "help", "-help", "--help" }).Any())
            {
                Output.Help();
                return;
            }            

            var challengeDate = GetChallengeDate(args);
            var solvers = GetSolvers(challengeDate);

            Output.Line($"Found {solvers.Count()} solvers for {challengeDate}...");

            foreach (var solver in solvers)
            {
                Output.Line($"Solving {challengeDate} with {solver.GetType().Name}...");
                var input = new Input(challengeDate);

                Output.Line($"Solving part 1...");
                Output.Line(await solver.SolvePartOne(input));

                Output.Line($"Solving part 2...");
                Output.Line(await solver.SolvePartTwo(input));
            }
        }

        private static DateOnly GetChallengeDate(string[] args)
        {           
            var today = DateTime.Today;
            if (args.Any())
            {
                if (args.Length > 1)
                {
                    Output.Warning($"Multiple arguments provided. Ignoring all arguments after '{args[0]}'...");
                }

                if (int.TryParse(args[0], out var day) && day > 0 && day < 31)
                {
                    return new DateOnly(today.Year, today.Month, day);
                }

                if (DateOnly.TryParse(args[0], out var date))
                {
                    return date;
                }

                if (DateTime.TryParse(args[0], out var datetime))
                {
                    return DateOnly.FromDateTime(datetime);
                }
            }

            return DateOnly.FromDateTime(today);
        }

        private static IEnumerable<ISolver> GetSolvers(DateOnly challengeDate)
        {
            return GetAllSolverImplementations()
                .Where(st => st.GetCustomAttribute<SolvesChallenge>()?.challengeDate == challengeDate)
                .Select(st => (ISolver)Activator.CreateInstance(st)!);
        }

        private static IEnumerable<Type> GetAllSolverImplementations()
        {
            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && typeof(ISolver).IsAssignableFrom(t));
        }
    }
}