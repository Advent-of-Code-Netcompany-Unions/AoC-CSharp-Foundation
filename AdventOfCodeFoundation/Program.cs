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
                Output.Line($"Solving {challengeDate} with {solver.GetType().Name}...\n");                
                await RunSolver(solver, challengeDate);
            }
        }

        private static async Task RunSolver(ISolver solver, DateOnly challengeDate)
        {            
            var input = new Input(challengeDate);            

            Output.Line($"Solving part 1...");
            var res = await solver.SolvePartOne(input);
            Output.Line($"Solved part 1 with result:\n{res}\n");            

            Output.Line($"Solving part 2...");
            res = await solver.SolvePartTwo(input);
            Output.Line($"Solved part 2 with result:\n{res}\n");            
        }

        private static DateOnly GetChallengeDate(string[] args)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            if(!args.Any())
                return today;

            if (args.Length > 1)
                Output.Warning($"Multiple arguments provided. Ignoring all arguments after '{args[0]}'...");

            var date = TryParseInt(args[0]) ?? TryParseDateOnly(args[0]) ?? TryParseDateTime(args[0]);
            if (!date.HasValue)
                throw new ArgumentException($"Could not parse argument {args[0]}.");

            return today;
        }

        private static DateOnly? TryParseInt(string arg)
        {
            var today = DateTime.Today;
            if (int.TryParse(arg, out var day) && day > 0 && day < 31)
                return new DateOnly(today.Year, today.Month, day);

            return null;
        }

        private static DateOnly? TryParseDateOnly(string arg)
        {
            if (DateOnly.TryParse(arg, out var date))
                return date;

            return null;
        }

        private static DateOnly? TryParseDateTime(string arg)
        {
            if (DateTime.TryParse(arg, out var datetime))
                return DateOnly.FromDateTime(datetime);

            return null;
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