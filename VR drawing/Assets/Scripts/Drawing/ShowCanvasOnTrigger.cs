using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ShowCanvasOnTrigger : MonoBehaviour
{
    public GameObject canvas; // Assign your canvas in the Inspector
    public XRNode controllerNode = XRNode.RightHand; // Choose which controller to use (RightHand/LeftHand)

    private bool canvasVisible = false;
    private bool wasTriggerPressed = false;

    void Start()
    {
        if (canvas != null)
        {
            canvas.SetActive(false); // Hide the canvas at the start
        }
    }

    void Update()
    {
        // Get the input device based on the chosen controller
        InputDevice device = InputDevices.GetDeviceAtXRNode(controllerNode);

        // Check if the trigger is pressed
        if (device.TryGetFeatureValue(CommonUsages.triggerButton, out bool isTriggerPressed))
        {
            if (isTriggerPressed && !wasTriggerPressed)
            {
                ToggleCanvas();
            }

            wasTriggerPressed = isTriggerPressed;
        }
    }

    void ToggleCanvas()
    {
        if (canvas != null)
        {
            canvasVisible = !canvasVisible; // Toggle the visibility state
            canvas.SetActive(canvasVisible); // Set the canvas active/inactive based on the new state
        }
    }
}
