using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public LayerMask AttractableLayer;
    public float gravity = 10;
    
    public List<Collider2D> AttractedObjects = new List<Collider2D>();
    [HideInInspector] public Transform starTransform;
    
    void Awake()
    {
        starTransform = GetComponent<Transform>();
    }

    void Update()
    {
        SetAttractedObjects();
    }

    void FixedUpdate()
    {
        AttractObjects();
    }

    void SetAttractedObjects()
    {
        float effectRadius = starTransform.localScale.x * 10;
        AttractedObjects = Physics2D.OverlapCircleAll(starTransform.position, effectRadius, AttractableLayer).ToList();
    }

    void AttractObjects()
    {
        for (int i = 0; i < AttractedObjects.Count; i++) {
            AttractedObjects[i].GetComponent<Attractable>().Attract(this);
        }
    }

}

