using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Boid))]
public class AvoidWallBoid : MonoBehaviour, IBoid
{

    [SerializeField]
    private Rect wall;

    private Rigidbody2D rBody;
    private Boid boid;

    [SerializeField]
    private float distance = 1;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        boid = GetComponent<Boid>().AddBehaviour(this);
    }

    public Vector2 UpdateBoid(List<Boid> allBoids)
    {
        wall = boid.WorldRect;
        if (rBody.position.x > wall.xMax - distance)
        {
            var desired = new Vector2(-boid.MaxSpeed, rBody.velocity.y);
            var steer = desired - rBody.velocity;
            return steer;
        }

        if (rBody.position.x < wall.xMin + distance)
        {
            var desired = new Vector2(boid.MaxSpeed, rBody.velocity.y);
            var steer = desired - rBody.velocity;
            return steer;
        }

        if (rBody.position.y < wall.yMin + distance)
        {
            var desired = new Vector2(rBody.velocity.x, boid.MaxSpeed);
            var steer = desired - rBody.velocity;
            return steer;
        }

        if (rBody.position.y > wall.yMax - distance)
        {
            var desired = new Vector2(rBody.velocity.x, -boid.MaxSpeed);
            var steer = desired - rBody.velocity;
            return steer;
        }

        return Vector2.zero;
    }
}
