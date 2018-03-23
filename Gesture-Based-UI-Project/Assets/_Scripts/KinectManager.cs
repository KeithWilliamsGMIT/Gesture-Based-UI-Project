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
	private PlayerTracking p1;

	/*
	 * This method returns the instance of this object.
	 */
	public static KinectManager getInstance() {
		return instance;
	}

	/*
	 * This method returns the positional data for player one.
	 */
	public PlayerTracking getPlayerOne() {
		return p1;
	}

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
					/*
					 * If the player is being tracked calculate the position and orientation.
					 */
					if (body.IsTracked) {
						#if UNITY_EDITOR
						Debug.Log("Tracking " + body.TrackingId + " --- " +
									"Right hand state: " + body.HandRightState + " --- " +
									"Right hand confidence: " + body.HandRightConfidence);
						#endif

						var pos = body.Joints[JointType.HandRight].Position;
						Vector3 position = new Vector3(pos.X, pos.Y, pos.Z);

						var ori = body.JointOrientations[JointType.HandRight].Orientation;
						Quaternion orientation = new Quaternion(ori.X, ori.Y, ori.Z, ori.W);

						#if UNITY_EDITOR
						Debug.Log("Position: " + position);
						Debug.Log("Orientation: " + orientation);
						#endif

						p1 = new PlayerTracking(position, orientation);
					}
				}

				frame.Dispose();
				frame = null;
			}
		}
	}

    //adding code to close Kinect sensor when exiting app
    public void OnApplicationQuit() {
		if (bodyFrameReader != null) {
			bodyFrameReader.IsPaused = true;
			bodyFrameReader.Dispose();
			bodyFrameReader = null;
		}

		if (sensor != null) {
			if (sensor.IsOpen)
			{
				sensor.Close();
			}

			sensor = null;
		}
    }
}
