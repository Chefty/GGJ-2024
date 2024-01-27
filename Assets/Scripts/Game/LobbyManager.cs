using System;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public static float fadeDuration = 2f;

    public static event Action OnTransitionToGameMode;

    private void Awake()
    {
        LobbyPlayerHandler.OnAllPlayersReady += TransitionToGame;
    }

    private void OnDestroy()
    {
        LobbyPlayerHandler.OnAllPlayersReady -= TransitionToGame;
    }

    [ContextMenu("TransitionToGame")]
    public void TransitionToGame()
    {
        OnTransitionToGameMode?.Invoke();
    }
}
