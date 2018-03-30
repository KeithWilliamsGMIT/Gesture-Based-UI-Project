using System.Collections;
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
	}
	
	/*
	 * If the game hasn't already started, start the game when player one makes the lasso gesture. If the ball
	 * falls below 0 on the y axis reset it's position.
	 */
	private void Update () {
		if (!isStarted && KinectManager.getInstance().getPlayer(servingPlayer).getState() == HandState.Lasso) {
			isStarted = true;
			ball.SimulateForce();
		} else if (ball.transform.position.y < 0) {
			Reset();
		}
	}

	/*
	 * Reset the game by reseting the balls position and determining whos turn it is to serve.
	 */
	private void Reset() {
		isStarted = false;
		BallController ballController = ball.GetComponent<BallController>();
		ballController.StopBall();

		if (ball.transform.position.z > 0) {
			servingPlayer = PlayerEnum.PLAYER_TWO;
			ballController.SetDirection(1);
			ball.transform.position = new Vector3(ballInitialPosition.x, ballInitialPosition.y, ballInitialPosition.z * -1);
		} else {
			ballController.SetDirection(-1);
			servingPlayer = PlayerEnum.PLAYER_ONE;
			ball.transform.position = ballInitialPosition;
		}
	}
}
