using System;
using TMPro;
using UnityEngine;

public class Tester : MonoBehaviour
{
    // serialized fields
    [SerializeField]
    private string command = default;
    
    // member vars
    private BashShell bashShell;

    // Unity - Initialize
    private void Awake()
    {
        var bashPath = Application.dataPath + "/Plugins/EditorTerminal/bash.exe";
        bashShell = new BashShell(bashPath);
    }
    private void Start()
    {
        var unityProjectRootDir = Application.dataPath;
        //bashShell.ExecuteCommand($"cd {unityProjectRootDir}");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Debug.Log($"Executing command: {command}");
            
            var commandOutputData = bashShell.ExecuteCommand(command);
            Debug.Log(commandOutputData.standardOutput);
            Debug.LogError(commandOutputData.standardError);
        }
    }

    // Unity - Dispose
    private void OnDestroy()
    {
        bashShell.Dispose();
    }
    private void OnApplicationQuit()
    {
        bashShell.Dispose();
    }
}
