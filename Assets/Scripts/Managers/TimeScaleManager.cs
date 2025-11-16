using UnityEngine;

public class TimeScaleManager : Singleton<TimeScaleManager>
{
    struct GameStateTimeScale
    {
        public GameState GameState;
        public float TimeScale;
    }

    [SerializeField] private GameStateTimeScale[] gameStatesTimeScales;

    protected override void Awake()
    {
        base.Awake();
        if (GameStateManager.Instance != null)
            GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (GameStateManager.Instance != null)
            GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        foreach (var gameStateTimeScale in gameStatesTimeScales)
        {
            if (gameStateTimeScale.GameState == state)
            {
                Time.timeScale = gameStateTimeScale.TimeScale;
                return;
            }
        }
    }
}
