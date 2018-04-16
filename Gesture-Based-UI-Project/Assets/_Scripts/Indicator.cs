using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is responsible for controlling an indicator to inform the player where an off screen racket
 * is located.
 */
public class Indicator : MonoBehaviour {

	[SerializeField]
	private Transform target;

	[SerializeField]
	private Camera camera;

	private Renderer renderer;

	/*
	 * Get a reference to the renderer component for later.
	 */
	public void Awake() {
		this.renderer = gameObject.GetComponent<Renderer>();
	}

	/*
	 * Display this gameobject at the edge of the screen when the target is off screen to indicate what
	 * direction the target is in.
	 */
	public void Update() {
		// Move Towards the target
		transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 1000);

		// Clamp to the screen view
		Vector3 pos = camera.WorldToViewportPoint(transform.position);

		if (pos.x < 0 || pos.x > 1 || pos.y < 0 || pos.y > 1) {
			this.renderer.enabled = true;
			pos.x = Mathf.Clamp01(pos.x);
			pos.y = Mathf.Clamp01(pos.y);
			transform.position = camera.ViewportToWorldPoint(pos);
		} else {
			this.renderer.enabled = false;
		}
	}
}
