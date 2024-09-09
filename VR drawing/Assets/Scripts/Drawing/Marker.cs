using System.Diagnostics;
using UnityEngine;

public class Marker : MonoBehaviour
{
    private Color markerColor = Color.blue; // Default color
    private Material markerMaterial; // Material used for drawing

    void Start()
    {
        // Get the MeshRenderer component from the marker object
        MeshRenderer renderer = GetComponent<MeshRenderer>();

        if (renderer != null)
        {
            // Get a unique instance of the material
            markerMaterial = renderer.material;
        }
        else
        {
            UnityEngine.Debug.LogError("No MeshRenderer found on the marker!");
        }
    }

    // Function to set marker color
    public void SetMarkerColor(Color color)
    {
        markerColor = color;

        // Update the material color
        if (markerMaterial != null)
        {
            markerMaterial.color = markerColor;
        }
        else
        {
            UnityEngine.Debug.LogError("Marker material is not assigned or found!");
        }
    }
}
