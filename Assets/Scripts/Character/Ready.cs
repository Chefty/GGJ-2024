using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ready : MonoBehaviour
{
    public static event Action<Transform> OnPlayerPressedReady;
    public static event Action<Transform> OnPlayerAwake;

    private void Awake() => OnPlayerAwake?.Invoke(transform);

    public void OnStart(InputAction.CallbackContext context)
    {
        if (context.canceled) OnPlayerPressedReady?.Invoke(transform);
    }
}
