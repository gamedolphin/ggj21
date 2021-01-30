using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetBoid),typeof(Boid))]
public class RandomBoid : MonoBehaviour, IBoid
{
    private Rigidbody2D rBody;
    private TargetBoid boid;

    [SerializeField]
    private float distanceForward = 2;
    [SerializeField]
    private float radius = 1;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        boid = GetComponent<TargetBoid>();

        GetComponent<Boid>().AddBehaviour(this);
    }

    public Vector2 UpdateBoid(List<Boid> allBoids)
    {

        var pointInFuture = rBody.position + rBody.velocity.normalized*distanceForward;
        var randomCircle = Random.insideUnitCircle;

        var target = pointInFuture + randomCircle*radius;

        boid.SetTarget(target);

        return Vector2.zero;
    }
}
