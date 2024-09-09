using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class PenSizeController : MonoBehaviour
{
    // Reference to the marker object
    public GameObject marker;

    // Reference to the Slider
    public Slider penSizeSlider;

    // Reference to the component that controls the pen size on the marker
    private MarkerScript markerScript;

    void Start()
    {
        // Get the Marker script from the marker GameObject
        markerScript = marker.GetComponent<MarkerScript>();

        // Ensure the slider value matches the current pen size at start
        penSizeSlider.value = markerScript._penSize;

        // Add listener to handle slider value changes
        penSizeSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    // Method to update pen size when slider value changes
    private void OnSliderValueChanged(float value)
    {
        if (markerScript != null)
        {
            markerScript._penSize = (int) (value * 10) + 1;
            markerScript.UpdatePenSize();
            UnityEngine.Debug.Log($"Slider value: {value}, Pen size set to: {markerScript._penSize}");
        }
    }
}
