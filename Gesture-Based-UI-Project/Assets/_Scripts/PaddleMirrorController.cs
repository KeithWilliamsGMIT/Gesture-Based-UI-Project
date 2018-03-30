using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is responsible for controlling the paddle mirror object that can only be seen by the player
 * controlling the racket.
 */
public class PaddleMirrorController : MonoBehaviour {

	private const float OPACITY = 0.2f;

	[SerializeField]
	private Transform originalPaddle;

	/*
	 * Set the opacity and rotation for this gameobject.
	 */
	public void Start () {
		Component[] renderers = gameObject.GetComponentsInChildren(typeof(Renderer));

		foreach (Renderer curRenderer in renderers)
		{
    		Color color;

        	foreach (Material material in curRenderer.materials)
        	{
            	color = material.color;
            	color.a = OPACITY;
            	material.color = color;
        	}
		}

		transform.rotation = originalPaddle.rotation;
	}
	
	/*
	 * Update the position of this gameoject to match the original gameobject.
	 */
	public void Update () {
		transform.position = Vector3.MoveTowards(transform.position, originalPaddle.position, 1000);
	}
}
