using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;
using UnityEngine;

/*
 * This class is responsible for managing the Kinect sensor. There should only ever be one Kinect manager, therefore
 * this class is a singleton. This class was adapted from:
 * https://andreasassetti.wordpress.com/2015/11/02/develop-a-game-using-unity3d-with-microsoft-kinect-v2/
 */
public class KinectManager : MonoBehaviour {

	private KinectSensor sensor;
	private static KinectManager instance = null;

	/*
	 * Create a new instance of this class if none already exist.
	 */
	public void Awake () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy(gameObject);
		}
	}

	/*
	 * Get a reference to the Kinect sensor.
	 */
	public void Start() {
		sensor = KinectSensor.GetDefault();

		if (sensor != null) {
			Debug.Log("Kinect sensor available");
		}
	}
}
