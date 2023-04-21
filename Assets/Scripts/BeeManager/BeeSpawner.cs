using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeManager
{
    public class BeeSpawner : MonoBehaviour
    {
        public GameObject beePrefab; // The prefab of the bee to spawn
        public float spawnRadius = 2.0f; // The radius around the beehive entrance where the bees should spawn
        public float spawnInterval = 5.0f; // The interval in seconds between bee spawns
        public int maxBees = 20; // The maximum number of bees to spawn
        public static GameObject gameObjectToMoveTo;
        public GameObject newGameObjectToMoveTo;
        private int numBeesSpawned = 0; // The number of bees spawned so far
        private float lastSpawnTime = 0.0f; // The time when the last bee was spawned

        void Start()
        {
            BeeSpawner.gameObjectToMoveTo = newGameObjectToMoveTo;
        }

        public void SpawnBee()
        {
            Debug.Log("BeeIsSpawned");
            // Calculate a random position within the spawn radius
            Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = transform.position + new Vector3(randomOffset.x, 0.0f, randomOffset.y);

            // Create a new instance of the bee prefab at the spawn position
            GameObject beeInstance = Instantiate(beePrefab, spawnPosition, Quaternion.identity);

            // Set the bee's rotation to 90 degrees on the X-Axis
            beeInstance.transform.rotation = Quaternion.Euler(0, 186, 0);

            // Set the bee's scale to 0.5f
            beeInstance.transform.localScale = new Vector3(0.026f, 0.026f, 0.026f);

            // Set the bee's flower points and gameObjectToMoveTo
            AIBee aiBee = beeInstance.GetComponent<AIBee>();
            if (aiBee != null)
            {
                aiBee.SetFlowerPoints(GameObject.FindGameObjectsWithTag("Flower"));
                gameObjectToMoveTo = GameObject.FindGameObjectWithTag(" BeehiveEntrance");
            }
        }
    }
}
