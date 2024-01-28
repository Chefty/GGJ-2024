using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ready : MonoBehaviour
{
    public static event Action<Transform> OnPlayerPressedReady;
    public static event Action<Transform> OnPlayerAwake;
    private bool _canBeReady = false;
    private readonly float _timeBeforeCanBeReady = 1f; // just to avoid launching the game when spawning by pressing Start

    private void Awake()
    {
        OnPlayerAwake?.Invoke(transform);
        StartCoroutine(DelayAllowReady());
    }

    private IEnumerator DelayAllowReady()
    {
        yield return new WaitForSeconds(_timeBeforeCanBeReady);
        _canBeReady = true;
    }

    public void OnStart(InputAction.CallbackContext context)
    {
        if (context.canceled && _canBeReady) OnPlayerPressedReady?.Invoke(transform);
    }
}
