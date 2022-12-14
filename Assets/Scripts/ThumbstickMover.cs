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
        transform.position = new Vector3(transform.position.x, 10f, transform.position.z);
    }

    
    public void OnInputChanged(InputEventData<Vector2> eventData)
    {
        if (eventData.MixedRealityInputAction == moveAction)
        {
            if (eventData.MixedRealityInputAction == moveAction && (Mathf.Abs(eventData.InputData.x) >= 0.5f || Mathf.Abs(eventData.InputData.y) >= 0.5f))
            {
                Vector3 temp = new Vector3(eventData.InputData.x, 0f, eventData.InputData.y);
                Vector3 localDelta = speed * temp;
                Vector3 tempPosition = transform.position + transform.GetChild(0).rotation * localDelta;
                transform.position = new Vector3(tempPosition.x, 10f, tempPosition.z);

                /*                if (transform.position.x > -10f)
                                {
                                    transform.position = new Vector3(tempPosition.x, 0.1f, tempPosition.z);
                                }
                                else
                                {
                                    transform.position = new Vector3(tempPosition.x + 0.1f, 0.1f, tempPosition.z);
                                }*/

            }
        }
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
