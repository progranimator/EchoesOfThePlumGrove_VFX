using System.Collections;
using UnityEngine;

namespace ButterflyManager
{
    public class ButterflyManager : MonoBehaviour
    {
        public float speed = 2.0f; // speed of movement
        public float height = 2.0f; // height of the triangle
        public float width = 4.0f; // width of the triangle
        public float rotationSpeed = 5.0f; // rotation speed in degrees per second
        public float rotationAmount = 5.0f;
        public float spawnTimer = 5.0f;
        public float RotationTimer = 5.0f;
        public float TimeMultiplier;
        public string TagName;
        public float negativeX;
        public float positiveX;
        public float negativeY;
        public float positiveY;
        public float negativeZ;
        public float positiveZ;

        private Vector3 startPosition; // starting position of the object

        void Start()
        {
            startPosition = transform.position;
            StartCoroutine(DestroyAfterTime());
        }

        void Update()
        {
            float time = Time.time * speed; // get the current time
            float x = Mathf.PingPong(time, width); // get the position along the x-axis
            float z = Mathf.Sin(time * Mathf.PI * 2.0f / width) * height; // get the position along the z-axis
    
            // calculate the next position
            Vector3 nextPosition = startPosition + new Vector3(x, 0, z);

            // lerp to the next position
            transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * speed);

            // check if it's time to switch axes
            if (Time.time % 2.0f < 0.01f)
            {
                // switch the width and height
                float temp = width;
                width = height;
                height = temp;
            }

            // determine movement direction
            Vector3 movementDirection = nextPosition - transform.position;

            // rotate bee based on movement direction
            if (movementDirection.x > 0)
            {
                transform.Rotate(Vector3.up, rotationSpeed * rotationAmount * positiveX);
            }
            else if (movementDirection.x < 0)
            {
                transform.Rotate(Vector3.up, rotationSpeed * rotationAmount * negativeX);
            }
            if (movementDirection.y > 0)
            {
                transform.Rotate(Vector3.right, rotationSpeed * rotationAmount * positiveY);
            }
            else if (movementDirection.y < 0)
            {
                transform.Rotate(Vector3.right, rotationSpeed * rotationAmount * negativeY);
            }
            if (movementDirection.z > 0)
            {
                transform.Rotate(Vector3.forward, rotationSpeed * rotationAmount * positiveZ);
            }
            else if (movementDirection.z < 0)
            {
                transform.Rotate(Vector3.forward, rotationSpeed * rotationAmount * negativeZ);
            }
        }

        IEnumerator DestroyAfterTime()
        {
            yield return new WaitForSeconds(spawnTimer);
            Destroy(gameObject);
        }
    }
}
