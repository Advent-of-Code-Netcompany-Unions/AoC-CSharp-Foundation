namespace UnionsAoCFoundation.Plumbing
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SolvesChallenge : Attribute
    {
        public readonly DateOnly challengeDate;

        public SolvesChallenge(string challengeDate)
        {
            this.challengeDate = DateOnly.Parse(challengeDate);
        }
    }
}
