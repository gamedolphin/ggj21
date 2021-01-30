using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Boid))]
public class FollowPathBoid : MonoBehaviour, IBoid
{
    private Rigidbody2D rBody;
    private Boid boid;

    private BoidPath path;

    private float pathPredict = 1;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        boid = GetComponent<Boid>().AddBehaviour(this);
        path = boid.Path;
    }

    public Vector2 UpdateBoid(List<Boid> allBoids)
    {
        path = boid.Path;
        if (path == null)
        {
            return Vector2.zero;
        }

        var worldRecord = float.PositiveInfinity;
        var pos = rBody.position + rBody.velocity.normalized * pathPredict;
        Vector3 normal = Vector3.zero;
        Vector3 target = Vector3.zero;
        for (int i = 0; i < path.positions.Count; i++)
        {
            var a = path.positions[i];
            var b = path.positions[(i + 1) % path.positions.Count]; // wrap around

            var normalPoint = GetNormalPoint(pos, a, b);

            var dir = b - a;
            if (normalPoint.x < Mathf.Min(a.x, b.x) ||
                normalPoint.x > Mathf.Max(a.x, b.x) ||
                normalPoint.y < Mathf.Min(a.y, b.y) ||
                normalPoint.y > Mathf.Max(a.y, b.y))
            {
                normalPoint = b;
                a = path.positions[(i + 1) % path.positions.Count];
                b = path.positions[(i + 2) % path.positions.Count];
                dir = b - a;
            }

            var d = Vector3.Distance(pos, normalPoint);
            if (d < worldRecord)
            {
                worldRecord = d;
                normal = normalPoint;

                dir = dir.normalized * pathPredict;

                target = normal;
                target += dir;
            }

        }

        if (worldRecord > path.radius)
        {
            return TargetBoid.Steer(target, rBody, 0, boid) * boid.PathMultiplier;
        }

        return Vector2.zero;
    }

    private Vector2 GetNormalPoint(Vector2 p,Vector2 a, Vector2 b)
    {
        var ap = p - a;
        var ab = b - a;
        ab.Normalize();

        ab = ab * Vector2.Dot(ap, ab);

        return a + ab;
    }

    /*
    // A function to get the normal point from a point (p) to a line segment (a-b)
    // This function could be optimized to make fewer new Vector objects
    function getNormalPoint(p, a, b) {
    // Vector from a to p
    let ap = p5.Vector.sub(p, a);
    // Vector from a to b
    let ab = p5.Vector.sub(b, a);
    ab.normalize(); // Normalize the line
    // Project vector "diff" onto line by using the dot product
    ab.mult(ap.dot(ab));
    let normalPoint = p5.Vector.add(a, ab);
    return normalPoint;
    }
     */
}
