using UnityEngine;

namespace BeeManager
{
    public class BeeHive : MonoBehaviour
    {
        public GameObject[] bees; // The array of bees to control
        public GameObject[] flowerPoints; // The array of flower points to move to
        public float hiveRotationSpeed = 1.0f; // The speed of the hive rotation
        public float timeUntilNextScript = 5.0f;
        public float timeElapsed = 0.0f;

        void Start()
        {
            // Set the flower points for each bee
            foreach (GameObject bee in bees)
            {
                if (bee != null) // Null check for bee object
                {
                    AIBee aiBee = bee.GetComponent<AIBee>();
                    if (aiBee != null) // Null check for AIBee component
                    {
                        aiBee.SetFlowerPoints(flowerPoints);
                    }
                }
            }
        }

        void Update()
        {
            // Set a random target position for each bee
            foreach (GameObject bee in bees)
            {
                if (bee != null) // Null check for bee object
                {
                    AIBee aiBee = bee.GetComponent<AIBee>();
                    if (aiBee != null) // Null check for AIBee component
                    {
                        Vector3 targetPosition = flowerPoints[Random.Range(0, flowerPoints.Length)].transform.position;
                        aiBee.SetTargetPosition(targetPosition);
                    }
                }
            }
        }
    }
}


