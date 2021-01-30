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

    private void Start()
    {
        var badBoid = Random.Range(0, initialBoidCount);
        for (int i = 0; i < initialBoidCount; i++)
        {
            var pos = new Vector2(Random.Range(worldContainer.xMin, worldContainer.xMax),
                                  Random.Range(worldContainer.yMin, worldContainer.yMax));
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
        var indx = boids.Count;
        boid.Initialize((b) => {
            boids.Remove(b);
        });

        boids.Add(boid);
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

            boid.UpdateBoid(boids);
        }
    }
}
