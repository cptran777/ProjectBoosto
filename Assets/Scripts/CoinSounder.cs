using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSounder : MonoBehaviour {
    AudioSource audioSource;
    [SerializeField] AudioClip coin;

    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void triggerCoinSound() {
        audioSource.PlayOneShot(coin);
    }
}
