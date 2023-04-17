// Two cameras.  One based on the Main Camera and the other on a new camera that takes over.

using UnityEngine;

namespace Materials.Snow
{
    public class SnowCamera : MonoBehaviour
    {
        public Camera cam;

        void Start()
        {
            // Set the current camera's settings from the main Scene camera
            cam.CopyFrom(Camera.main);
        }
    }
}