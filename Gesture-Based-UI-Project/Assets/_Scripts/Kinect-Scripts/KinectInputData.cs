using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

[System.Serializable]
public class KinectInputData{

    public enum KinectUIClickGesture
    {
        HandState, WaitOver
    }
    public enum KinectUIHandType
    {
        Right, Left
    }

    //get which hand is tracked
    public KinectUIHandType trackingHand = KinectUIHandType.Right;

    private bool _isClicking; // check if hand is clicking the button

    private GameObject _hoveringObject;

    // Joint type, we need it for getting body's hand world position
    private JointType handType { }
     // Hovering Gameobject getter setter, needed for WaitOver like clicking detection
     public GameObject HoveringObject { }
     public HandState CurrentHandState { get; private set; }
    // Click gesture of button
    public KinectUIClickGesture ClickGesture { get; private set; }
    // Is this hand tracking started
    public bool IsTracking { get; private set; }
    // Is this hand over a UI component
    public bool IsHovering { get; set; }
    // Is hand pressing a button
    public bool IsPressing { get; set; }
     // Global position of tracked hand
     public Vector3 HandPosition { get; private set; }
    // Temporary hand position of hand, used for draging check
    public Vector3 TempHandPosition { get; private set; }
    // Hover start time, used for waitover type buttons
    public float HoverTime { get; set; }
    // Amout of wait over , between 1 - 0 , when reaches 1 button is clicked
    public float WaitOverAmount { get; set; }

    // Must be called for each hand
    public void UpdateComponent(Body body)
    {
        HandPosition = GetVector3FromJoint(body.Joints[handType]);
        CurrentHandState = GetStateFromJointType(body, handType);
        IsTracking = true;
    }
     // Get hand state data from kinect body
     private HandState GetStateFromJointType(Body body, JointType type) { }
    // Get Vector3 position from Joint position
     private Vector3 GetVector3FromJoint(Windows.Kinect.Joint joint) { } 
    
}
