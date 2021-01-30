using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Boid))]
public class AvoidBoid : MonoBehaviour, IBoid
{

    private Rigidbody2D rBody;
    private Boid boid;

    [SerializeField]
    private float avoidCircle = 10;

    [SerializeField]
    private LayerMask layer;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        boid = GetComponent<Boid>().AddBehaviour(this);
    }

    public Vector2 UpdateBoid(List<Boid> allBoids)
    {
        var otherBoids = Physics2D.OverlapCircleAll(rBody.position, avoidCircle, layer);
        var count = 0;
        var sum = Vector2.zero;
        var steer = Vector2.zero;
        foreach(var boid in otherBoids)
        {
            if (boid.gameObject == gameObject) continue;

            var diff = transform.position - boid.transform.position;
            var distance = diff.magnitude;

            if (distance > 0)
            {
                diff = diff.normalized/distance;
                sum += new Vector2(diff.x, diff.y);
                count++;
            }
        }

        if (count>0)
        {
            sum /= count;
            sum = sum.normalized * boid.MaxSpeed;

            steer = sum - rBody.velocity;
        }

        return steer * boid.SeparationMultiplier;
    }
}
