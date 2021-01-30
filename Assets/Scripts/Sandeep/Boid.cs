using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoid {
    public Vector2 UpdateBoid(List<Boid> allBoids);
}

public class Boid : MonoBehaviour
{
    private Rigidbody2D rBody;

    [SerializeField]
    private float maxSpeed = 10;

    public float MaxSpeed {
        get
        {
            return maxSpeed;
        }
    }

    public Rigidbody2D RBody {
        get
        {
            return rBody;
        }
    }

    public float SeparationMultiplier = 1.5f;
    public float AlignmentMultiplier = 1.0f;
    public float CohesionMultiplier = 1.0f;
    public float PathMultiplier = 4.0f;

    public Rect WorldRect = new Rect(0,0,20,20);

    private List<IBoid> otherBehaviours = new List<IBoid>();
    private System.Action<Boid> onDestroy;

    public bool IsBad = false;

    public BoidPath Path = null;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    public void Initialize(System.Action<Boid> onDes)
    {
        onDestroy = onDes;
    }

    public Boid AddBehaviour(IBoid behaviour)
    {
        otherBehaviours.Add(behaviour);
        return this;
    }

    public void UpdateBoid(List<Boid> allBoids)
    {
        var force = Vector2.zero;
        foreach(var boid in otherBehaviours) {
            force += boid.UpdateBoid(allBoids);
        }
        rBody.AddForce(force);

        SteerToVelocity();
    }

    private void SteerToVelocity()
    {
         // var rotation = Quaternion.LookRotation(rBody.velocity);
         // rBody.MoveRotation(rotation);
    }

    private void OnDestroy()
    {
        onDestroy?.Invoke(this);
    }
}
