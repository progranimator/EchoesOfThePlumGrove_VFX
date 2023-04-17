using UnityEngine;

namespace BeeManager
{
    public class AIBee : MonoBehaviour
    {
        public GameObject[] flowerPoints; // The array of flower points to move to
        public float lerpSpeed = 2.0f; // The speed of the bee when lerping
        public float pause = 1.0f; // The time in seconds the bee stays at its lerped destination
        public float endOfLerpPause = 5.0f; // The time in seconds the bee pauses at the end of each lerp
        public GameObject boundingSphere;

        // Lerp state variables
        private bool isLerping = false;
        private float lerpStartTime;
        private Vector3 lerpStartPosition;
        private Vector3 targetPosition;
        private float pauseStartTime;
        private float endOfLerpPauseStartTime;
        private int numLocationChanges = 0;

        void Start()
        {
            // Set the bee's initial position to its starting point
            targetPosition = transform.position;
        }

        // Method to set the flower points array
        public void SetFlowerPoints(GameObject[] flowers)
        {
            flowerPoints = flowers;
        }

        // Method to set the target position for the bee
        public void SetTargetPosition(Vector3 target)
        {
            targetPosition = target;
            isLerping = true;
            lerpStartTime = Time.time;
            lerpStartPosition = transform.position;
            pauseStartTime = Time.time;
            endOfLerpPauseStartTime = 0.0f;
        }

        void Update()
        {
            // Check if we're currently lerping to a new target position
            if (isLerping)
            {
                // Calculate the current lerp time
                float distanceToTarget = Vector3.Distance(lerpStartPosition, targetPosition);
                float timeToLerp = distanceToTarget / lerpSpeed;
                float t = (Time.time - lerpStartTime) / timeToLerp;

                // Lerp between the start position and target position with a slow ease-in ease-out
                if (transform.position != targetPosition)
                {
                    Vector3 lerpedPosition =
                        Vector3.Lerp(lerpStartPosition, targetPosition, Mathf.SmoothStep(0.0f, 1.0f, t));
                    transform.position = lerpedPosition;
                }
                else
                {
                    // The lerp is complete, pause for 5 seconds
                    if (Time.time - endOfLerpPauseStartTime >= endOfLerpPause)
                    {
                        // Reset the lerp state and set a new target position
                        numLocationChanges++;
                        if (numLocationChanges >= 3)
                        {
                            numLocationChanges = 0;
                            SetTargetPosition(flowerPoints[Random.Range(0, flowerPoints.Length)].transform.position);
                        }
                        else
                        {
                            targetPosition = GetRandomPointInBounds();
                            lerpStartTime = Time.time;
                            lerpStartPosition = transform.position;
                            pauseStartTime = Time.time;
                            endOfLerpPauseStartTime = 0.0f;
                        }
                    }
                    else
                    {
                        // Still in the end-of-lerp pause
                        endOfLerpPauseStartTime = Time.time;
                    }
                }
            }
            else
            {
                // Check if the pause time has elapsed
                if (Time.time - pauseStartTime >= pause)
                {
                    // Set the new target position and start lerping to it
                    targetPosition = GetRandomPointInBounds();
                    // Set the lerp state variables
                    isLerping = true;
                    lerpStartTime = Time.time;
                    lerpStartPosition = transform.position;
                    pauseStartTime = Time.time;
                    endOfLerpPauseStartTime = 0.0f;
                }
            }
        }

// Method to get a random point inside the bounding sphere
        private Vector3 GetRandomPointInBounds()
        {
            Vector3 randomPoint = boundingSphere.transform.position +
                                  Random.insideUnitSphere * boundingSphere.transform.localScale.x / 2.0f;
            randomPoint.y = transform.position.y; // Keep the same y value as the bee
            return randomPoint;
        }
    }
}
