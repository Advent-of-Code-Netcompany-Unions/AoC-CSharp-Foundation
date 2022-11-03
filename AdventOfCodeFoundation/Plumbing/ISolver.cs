using System.Reflection;

namespace UnionsAoCFoundation.Plumbing
{
    internal interface ISolver
    {
        public Task<string> SolvePartOne(IEnumerable<string> inputLines);
        public Task<string> SolvePartTwo(IEnumerable<string> inputLines);
    }
}
