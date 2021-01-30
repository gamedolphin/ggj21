using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Boid))]
public class TargetBoid : MonoBehaviour, IBoid
{
    private Rigidbody2D rBody;
    private Boid boid;

    private Vector2 target = new Vector2(10, 10);

    [SerializeField]
    private float stoppingDistance = 10;

    public void SetTarget(Vector2 _target)
    {
        target = _target;
    }

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        boid = GetComponent<Boid>();

        boid.AddBehaviour(this);
    }

    public Vector2 UpdateBoid(List<Boid> allBoids)
    {
        var desired = target - rBody.position;
        var distance = desired.magnitude;
        desired.Normalize();

        if (distance < stoppingDistance)
        {
            desired = Map(distance, 0, stoppingDistance, 0, boid.MaxSpeed) * desired;
        }
        else
        {
            desired = desired * boid.MaxSpeed;
        }

        var steerForce = desired - rBody.velocity;

        return steerForce * boid.CohesionMultiplier;
    }

    public float Map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}
