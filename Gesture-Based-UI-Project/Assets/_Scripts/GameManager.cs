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
	private bool isStarted = false;
	
	/*
	 * If the game hasn't already started, start the game when player one makes the lasso gesture.
	 */
	private void Update () {
		if (!isStarted && KinectManager.getInstance().getPlayer(PlayerEnum.PLAYER_ONE).getState() == HandState.Lasso) {
			isStarted = true;
			ball.SimulateForce();
		}
	}
}
