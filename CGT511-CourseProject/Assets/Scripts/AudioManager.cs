using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance;
    enum AudioIndex {
        touch = 0,
        success = 1
    }

    public AudioSource source;
    public AudioClip[] clips;

    public Vector2 pitchRange;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayTouched() {
        source.clip = clips[(int) AudioIndex.touch];
        source.pitch = Random.Range(pitchRange.x, pitchRange.y);
        source.Play();
    }

    public void PlaySuccess() {
        source.clip = clips[(int) AudioIndex.success];
        source.Play();
    }
}
