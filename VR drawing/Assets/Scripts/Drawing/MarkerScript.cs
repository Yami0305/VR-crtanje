using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Code base author Justin P. Barnett: https://www.youtube.com/watch?v=sHE5ubsP-E8, https://www.youtube.com/@JustinPBarnett
public class MarkerScript : MonoBehaviour
{
    [SerializeField] private Transform _tip;
    public int _penSize = 5;

    private Renderer _renderer;
    private Color _penColor; // Store the pen color
    private Color[] _colors;
    private float _tipHeight;

    private RaycastHit _touch;
    private Drawable _drawable;
    private Vector2 _touchPos;
    private bool _touchedLastFrame;
    private Vector2 _lastTouchPos;
    private Quaternion _lastTouchRot;

    void Start()
    {
        _renderer = _tip.GetComponent<Renderer>();
        _tipHeight = _tip.localScale.y;
        UpdatePenColor(); // Initialize pen color
        UpdatePenSize(); // Initialize pen size
    }

    void Update()
    {
        Draw();
    }

    private void Draw()
    {
        if (Physics.Raycast(_tip.position, transform.up, out _touch, _tipHeight))
        {
            if (_touch.transform.CompareTag("Drawable"))
            {
                if (_drawable == null)
                {
                    _drawable = _touch.transform.GetComponent<Drawable>();
                }
                UpdatePenColor();
                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                var x = (int)(_touchPos.x * _drawable.textureSize.x - (_penSize / 2));
                var y = (int)(_touchPos.y * _drawable.textureSize.y - (_penSize / 2));

                if (y < 0 || y > _drawable.textureSize.y || x < 0 || x > _drawable.textureSize.x) return;

                if (_touchedLastFrame)
                {
                    _drawable.texture.SetPixels(x, y, _penSize, _penSize, _colors);

                    // Interpolating between previous touch position and current position
                    for (float f = 0.01f; f < 1.00f; f += 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);
                        _drawable.texture.SetPixels(lerpX, lerpY, _penSize, _penSize, _colors);
                    }

                    transform.rotation = _lastTouchRot;

                    _drawable.texture.Apply();
                }

                _lastTouchPos = new Vector2(x, y);
                _lastTouchRot = transform.rotation;
                _touchedLastFrame = true;
                return;
            }
        }

        _drawable = null;
        _touchedLastFrame = false;
    }

    // Method to update pen size and recalculate colors array
    public void UpdatePenSize()
    {
        // Update the colors array based on the new pen size
        _colors = Enumerable.Repeat(_penColor, _penSize * _penSize).ToArray();
        UnityEngine.Debug.Log($"Pen size updated to: {_penSize}, Colors array length: {_colors.Length}");
    }

    // Method to update pen color
    public void UpdatePenColor()
    {
        _penColor = _renderer.material.color; // Get the current color from the marker's material
        UpdatePenSize(); // Ensure colors array is updated based on the new color
        UnityEngine.Debug.Log($"Pen color updated to: {_penColor}");
    }

    // Method to set a new color for the marker
    public void SetPenColor(Color newColor)
    {
        _renderer.material.color = newColor; // Change the marker's material color
        UpdatePenColor(); // Update pen color and recalculate colors array
    }
}
