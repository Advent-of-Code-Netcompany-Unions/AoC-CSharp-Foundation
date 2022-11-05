using AdventOfCodeFoundation.IO;

namespace UnionsAoCFoundation.Plumbing
{
    internal interface ISolver
    {
        public Task<string> SolvePartOne(Input input);
        public Task<string> SolvePartTwo(Input input);
    }
}
