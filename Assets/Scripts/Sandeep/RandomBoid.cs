using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Boid))]
public class RandomBoid : MonoBehaviour, IBoid
{
    private Rigidbody2D rBody;
    private Boid boid;

    [SerializeField]
    private float distanceForward = 2;
    [SerializeField]
    private float radius = 1;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        boid = GetComponent<Boid>().AddBehaviour(this);
    }

    public Vector2 UpdateBoid(List<Boid> allBoids)
    {

        var pointInFuture = rBody.position + rBody.velocity.normalized*distanceForward;
        var randomCircle = Random.insideUnitCircle;

        var target = pointInFuture + randomCircle*radius;

        return TargetBoid.Steer(target, rBody, 10, boid);
    }
}
