using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class PostProcessManager : MonoBehaviour
{
    [SerializeField] private Image _fadeImage;

    private void Awake()
    {
        LobbyManager.OnTransitionToGameMode += TransitionToGame;
    }

    private void OnDestroy()
    {
        LobbyManager.OnTransitionToGameMode -= TransitionToGame;
    }

    public void TransitionToGame()
    {
        StartCoroutine(DoTransitionToGameMode());
    }

    private IEnumerator DoTransitionToGameMode()
    {
        _fadeImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(LobbyManager.fadeDuration);
        _fadeImage.gameObject.SetActive(false);
    }
}
