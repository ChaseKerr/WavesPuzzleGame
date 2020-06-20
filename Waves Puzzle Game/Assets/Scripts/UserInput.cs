using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    // PRIVATE
    private float tapTimeStamp = 0.0f;  // time at which the user clicked or tapped the screen (Time.time)
    private bool userHolding = false;   // true if the player is currently holding on the screen (opposed to a tap or click)

    // OBJECTS
    //[SerializeField]
    //private BlocksController blockController; // blocks controller script

    // PRIVATE INSPECTOR
    [SerializeField]
    private float minHoldTime = 0.0f;           // minimum amount of time for a click to become a hold
    
    // BASIC UNITY
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.touchCount > 0) // at least 1 touch
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                // touch started
                case TouchPhase.Began:

                    tapTimeStamp = Time.time; // record time of initial tap

                    // not inspecting block (check if one was tapped on)
                    if (!blockController.IsInspectingBlock())
                    {
                        RaycastHit hit;
                        Ray ray = Camera.main.ScreenPointToRay(touch.position);

                        if (Physics.Raycast(ray, out hit))
                        {
                            if (hit.transform.tag == "Block")
                            {
                                blockController.SetAsActiveBlock(hit.transform);
                                print(hit.transform.name); // OMIT
                            }
                        }
                    }
                    break;

                // hold / touch not moving
                case TouchPhase.Stationary:
                    
                    if (!userHolding && Time.time - tapTimeStamp >= minHoldTime)
                    {
                        userHolding = true;
                    }
                    
                    break;

                // hold / touch is moving
                case TouchPhase.Moved:
                    if (!userHolding)
                    {
                        userHolding = true;
                    }

                    // inpsecting block
                    if (blockController.IsInspectingBlock())
                    {
                        blockController.RotateInspectBlock(-touch.deltaPosition.x);
                    }
                    break;
                
                // touch ended
                case TouchPhase.Ended:

                    // user held
                    if (userHolding)
                    {
                        // user was moving block
                        if (blockController.IsMovingBlock())
                        {
                            blockController.DropBlock();
                        }
                    }
                    // user tapped
                    else
                    {
                        // user was inspecting block
                        if (blockController.IsInspectingBlock())
                        {
                            blockController.StopInspectBlock();
                        }

                        // user tapped on a block
                        else if (blockController.ABlockIsActive())
                        {
                            // inspect block
                            if (!blockController.ActiveBlockOnGrid()) // block isn't placed on grid
                            {
                                blockController.InspectBlock();
                            }
                            // rotate placed block
                            else // block is on grid
                            {
                                Vector3 rotation = new Vector3(0, 90, 0);
                                blockController.RotateBlock(rotation);
                            }
                        }
                    }
                    // reset variables
                    tapTimeStamp = 0.0f;
                    userHolding = false;

                    break;
            }
        }
        */
    }

    // PUBLIC
    public bool UserIsHolding()
    {
        return userHolding;
    }
}
