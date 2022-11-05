using AdventOfCodeFoundation.IO;
using UnionsAoCFoundation.Plumbing;

namespace AdventOfCodeFoundation.Solvers._2015
{
    [SolvesChallenge("2015/12/1")]
    internal class Day1Solver2015 : ISolver
    {
        async Task<string> ISolver.SolvePartOne(Input input)
        {
            return (await input.GetRawInput()).Aggregate(0, (s, c) => c == '(' ? s + 1 : s - 1).ToString();
        }

        async Task<string> ISolver.SolvePartTwo(Input input)
        {
            var instructions = await input.GetRawInput();
            var floor = 0;
            for(var i = 0; i < instructions.Length; i++)
            {
                floor += instructions[i] == '(' ? 1 : -1;
                if(floor < 0)
                {
                    return $"{i + 1}";
                }
            }

            return "No solution found.";
        }
    }
}
