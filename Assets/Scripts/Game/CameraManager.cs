using System;
using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera lobbyCamera;
    public Camera[] gameCameras;

    private Camera currentCamera;

    public static event Action<Camera> OnSwitchCamera;

    private void Awake()
    {
        LobbyManager.OnTransitionToGameMode += TransitionToGame;
        InitializeCameras();
    }

    private void OnDestroy()
    {
        LobbyManager.OnTransitionToGameMode -= TransitionToGame;
    }

    private void InitializeCameras()
    {
        currentCamera = lobbyCamera;
        lobbyCamera.gameObject.SetActive(true);

        for (int i = 0; i < gameCameras.Length; i++)
        {
            gameCameras[i].gameObject.SetActive(false);
        }
    }

    public void TransitionToGame()
    {
        StartCoroutine(DoTransitionToGameMode());
    }

    private IEnumerator DoTransitionToGameMode()
    {
        yield return new WaitForSeconds(LobbyManager.fadeDuration / 2f);
        SwitchToNewCamera(gameCameras[0]);
    }

    private void SwitchToNewCamera(Camera newCamera)
    {
        currentCamera.gameObject.SetActive(false);
        currentCamera = newCamera;
        currentCamera.gameObject.SetActive(true);
        OnSwitchCamera?.Invoke(currentCamera);
    }
}
