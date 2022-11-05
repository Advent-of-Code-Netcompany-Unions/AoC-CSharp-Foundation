using UnionsAoCFoundation.Plumbing;

namespace UnionsAoCFoundation.Solvers
{
    [SolvesChallenge("2022-11-01")]
    public class ExampleSolver : ISolver
    {
        public Task<string> SolvePartOne(IEnumerable<string> inputLines)
        {
            return Task.FromResult("Hello, ");            
        }

        public async Task<string> SolvePartTwo(IEnumerable<string> inputLines)
        {
            return await Task.Run(() => "World!");
        }
    }
}
