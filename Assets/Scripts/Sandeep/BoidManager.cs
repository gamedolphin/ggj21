using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    private List<Boid> boids = new List<Boid>();

    [SerializeField]
    private Boid boidPrefab = null;

    [SerializeField]
    private int initialBoidCount = 10;

    [SerializeField]
    private float borderBuffer;

    [SerializeField]
    private Rect worldContainer;

    [SerializeField]
    private BoidPath path;

    public float SeparationMultiplier = 1.5f;
    public float AlignmentMultiplier = 1.0f;
    public float CohesionMultiplier = 1.0f;
    public float PathMultiplier = 4.0f;

    public float TargetTimeout = 5.0f;

    public float MinBoidSpeed = 5.0f;
    public float MaxBoidSpeed = 20.0f;
    public float AvoidCircle = 2.0f;
    public float AlignCircle = 2.0f;

    public List<Transform> SpawnPoints = new List<Transform>();
    public float SpawnSpread = 1.0f;

    public bool KillOthers = false;

    private void Start()
    {
        var badBoid = Random.Range(0, initialBoidCount);
        for (int i = 0; i < initialBoidCount; i++)
        {
            var pos = new Vector2(Random.Range(worldContainer.xMin, worldContainer.xMax),
                                  Random.Range(worldContainer.yMin, worldContainer.yMax));
            if (SpawnPoints.Count > 0)
            {
                var randomSpawn = SpawnPoints[Random.Range(0, SpawnPoints.Count)];
                var randomPos = randomSpawn.position;
                pos = new Vector2(Random.Range(randomPos.x - SpawnSpread, randomPos.x + SpawnSpread),
                                  Random.Range(randomPos.y - SpawnSpread, randomPos.y + SpawnSpread));
            }
            CreateBoid(pos, worldContainer, i==badBoid);
        }

    }

    private void CreateBoid(Vector2 pos, Rect world, bool isBad)
    {
        var boid = Instantiate(boidPrefab);
        boid.transform.SetParent(transform, true);

        boid.transform.position = pos;
        boid.WorldRect = world;
        boid.IsBad = isBad;
        boid.MaxSpeed = Random.Range(MinBoidSpeed, MaxBoidSpeed);
        boid.IsInverted = KillOthers;
        var indx = boids.Count;
        boid.Initialize(OnBoidDestroyed);

        boids.Add(boid);

        GameManager.Instance.OnBoidCreated();
    }

    private void OnBoidDestroyed(Boid b)
    {
        boids.Remove(b);
        GameManager.Instance.OnBoidDestroyed(KillOthers, b.RBody.position);
    }

    private void Update()
    {
        foreach (var boid in boids)
        {

            boid.Path = path;
            boid.SeparationMultiplier = SeparationMultiplier;
            boid.AlignmentMultiplier = AlignmentMultiplier;
            boid.CohesionMultiplier = CohesionMultiplier;
            boid.PathMultiplier = PathMultiplier;
            boid.TargetTimeout = TargetTimeout;
            boid.AvoidCircle = AvoidCircle;
            boid.AlignCircle = AlignCircle;
            boid.UpdateBoid(boids);
        }
    }
}
