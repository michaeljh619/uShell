using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    private BashShell bashShell;

    private void Awake()
    {
        bashShell = new BashShell();
    }

    private void OnDestroy()
    {
        bashShell.Dispose();
    }
    private void OnApplicationQuit()
    {
        bashShell.Dispose();
    }
}
