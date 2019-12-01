using UnityEngine;

public static class Pauser {
    public enum PauseState {
        PAUSED,
        ACTIVE
    }

    private static readonly float lastActiveTimeScale;
    private static readonly Swapper<PauseState, float> _pauseStatus;

    static Pauser() {
        _pauseStatus = new Swapper<PauseState, float>((scale, active) => {
            if (active) {
                Time.timeScale = scale;
            }
        }) {
            [PauseState.PAUSED] = 0,
            [PauseState.ACTIVE] = Time.timeScale
        };
    }

    public static PauseState PauseStatus {
        get => _pauseStatus.CurrentKey;
        set => _pauseStatus.CurrentKey = value;
    }
}