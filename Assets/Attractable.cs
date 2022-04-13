using UnityEngine;

public class Attractable : MonoBehaviour
{
    [SerializeField] private bool rotationEnabled = true; //SerializeField lets us toggle this priv variable via the Unity editor
    [SerializeField] Attractor currentAttractor;

    Transform projectileTransform;
    Collider2D projectileCollider;
    Rigidbody2D projectileRigidbody;

    private void Awake()
    {
        projectileTransform = GetComponent<Transform>();
        projectileCollider = GetComponent<Collider2D>();
        projectileRigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        if (currentAttractor != null) {

            //check every frame to see if the attracted object is still within the Attractor's sphere of influence
            if (!currentAttractor.AttractedObjects.Contains(projectileCollider)) { 
                currentAttractor = null;
            }

            if (rotationEnabled) {
                RotateTowardsAttractor();
            }
        }
    }

    public void Attract(Attractor gravSource)
    {
        Vector2 attractionDir = (Vector2)gravSource.starTransform.position - projectileRigidbody.position;
        float distance = attractionDir.magnitude;
        Rigidbody2D starRigidbody = gravSource.GetComponent<Rigidbody2D>();
        float forceMagnitude = gravSource.gravity * projectileRigidbody.mass * starRigidbody.mass / Mathf.Pow(distance, 2);
        Vector3 force = attractionDir.normalized * forceMagnitude;
        projectileRigidbody.AddForce(force);

        if (currentAttractor == null) {
            currentAttractor = gravSource;
        }

    }

    void RotateTowardsAttractor() 
    {
        Vector2 distanceVector = (Vector2)currentAttractor.starTransform.position - (Vector2)projectileTransform.position;
        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        projectileTransform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }
}
