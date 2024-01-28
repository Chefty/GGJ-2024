using System.Collections;
using DG.Tweening;
using UnityEngine;

public class FartBehaviour : MonoBehaviour
{
    public bool IsFartBig = false;

    [SerializeField] private GameObject fartGameObject;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip smallFartAudio;
    [SerializeField] private AudioClip bigFartAudio;

    private Vector3 fartLocalScaleCache;
    
    private void OnEnable() {
        fartLocalScaleCache = fartGameObject.transform.localScale;
        fartGameObject.transform.localScale = Vector3.zero;
        audioSource.PlayOneShot(IsFartBig ? bigFartAudio : smallFartAudio);
        StartCoroutine(InitFartFog());
    }

    private IEnumerator InitFartFog()
    {
        yield return new WaitForSeconds(1f);
        fartGameObject.transform.DOScale(fartLocalScaleCache, .5f);
        Destroy(gameObject, 2.5f);
    }
}
