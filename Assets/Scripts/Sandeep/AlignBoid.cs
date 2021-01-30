using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Boid))]
public class AlignBoid : MonoBehaviour, IBoid
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

        /*
          float neighbordist = 50;
          PVector sum = new PVector(0,0);
          int count = 0;
          for (Boid other : boids) {
          float d = PVector.dist(location,other.location);
          if ((d > 0) && (d < neighbordist)) {
          sum.add(other.velocity);

          For an average, we need to keep track of how many boids are within the distance.

          count++;
          }
          }
          if (count > 0) {
          sum.div(count);
          sum.normalize();
          sum.mult(maxspeed);
          PVector steer = PVector.sub(sum,velocity);
          steer.limit(maxforce);
          return steer;

          If we don’t find any close boids, the steering force is zero.

          } else {
          return new PVector(0,0);
          }
         */
        var sum = Vector2.zero;
        var count = 0;
        foreach (var boid in allBoids)
        {
            if (boid == null) continue;
            if (boid.gameObject == gameObject) continue;
            var diff = rBody.position - boid.RBody.position;
            var distance = diff.magnitude;

            if (distance > 0 && distance < alignCircle)
            {
                sum += boid.RBody.velocity;
                count += 1;
            }
        }
        if (count > 0)
        {
            sum /= allBoids.Count;
            sum = sum.normalized * boid.MaxSpeed;
            var steer = sum - rBody.velocity;

            return steer * boid.AlignmentMultiplier;
        }

        return Vector2.zero;
    }
}
