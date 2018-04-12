using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;
using UnityEngine;
using UnityEngine.UI;
using Assets._Scripts;

/*
 * This class is responsible for managing a game. For example, serving the ball at the start.
 */
public class GameManager : MonoBehaviour {

    private Text winText;
    private GameObject winBtn;
    [SerializeField]
	private BallController ball;
	private Vector3 ballInitialPosition;
	private PlayerEnum servingPlayer;
	private bool isStarted = false;
	[SerializeField]
	private bool isSinglePlayer;

    private List<PlayerEntity> players;

	/*
	 * Get the initial position of the ball so we can reset its position later.
	 */
	private void Start() {
		servingPlayer = PlayerEnum.PLAYER_ONE;
		ballInitialPosition = ball.transform.position;
		ball.GetComponent<Rigidbody>().useGravity = false;
		ball.enabled = false;

        winText = GameObject.Find("WinText").GetComponent<Text>();
        winBtn = GameObject.Find("WinButton");
        winBtn.SetActive(false);
        winText.enabled = false;

        players = new List<PlayerEntity>();
        PlayerEntity p1 = new PlayerEntity();
        PlayerEntity p2 = new PlayerEntity();

        //Ready player 1
        p1.setPlayerName(PlayerPrefs.GetString("Player0Name"));
        p1.setPlayerScore(0);
        players.Add(p1);
        //Add second player
        p2.setPlayerName(PlayerPrefs.GetString("Player1Name"));
        p2.setPlayerScore(0);
        players.Add(p2);



        //Set player names from saved values
        Text p1Text = GameObject.Find("Player1Text").GetComponent<Text>();
        p1Text.text = players[0].getPlayerName();
        p1Text.text += ": " + players[0].getPlayerScore();

        Text p2Text = GameObject.Find("Player2Text").GetComponent<Text>();
        p2Text.text = players[1].getPlayerName();
        p2Text.text += ": "+players[1].getPlayerScore();

        Debug.Log("New Scene! "+PlayerPrefs.GetString("Player0Name"));
        Debug.Log(PlayerPrefs.GetString("Player1Name"));

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
        

        if (!isSinglePlayer && ball.transform.position.z > 0) {
			servingPlayer = PlayerEnum.PLAYER_TWO;
            //Update player 2 score
            int score = players[1].getPlayerScore();
            players[1].setPlayerScore(score + 1);
            
            //Update player 2 score text
            Text p2Text = GameObject.Find("Player2Text").GetComponent<Text>();
            p2Text.text = players[1].getPlayerName();
            p2Text.text += ": " + players[1].getPlayerScore();
            //Check for Win condition
            if (players[1].getPlayerScore() == 15)
            {
                //Set the win button to active
                winBtn.SetActive(true);
                //Set the win text
                winText.enabled = true;
                winText.text = players[1].getPlayerName() + " Wins!";
            }
            ball.SetDirection(1);
			ball.transform.position = new Vector3(ballInitialPosition.x, ballInitialPosition.y, ballInitialPosition.z * -1);
		} else {
			ball.SetDirection(-1);
            servingPlayer = PlayerEnum.PLAYER_ONE;
            //Update player 1 score
            int score = players[0].getPlayerScore();
            players[0].setPlayerScore(score + 1);
            
            //Update player 1 score text
            Text p1Text = GameObject.Find("Player1Text").GetComponent<Text>();
            p1Text.text = players[0].getPlayerName();
            p1Text.text += ": " + players[0].getPlayerScore();

            //Check for Win condition
            if (players[0].getPlayerScore() == 15)
            {
                //Set the win button to active
                winBtn.SetActive(true);
                //Set the win text
                winText.enabled = true;
                winText.text = players[0].getPlayerName() + " Wins!";
            }

            ball.transform.position = ballInitialPosition;
		}

		ball.enabled = false;
	}
}
