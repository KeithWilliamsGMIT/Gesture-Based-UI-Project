using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is responsible for tracking a paddle using the data from the KinectManager.
 */
public class PaddleController : MonoBehaviour {
	private const byte MULTIPLIER = 10;
	
	private Vector3 initialPostition;

	/*
	 * Store the initial position of the paddle.
	 */
	public void Awake() {
		initialPostition = transform.position;
	}

	/*
	 * Update the position of the paddle at each frame using data from the kinect.
	 */
	public void Update () {
		Vector2 pos = KinectManager.getInstance().getPlayerOne().getPosition() * MULTIPLIER;
		this.transform.position = initialPostition + new Vector3(pos.x, pos.y);
	}
}
