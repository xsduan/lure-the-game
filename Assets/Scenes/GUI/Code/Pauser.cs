using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pauser
{
    public enum PauseState { PAUSED, ACTIVE }

    private static float lastActiveTimeScale;
    public static PauseState CurrentPauseState { get; private set; }

    public static void SetPauseState(PauseState pauseState)
    {
        if (CurrentPauseState == pauseState) return;
        switch (pauseState)
        {
            case PauseState.PAUSED:
                SetPause();
                break;
            case PauseState.ACTIVE:
                SetActive();
                break;
        }
        CurrentPauseState = pauseState;
    }

    private static void SetPause()
    {
        lastActiveTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    private static void SetActive() => Time.timeScale = lastActiveTimeScale;
}
