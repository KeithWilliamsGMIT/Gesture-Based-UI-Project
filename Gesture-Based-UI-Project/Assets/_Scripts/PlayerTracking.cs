using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is responsible for representing data from the the Kinect.
 */
public class PlayerTracking : MonoBehaviour {
	private Vector3 position;
	private Quaternion orientation;

	/*
	 * This is the default constructor.
	 */
	public PlayerTracking() {
		this.position = new Vector3();
		this.orientation = new Quaternion();
	}


	/*
	 * This constructor creates a new instance of this class by specifying the position and orientation.
	 */
	public PlayerTracking(Vector3 position, Quaternion orientation) {
		this.position = position;
		this.orientation = orientation;
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
}
