using System.Reflection;
using UnionsAoCFoundation.Plumbing;

namespace AdventOfCodeFoundation
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            foreach (var solver in GetSolvers(DateOnly.FromDateTime(new DateTime(2022, 11, 1))))
            {
                Console.Write(await solver.SolvePartOne(null));
                Console.Write(await solver.SolvePartTwo(null));
            }
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