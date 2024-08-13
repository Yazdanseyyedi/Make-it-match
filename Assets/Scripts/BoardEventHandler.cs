using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardEventHandler : ScriptableObject
{
    public event Action DestroyMatches;
    public event Action OnTimerEnd;


    public void RaiseDestroyMatchesAction()
    {
        DestroyMatches?.Invoke();
    }

    public void RaiseOnTimerEndAction()
    {
        OnTimerEnd?.Invoke();
    }
}
