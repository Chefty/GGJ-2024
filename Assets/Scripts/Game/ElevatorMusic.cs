using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class ElevatorMusic : MonoBehaviour
    {
        [SerializeField] private AudioClip[] musics;
        [SerializeField] private AudioSource source;
        [SerializeField] private float volume;

        private List<AudioClip> nextMusic;
        
        private void Awake()
        {
            nextMusic = new List<AudioClip>(musics);
            source.volume = 0;
            PlayRandom();
        }

        private void PlayRandom()
        {
            var randomMusic = nextMusic[Random.Range(0, nextMusic.Count)];
            nextMusic = new List<AudioClip>(musics);
            nextMusic.Remove(randomMusic);

            source.clip = randomMusic;
            source.Play();
            source.DOFade(volume, 30f);
            source.DOFade(0f, 30f).SetDelay(randomMusic.length - 30f).OnComplete(PlayRandom);
        }
    }
}