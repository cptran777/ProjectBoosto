using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningCoin : MonoBehaviour {
    Vector3 startingRotation;
    Vector3 rotationIncrement = Vector3.back;

    [SerializeField] float rotationSpeed = 10f;

    void Start() {
        startingRotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update() {
        Vector3 newRotation = transform.eulerAngles;
        newRotation.y = (newRotation.y + (rotationSpeed * 10 * Time.deltaTime)) % 360;
        transform.eulerAngles = newRotation;
    }

    private void OnCollisionEnter(Collision collision) {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        gameObject.SetActive(false);
    }
}
