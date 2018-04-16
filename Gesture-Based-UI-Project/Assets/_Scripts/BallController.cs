using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class can be used to simulate two players hitting the ball back and over using the keyboard rather than the
 * Kinect. These controls are only available in Unity Editor for debug purposes. This script requires a rigidbody to
 * be attached to the gameobject.
 */
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]

public class BallController : MonoBehaviour {

	// This is the force to apply to the ball. This force is dampened by the rigidbodys drag property.
	[SerializeField]
	private float sideForce = 400f;
	[SerializeField]
	private float upForce = 150f;

	// The rigidbody component attached to this gameobject.
	private Rigidbody rigidbody;

	// The audiosource component attached to this gameobject.
	private AudioSource audioSource;

	// A collection of audio clips that can be played when the ball collides with the racket.
	[SerializeField]
	private AudioClip[] racketCollisionClips;

	// A collection of audio clips that can be played when the ball collides with the table.
	[SerializeField]
	private AudioClip[] tableCollisionClips;

	/*
	 * This value represents the direction in which the ball will move when the force is applied to it. The value can
	 * either be 1 (forward) or -1 (backward).
	 */
	private short direction = -1;

	// Set the direction of the ball for the next time force is applied to it, for example, when serving.
	public void SetDirection(short direction) {
		this.direction = direction;
	}

	// Use this for initialization
	private void Start () {
		#if UNITY_EDITOR
		Debug.Log("DEBUG: Press SPACE to simulate hitting the ball.");
		#endif

		// Get the rigidbody component attached to this gameobject.
		rigidbody = gameObject.GetComponent<Rigidbody>();

		// Get the audio source component attached to this gameobject.
		audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	private void Update () {
		#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.Space)) {
			Debug.Log("DEBUG: Simulated force on ball.");
			SimulateForce();
			PlayRandomClip(racketCollisionClips);
		}
		#endif
	}

	/*
	 * Detect collisions with rackets and simulate force.
	 */
	public void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag.Equals("racket"))
		{
			SimulateForce();

			PlayRandomClip(racketCollisionClips);
		} else if (collision.gameObject.tag.Equals("table")) {
			PlayRandomClip(tableCollisionClips);
		}
	}

	// Simulate hitting the ball by applying directional force.
	public void SimulateForce() {
		// Set the velocity of the ball to 0 before applying force.
		StopBall();

		// Set a random roatation on the ball.
		transform.LookAt(new Vector3(Random.Range(-2.5f, 2.5f) , 1, 4.5f * direction));

		// Apply a directional (forward/backward and up) force to the ball. The up force is applied to counteract gravity.
		rigidbody.AddForce((transform.forward * sideForce) + (Vector3.up * upForce));

		// Invert the direction for next time.
		direction *= -1;
	}

	// Stop the ball by setting the velocity and angular velocity to zero.
	public void StopBall() {
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
	}

	// Play a random sound out of a collection audio clips.
	private void PlayRandomClip(AudioClip[] clips) {
		audioSource.clip = clips[Random.Range(0, clips.Length -1)];
		audioSource.Play();
	}
}
