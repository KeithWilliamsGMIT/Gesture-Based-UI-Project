﻿using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;
using UnityEngine;

/*
 * This class is responsible for managing a game. For example, serving the ball at the start.
 */
public class GameManager : MonoBehaviour {

	[SerializeField]
	private BallController ball;
	private Vector3 ballInitialPosition;
	private PlayerEnum servingPlayer;
	private bool isStarted = false;

	/*
	 * Get the initial position of the ball so we can reset its position later.
	 */
	private void Start() {
		servingPlayer = PlayerEnum.PLAYER_ONE;
		ballInitialPosition = ball.transform.position;
		ball.GetComponent<Rigidbody>().useGravity = false;
		ball.enabled = false;
		
		#if UNITY_EDITOR
		Debug.Log("DEBUG: Press SPACE to serve the ball.");
		#endif
	}
	
	/*
	 * If the game hasn't already started, start the game when player one makes the lasso gesture. If the ball
	 * falls below 0 on the y axis reset it's position.
	 */
	private void Update () {
		#if UNITY_EDITOR
		if (!isStarted && Input.GetKeyDown(KeyCode.Space)) {
			Debug.Log("DEBUG: Served using SPACE.");
			Serve();
		}
		#endif

		if (!isStarted && KinectManager.getInstance().getPlayer(servingPlayer).getState() == HandState.Lasso) {
			Serve();
		} else if (ball.transform.position.y < 0) {
			Reset();
		}
	}

	/*
	 * Serve the ball. by simulating a force on the ball.
	 */
	private void Serve() {
		isStarted = true;
		ball.enabled = true;
		ball.GetComponent<Rigidbody>().useGravity = true;
		ball.SimulateForce();
	}

	/*
	 * Reset the game by reseting the balls position and determining whos turn it is to serve.
	 */
	private void Reset() {
		isStarted = false;
		ball.GetComponent<Rigidbody>().useGravity = false;
		ball.StopBall();

		if (ball.transform.position.z > 0) {
			servingPlayer = PlayerEnum.PLAYER_TWO;
			ball.SetDirection(1);
			ball.transform.position = new Vector3(ballInitialPosition.x, ballInitialPosition.y, ballInitialPosition.z * -1);
		} else {
			ball.SetDirection(-1);
			servingPlayer = PlayerEnum.PLAYER_ONE;
			ball.transform.position = ballInitialPosition;
		}

		ball.enabled = false;
	}
}
