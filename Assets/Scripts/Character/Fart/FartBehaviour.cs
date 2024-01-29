using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class FartBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject fartGameObject;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip smallFartAudio;
    [SerializeField] private AudioClip bigFartAudio;

    private readonly WaitForSeconds fartDelay = new(GameConstants.FART_DELAY);
    private Vector3 fartLocalScaleCache;
    private bool isBigFart = false;

    public void InitFart(Vector3 playerPosition, float fartAmount) 
    {
        transform.position = playerPosition;
        isBigFart = fartAmount > GameConstants.DEFAULT_FART_VALUE;
        fartLocalScaleCache = fartGameObject.transform.localScale;
        fartGameObject.transform.localScale = Vector3.zero;
        InitFartSound();
        StartCoroutine(InitFartFog());
    }

    private void InitFartSound()
    {
        audioSource.pitch = Random.Range(0.25f, 2f);
        Debug.Log(isBigFart);
        audioSource.PlayOneShot(isBigFart ? bigFartAudio : smallFartAudio);
    }

    private IEnumerator InitFartFog()
    {
        yield return fartDelay;
        var finalFartScale = isBigFart ? fartLocalScaleCache * 3 : fartLocalScaleCache;
        fartGameObject.transform.DOScale(finalFartScale, 2f);
        Destroy(gameObject, 3f);
    }
}
