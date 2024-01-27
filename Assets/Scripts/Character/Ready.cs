using System;
using UnityEngine;

public class Ready : MonoBehaviour
{
    public static event Action<Transform> OnPlayerPressedReady;
    public static event Action<Transform> OnPlayerAwake;

    private void Awake() => OnPlayerAwake?.Invoke(transform);

    public void OnPressedReady() => OnPlayerPressedReady?.Invoke(transform);

}
