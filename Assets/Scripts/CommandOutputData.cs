public struct CommandOutputData
{
    public string standardOutput;
    public string standardError;

    public CommandOutputData(string standardOutput, string standardError)
    {
        this.standardOutput = standardOutput;
        this.standardError = standardError;
    }
}
