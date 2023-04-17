using UnityEngine;

namespace BeeManager
{
    public class BeeWave : MonoBehaviour
    {
        public float amplitude = 1f; // The amplitude of the up and down movement
        public float frequency = 1f; // The frequency of the up and down movement

        private Vector3 startPos; // The starting position of the bee

        // Start is called before the first frame update
        void Start()
        {
            startPos = transform.position; // Store the starting position of the bee
        }

        // Update is called once per frame
        void Update()
        {
            // Calculate the new Y position based on a sine wave*excuse my 
            float newY = startPos.y + amplitude * Mathf.Sin(Time.time * frequency);

            // Update the bee's position
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}