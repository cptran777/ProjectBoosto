using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {
    /**
     * Reference to the rocket thrust audio sound that is attached to the same game object as this
     * script
     */
    AudioSource audioSource;

    [SerializeField] AudioClip backgroundMusic;

    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.1f;
        audioSource.PlayOneShot(backgroundMusic);
    }

    // Update is called once per frame
    void Update() {
        if (!audioSource.isPlaying) {
            // If the music is over, start playing again
            audioSource.PlayOneShot(backgroundMusic);
        }
    }
}
