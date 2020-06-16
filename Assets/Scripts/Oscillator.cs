using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Only one of this script can happen on a component (prevents accidentally adding 2 of these onto
// a component and having crazy behavior
[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {
    [SerializeField] Vector3 movementVector = new Vector3(0, 8f, 0);
    [SerializeField] float period = 10f;

    float movementFactor;

    Vector3 startingPosition;

    // Start is called before the first frame update
    void Start() {
        // Gets the transform of the object that this script is attached to
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update() {
        // Grows continually from 0
        // Since we're using Time.time, this is automatically framerate independent
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period;

        // Number of radians it takes to get through a circle. There are 3.1415 (PI) radians in half
        // a circle
        const float tau = Mathf.PI * 2;
        // Period represents the time it takes to go through a full cycle, so that means that multiplying
        // this by tau gives us where we are on the sin wave currently
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSinWave / 2f + 0.5f;

        Vector3 offset = movementFactor * movementVector;
        transform.position = startingPosition + offset;
    }
}
