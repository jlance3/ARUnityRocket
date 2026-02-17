using UnityEngine;

public class RocketTrajectory : MonoBehaviour
{
    public Transform source;       // Starting point of the rocket
    public Transform destination;  // Target point of the rocket
    public float arcHeight = 5f;   // Maximum height of the arc
    public float speed = 5f;       // Speed of the rocket

    private Vector3 startPos;
    private Vector3 endPos;
    private float journeyLength;
    private float startTime;

    private void Start()
    {
        if (source == null || destination == null)
        {
            //Debug.LogError("Source and Destination must be assigned!");
            return;
        }

        startPos = source.position;
        endPos = destination.position;
        journeyLength = Vector3.Distance(startPos, endPos);
        startTime = Time.time;
    }

    private void Update()
    {
        if (source == null || destination == null)
        {
            Destroy(gameObject);
            return;
        }
        // Calculate normalized time (0 to 1)
        float distanceCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distanceCovered / journeyLength;

        // Compute current position along the trajectory
        Vector3 currentPosition = CalculatePosition(fractionOfJourney);

        // Compute the forward vector for orientation
        Vector3 nextPosition = CalculatePosition(Mathf.Min(fractionOfJourney + 0.01f, 1f));
        Vector3 forward = (nextPosition - currentPosition).normalized;

        // Update the position and orientation of the rocket
        transform.position = currentPosition;
        if (forward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(forward);
        }

        // Destroy the rocket once it reaches the destination
        if (fractionOfJourney >= 1f || destination == null)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Calculates the position along the trajectory based on the given fraction of the journey.
    /// </summary>
    /// <param name="t">Normalized time (0 to 1)</param>
    /// <returns>Position along the trajectory</returns>
    private Vector3 CalculatePosition(float t)
    {
        // Interpolate horizontal position
        Vector3 interpolatedPos = Vector3.Lerp(startPos, endPos, t);

        // Add arc height
        float height = arcHeight * Mathf.Sin(Mathf.PI * t);
        interpolatedPos.y += height;

        return interpolatedPos;
    }
}