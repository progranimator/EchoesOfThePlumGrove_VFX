using UnityEditor;
using UnityEngine;

public class TimeOfDay : EditorWindow
{
    private Light light;
    private float lightRotation;

    void OnEnable()
    {
        // Find the first light in the scene
        light = FindObjectOfType<Light>();
    }

    void OnGUI()
    {
        // Add a slider to the window to control the light rotation
        lightRotation = EditorGUILayout.Slider("Light Rotation", lightRotation, 0f, 360f);

        // Update the light rotation based on the slider value
        light.transform.rotation = Quaternion.Euler(lightRotation, 0f, 0f);
    }

    [MenuItem("Window/TimeOfDay")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(TimeOfDay));
    }
}