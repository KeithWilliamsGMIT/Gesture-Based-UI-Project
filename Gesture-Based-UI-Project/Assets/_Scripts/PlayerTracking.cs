using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;
using UnityEngine;

/*
 * This class is responsible for representing data from the the Kinect.
 */
public class PlayerTracking : MonoBehaviour {
	private Vector3 position;
	private Quaternion orientation;
	private HandState state;

	/*
	 * This is the default constructor.
	 */
	public PlayerTracking() {
		this.position = new Vector3();
		this.orientation = new Quaternion();
		this.state = HandState.Unknown;
	}


	/*
	 * This constructor creates a new instance of this class by specifying the position, orientation and hand state.
	 */
	public PlayerTracking(Vector3 position, Quaternion orientation, HandState state) {
		this.position = position;
		this.orientation = orientation;
		this.state = state;
	}

	/*
	 * This method returns the stored position.
	 */
	public Vector3 getPosition() {
		return position;
	}

	/*
	 * This method returns the stored orientation.
	 */
	public Quaternion getOrientation() {
		return orientation;
	}

	/*
	 * This method returns the stored state.
	 */
	public HandState getState() {
		return state;
	}
}
