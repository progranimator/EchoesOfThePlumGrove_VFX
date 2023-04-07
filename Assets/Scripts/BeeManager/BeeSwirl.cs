using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        float x = center.position.x + Mathf.Cos(currentAngleX + xSwirlOffset) * (swirlRadius + xSwirlAmplitude);
        float y = center.position.y + Mathf.Sin(currentAngleY + ySwirlOffset) * (swirlRadius + ySwirlAmplitude);
        float z = center.position.z + Mathf.Cos(currentAngleZ + zSwirlOffset) * (swirlRadius + zSwirlAmplitude);

        // Set the new position of the bee
        transform.position = new Vector3(x, y, z);
    }
}
