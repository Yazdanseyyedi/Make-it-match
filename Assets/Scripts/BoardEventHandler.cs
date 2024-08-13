using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardEventHandler : ScriptableObject
{
    public event Action DestroyMatches;

    public void RaiseDestroyMatchesAction()
    {
        DestroyMatches?.Invoke();
    }
}
