using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ButterflyManager
{
    public class ButterflyGarden : MonoBehaviour
    {
        public List<GameObject> butterflies; // list of butterflies to manage
        public List<GameObject> targetObjects; // list of objects to move towards when a butterfly is selected
        public float selectionInterval = 10f; // how often to select a random butterfly (in seconds)
        public float moveSpeed = 5f; // the speed at which butterflies move towards the target location
        public float pauseDuration = 5f; // how long to pause at the target location (in seconds)

        private GameObject selectedButterfly; // the currently selected butterfly
        private GameObject selectedTarget; // the target object that the selected butterfly is moving towards
        private bool isMoving = false; // whether the selected butterfly is currently moving towards the target object
        private Vector3 initialPosition; // the starting position of the selected butterfly
        private float selectionTimer = 0f; // a timer used to keep track of when to select a new butterfly

        void Start()
        {
            // set the initial selection timer to a random value within the selection interval
            selectionTimer = Random.Range(0f, selectionInterval);
        }

        void Update()
        {
            // update the selection timer
            selectionTimer += Time.deltaTime;

            // check if it's time to select a new butterfly
            if (selectionTimer >= selectionInterval)
            {
                // select a random butterfly from the list
                int index = Random.Range(0, butterflies.Count);
                selectedButterfly = butterflies[index];
                isMoving = true;
                initialPosition = selectedButterfly.transform.position;

                // select a random target object from the list
                int targetIndex = Random.Range(0, targetObjects.Count);
                selectedTarget = targetObjects[targetIndex];

                // reset the selection timer
                selectionTimer = 0f;
            }

            // move the selected butterfly towards the selected target object
            if (isMoving)
            {
                selectedButterfly.transform.position = Vector3.MoveTowards(selectedButterfly.transform.position, selectedTarget.transform.position, moveSpeed * Time.deltaTime);

                // check if the selected butterfly has reached the selected target object
                if (selectedButterfly.transform.position == selectedTarget.transform.position)
                {
                    isMoving = false;
                    StartCoroutine(PauseAndMove());
                }
            }
        }

        IEnumerator PauseAndMove()
        {
            // pause for the specified duration
            yield return new WaitForSeconds(pauseDuration);

            // reset the position of the selected butterfly
            selectedButterfly.transform.position = initialPosition;

            // move the butterfly again
            isMoving = true;
        }
    }
}
