using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ColorPicker : MonoBehaviour
{
    [Header("References")]
    public XRRayInteractor rightRayInteractor; // Reference to the ray interactor
    public ActionBasedController rightController; // Reference to the Action-Based controller
    public MeshRenderer colorWheelRenderer; // Reference to the MeshRenderer of the color wheel
    public Marker markerScript; // Reference to the marker script

    void Update()
    {
        // Debug the input action value
        if (rightController != null)
        {
            float actionValue = rightController.selectAction.action.ReadValue<float>();
            UnityEngine.Debug.Log($"Action Value: {actionValue}");

            // Check if the action value exceeds the threshold
            if (actionValue > 0.1f)
            {
                UnityEngine.Debug.Log("Trigger Pressed!");

                // Perform the raycast using XRRayInteractor
                if (rightRayInteractor != null && rightRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
                {
                    UnityEngine.Debug.Log("Raycast Hit!");

                    // Check if we hit the color wheel mesh
                    if (hit.collider != null && hit.collider.gameObject == colorWheelRenderer.gameObject)
                    {
                        UnityEngine.Debug.Log("Hit Color Wheel!");

                        // Get the color from the texture at the hit point
                        Vector2 textureCoord = hit.textureCoord;
                        Color selectedColor = GetColorAtHit(textureCoord);

                        // Apply the color to the marker
                        markerScript.SetMarkerColor(selectedColor);
                    }
                }
            }
        }
    }

    Color GetColorAtHit(Vector2 uvCoords)
    {

        Texture2D texture = colorWheelRenderer.material.mainTexture as Texture2D; // Get the texture from the material
        if (texture == null)
        {
            UnityEngine.Debug.LogError("Texture is not a Texture2D.");
            return Color.blue; // Default color
        }

        // Convert UV coordinates to pixel coordinates
        Vector2 pixelCoords = new Vector2(uvCoords.x * texture.width, uvCoords.y * texture.height);
        int x = Mathf.FloorToInt(pixelCoords.x);
        int y = Mathf.FloorToInt(pixelCoords.y);

        // Clamp coordinates to avoid out-of-bounds errors
        x = Mathf.Clamp(x, 0, texture.width - 1);
        y = Mathf.Clamp(y, 0, texture.height - 1);

        // Retrieve color from the texture
        Color color = texture.GetPixel(x, y);
        UnityEngine.Debug.Log($"Color Retrieved: {color}");
        return color;
    }
}
