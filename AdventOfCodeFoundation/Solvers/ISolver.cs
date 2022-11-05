using AdventOfCodeFoundation.IO;

namespace AdventOfCodeFoundation.Solvers
{
    internal interface ISolver
    {
        public Task<string> SolvePartOne(Input input);
        public Task<string> SolvePartTwo(Input input);

        public async Task Run(DateOnly challengeDate)
        {
            Output.Line($"Solving {challengeDate} with {GetType().Name}...\n");

            var input = new Input(challengeDate);
            await SolvePart(1, input);
            await SolvePart(2, input);
        }

        private async Task SolvePart(int part, Input input)
        {
            Output.Line($"Solving part {part}...");
            var res = await (part == 1 ? SolvePartOne(input) : SolvePartTwo(input));
            Output.Line($"Solved part {part} with result:\n{res}\n");

        }
    }
}
