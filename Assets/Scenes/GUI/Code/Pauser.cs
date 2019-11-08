using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pauser
{
    public enum PauseState { PAUSED, ACTIVE }

    private static float lastActiveTimeScale;
    private static readonly Swapper<PauseState, float> _pauseStatus;
    public static PauseState PauseStatus
    { get => _pauseStatus.CurrentKey; set => _pauseStatus.CurrentKey = value; }

    static Pauser()
    {
        _pauseStatus = new Swapper<PauseState, float>((scale, active) => { if (active) Time.timeScale = scale; });
        _pauseStatus[PauseState.PAUSED] = 0;
        _pauseStatus[PauseState.ACTIVE] = Time.timeScale;
    }
}
