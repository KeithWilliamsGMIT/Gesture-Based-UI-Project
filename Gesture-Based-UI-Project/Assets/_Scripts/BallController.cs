using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class can be used to simulate two players hitting the ball back and over using the keyboard rather than the
 * Kinect. These controls are only available in Unity Editor for debug purposes. This script requires a rigidbody to
 * be attached to the gameobject.
 */
 [RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour {

	// This is the force to apply to the ball. This force is dampened by the rigidbodys drag property.
	private const float sideForce = 300f;
	private const float upForce = 100f;

	// The rigidbody component attached to this gameobject.
	private Rigidbody rigidbody;

	/*
	 * This value represents the direction in which the ball will move when the force is applied to it. The value can
	 * either be 1 (forward) or -1 (backward).
	 */
	private short direction = -1;

	// Use this for initialization
	private void Start () {
		#if UNITY_EDITOR
		Debug.Log("DEBUG: Press SPACE to simulate hitting the ball.");

		// Get the rigidbody component attached to this gameobject.
		rigidbody = gameObject.GetComponent<Rigidbody>();

		// Output an error if there is no rigidbody attached to the gameobject.
		if (rigidbody == null) {
			Debug.LogError("DEBUG: No rigidbody attached to gameobject.");
		}
		#endif
	}
	
	// Update is called once per frame
	private void Update () {
		#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.Space)) {
			Debug.Log("DEBUG: Simulated force on ball.");
			SimulateForce();
		}
		#endif
	}

	// Simulate hitting the ball by applying directional force.
	public void SimulateForce() {
		// Set the velocity of the ball to 0 before applying force.
		rigidbody.velocity = Vector3.zero;

		// Apply a directional (forward/backward and up) force to the ball. The up force is applied to counteract gravity.
		rigidbody.AddForce((Vector3.forward * sideForce * direction) + (Vector3.up * upForce));

		// Invert the direction for next time.
		direction *= -1;
	}
}
