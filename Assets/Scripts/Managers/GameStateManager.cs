using System;
using UnityEngine;

public enum GameState
{
    LastState,
    InGame,
    Menu
}

public class GameStateManager : Singleton<GameStateManager>
{
    public Action<GameState> OnGameStateChanged;

    public GameState State
    {
        get => gameState;
        set
        {
            if (gameState == value)
                return;

            if (value == GameState.LastState)
                value = lastGameState;

            lastGameState = gameState;
            gameState = value;
            OnGameStateChanged?.Invoke(gameState);
        }
    }

    private GameState gameState;
    private GameState lastGameState;
}
