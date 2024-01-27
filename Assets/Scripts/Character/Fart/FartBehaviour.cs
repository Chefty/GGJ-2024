using System.Collections;
using UnityEngine;

public class FartBehaviour : MonoBehaviour
{
    public bool IsFartBig = false;

    [SerializeField] private ParticleSystem fartVolumetricFog;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip smallFartAudio;
    [SerializeField] private AudioClip bigFartAudio;
    

    private void OnEnable() {
        audioSource.PlayOneShot(IsFartBig ? bigFartAudio : smallFartAudio);
        StartCoroutine(InitFartFog());
    }

    private IEnumerator InitFartFog()
    {
        yield return new WaitForSeconds(1f);
        fartVolumetricFog.Play();
        Destroy(gameObject, 2f);
    }
}
