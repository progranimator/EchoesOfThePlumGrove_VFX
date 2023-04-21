using System.Collections;
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
        public int flowersVisited = 0;
        public GameObject newBee;
        public float Speed_returnToHive = 1.0f;
        public BeeSpawner beeSpawner;
        public BoxCollider beeCollider;
        private GameObject newGameObjectToMoveTo;
        
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
            // Find the BeeSpawner script
            beeSpawner = FindObjectOfType<BeeSpawner>();
            //BeeSpawner.gameObjectToMoveTo = newGameObjectToMoveTo;
            // Assign a value to the beeCollider variable
            beeCollider = GetComponent<BoxCollider>();
        }

        private void ShuffleArray<T>(T[] array)
        {
            // Loop through the array
            for (int i = 0; i < array.Length; i++)
            {
                // Generate a random index within the range of valid indices for the array
                int randomIndex = Random.Range(i, array.Length);

                // Swap the current element with the element at the random index
                T temp = array[i];
                array[i] = array[randomIndex];
                array[randomIndex] = temp;
            }
        }

        // Method to set the flower points array
        public void SetFlowerPoints(GameObject[] flowers)
        {
            flowerPoints = flowers;
            ShuffleArray(flowerPoints);
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

            // Choose a random flower point
            int numFlowerPoints = flowerPoints.Length;
            int randomIndex = Random.Range(0, numFlowerPoints);
            newGameObjectToMoveTo = flowerPoints[randomIndex];
        }

        // The index of the current flower point in the array
        private int newGameObjectMoveTo = 0;

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

                        if (numLocationChanges >= flowerPoints.Length)
                        {
                            numLocationChanges = 0;
                        }

                        newGameObjectMoveTo = numLocationChanges;

                        // Check if we've visited 4 flowers
                        if (flowersVisited == 4)
                        {
                            // Check if we've reached the gameObjectToMoveTo
                            

                            targetPosition = BeeSpawner.gameObjectToMoveTo.transform.position;
                            isLerping = true;
                            lerpStartTime = Time.time;
                            lerpStartPosition = transform.position;
                            pauseStartTime = Time.time;
                            endOfLerpPauseStartTime = 0.0f;
                        }
                        else
                        {
                            // Move to the next flower point
                            // Get the length of the flowerPoints array
                            int numFlowerPoints = flowerPoints.Length;

                            // Generate a random index within the range of valid indices for the array
                            int randomIndex = Random.Range(0, numFlowerPoints);

                            // Get the position of the randomly selected flower point
                            targetPosition = flowerPoints[randomIndex].transform.position;

                            lerpStartTime = Time.time;
                            lerpStartPosition = transform.position;
                            pauseStartTime = Time.time;
                            endOfLerpPauseStartTime = 0.0f;
                            flowersVisited++;
                        }
                    }
                    else
                    {
                        // Pause for 5 seconds at the end of the lerp
                        endOfLerpPauseStartTime = Time.time;
                    }
                }
            }
            else
            {
                // The bee is not currently lerping, pause for 1 second at the flower point
                if (Time.time - pauseStartTime >= pause)
                {
                    // Set a new target position
                    numLocationChanges++;

                    if (numLocationChanges >= flowerPoints.Length)
                    {
                        numLocationChanges = 0;
                    }

                    newGameObjectMoveTo = numLocationChanges;
                    targetPosition = flowerPoints[newGameObjectMoveTo].transform.position;
                    isLerping = true;
                    lerpStartTime = Time.time;
                    lerpStartPosition = transform.position;
                    pauseStartTime = Time.time;
                    endOfLerpPauseStartTime = 0.0f;
                }
            }
        }
        
        private IEnumerator DisableBoxCollider(BoxCollider collider)
        {
            collider.enabled = false;
            yield return new WaitForSeconds(5.0f);
            collider.enabled = true;
        }
        
        void OnTriggerEnter(Collider other)
        {
            // Check if the collider is a flower
            if (other.CompareTag(" BeehiveEntrance"))
            {
                Debug.Log("Bee entered beehive entrance.");
                beeSpawner.SpawnBee();
                Destroy(gameObject);
                //StartCoroutine(DestroyAndSpawnBee());
            }
        }

        IEnumerator DestroyAndSpawnBee()
        {
            
            // Hide the mesh renderer
            //GetComponent<MeshRenderer>().enabled = false;
            //StartCoroutine(DisableBoxCollider(beeCollider));
            // Wait for 5 seconds before spawning a new bee
            //yield return new WaitForSeconds(1);
            Destroy(gameObject);
            return(null);
            /*
            // Spawn a new bee
            newBee = Instantiate(gameObject, transform.position, Quaternion.identity);

            // Set the flower points for the new bee
            newBee.GetComponent<AIBee>().SetFlowerPoints(flowerPoints);

            // Reset the flowers visited count for the new bee
            newBee.GetComponent<AIBee>().flowersVisited = 0;

            // Show the mesh renderer for the new bee
            newBee.GetComponent<MeshRenderer>().enabled = true;

            // Set the target position for the new bee
            newBee.GetComponent<AIBee>().SetTargetPosition(targetPosition);
            */
        }


    }
}
                            