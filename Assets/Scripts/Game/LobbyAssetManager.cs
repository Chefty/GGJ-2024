using System.Collections;
using UnityEngine;

public class LobbyAssetManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _assetsToDisable;

    private void Awake()
    {
        LobbyManager.OnTransitionToGameMode += DisableLobbyAssets;
    }

    private void OnDestroy()
    {
        LobbyManager.OnTransitionToGameMode -= DisableLobbyAssets;
    }

    private void DisableLobbyAssets()
    {
        StartCoroutine(DisplayCountdown());
    }

    private IEnumerator DisplayCountdown()
    {
        yield return new WaitForSeconds(LobbyManager.fadeDuration / 2f);
        for (int i = 0; i < _assetsToDisable.Length; i++)
        {
            _assetsToDisable[i].SetActive(false);
        }
    }
}
