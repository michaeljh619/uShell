using System;
using System.Diagnostics;

/// <summary>
/// Represents a bash shell.
/// </summary>
public class BashShell
{
    // member vars
    private readonly Process process;
    
    // Construction / Destruction
    public BashShell(string shellExecutablePath)
    {
        // perform error checks on given shell executable path
        TestShellPath(shellExecutablePath);
        
        // create process
        process = new Process()
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = shellExecutablePath,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            }
        };
        
        // initialization
        process.Start();
        process.StandardInput.AutoFlush = true;
    }
    ~BashShell()
    {
        Dispose();
    }
    /// <summary>
    /// Kills the bash process. If the bash process cannot be killed, returns false and does not throw
    /// an error.
    /// </summary>
    /// <returns>True if process is killed, false if process unable to be killed (already killed)</returns>
    public bool Dispose()
    {
        if (process == null || process.HasExited)
            return false;
        
        process.Kill();
        return true;
    }
    
    // public methods
    public CommandOutputData ExecuteCommand(string cmd)
    {
        // var escapedArgs = cmd.Replace("\"", "\\\"");
        var streamWriter = process.StandardInput;
        streamWriter.WriteLine(cmd);
        streamWriter.Flush();

        var output = "";
        while (process.StandardOutput.Peek() > -1)
        {
            output += process.StandardOutput.ReadLine();
        }
        
        var error = "";
        while (process.StandardOutput.Peek() > -1)
        {
            error += process.StandardOutput.ReadLine();
        }
        
        return new CommandOutputData(output, error);
    }
    
    // private methods
    private static void TestShellPath(string shellPath)
    {
        
    }
}