using System;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public static float fadeDuration = 2f;

    public static event Action OnTransitionToGameMode;

    [ContextMenu("TransitionToGame")]
    public void TransitionToGame()
    {
        OnTransitionToGameMode?.Invoke();
    }
}
