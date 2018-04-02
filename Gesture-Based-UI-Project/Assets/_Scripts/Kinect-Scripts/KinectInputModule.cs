using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Windows.Kinect;
using static KinectInputData;

[AddComponentMenu("Kinect/Kinect Input Module")]
[RequireComponent(typeof(EventSystem))]
public class KinectInputModule : BaseInputModule
{
    // Kinect input data, we can set it's size to two for both hands
    public KinectInputData[] _inputData;
    // Scroll or drag start treshold
    [SerializeField]
    private float _scrollTreshold = .5f;
    // Drag or scroll speed for scrollView
    [SerializeField]
    private float _scrollSpeed = 3.5f;
    // Wait over time for wait over buttons
    [SerializeField]
    private float _waitOverTime = 2f;
    // Pointer event data for hand tracking
    PointerEventData _handPointerData;

    // Call this from your Kinect body view
    public void TrackBody(Body body)
    {
        for (int i = 0; i < _inputData.Length; i++)
        {
            // update kinectInput data
            _inputData[i].UpdateComponent(body);
        }
    }
    
    // get a pointer event data for a screen position
    private PointerEventData GetLookPointerEventData(Vector3 componentPosition) { }
    public override void Process()
    {
        ProcessHover();
        ProcessClick();
        ProcessWaitOver();
    }

    private void ProcessWaitOver()
    {
        throw new NotImplementedException();
    }

    private void ProcessClick()
    {
        for (int i = 0; i < _inputData.Length; i++)
        {
            //Check if we are tracking hand state not wait over
            if (!_inputData[i].IsHovering || _inputData[i].ClickGesture != KinectUIClickGesture.HandState) continue;
            // If hand state is not tracked reset properties
            if (_inputData[i].CurrentHandState == HandState.NotTracked)
            {
                _inputData[i].IsPressing = false;
            }
            // When we close hand and we are not pressing set property as pressed
            if (!_inputData[i].IsPressing && _inputData[i].CurrentHandState == HandState.Closed)
            {
                _inputData[i].IsPressing = true;
            }
            // If hand state is opened and is pressed, make click action
            else if (_inputData[i].IsPressing && (_inputData[i].CurrentHandState == HandState.Open))
            {
                PointerEventData lookData = GetLookPointerEventData(_inputData[i].GetHandScreenPosition());
                eventSystem.SetSelectedGameObject(null);
                // If there is a gameobject under cursor click it
                if (lookData.pointerCurrentRaycast.gameObject != null)
                {
                    GameObject go = lookData.pointerCurrentRaycast.gameObject;
                    ExecuteEvents.ExecuteHierarchy(go, lookData, ExecuteEvents.submitHandler);
                }
                _inputData[i].IsPressing = false;
            }
        }
    }

    private void ProcessHover()
    {
        for (int i = 0; i < _inputData.Length; i++)
        {
            PointerEventData pointer = GetLookPointerEventData(_inputData[i].GetHandScreenPosition());
            var obj = _handPointerData.pointerCurrentRaycast.gameObject;
            HandlePointerExitAndEnter(pointer, obj);
            // Hover update
            _inputData[i].IsHovering = obj != null ? true : false;
            _inputData[i].HoveringObject = obj;
        }
    }

    // Gets current kinect hand data if tracking it. Used from components like hand cursor
    public KinectInputData GetHandData(KinectUIHandType handType) { }
}
