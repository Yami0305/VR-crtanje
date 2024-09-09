using UnityEngine;

public class MaterialHaptics : MonoBehaviour
{
    // Haptic feedback strength for this material
    public ushort hapticStrength = 1000;

    // Get the haptic feedback strength for this material
    public ushort GetHapticStrength()
    {
        return hapticStrength;
    }
}
