using UnityEngine;

public static class TargetBoid
{
    public static Vector2 Steer(Vector2 target, Rigidbody2D rBody, float stoppingDistance, Boid boid)
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

        return steerForce;
    }

    public static float Map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}
