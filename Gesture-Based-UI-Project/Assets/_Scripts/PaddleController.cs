using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is responsible for tracking a paddle using the data from the KinectManager.
 */
public class PaddleController : MonoBehaviour {
	private const byte MULTIPLIER = 10;

	[SerializeField]
    private PlayerEnum player;

	private float smoothTime = 0.3f;
	
	private Vector3 initialPostition;

	private Vector3 velocity = Vector3.zero;

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
		Vector2 pos = KinectManager.getInstance().getPlayer(player).getPosition();

		// Split the screen and flip the x-axis for player one.
		if (player == PlayerEnum.PLAYER_ONE) {
			pos.x = pos.x + 0.5f;
			pos.x = pos.x * -1;
		} else {
			pos.x = pos.x - 0.5f;
		}

		pos = pos * MULTIPLIER;
		this.transform.position =  Vector3.SmoothDamp(transform.position, initialPostition + new Vector3(pos.x, pos.y), ref velocity, smoothTime);
	}
}
