using UnityEngine;

namespace BeeManager
{
    public class BeeSwirl : MonoBehaviour
    {
        public Transform center; // the center point to swirl around
        public float swirlSpeed = 1.0f; // the speed of the swirl
        public float swirlRadius = 2.0f; // the radius of the swirl

        // Set the range for the interval for each axis
        public float xSwirlInterval = 2.0f;
        public float ySwirlInterval = 3.0f;
        public float zSwirlInterval = 4.0f;

        // Set the minimum and maximum values for the amplitude for each axis
        public float xSwirlAmplitudeMin = 0.2f;
        public float xSwirlAmplitudeMax = 0.3f;
        public float ySwirlAmplitudeMin = 0.3f;
        public float ySwirlAmplitudeMax = 0.4f;
        public float zSwirlAmplitudeMin = 0.4f;
        public float zSwirlAmplitudeMax = 0.5f;

        public float swirlAngleFluctuation = 0.1f; // the amount to fluctuate the swirl angle by

        private float currentAngleX = 0.0f; // the current angle of the bee around the center on the X-axis
        private float currentAngleY = 0.0f; // the current angle of the bee around the center on the Y-axis
        private float currentAngleZ = 0.0f; // the current angle of the bee around the center on the Z-axis
        private float xSwirlOffset = 0.0f; // the offset for the X-axis swirl
        private float ySwirlOffset = 0.0f; // the offset for the Y-axis swirl
        private float zSwirlOffset = 0.0f; // the offset for the Z-axis swirl

        // Start is called before the first frame update
        void Start()
        {
            // Set the initial offsets for each axis
            xSwirlOffset = Random.Range(0.0f, xSwirlInterval);
            ySwirlOffset = Random.Range(0.0f, ySwirlInterval);
            zSwirlOffset = Random.Range(0.0f, zSwirlInterval);
        }

        // Update is called once per frame
        void Update()
        {
            // Calculate the new position of the bee around the center on each axis
            currentAngleX += swirlSpeed * Time.deltaTime;
            currentAngleY += swirlSpeed * Time.deltaTime;
            currentAngleZ += swirlSpeed * Time.deltaTime;

            float xSwirlAmplitude = Random.Range(xSwirlAmplitudeMin, xSwirlAmplitudeMax);
            float ySwirlAmplitude = Random.Range(ySwirlAmplitudeMin, ySwirlAmplitudeMax);
            float zSwirlAmplitude = Random.Range(zSwirlAmplitudeMin, zSwirlAmplitudeMax);

            float xSwirlAngle = currentAngleX + xSwirlOffset + Random.Range(-swirlAngleFluctuation, swirlAngleFluctuation);
            float ySwirlAngle = currentAngleY + ySwirlOffset + Random.Range(-swirlAngleFluctuation, swirlAngleFluctuation);
            float zSwirlAngle = currentAngleZ + zSwirlOffset + Random.Range(-swirlAngleFluctuation, swirlAngleFluctuation);

            float x = center.position.x + Mathf.Cos(xSwirlAngle) *
                (swirlRadius + xSwirlAmplitude * Mathf.Sin(Time.time / xSwirlInterval * 2 * Mathf.PI));
            float y = center.position.y + Mathf.Cos(ySwirlAngle) *
                (swirlRadius + ySwirlAmplitude * Mathf.Sin(Time.time / ySwirlInterval * 2 * Mathf.PI));
            float z = center.position.z + Mathf.Cos(zSwirlAngle) *
                (swirlRadius + zSwirlAmplitude * Mathf.Sin(Time.time / zSwirlInterval * 2 * Mathf.PI));

            // Update the position of the bee
            transform.position = new Vector3(x, y, z);
        }
    }
}