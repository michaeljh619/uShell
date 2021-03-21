using System.Diagnostics;
using UnityEngine;

/// <summary>
/// Represents a bash shell.
/// </summary>
public class BashShell
{
    // member vars
    private readonly Process process;
    
    // Construction / Destruction
    public BashShell()
    {
        var bashExePath = GetBashExePath();
        
        process = new Process()
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = bashExePath,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            }
        };
        
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
    public string ExecuteCommand(string cmd)
    {
        // var escapedArgs = cmd.Replace("\"", "\\\"");
        var streamWriter = process.StandardInput;
        streamWriter.WriteLine(cmd);

        return process.StandardOutput.ReadToEnd();
    }

    // private static methods
    private static string GetBashExePath()
    {
        return Application.dataPath + "/Plugins/EditorTerminal/bash.exe";
    }
}