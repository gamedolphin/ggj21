using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    private Rigidbody2D rBody;

    private Vector2 target = new Vector2(10, 10);

    [SerializeField]
    private float maxSpeed = 10;

    [SerializeField]
    private float stoppingDistance = 10;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        MoveToTarget();
        SteerToVelocity();
    }

    private void MoveToTarget()
    {
        var desired = target - rBody.position;
        var distance = desired.magnitude;
        desired.Normalize();

        if (distance < stoppingDistance)
        {
            desired = Map(distance, 0, stoppingDistance, 0, maxSpeed) * desired;
        }
        else
        {
            desired = desired * maxSpeed;
        }

        var steerForce = desired - rBody.velocity;

        rBody.AddForce(steerForce);
    }


    public float Map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    private void SteerToVelocity()
    {
        var movementDirection = Vector2.Angle(rBody.velocity, Vector2.left) + Mathf.PI/2;

        rBody.SetRotation(movementDirection);
    }

    public void SetTarget(Vector2 _target)
    {
        target = _target;
    }

}
