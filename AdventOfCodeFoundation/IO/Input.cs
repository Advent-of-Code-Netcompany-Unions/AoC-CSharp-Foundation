namespace AdventOfCodeFoundation.IO
{
    public class Input
    {
        private readonly string file;

        public Input(string fileName)
        {
            file = fileName;
        }

        public Input(DateOnly challengeDate)
        {
            file = $"../../../Inputs/{challengeDate.Year}/{challengeDate.Day}";
        }

        public async Task<string> GetRawInput()
        {
            return await File.ReadAllTextAsync(file);
        }

        public async Task<IEnumerable<string>> GetInputLines()
        {
            return await File.ReadAllLinesAsync(file);
        }
    }
}
