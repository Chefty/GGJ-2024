using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class LobbyToElevatorTransitionManager : MonoBehaviour
{
    [SerializeField] private RectTransform _rect;
    [SerializeField] private RectTransform _stripeRect;
    // vhs effect
    [SerializeField] private Material _fullscreenVHSMaterial;
    public Volume volume; // Assign this in the inspector

    private float _cacheVHSIntensity;

    private void Awake()
    {
        LobbyManager.OnTransitionToGameMode += TransitionToGame;
        _rect.anchoredPosition = Vector2.right * _stripeRect.rect.width;
        _rect.gameObject.SetActive(false);
        _cacheVHSIntensity = _fullscreenVHSMaterial.GetFloat("_Intensity");
        _fullscreenVHSMaterial.SetFloat("_Intensity", 0f);
        SetPostProcessingEffects(false);
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
        _rect.gameObject.SetActive(true);
        DOVirtual.Float(0f, 1, .5f, value =>
        {
            _rect.anchoredPosition = Vector2.right * Mathf.Lerp(-_stripeRect.rect.width, 0f, value);
        });
        // we wait for the scene to change
        yield return new WaitForSeconds(LobbyManager.fadeDuration - 1f);
        SetPostProcessingEffects(true);
        _fullscreenVHSMaterial.SetFloat("_Intensity", _cacheVHSIntensity);
        DOVirtual.Float(0f, 1, .5f, value =>
        {
            _rect.anchoredPosition = Vector2.right * Mathf.Lerp(0f, _stripeRect.rect.width, value);
        })
        .OnComplete(() =>
        {
            _rect.gameObject.SetActive(false);
        });
    }

    private void SetPostProcessingEffects(bool value)
    {
        if (volume.profile.TryGet(out LensDistortion lensDistortion))
        {
            lensDistortion.active = value;
        }
        if (volume.profile.TryGet(out ChromaticAberration chromaticAberation))
        {
            chromaticAberation.active = value;
        }
    }
}
