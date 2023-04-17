using UnityEngine;

namespace BeeManager
{
    public class AIBee_BasicMovement : MonoBehaviour
    {
        public float moveSpeed = 5.0f;
        public float lerpDuration = 1.0f; // The duration to lerp between points
        public GameObject[] targetPoints; // The array of target points to move to
        public Transform targetSphere; // The transform of the target sphere
        public GameObject startLocation; // The GameObject representing the bee's start location

        private Vector3 targetPosition;

        // Lerp state variables
        private bool isLerping = false;
        private float lerpStartTime;
        private Vector3 lerpStartPosition;

        // Random movement variables
        public float randomOffsetMagnitude = 1.0f; // The magnitude of the random movement
        public float randomOffsetSpeed = 1.0f; // The speed of the random movement
        public float yOffsetMagnitude = 1.0f; // The magnitude of the random y-offset

        void Start()
        {
            // Set the bee's initial position to the position of the start location GameObject
            transform.position = startLocation.transform.position;
        }

        void Update()
        {
            // Check if we're currently lerping to a new target position
            if (isLerping)
            {
                // Calculate the current lerp time
                float t = (Time.time - lerpStartTime) / lerpDuration;

                // Calculate the random offset on the x, y and z axes
                float xOffset = Random.Range(-randomOffsetMagnitude, randomOffsetMagnitude);
                float yOffset = Random.Range(-yOffsetMagnitude, yOffsetMagnitude);
                float zOffset = Random.Range(-randomOffsetMagnitude, randomOffsetMagnitude);

                // Lerp between the start position and target position with the random offset on the x, y and z axes
                if (transform.position != targetPosition)
                {
                    Vector3 offsetPosition = new Vector3(xOffset, yOffset, zOffset);
                    Vector3 lerpedPosition = Vector3.Lerp(lerpStartPosition, targetPosition + offsetPosition, t);
                    transform.position = lerpedPosition;
                }
                else
                {
                    transform.position = targetPosition; // Set the bee's position to the target position without the offset
                }

                // Check if the lerp is complete
                if (t >= 1.0f)
                {
                    // Set the new target position and reset the lerp state
                    targetPosition = targetPoints[Random.Range(0, targetPoints.Length)].transform.position;
                    isLerping = false;
                }
            }
            else
            {
                // Move towards the target position
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                // Check if the target position has been reached
                if (transform.position == targetPosition)
                {
                    // Start lerping to the new target position
                    lerpStartTime = Time.time;
                    lerpStartPosition = transform.position;
                    isLerping = true;
                }
            }
        }
    }
}
