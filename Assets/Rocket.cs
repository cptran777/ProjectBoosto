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

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 900f;

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
        ProcessThrustInput();
        HandleRotationInput();
    }

    private void OnCollisionEnter(Collision collision) {
        print("collided");
        switch (collision.gameObject.tag) {
            case "Friendly": print("friendly collision"); break;
            default: print("Unfriendly shit"); break;
        }

        foreach (ContactPoint contact in collision.contacts) {

        }
    }

    private void HandleRotationInput() {
        // Take manual control of rotation
        rigidBody.freezeRotation = true;

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        } else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        // Resume physics control of rotation
        rigidBody.freezeRotation = false;
    }

    private void ProcessThrustInput() {
        if (Input.GetKey(KeyCode.Space)) {
            float thrustThisFrame = mainThrust * Time.deltaTime;
            rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
            if (!rocketThrustAudio.isPlaying) {
                rocketThrustAudio.Play();
            }
        } else {
            rocketThrustAudio.Stop();
        }
    }
}
