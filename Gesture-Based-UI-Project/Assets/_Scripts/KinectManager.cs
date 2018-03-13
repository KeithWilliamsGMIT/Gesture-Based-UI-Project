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
	private Body[] bodies = null;
	private BodyFrameReader bodyFrameReader;

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

			bodyFrameReader = sensor.BodyFrameSource.OpenReader();

            //Opening Kinect sensor at the start of the game to be able to use it.
            if (!sensor.IsOpen)
            {
                sensor.Open();
            }

			bodies = new Body[sensor.BodyFrameSource.BodyCount];
        }
	}

	/*
	 * tack player positions every frame.
	 */
	public void Update() {
		if (bodyFrameReader != null)
		{
			var frame = bodyFrameReader.AcquireLatestFrame();

			if (frame != null)
			{
				frame.GetAndRefreshBodyData(bodies);

				foreach (var body in bodies) {
					if (body.IsTracked) {
						// Tracked player...
					}
				}
			}
		}
	}

    //adding code to close Kinect sensor when exiting app
    public void OnApplicationQuit() {

        if (sensor.IsOpen)
        {
            sensor.Close();
        }
    }
}
