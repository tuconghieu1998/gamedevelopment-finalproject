using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    [SerializeField] public AudioClip[] clips;
    [SerializeField] float delayBetweenClips;

    bool canPlay;
    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        canPlay = true;
    }

    public bool isPlaying()
    {
        return source.isPlaying;
    }

    public void Play()
    {
        if (!canPlay) return;
        GameManager.Instance.Timer.Add(() =>
        {
            canPlay = true;
        }, delayBetweenClips);
        canPlay = false;
        AudioClip clip = clips[Random.Range(0, clips.Length - 1)];
        source.PlayOneShot(clip);
    }
}
