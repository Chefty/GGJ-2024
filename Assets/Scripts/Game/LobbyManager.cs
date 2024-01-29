using System;
using System.Collections;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public static float fadeDuration = 3f;
    public static float lobbyAnimationDuration = 5f;

    public static event Action OnStartCountDown;
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
        StartCoroutine(DoTransitionToGameMode());
    }

    private IEnumerator DoTransitionToGameMode()
    {
        LobbyPlayerHandler.OnAllPlayersReady -= TransitionToGame;
        OnStartCountDown?.Invoke();
        yield return new WaitForSeconds(lobbyAnimationDuration);
        OnTransitionToGameMode?.Invoke();
    }
}
