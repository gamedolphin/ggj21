using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Boid))]
public class CohesionBoid : MonoBehaviour, IBoid
{

    private Rigidbody2D rBody;
    private Boid boid;

    [SerializeField]
    private float alignCircle = 10;

    [SerializeField]
    private LayerMask layer;

    private bool hasPath = false;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        boid = GetComponent<Boid>().AddBehaviour(this);
        hasPath = boid.Path != null;
    }

    public Vector2 UpdateBoid(List<Boid> allBoids)
    {
        if (hasPath)
        {
            return Vector2.zero;
        }

        var sum = Vector2.zero;
        var count = 0;
        foreach (var boid in allBoids)
        {
            if (boid.gameObject == gameObject) continue;
            var diff = rBody.position - boid.RBody.position;
            var distance = diff.magnitude;

            if (distance > 0 && distance < alignCircle)
            {
                sum += boid.RBody.position;
                count += 1;
            }
        }
        if (count > 0)
        {

            sum /= count;
            return TargetBoid.Steer(sum, rBody, 10, boid) * boid.CohesionMultiplier;
        }

        return Vector2.zero;
    }
}
