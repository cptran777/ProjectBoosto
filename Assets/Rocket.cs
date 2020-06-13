using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
    /**
     * Reference to the rigidbody object that is attached to the same game object as this script
     */
    Rigidbody rigidBody;
    /**
     * Reference to the rocket thrust audio sound that is attached to the same game object as this
     * script
     */
    AudioSource rocketThrustAudio;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        rocketThrustAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        ProcessInput();
    }

    private void ProcessInput() {
        if (Input.GetKey(KeyCode.Space)) {
            rigidBody.AddRelativeForce(Vector3.up);
            if (!rocketThrustAudio.isPlaying) {
                rocketThrustAudio.Play();
            }
        } else {
            rocketThrustAudio.Stop();
        }
        
        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward);
        } else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(-Vector3.forward);
        }
    }
}
