using UnityEngine;

public class VirtualObjectAlignment : MonoBehaviour
{
    public Transform controller;
    public Transform objectReference; // Object representing the original position of the virtual object

    void Update()
    {
        // Calculate the desired position of the virtual object relative to the controller using the reference object
        Vector3 desiredPosition = controller.TransformPoint(objectReference.localPosition);

        // Cast a ray from the controller to the desired position of the virtual object
        RaycastHit hit;
        bool collision = Physics.Raycast(controller.position, desiredPosition - controller.position, out hit);

        // If a collision occurs, move the virtual object to the point of collision
        if (collision)
        {
            transform.position = hit.point;
        }
        else
        {
            // If no collision, move the virtual object to its desired position
            transform.position = desiredPosition;
        }

        // Set the rotation of the virtual object to match the controller's rotation
        transform.rotation = controller.rotation;
    }
}
