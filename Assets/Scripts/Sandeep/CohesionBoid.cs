using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetBoid),typeof(Boid))]
public class CohesionBoid : MonoBehaviour, IBoid
{

    private Rigidbody2D rBody;
    private TargetBoid boid;

    [SerializeField]
    private float alignCircle = 10;

    [SerializeField]
    private LayerMask layer;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        boid = GetComponent<TargetBoid>();
        GetComponent<Boid>().AddBehaviour(this);
    }

    public Vector2 UpdateBoid(List<Boid> allBoids)
    {
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
            boid.SetTarget(sum);
        }

        return Vector2.zero;
    }
}
