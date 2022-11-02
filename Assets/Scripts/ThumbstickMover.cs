using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ThumbstickMover : InputSystemGlobalListener, IMixedRealityInputHandler<Vector2>
{
    public MixedRealityInputAction moveAction;
    public float speed = 10f;
    Rigidbody rb;
    float xMove;
    float zMove;
    private void InputSystemGlobalListenerStart()
    {
        rb = GetComponent<Rigidbody>(); //rb equals the rigidbody on the player
    }
    public void OnInputChanged(InputEventData<Vector2> eventData)
    {
        if (eventData.MixedRealityInputAction == moveAction)
        {
            if (eventData.MixedRealityInputAction == moveAction && (Mathf.Abs(eventData.InputData.x) >= 0.5f || Mathf.Abs(eventData.InputData.y) >= 0.5f))
            {
                Vector3 temp = new Vector3(eventData.InputData.x, 0.5f, eventData.InputData.y);
                Vector3 localDelta = speed * temp;
                Debug.Log(localDelta);
                Vector3 tempPosition = transform.position + transform.GetChild(0).rotation * localDelta;
                transform.position = new Vector3(tempPosition.x, 0.5f, tempPosition.z);

            }
        }
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
