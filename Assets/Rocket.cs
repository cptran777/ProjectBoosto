using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {
    /**
     * Reference to the rigidbody object that is attached to the same game object as this script
     */
    Rigidbody rigidBody;
    /**
     * Reference to the rocket thrust audio sound that is attached to the same game object as this
     * script
     */
    AudioSource audioSource;

    enum PlayerState { Alive, Dying, Transcendance };
    PlayerState playerState = PlayerState.Alive;

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 900f;
    [SerializeField] float endSceneDelay = 2f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip victory;
    [SerializeField] AudioClip coin;
    [SerializeField] GameObject coinSounder;

    [SerializeField] ParticleSystem mainEngineExhaust;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        ProcessInput();
    }

    private void ProcessInput() {
        if (playerState == PlayerState.Alive) {
            ProcessThrustInput();
            HandleRotationInput();
        }
    }

    private void OnCollisionEnter(Collision collision) {
        print("collided");
        if (playerState != PlayerState.Alive) { return; }

        // We only care about collisions while the player is considered alive. Any further collisions
        // after death or victory should be ignored
        switch (collision.gameObject.tag) {
            case "Friendly": print("friendly collision"); break;
            case "Finish":
                GameObject[] remainingCoins = GameObject.FindGameObjectsWithTag("Coin");
                if (remainingCoins.Length == 0) {
                    print("Finished");
                    HandleLevelEnd(PlayerState.Transcendance);
                    audioSource.PlayOneShot(victory);
                    successParticles.Play();
                    Invoke("LoadNextScene", endSceneDelay);
                }
                break;
            default:
                print("Unfriendly shit");
                HandleLevelEnd(PlayerState.Dying);
                audioSource.PlayOneShot(death);
                deathParticles.Play();
                Invoke("LoadNextScene", endSceneDelay);
                break;
        }


        //foreach (ContactPoint contact in collision.contacts) {

        //}
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Coin") {
            print("triggered enter");
            GameObject.Find("Coin Sounder").GetComponent<CoinSounder>().triggerCoinSound();
        }
    }

    private void LoadNextScene() {
        if (playerState == PlayerState.Transcendance) {
            int newSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            newSceneIndex = newSceneIndex <= SceneManager.sceneCountInBuildSettings ? newSceneIndex : 0;
            SceneManager.LoadScene(newSceneIndex);
        } else if (playerState == PlayerState.Dying) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void HandleLevelEnd(PlayerState newState) {
        playerState = newState;
        audioSource.Stop();
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
            if (!audioSource.isPlaying) {
                audioSource.PlayOneShot(mainEngine);
            }
            if (!mainEngineExhaust.isPlaying) {
                mainEngineExhaust.Play();
            }
        } else {
            audioSource.Stop();
            mainEngineExhaust.Stop();
        }
    }
}
