using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public interface IBoid
{
    public Vector2 UpdateBoid(List<Boid> allBoids);
}

public class Boid : MonoBehaviour
{
    private Rigidbody2D rBody;

    [SerializeField]
    private float maxSpeed = 10;

    [SerializeField]
    private SpriteRenderer deadBoidPrefab;

    public float MaxSpeed
    {
        get
        {
            return maxSpeed;
        }

        set
        {
            maxSpeed = value;
        }
    }

    public Rigidbody2D RBody
    {
        get
        {
            return rBody;
        }
    }

    public float SeparationMultiplier = 1.5f;
    public float AlignmentMultiplier = 1.0f;
    public float CohesionMultiplier = 1.0f;
    public float PathMultiplier = 4.0f;

    public float AvoidCircle = 10;

    public float TargetTimeout = 5.0f;

    public Rect WorldRect = new Rect(0, 0, 20, 20);

    private List<IBoid> otherBehaviours = new List<IBoid>();
    private System.Action<Boid> onDestroy;

    public bool IsBad = false;

    public BoidPath Path = null;

    public float AlignCircle = 2.0f;

    public bool IsInverted = false;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    public void Initialize(System.Action<Boid> onDes)
    {
        onDestroy = onDes;
        rBody.velocity = new Vector2(Random.Range(0, MaxSpeed/2), Random.Range(0, MaxSpeed/2));

        var destroy = GetComponent<DestroyByBullet>();
        if (destroy != null)
        {
            destroy.SetDestroyBullet(IsInverted);
        }
    }

    public Boid AddBehaviour(IBoid behaviour)
    {
        otherBehaviours.Add(behaviour);
        return this;
    }

    public void UpdateBoid(List<Boid> allBoids)
    {
        var force = Vector2.zero;
        foreach (var boid in otherBehaviours)
        {
            force += boid.UpdateBoid(allBoids);
        }
        rBody.AddForce(force);

        SteerToVelocity();
    }

    private void SteerToVelocity()
    {
        Vector2 v = rBody.velocity;
        var angle = -Mathf.Atan2(v.x, v.y) * Mathf.Rad2Deg + Mathf.PI / 2;
        rBody.SetRotation(Mathf.LerpAngle(rBody.rotation, angle, 0.1f));
    }

    private void OnDestroy()
    {
        onDestroy?.Invoke(this);

        var deadBoid = Instantiate(deadBoidPrefab);

        deadBoid.transform.position = rBody.position;
        deadBoid.transform.rotation = transform.rotation;
        var seq = DOTween.Sequence();
        seq.Append(deadBoid.transform.DOMove(rBody.position + Random.insideUnitCircle * 4, 2f));
        seq.Append(deadBoid.DOFade(0, 1f));
        seq.OnComplete(() => GameObject.Destroy(deadBoid.gameObject));

        if (IsBad)
        {
            if (IsInverted)
            {
                GameManager.Instance.GameLost();
            }
            else
            {
                GameManager.Instance.GameWon(rBody.position);
            }

        }
    }
}
